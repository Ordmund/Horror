using System;
using Camera;
using Zenject;

namespace Cheats
{
    public class CheatsHandler : ICheatsHandler, IInitializable, IDisposable
    {
        private readonly ICheatsPanelNotifier _cheatsPanelNotifier;
        private readonly ICameraSwitchDispatcher _cameraSwitchDispatcher;

        public CheatsHandler(ICheatsPanelNotifier cheatsPanelNotifier, ICameraSwitchDispatcher cameraSwitchDispatcher)
        {
            _cheatsPanelNotifier = cheatsPanelNotifier;
            _cameraSwitchDispatcher = cameraSwitchDispatcher;
        }

        public void Initialize()
        {
            _cheatsPanelNotifier.OnFirstPersonCameraButtonClicked += OnFirstPersonCameraButtonClicked;
            _cheatsPanelNotifier.OnFrontCameraButtonClicked += OnFrontCameraButtonClicked;
            _cheatsPanelNotifier.OnBackCameraButtonClicked += OnBackCameraButtonClicked;
            _cheatsPanelNotifier.OnLeftCameraButtonClicked += OnLeftCameraButtonClicked;
            _cheatsPanelNotifier.OnRightCameraButtonClicked += OnRightCameraButtonClicked;
            _cheatsPanelNotifier.OnTopDownCameraButtonClicked += OnTopDownCameraButtonClicked;
        }

        private void OnFirstPersonCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.FirstPerson);
        }

        private void OnFrontCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.ThirdPersonFront);
        }

        private void OnBackCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.ThirdPersonBack);
        }

        private void OnLeftCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.ThirdPersonLeft);
        }

        private void OnRightCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.ThirdPersonRight);
        }

        private void OnTopDownCameraButtonClicked()
        {
            _cameraSwitchDispatcher.RequestSwitchCameraMode(CameraMode.TopDown);
        }

        public void Dispose()
        {
            _cheatsPanelNotifier.OnFirstPersonCameraButtonClicked -= OnFirstPersonCameraButtonClicked;
            _cheatsPanelNotifier.OnFrontCameraButtonClicked -= OnFrontCameraButtonClicked;
            _cheatsPanelNotifier.OnBackCameraButtonClicked -= OnBackCameraButtonClicked;
            _cheatsPanelNotifier.OnLeftCameraButtonClicked -= OnLeftCameraButtonClicked;
            _cheatsPanelNotifier.OnRightCameraButtonClicked -= OnRightCameraButtonClicked;
            _cheatsPanelNotifier.OnTopDownCameraButtonClicked -= OnTopDownCameraButtonClicked;
        }
    }
}