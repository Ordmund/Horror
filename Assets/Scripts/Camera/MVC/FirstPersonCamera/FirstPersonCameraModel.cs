using System;
using Core.MVC;
using UnityEngine;

namespace Camera
{
    [Serializable]
    public class FirstPersonCameraModel : BaseModel
    {
        public Vector3 positionOffset;
    }
}