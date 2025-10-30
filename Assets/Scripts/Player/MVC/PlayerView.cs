using System.Collections.Generic;
using Core.MVC;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _headTargetTransform;
        [SerializeField] private Transform _bodyTargetTransform;

        public Transform Head => _headTargetTransform;
        public Vector3 Position => _transform.position;
        public Quaternion BodyRotation => _bodyTargetTransform.rotation;
        public float StandingCharacterHeight => _standingCharacterHeight;
        public float SlopeAngleLimit => _characterController.slopeLimit;
        public bool IsGrounded => _characterController.isGrounded;

        private Vector3 _standingCharacterHeadPosition;
        private Vector3 _standingCharacterCenter;
        private float _standingCharacterHeight;
        private int _xVelocityHash;
        private int _yVelocityHash;
        private int _crouchingHash;
        private int _movingHash;
        private int _jumpingHash;
        private int _turnLeftHash;
        private int _turnRightHash;
        
        private const string XVelocityKey = "X Velocity";
        private const string YVelocityKey = "Y Velocity";
        private const string CrouchingKey = "Crouching";
        private const string MovingKey = "Moving";
        private const string JumpingKey = "Jumping";
        private const string TurnLeftKey = "Turn Left";
        private const string TurnRightKey = "Turn Right";
        private const string TurnLeftAnimationClipName = "Turn Left";
        private const string TurnRightAnimationClipName = "Turn Right";
        private const string CrouchingTurnLeftAnimationClipName = "Crouching Turn Left";
        private const string CrouchingTurnRightAnimationClipName = "Crouching Turn Right";

        private readonly Dictionary<string, float> _turnAnimationLengths = new()
        {
            { TurnLeftAnimationClipName, 0f },
            { TurnRightAnimationClipName, 0f },
            { CrouchingTurnLeftAnimationClipName, 0f },
            { CrouchingTurnRightAnimationClipName, 0f }
        };

        private void Awake()
        {
            _xVelocityHash = Animator.StringToHash(XVelocityKey);
            _yVelocityHash = Animator.StringToHash(YVelocityKey);
            _crouchingHash = Animator.StringToHash(CrouchingKey);
            _movingHash = Animator.StringToHash(MovingKey);
            _jumpingHash = Animator.StringToHash(JumpingKey);
            _turnLeftHash = Animator.StringToHash(TurnLeftKey);
            _turnRightHash = Animator.StringToHash(TurnRightKey);

            CacheTurnAnimationLengths();
        }

        private void CacheTurnAnimationLengths()
        {
            var controller = _animator.runtimeAnimatorController;

            foreach (var clip in controller.animationClips)
            {
                if (_turnAnimationLengths.ContainsKey(clip.name))
                {
                    _turnAnimationLengths[clip.name] = clip.length;
                }
            }
        }
        
        public void CacheCharacterHeight()
        {
            _standingCharacterHeadPosition = _headTargetTransform.localPosition;
            
            _standingCharacterCenter = _characterController.center;
            _standingCharacterHeight = _characterController.height;
        }
        
        public void Move(Vector3 motion)
        {
            _characterController.Move(motion);
        }

        public void SetHeadTargetRotation(Quaternion rotation)
        {
            _headTargetTransform.rotation = rotation;
        }

        public void SetBodyRotation(Quaternion rotation)
        {
            _bodyTargetTransform.rotation = rotation;
        }
        
        public void SetCrouchingView(Vector3 headPosition, Vector3 characterCenter, float characterHeight)
        {
            _headTargetTransform.localPosition = headPosition;
            
            _characterController.center = characterCenter;
            _characterController.height = characterHeight;

            _animator.SetBool(_crouchingHash, true);
        }

        public void SetStandingView()
        {
            _headTargetTransform.localPosition = _standingCharacterHeadPosition;
            
            _characterController.center = _standingCharacterCenter;
            _characterController.height = _standingCharacterHeight;
            
            _animator.SetBool(_crouchingHash, false);
        }

        public void SetMovementAnimationVelocity(Vector2 velocity)
        {
            _animator.SetFloat(_xVelocityHash, velocity.x);
            _animator.SetFloat(_yVelocityHash, velocity.y);
        }
        
        public void SetMovingAnimationState(bool isMoving)
        {
            _animator.SetBool(_movingHash, isMoving);
        }
        
        public void SetJumpingAnimationState(bool isJumping)
        {
            _animator.SetBool(_jumpingHash, isJumping);
        }

        public void SetTurnAnimationTrigger(bool toLeft)
        {
            var hash = toLeft ? _turnLeftHash : _turnRightHash;
            
            _animator.SetTrigger(hash);
        }
        
        public float GetTurnAnimationLength(bool isCrouching, bool turnLeft)
        {
            return turnLeft switch
            {
                true when isCrouching => _turnAnimationLengths[CrouchingTurnLeftAnimationClipName],
                true => _turnAnimationLengths[TurnLeftAnimationClipName],
                false when isCrouching => _turnAnimationLengths[CrouchingTurnRightAnimationClipName],
                false => _turnAnimationLengths[TurnRightAnimationClipName]
            };
        }
    }
}