using System;
using Core.MVC;
using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class FirstPersonCameraController : BaseController<FirstPersonCameraView, FirstPersonCameraModel>, IInitializable, IDisposable
    {
        private readonly IPlayerTransformNotifier _playerTransformNotifier;
        private readonly ICameraSwitchDispatcher _cameraSwitchDispatcher;

        private bool _isEnabled;

        public FirstPersonCameraController(FirstPersonCameraView view, FirstPersonCameraModel model, IPlayerTransformNotifier playerTransformNotifier,
            ICameraSwitchDispatcher cameraSwitchDispatcher) : base(view, model)
        {
            _playerTransformNotifier = playerTransformNotifier;
            _cameraSwitchDispatcher = cameraSwitchDispatcher;
        }

        public void Initialize()
        {
            SubscribeOnEvents();

            View.SetCameraEnabled(true);
            _isEnabled = true;
        }

        private void UpdateCameraPosition(Vector3 position)
        {
            if (_isEnabled)
            {
                View.SetCameraPosition(position + Model.positionOffset);
            }
        }

        private void UpdateCameraRotation(Quaternion rotation)
        {
            if (_isEnabled)
            {
                View.SetCameraRotation(rotation);
            }
        }

        private void UpdateCamera(CameraMode mode)
        {
            _isEnabled = mode == CameraMode.FirstPerson;
            View.SetCameraEnabled(_isEnabled);
        }

        private void SubscribeOnEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged += UpdateCameraPosition;
            _playerTransformNotifier.OnHeadRotationChanged += UpdateCameraRotation;
            _cameraSwitchDispatcher.OnSwitchCameraModeRequested += UpdateCamera;
        }

        private void UnsubscribeFromEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged -= UpdateCameraPosition;
            _playerTransformNotifier.OnHeadRotationChanged -= UpdateCameraRotation;
            _cameraSwitchDispatcher.OnSwitchCameraModeRequested -= UpdateCamera;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}