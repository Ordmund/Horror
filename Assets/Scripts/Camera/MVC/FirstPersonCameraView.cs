using Core.MVC;
using UnityEngine;

namespace Camera
{
    public class FirstPersonCameraView : BaseView
    {
        [SerializeField] private Transform _cameraTransform;

        public void SetCameraPosition(Vector3 position)
        {
            _cameraTransform.position = position;
        }
        
        public void SetCameraRotation(Quaternion rotation)
        {
            _cameraTransform.rotation = rotation;
        }
    }
}