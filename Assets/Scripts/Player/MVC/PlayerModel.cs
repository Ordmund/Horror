using System;
using Core.MVC;

namespace Player
{
    [Serializable]
    public class PlayerModel : BaseModel
    {
        public float cameraSensitivity;
        public float cameraPitchClamp;
        public float movementSpeed;
        public float jumpHeight;
    }
}