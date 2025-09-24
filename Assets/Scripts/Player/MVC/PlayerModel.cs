using System;
using Core.MVC;

namespace Player
{
    [Serializable]
    public class PlayerModel : BaseModel
    {
        public float cameraSensitivity;
        public float cameraAngleClamp;
    }
}