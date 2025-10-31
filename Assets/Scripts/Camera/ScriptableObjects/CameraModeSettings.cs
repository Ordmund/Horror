using Core.MVC;
using UnityEngine;

namespace Camera
{
    public class CameraModeSettings : ScriptableObject, IModelLoader<ThirdPersonCameraModel>
    {
        [SerializeField] private ThirdPersonCameraModel model;

        public ThirdPersonCameraModel LoadModel()
        {
            return model;
        }
    }
}