using System;
using UnityEngine;

namespace Player
{
    public interface IPlayerTransformNotifier
    {
        public event Action<Vector3> OnHeadPositionChanged;
        public event Action<Quaternion> OnHeadRotationChanged;

        public void NotifyHeadPositionChanged(Vector3 position);
        public void NotifyHeadRotationChanged(Quaternion rotation);
    }
}