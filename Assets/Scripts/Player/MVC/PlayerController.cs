using System;
using Constants;
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

        private Quaternion _basicBodyRotation;
        private Quaternion _bodyTargetRotation;
        private float _turningStartTime;
        private float _turningProgress;
        private float _turningEndTime;

        private Vector2 _inputLookDirection;
        private Vector2 _inputMoveDirection;
        private float _currentPitchAngle;
        private float _jumpVelocity;
        private bool _isJumpPressed;
        private bool _isSprintPressed;
        private bool _isCrouchPressed;
        private CharacterState _state;

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
            TryTurnCharacter();

            UpdateMovementMotion();
            UpdateCameraDirection();

            ClearInputTriggers();
        }

        #region Look

        private void UpdateCameraDirection()
        {
            var xAngle = _inputLookDirection.x * Model.cameraSensitivity * Time.deltaTime;
            var yAngle = -_inputLookDirection.y * Model.cameraSensitivity * Time.deltaTime;

            var yAngleClamped = Mathf.Clamp(_currentPitchAngle + yAngle, -Model.cameraPitchClamp, Model.cameraPitchClamp) - _currentPitchAngle;
            _currentPitchAngle += yAngleClamped;

            var yaw = Quaternion.AngleAxis(xAngle, Vector3.up);
            var pitch = Quaternion.AngleAxis(yAngleClamped, Vector3.right);

            View.SetHeadTargetRotation(yaw * View.Head.rotation * pitch);

            _playerTransformNotifier.NotifyHeadRotationChanged(View.Head.rotation);

            _inputLookDirection = Vector2.zero;
        }

        #endregion

        #region Movement

        private void UpdateMovementMotion()
        {
            var moveDirection = View.Head.right * _inputMoveDirection.x + View.Head.forward * _inputMoveDirection.y;
            var movementSpeed = _state.HasFlag(CharacterState.Crouching) ? Model.crouchSpeed : _isSprintPressed ? Model.sprintSpeed : Model.movementSpeed;
            var motion = moveDirection * movementSpeed;

            UpdateJumpVelocity();

            motion.y = _jumpVelocity;
            View.Move(motion * Time.deltaTime);

            var isMoving = motion != Vector3.zero;
            if (isMoving)
            {
                View.SetBodyRotation(View.Head.rotation);
            }

            View.SetMovementAnimationVelocity(_inputMoveDirection * movementSpeed);
            View.SetMovingAnimationState(isMoving);
            UpdateCharacterMovingState(isMoving);

            _playerTransformNotifier.NotifyHeadPositionChanged(View.Head.position);
        }

        private void UpdateCharacterMovingState(bool isMoving)
        {
            switch (isMoving)
            {
                case true when _isSprintPressed: _state |= CharacterState.Moving | CharacterState.Running; break;
                case true: _state = (_state | CharacterState.Moving) & ~CharacterState.Running; break;
                case false: _state &= ~(CharacterState.Moving | CharacterState.Running); break;
            }
        }

        #endregion

        #region Jump

        private void UpdateJumpVelocity()
        {
            switch (View.IsGrounded)
            {
                case true when _isJumpPressed && !_state.HasFlag(CharacterState.Sliding):
                    //Third kinematic equation: [v² = v₀² + 2aΔx], where v₀ = 0, because our starting point is from the ground
                    _jumpVelocity = Mathf.Sqrt(2f * -Physics.gravity.y * Model.jumpHeight);
                    _state |= CharacterState.Jumping;
                    break;

                case true:
                    _jumpVelocity = 0f;
                    _state &= ~CharacterState.Jumping;
                    break;

                case false:
                    _jumpVelocity += Physics.gravity.y * Time.deltaTime;
                    break;
            }

            View.SetJumpingAnimationState(_state.HasFlag(CharacterState.Jumping));
        }

        #endregion

        #region Crouch

        private void TryCrouch()
        {
            if (_isCrouchPressed && !_state.HasFlag(CharacterState.Crouching))
            {
                View.SetCrouchingView(Model.crouchedHeadPosition, Model.crouchedCenter, Model.crouchedHeight);

                _state |= CharacterState.Crouching;
            }
        }

        private void TryStandUp()
        {
            if (_state.HasFlag(CharacterState.Crouching) && !_isCrouchPressed)
            {
                var headroom = View.StandingCharacterHeight - Model.crouchedHeight;
                var isThereNoObstaclesAbove = !Physics.Raycast(View.Position, Vector3.up, headroom, RaycastLayers.GroundMask);
                if (isThereNoObstaclesAbove)
                {
                    View.SetStandingView();

                    _state &= ~CharacterState.Crouching;
                }
            }
        }

        #endregion

        #region Slide

        private void TrySlideDownSlope()
        {
            if (View.IsGrounded && Physics.Raycast(View.Position, Vector3.down, out var hit, View.StandingCharacterHeight))
            {
                var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                if (slopeAngle > View.SlopeAngleLimit)
                {
                    var slideDirection = new Vector3(hit.normal.x, -hit.normal.y, hit.normal.z);

                    View.Move(slideDirection * Model.slideSpeed * Time.deltaTime);
                    View.SetBodyRotation(View.Head.rotation);

                    _state |= CharacterState.Sliding;
                }
                else
                {
                    _state &= ~CharacterState.Sliding;
                }
            }
        }

        #endregion

        #region Turn

        private void TryTurnCharacter()
        {
            if (_state.HasFlag(CharacterState.Turning))
            {
                RotateCharacter();
            }
            else
            {
                var bodyYAngle = View.BodyRotation.eulerAngles.y;
                var headRotationYAngle = View.Head.rotation.eulerAngles.y;

                var deltaAngle = Mathf.DeltaAngle(bodyYAngle, headRotationYAngle);
                if (Mathf.Abs(deltaAngle) > Model.headHorizontalRotationMaxAngle)
                {
                    StartCharacterTurning(deltaAngle < 0f);
                }
            }
        }

        private void StartCharacterTurning(bool turnLeft)
        {
            _state |= CharacterState.Turning;

            _turningStartTime = _turningProgress = Time.time;
            _turningEndTime = _turningStartTime + View.GetTurnAnimationLength(_state.HasFlag(CharacterState.Crouching), turnLeft);

            var rotationAngle = turnLeft ? -Model.bodyRotationAngle : Model.bodyRotationAngle;
            var bodyRotation = View.BodyRotation.eulerAngles;
            _bodyTargetRotation = Quaternion.Euler(bodyRotation.x, bodyRotation.y + rotationAngle, bodyRotation.z);
            _basicBodyRotation = View.BodyRotation;

            View.SetTurnAnimationTrigger(turnLeft);
        }

        private void RotateCharacter()
        {
            _turningProgress += Time.deltaTime;

            var animationProgress = (_turningProgress - _turningStartTime) / (_turningEndTime - _turningStartTime);
            View.SetBodyRotation(Quaternion.Slerp(_basicBodyRotation, _bodyTargetRotation, animationProgress));

            if (_turningProgress >= _turningEndTime || HasAnyState(CharacterState.Moving | CharacterState.Jumping | CharacterState.Sliding))
            {
                _state &= ~CharacterState.Turning;
            }
        }

        #endregion

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

        private bool HasAnyState(CharacterState flags)
        {
            return (_state & flags) != 0;
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