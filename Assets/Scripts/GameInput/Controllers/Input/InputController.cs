using Core.Controllers;
using Core.Managers.Injectable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class InputController : ControllerBase
    {
        private readonly IUnityCallbacksBehaviour _unityCallbacksBehaviour;
        private readonly IInputNotifier _inputNotifier;

        private InputAction _lookAction;
        private InputAction _moveAction;

        public InputController(IUnityCallbacksBehaviour unityCallbacksBehaviour, IInputNotifier inputNotifier)
        {
            _unityCallbacksBehaviour = unityCallbacksBehaviour;
            _inputNotifier = inputNotifier;
        }

        public override void Initialize()
        {
            base.Initialize();

            SubscribeOnUpdate();
            FindInputActions();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            UnsubscribeFromUpdate();
        }

        private void SubscribeOnUpdate()
        {
            _unityCallbacksBehaviour.OnFixedUpdate += OnFixedUpdate;
        }

        private void UnsubscribeFromUpdate()
        {
            _unityCallbacksBehaviour.OnFixedUpdate -= OnFixedUpdate;
        }

        private void FindInputActions()
        {
            _lookAction = InputSystem.actions.FindAction("Look");
            _moveAction = InputSystem.actions.FindAction("Move");
        }

        private void OnFixedUpdate()
        {
            if (_moveAction.IsPressed())
            {
                var direction = _moveAction.ReadValue<Vector2>();
                
                _inputNotifier.NotifyMoveIsPressed(direction);
            }

            if (_lookAction.IsPressed())
            {
                var direction = _lookAction.ReadValue<Vector2>();
                
                _inputNotifier.NotifyMouseIsMoved(direction);
            }
        }
    }
}