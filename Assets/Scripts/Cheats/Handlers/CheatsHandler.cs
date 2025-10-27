using System;
using ColoredLogger;
using Zenject;

namespace Cheats
{
    public class CheatsHandler : ICheatsHandler, IInitializable, IDisposable
    {
        private readonly ICheatsPanelNotifier _cheatsPanelNotifier;

        public CheatsHandler(ICheatsPanelNotifier cheatsPanelNotifier)
        {
            _cheatsPanelNotifier = cheatsPanelNotifier;
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
            "First Person Camera Cheats".Log(LogColor.BlueViolet);
            //TODO First Person Camera Cheat
        }
        
        private void OnFrontCameraButtonClicked()
        {
            "Front Camera Cheats".Log(LogColor.BlueViolet);
            //TODO Front Camera Cheat
        }
        
        private void OnBackCameraButtonClicked()
        {
            "Back Camera Cheats".Log(LogColor.BlueViolet);
            //TODO Back Camera Cheat
        }
        
        private void OnLeftCameraButtonClicked()
        {
            "Left Camera Cheats".Log(LogColor.BlueViolet);
            //TODO Left Camera Cheat
        }
        
        private void OnRightCameraButtonClicked()
        {
            "Right Camera Cheats".Log(LogColor.BlueViolet);
            //TODO Right Camera Cheat
        }
        
        private void OnTopDownCameraButtonClicked()
        {
            "Top Down Camera Cheats".Log(LogColor.BlueViolet);
            //TODO Top Down Camera Cheat
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