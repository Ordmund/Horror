using System;

namespace Cheats
{
    public interface ICheatsPanelNotifier
    {
        event Action OnFirstPersonCameraButtonClicked;
        event Action OnFrontCameraButtonClicked;
        event Action OnBackCameraButtonClicked;
        event Action OnLeftCameraButtonClicked;
        event Action OnRightCameraButtonClicked;
        event Action OnTopDownCameraButtonClicked;
        
        void NotifyFirstPersonCameraButtonClicked();
        void NotifyFrontCameraButtonClicked();
        void NotifyBackCameraButtonClicked();
        void NotifyLeftCameraButtonClicked();
        void NotifyRightCameraButtonClicked();
        void NotifyTopDownCameraButtonClicked();
    }
}