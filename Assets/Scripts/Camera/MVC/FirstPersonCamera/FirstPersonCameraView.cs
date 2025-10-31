using Core.MVC;
using UnityEngine;

namespace Camera
{
    public class FirstPersonCameraView : BaseView
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _cameraTransform;

        public void SetCameraEnabled(bool isEnabled)
        {
            _camera.enabled = isEnabled;
        }

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