using Core.MVC;
using UnityEngine;

namespace Camera
{
    public class FirstPersonCameraView : BaseView
    {
        [SerializeField] private Transform _trackingTarget;

        public void SetTrackingTargetPosition(Vector3 position)
        {
            _trackingTarget.position = position;
        }
        
        public void SetTrackingTargetRotation(Quaternion rotation)
        {
            _trackingTarget.rotation = rotation;
        }
    }
}