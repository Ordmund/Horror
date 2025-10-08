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
        private bool _isSprintPressed;
        private bool _isCrouchPressed;
        private bool _isCrouching;
        private bool _isSliding;

        public PlayerController(PlayerView view, PlayerModel model, ITickNotifier tickNotifier, IInputNotifier inputNotifier, IPlayerTransformNotifier playerTransformNotifier) : base(view, model)
        {
            _tickNotifier = tickNotifier;
            _inputNotifier = inputNotifier;
            _playerTransformNotifier = playerTransformNotifier;
        }

        public void Initialize()
        {
            TryLoadModelFromScriptableObject<PlayerSettings>();
            
            View.CacheCharacterHeight();
            
            SubscribeOnEvents();

            NotifyPlayerSpawnPosition();
        }

        private void NotifyPlayerSpawnPosition()
        {
            _playerTransformNotifier.NotifyHeadPositionChanged(View.Head.position);
            _playerTransformNotifier.NotifyHeadRotationChanged(View.Head.rotation);
        }

        private void OnTick()
        {
            TrySlideDownSlope();
            TryCrouch();
            TryStandUp();
            
            UpdateMovementMotion();
            UpdateCameraDirection();
            
            ClearInputTriggers();
        }

        private void TryCrouch()
        {
            if (_isCrouchPressed)
            {
                View.SetCrouchingView(Model.crouchedHeadPosition, Model.crouchedCenter, Model.crouchedHeight);

                _isCrouching = true;
            }
        }

        private void TryStandUp()
        {
            if (_isCrouching && !_isCrouchPressed)
            {
                var headroom = View.StandingCharacterHeight - Model.crouchedHeight;
                var isThereNoObstaclesAbove = !Physics.Raycast(View.Position, Vector3.up, headroom);
                if (isThereNoObstaclesAbove)
                {
                    View.SetStandingView();

                    _isCrouching = false;
                }
            }
        }
        
        private void TrySlideDownSlope()
        {
            if (View.IsGrounded)
            {
                if (Physics.Raycast(View.Position, Vector3.down, out var hit, View.StandingCharacterHeight))
                {
                    var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                    if (slopeAngle > View.SlopeAngleLimit)
                    {
                        var slideDirection = new Vector3(hit.normal.x, -hit.normal.y, hit.normal.z);
                        
                        View.Move(slideDirection * Model.slideSpeed * Time.deltaTime);

                        _isSliding = true;
                    }
                    else
                    {
                        _isSliding = false;
                    }
                }
            }
        }
        
        private void UpdateMovementMotion()
        {
            var moveDirection = View.Head.right * _inputMoveDirection.x + View.Head.forward * _inputMoveDirection.y;
            var movementSpeed = _isCrouching ? Model.crouchSpeed : _isSprintPressed ? Model.sprintSpeed : Model.movementSpeed;
            var motion = moveDirection * movementSpeed;

            switch (View.IsGrounded)
            {
                //Third kinematic equation: [v² = v₀² + 2aΔx], where v₀ = 0, because our starting point is from the ground
                case true when _isJumpPressed && !_isSliding:
                    _jumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * Model.jumpHeight);
                    break;
                case false:
                    _jumpVelocity += Physics.gravity.y * Time.deltaTime;
                    break;
            }

            motion.y = _jumpVelocity;
            View.Move(motion * Time.deltaTime);

            _playerTransformNotifier.NotifyHeadPositionChanged(View.Head.position);
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
        
        private void OnSprintPressed()
        {
            _isSprintPressed = true;
        }
        
        private void OnCrouchPressed()
        {
            _isCrouchPressed = true;
        }
        
        private void ClearInputTriggers()
        {
            _isJumpPressed = false;
            _isSprintPressed = false;
            _isCrouchPressed = false;
            _inputMoveDirection = Vector2.zero;
        }

        private void SubscribeOnEvents()
        {
            _tickNotifier.SubscribeOnTick(OnTick);
            
            _inputNotifier.LookIsInteracted += OnLookInteracted;
            _inputNotifier.MoveIsPressed += OnMovePressed;
            _inputNotifier.JumpIsPressed += OnJumpPressed;
            _inputNotifier.SprintIsPressed += OnSprintPressed;
            _inputNotifier.CrouchIsPressed += OnCrouchPressed;
        }

        private void UnsubscribeFromEvents()
        {
            _tickNotifier.UnsubscribeFromTick(OnTick);
            
            _inputNotifier.LookIsInteracted -= OnLookInteracted;
            _inputNotifier.MoveIsPressed -= OnMovePressed;
            _inputNotifier.JumpIsPressed -= OnJumpPressed;
            _inputNotifier.SprintIsPressed -= OnSprintPressed;
            _inputNotifier.CrouchIsPressed -= OnCrouchPressed;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}