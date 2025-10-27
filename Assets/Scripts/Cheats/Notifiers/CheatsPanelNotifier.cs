using System;

namespace Cheats
{
    public class CheatsPanelNotifier : ICheatsPanelNotifier
    {
        public event Action OnFirstPersonCameraButtonClicked;
        public event Action OnFrontCameraButtonClicked;
        public event Action OnBackCameraButtonClicked;
        public event Action OnLeftCameraButtonClicked;
        public event Action OnRightCameraButtonClicked;
        public event Action OnTopDownCameraButtonClicked;
        
        public void NotifyFirstPersonCameraButtonClicked()
        {
            OnFirstPersonCameraButtonClicked?.Invoke();
        }

        public void NotifyFrontCameraButtonClicked()
        {
            OnFrontCameraButtonClicked?.Invoke();
        }

        public void NotifyBackCameraButtonClicked()
        {
            OnBackCameraButtonClicked?.Invoke();
        }

        public void NotifyLeftCameraButtonClicked()
        {
            OnLeftCameraButtonClicked?.Invoke();
        }

        public void NotifyRightCameraButtonClicked()
        {
            OnRightCameraButtonClicked?.Invoke();
        }

        public void NotifyTopDownCameraButtonClicked()
        {
            OnTopDownCameraButtonClicked?.Invoke();
        }
    }
}