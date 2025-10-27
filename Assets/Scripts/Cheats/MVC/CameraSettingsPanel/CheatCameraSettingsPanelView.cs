using Core.MVC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cheats
{
    public class CheatCameraSettingsPanelView : BaseView
    {
        [SerializeField] private Button _firstPersonCameraButton;
        [SerializeField] private Button _frontCameraButton;
        [SerializeField] private Button _backCameraButton;
        [SerializeField] private Button _leftCameraButton;
        [SerializeField] private Button _rightCameraButton;
        [SerializeField] private Button _topDownCameraButton;

        public event UnityAction OnFirstPersonCameraButtonClicked;
        public event UnityAction OnFrontCameraButtonClicked;
        public event UnityAction OnBackCameraButtonClicked;
        public event UnityAction OnLeftCameraButtonClicked;
        public event UnityAction OnRightCameraButtonClicked;
        public event UnityAction OnTopDownCameraButtonClicked;
        
        private void OnEnable()
        {
            _firstPersonCameraButton.onClick.AddListener(OnFirstPersonCameraButtonClicked);
            _frontCameraButton.onClick.AddListener(OnFrontCameraButtonClicked);
            _backCameraButton.onClick.AddListener(OnBackCameraButtonClicked);
            _leftCameraButton.onClick.AddListener(OnLeftCameraButtonClicked);
            _rightCameraButton.onClick.AddListener(OnRightCameraButtonClicked);
            _topDownCameraButton.onClick.AddListener(OnTopDownCameraButtonClicked);
        }

        private void OnDisable()
        {
            _firstPersonCameraButton.onClick.RemoveAllListeners();
            _frontCameraButton.onClick.RemoveAllListeners();
            _backCameraButton.onClick.RemoveAllListeners();
            _leftCameraButton.onClick.RemoveAllListeners();
            _rightCameraButton.onClick.RemoveAllListeners();
            _topDownCameraButton.onClick.RemoveAllListeners();
        }
    }
}