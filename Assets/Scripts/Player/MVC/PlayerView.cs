using Core.MVC;
using UnityEngine;

namespace Player
{
    public class PlayerView : BaseView
    {
        [SerializeField] private Transform _headTransform;

        public Quaternion HeadRotation => _headTransform.rotation;

        public void SetHeadRotation(Quaternion rotation)
        {
            _headTransform.rotation = rotation;
        }
    }
}