using System;
using Core.Managers.Injectable;
using Core.MVC;
using GameInput;
using Player.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : BaseController<PlayerView, PlayerModel>, IInitializable, IDisposable
    {
        private readonly ITickNotifier _tickNotifier;
        private readonly IInputNotifier _inputNotifier;
        private readonly IPlayerTransformNotifier _playerTransformNotifier;
        
        private Vector2 _inputLookDirection;

        public PlayerController(PlayerView view, PlayerModel model, ITickNotifier tickNotifier, IInputNotifier inputNotifier, IPlayerTransformNotifier playerTransformNotifier) : base(view, model)
        {
            _tickNotifier = tickNotifier;
            _inputNotifier = inputNotifier;
            _playerTransformNotifier = playerTransformNotifier;
        }

        public void Initialize()
        {
            TryLoadModelFromScriptableObject<PlayerSettings>();
            
            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _tickNotifier.SubscribeOnTick(UpdateCameraDirection);
            _inputNotifier.LookIsInteracted += OnLookInteracted;
        }

        private void UnsubscribeFromEvents()
        {
            _tickNotifier.UnsubscribeFromTick(UpdateCameraDirection);
            _inputNotifier.LookIsInteracted -= OnLookInteracted;
        }

        private void OnLookInteracted(Vector2 direction)
        {
            _inputLookDirection = direction;
        }

        private void UpdateCameraDirection()
        {
            var xAngle = _inputLookDirection.x * Time.deltaTime * Model.cameraSensitivity;
            var yAngle = -_inputLookDirection.y * Time.deltaTime * Model.cameraSensitivity;
            
            var yaw = Quaternion.AngleAxis(xAngle, Vector3.up);
            var pitch = Quaternion.AngleAxis(yAngle, Vector3.right);
            
            View.SetHeadRotation(yaw * View.HeadRotation);
            View.SetHeadRotation(View.HeadRotation * pitch);
            
            _playerTransformNotifier.NotifyHeadRotationChanged(View.HeadRotation);
            
            _inputLookDirection = Vector2.zero;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}