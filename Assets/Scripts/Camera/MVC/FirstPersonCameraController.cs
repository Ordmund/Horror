using System;
using Core.MVC;
using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class FirstPersonCameraController : BaseController<FirstPersonCameraView, FirstPersonCameraModel>, IInitializable, IDisposable
    {
        private readonly IPlayerTransformNotifier _playerTransformNotifier;

        public FirstPersonCameraController(FirstPersonCameraView view, FirstPersonCameraModel model, IPlayerTransformNotifier playerTransformNotifier) : base(view, model)
        {
            _playerTransformNotifier = playerTransformNotifier;
        }

        public void Initialize()
        {
            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged += UpdateCameraPosition;
            _playerTransformNotifier.OnHeadRotationChanged += UpdateCameraRotation;

        }

        private void UnsubscribeFromEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged -= UpdateCameraPosition;
            _playerTransformNotifier.OnHeadRotationChanged -= UpdateCameraRotation;

        }

        private void UpdateCameraPosition(Vector3 position)
        {
            View.SetCameraPosition(position);
        }

        private void UpdateCameraRotation(Quaternion rotation)
        {
            View.SetCameraRotation(rotation);
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}