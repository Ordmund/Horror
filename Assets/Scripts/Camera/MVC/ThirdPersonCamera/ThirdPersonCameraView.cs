using Core.MVC;
using UnityEngine;

namespace Camera
{
    public class ThirdPersonCameraView : BaseView
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _cameraTransform;

        public void SetupCamera(Vector3 position, Vector3 rotation)
        {
            _cameraTransform.localPosition = position;
            _cameraTransform.rotation = Quaternion.Euler(rotation);
        }

        public void SetCameraPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetCameraEnabled(bool isEnabled)
        {
            _camera.enabled = isEnabled;
        }
    }
}