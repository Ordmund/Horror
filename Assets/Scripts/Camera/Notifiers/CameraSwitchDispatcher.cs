using System;

namespace Camera
{
    public class CameraSwitchDispatcher : ICameraSwitchDispatcher
    {
        public event Action<CameraMode> OnSwitchCameraModeRequested;

        public void RequestSwitchCameraMode(CameraMode mode)
        {
            OnSwitchCameraModeRequested?.Invoke(mode);
        }
    }
}