using Core.MVC;
using UnityEngine;

namespace Cheats
{
    public class CheatsPanelView : BaseView
    {
        [SerializeField] private GameObject _cameraSettingsPanel;

        private GameObject _activePanel;

        private void Awake()
        {
            _activePanel = _cameraSettingsPanel;
        }

        public void SetActivePanel(CheatsPanelType panelType)
        {
            _activePanel.SetActive(false);
            
            switch (panelType)
            {
                case CheatsPanelType.Default:
                case CheatsPanelType.CameraSettings:
                    _activePanel = _cameraSettingsPanel;
                    break;
            }
            
            _activePanel.SetActive(true);
        }
    }
}