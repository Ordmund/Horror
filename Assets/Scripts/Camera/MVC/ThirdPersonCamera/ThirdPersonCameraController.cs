using System;
using System.Linq;
using Core.MVC;
using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class ThirdPersonCameraController : BaseController<ThirdPersonCameraView, ThirdPersonCameraModel>, IInitializable, IDisposable
    {
        private readonly IPlayerTransformNotifier _playerTransformNotifier;
        private readonly ICameraSwitchDispatcher _cameraSwitchDispatcher;

        private bool _isEnabled;

        public ThirdPersonCameraController(ThirdPersonCameraView view, ThirdPersonCameraModel model, IPlayerTransformNotifier playerTransformNotifier,
            ICameraSwitchDispatcher cameraSwitchDispatcher) : base(view, model)
        {
            _playerTransformNotifier = playerTransformNotifier;
            _cameraSwitchDispatcher = cameraSwitchDispatcher;
        }

        public void Initialize()
        {
            TryLoadModelFromScriptableObject<CameraModeSettings>();

            View.SetCameraEnabled(false);

            SubscribeOnEvents();
        }

        private void SetCameraLocalPosition(Vector3 position)
        {
            if (_isEnabled)
            {
                View.SetCameraPosition(position);
            }
        }

        private void UpdateCamera(CameraMode mode)
        {
            switch (mode)
            {
                case CameraMode.ThirdPersonFront:
                case CameraMode.ThirdPersonBack:
                case CameraMode.ThirdPersonLeft:
                case CameraMode.ThirdPersonRight:
                case CameraMode.TopDown:
                    var cameraSetup = Model.cameraSetups.First(setup => setup.mode == mode);
                    View.SetupCamera(cameraSetup.position, cameraSetup.rotation);
                    View.SetCameraEnabled(true);
                    _isEnabled = true;
                    break;

                default:
                    View.SetCameraEnabled(false);
                    _isEnabled = false;
                    break;
            }
        }

        private void SubscribeOnEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged += SetCameraLocalPosition;
            _cameraSwitchDispatcher.OnSwitchCameraModeRequested += UpdateCamera;
        }

        private void UnsubscribeOnEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged -= SetCameraLocalPosition;
            _cameraSwitchDispatcher.OnSwitchCameraModeRequested -= UpdateCamera;
        }

        public void Dispose()
        {
            UnsubscribeOnEvents();
        }
    }
}