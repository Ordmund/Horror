using System;
using Core.Managers.Injectable;
using Core.MVC;
using GameInput;
using Player.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : BaseController<PlayerView, PlayerModel>, IInitializable, IDisposable
    {
        private readonly ITickNotifier _tickNotifier;
        private readonly IInputNotifier _inputNotifier;
        private readonly IPlayerTransformNotifier _playerTransformNotifier;
        
        private Vector3 _motion;
        private Vector2 _inputLookDirection;
        private Vector2 _inputMoveDirection;
        private bool _isJumpPressed;

        public PlayerController(PlayerView view, PlayerModel model, ITickNotifier tickNotifier, IInputNotifier inputNotifier, IPlayerTransformNotifier playerTransformNotifier) : base(view, model)
        {
            _tickNotifier = tickNotifier;
            _inputNotifier = inputNotifier;
            _playerTransformNotifier = playerTransformNotifier;
        }

        public void Initialize()
        {
            TryLoadModelFromScriptableObject<PlayerSettings>();
            
            SubscribeOnEvents();

            NotifyPlayerSpawnPosition();
        }

        private void NotifyPlayerSpawnPosition()
        {
            _playerTransformNotifier.NotifyHeadPositionChanged(View.Head.position);
            _playerTransformNotifier.NotifyHeadRotationChanged(View.Head.rotation);
        }
        
        private void OnLookInteracted(Vector2 direction)
        {
            _inputLookDirection = direction;
        }

        private void OnMovePressed(Vector2 direction)
        {
            _inputMoveDirection = direction;
        }
        
        private void OnJumpPressed()
        {
            _isJumpPressed = true;
        }
        
        private void UpdateCameraDirection()
        {
            var xAngle = _inputLookDirection.x * Model.cameraSensitivity * Time.deltaTime;
            var yAngle = -_inputLookDirection.y * Model.cameraSensitivity * Time.deltaTime;
            
            var yaw = Quaternion.AngleAxis(xAngle, Vector3.up);
            var pitch = Quaternion.AngleAxis(yAngle, Vector3.right);
            
            View.SetHeadRotation(yaw * View.Head.rotation);
            View.SetHeadRotation(View.Head.rotation * pitch);
            
            _playerTransformNotifier.NotifyHeadRotationChanged(View.Head.rotation);
            
            _inputLookDirection = Vector2.zero;
        }
        
        private void UpdateMovementMotion()
        {
            var moveDirection = View.Head.right * _inputMoveDirection.x + View.Head.forward * _inputMoveDirection.y;
            _motion = moveDirection * Model.movementSpeed * Time.deltaTime;

            if (View.IsGrounded && _motion.y < 0f)
                _motion.y = 0f;

            if (View.IsGrounded && _isJumpPressed)
            {
                //Third kinematic equation: [v² = v₀² + 2aΔx], where v₀ = 0, because our starting point is from the ground
                _motion.y = Mathf.Sqrt(2f * -Physics.gravity.y * Model.jumpHeight);
            }

            if (!View.IsGrounded)
            {
                _motion.y += Physics.gravity.y * Time.deltaTime;
            }

            View.Move(_motion);

            _playerTransformNotifier.NotifyHeadPositionChanged(View.Head.position);

            _isJumpPressed = false;
            _inputMoveDirection = Vector2.zero;
        }

        private void SubscribeOnEvents()
        {
            _tickNotifier.SubscribeOnLateTick(UpdateCameraDirection);
            _tickNotifier.SubscribeOnTick(UpdateMovementMotion);
            _inputNotifier.LookIsInteracted += OnLookInteracted;
            _inputNotifier.MoveIsPressed += OnMovePressed;
            _inputNotifier.JumpIsPressed += OnJumpPressed;
        }

        private void UnsubscribeFromEvents()
        {
            _tickNotifier.UnsubscribeFromLateTick(UpdateCameraDirection);
            _tickNotifier.UnsubscribeFromTick(UpdateMovementMotion);
            _inputNotifier.LookIsInteracted -= OnLookInteracted;
            _inputNotifier.MoveIsPressed -= OnMovePressed;
            _inputNotifier.JumpIsPressed -= OnJumpPressed;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}