using System;
using Core.MVC;
using Zenject;

namespace Cheats
{
    public class CheatCameraSettingsPanelController : BaseController<CheatCameraSettingsPanelView, EmptyModel>, IInitializable, IDisposable
    {
        private readonly ICheatsPanelNotifier _cheatsPanelNotifier;

        public CheatCameraSettingsPanelController(CheatCameraSettingsPanelView view, EmptyModel model, ICheatsPanelNotifier cheatsPanelNotifier) : base(view, model)
        {
            _cheatsPanelNotifier = cheatsPanelNotifier;
        }

        public void Initialize()
        {
            View.OnFirstPersonCameraButtonClicked += OnFirstPersonCameraButtonClicked;
            View.OnFrontCameraButtonClicked += OnFrontCameraButtonClicked;
            View.OnBackCameraButtonClicked += OnBackCameraButtonClicked;
            View.OnLeftCameraButtonClicked += OnLeftCameraButtonClicked;
            View.OnRightCameraButtonClicked += OnRightCameraButtonClicked;
            View.OnTopDownCameraButtonClicked += OnTopDownCameraButtonClicked;
        }
        
        private void OnFirstPersonCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyFirstPersonCameraButtonClicked();
        }

        private void OnFrontCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyFrontCameraButtonClicked();
        }
        
        private void OnBackCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyBackCameraButtonClicked();
        }
        
        private void OnLeftCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyLeftCameraButtonClicked();
        }
        
        private void OnRightCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyRightCameraButtonClicked();
        }
        
        private void OnTopDownCameraButtonClicked()
        {
            _cheatsPanelNotifier.NotifyTopDownCameraButtonClicked();
        }

        public void Dispose()
        {
            View.OnFirstPersonCameraButtonClicked -= OnFirstPersonCameraButtonClicked;
            View.OnFrontCameraButtonClicked -= OnFrontCameraButtonClicked;
            View.OnBackCameraButtonClicked -= OnBackCameraButtonClicked;
            View.OnLeftCameraButtonClicked -= OnLeftCameraButtonClicked;
            View.OnRightCameraButtonClicked -= OnRightCameraButtonClicked;
            View.OnTopDownCameraButtonClicked -= OnTopDownCameraButtonClicked;
        }
    }
}