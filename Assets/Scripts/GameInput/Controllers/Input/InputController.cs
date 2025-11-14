using Core.Controllers;
using Core.Managers.Injectable;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class InputController : ControllerBase
    {
        private readonly ITickNotifier _tickNotifier;
        private readonly IInputNotifier _inputNotifier;

        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private InputAction _crouchAction;
        private InputAction _equipmentWheelAction;

        public InputController(ITickNotifier tickNotifier, IInputNotifier inputNotifier)
        {
            _tickNotifier = tickNotifier;
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
            _tickNotifier.SubscribeOnTick(CheckActionIsPressed);
        }

        private void UnsubscribeFromUpdate()
        {
            _tickNotifier.UnsubscribeFromTick(CheckActionIsPressed);
        }

        private void FindInputActions()
        {
            _lookAction = InputSystem.actions.FindAction("Look");
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
            _sprintAction = InputSystem.actions.FindAction("Sprint");
            _crouchAction = InputSystem.actions.FindAction("Crouch");
            _equipmentWheelAction = InputSystem.actions.FindAction("Equipment Wheel");
        }

        private void CheckActionIsPressed()
        {
            if (_moveAction.IsPressed())
            {
                var direction = _moveAction.ReadValue<Vector2>();

                _inputNotifier.NotifyMoveIsPressed(direction);
            }

            if (_lookAction.IsPressed())
            {
                var direction = _lookAction.ReadValue<Vector2>();

                _inputNotifier.NotifyLookIsInteracted(direction);
            }

            if (_jumpAction.IsPressed())
            {
                _inputNotifier.NotifyJumpIsPressed();
            }

            if (_sprintAction.IsPressed())
            {
                _inputNotifier.NotifySprintIsPressed();
            }

            if (_crouchAction.IsPressed())
            {
                _inputNotifier.NotifyCrouchIsPressed();
            }

            if (_equipmentWheelAction.IsPressed())
            {
                _inputNotifier.NotifyEquipmentWheelIsPressed();
            }
        }
    }
}