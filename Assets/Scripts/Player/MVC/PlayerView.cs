using Core.MVC;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _headTransform;
        
        public Transform Transform => _transform ??= transform;
        public Transform Head => _headTransform;
        public float Height => _characterController.height;
        public float SlopeAngleLimit => _characterController.slopeLimit;
        public bool IsGrounded => _characterController.isGrounded;

        private Transform _transform;

        public void SetHeadRotation(Quaternion rotation)
        {
            _headTransform.rotation = rotation;
        }

        public void Move(Vector3 motion)
        {
            _characterController.Move(motion);
        }
    }
}