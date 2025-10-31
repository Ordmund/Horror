using System;
using System.Collections.Generic;
using Core.MVC;
using UnityEngine;

namespace Camera
{
    [Serializable]
    public class ThirdPersonCameraModel : BaseModel
    {
        public List<CameraSetup> cameraSetups;
    }

    [Serializable]
    public struct CameraSetup
    {
        public CameraMode mode;
        public Vector3 position;
        public Vector3 rotation;
    }
}