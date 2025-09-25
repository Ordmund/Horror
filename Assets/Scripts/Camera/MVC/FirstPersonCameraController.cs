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
            _playerTransformNotifier.OnHeadPositionChanged += UpdateTrackingTargetPosition;
            _playerTransformNotifier.OnHeadRotationChanged += UpdateTrackingTargetRotation;

        }

        private void UnsubscribeFromEvents()
        {
            _playerTransformNotifier.OnHeadPositionChanged -= UpdateTrackingTargetPosition;
            _playerTransformNotifier.OnHeadRotationChanged -= UpdateTrackingTargetRotation;

        }

        private void UpdateTrackingTargetPosition(Vector3 position)
        {
            View.SetTrackingTargetPosition(position);
        }

        private void UpdateTrackingTargetRotation(Quaternion rotation)
        {
            View.SetTrackingTargetRotation(rotation);
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}