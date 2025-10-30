using System;
using Core.MVC;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerModel : BaseModel
    {
        [Header("Animation")] 
        public float headHorizontalRotationMaxAngle;
        public float bodyRotationAngle;
        
        [Header("Camera")]
        public float cameraSensitivity;
        public float cameraPitchClamp;
        
        [Header("Speed")]
        public float movementSpeed;
        public float sprintSpeed;
        public float crouchSpeed;
        public float slideSpeed;
        
        [Header("Jump")]
        public float jumpHeight;
        
        [Header("Crouching")]
        public Vector3 crouchedHeadPosition;
        public Vector3 crouchedCenter;
        public float crouchedHeight;
    }
}