using Core.MVC;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseView
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _headTransform;
        
        public Transform Head => _headTransform;

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