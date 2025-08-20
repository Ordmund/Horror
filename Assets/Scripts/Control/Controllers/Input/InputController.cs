using Core.Controllers;
using Core.Managers.Injectable;
using Notifiers;
using UnityEngine;

namespace Controllers.InputControllers
{
    public class InputController : ControllerBase
    {
        private readonly IUnityCallbacksBehaviour _unityCallbacksBehaviour;
        private readonly IInputNotifier _inputNotifier;

        public InputController(IUnityCallbacksBehaviour unityCallbacksBehaviour, IInputNotifier inputNotifier)
        {
            _unityCallbacksBehaviour = unityCallbacksBehaviour;
            _inputNotifier = inputNotifier;
        }

        public override void Initialize()
        {
            base.Initialize();

            SubscribeOnUpdate();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            UnsubscribeFromUpdate();
        }

        private void SubscribeOnUpdate()
        {
            _unityCallbacksBehaviour.OnUpdate += OnUpdate;
        }

        private void UnsubscribeFromUpdate()
        {
            _unityCallbacksBehaviour.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _inputNotifier.NotifyForwardIsPressed();
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _inputNotifier.NotifyRightIsPressed();
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                _inputNotifier.NotifyLeftIsPressed();
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _inputNotifier.NotifyBackwardIsPressed();
            }
        }
    }
}