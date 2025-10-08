using Core.MVC;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _headTransform;
        
        public Transform Head => _headTransform;
        public Vector3 Position => _transform.position;
        public float StandingCharacterHeight => _standingCharacterHeight;
        public float SlopeAngleLimit => _characterController.slopeLimit;
        public bool IsGrounded => _characterController.isGrounded;
        
        private Vector3 _standingCharacterHeadPosition;
        private Vector3 _standingCharacterCenter;
        private float _standingCharacterHeight;

        public void CacheCharacterHeight()
        {
            _standingCharacterHeadPosition = Head.localPosition;
            
            _standingCharacterCenter = _characterController.center;
            _standingCharacterHeight =  _characterController.height;
        }

        public void SetHeadRotation(Quaternion rotation)
        {
            _headTransform.rotation = rotation;
        }

        public void Move(Vector3 motion)
        {
            _characterController.Move(motion);
        }

        public void SetCrouchingView(Vector3 headPosition, Vector3 characterCenter, float characterHeight)
        {
            Head.transform.localPosition = headPosition;
            
            _characterController.center = characterCenter;
            _characterController.height = characterHeight;
        }

        public void SetStandingView()
        {
            Head.transform.localPosition = _standingCharacterHeadPosition;
            
            _characterController.center = _standingCharacterCenter;
            _characterController.height = _standingCharacterHeight;
        }
    }
}