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
        
        private Vector2 _inputLookDirection;
        private Vector2 _inputMoveDirection;
        private float _currentPitchAngle;
        private float _jumpVelocity;
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
            
            var yAngleClamped = Mathf.Clamp(_currentPitchAngle + yAngle, -Model.cameraPitchClamp, Model.cameraPitchClamp) - _currentPitchAngle;
            _currentPitchAngle += yAngleClamped;
            
            var yaw = Quaternion.AngleAxis(xAngle, Vector3.up);
            var pitch = Quaternion.AngleAxis(yAngleClamped, Vector3.right);
            
            View.SetHeadRotation(yaw * View.Head.rotation * pitch);
            
            _playerTransformNotifier.NotifyHeadRotationChanged(View.Head.rotation);
            
            _inputLookDirection = Vector2.zero;
        }

        private void UpdateMovementMotion()
        {
            var moveDirection = View.Head.right * _inputMoveDirection.x + View.Head.forward * _inputMoveDirection.y;
            var motion = moveDirection * Model.movementSpeed;

            switch (View.IsGrounded)
            {
                case true when _isJumpPressed:
                    //Third kinematic equation: [v² = v₀² + 2aΔx], where v₀ = 0, because our starting point is from the ground
                    _jumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * Model.jumpHeight);
                    break;
                case false:
                    _jumpVelocity += Physics.gravity.y * Time.deltaTime;
                    break;
            }

            motion.y = _jumpVelocity;
            View.Move(motion * Time.deltaTime);

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