using System;

namespace Camera
{
    public interface ICameraSwitchDispatcher
    {
        event Action<CameraMode> OnSwitchCameraModeRequested;

        void RequestSwitchCameraMode(CameraMode mode);
    }
}