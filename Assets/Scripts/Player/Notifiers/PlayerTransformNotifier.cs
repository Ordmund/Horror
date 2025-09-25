using System;
using UnityEngine;

namespace Player
{
    public class PlayerTransformNotifier : IPlayerTransformNotifier
    {
        public event Action<Vector3> OnHeadPositionChanged;
        public event Action<Quaternion> OnHeadRotationChanged;
        
        public void NotifyHeadPositionChanged(Vector3 position)
        {
            OnHeadPositionChanged?.Invoke(position);
        }

        public void NotifyHeadRotationChanged(Quaternion rotation)
        {
            OnHeadRotationChanged?.Invoke(rotation);
        }
    }
}