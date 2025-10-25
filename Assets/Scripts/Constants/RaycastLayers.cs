using UnityEngine;

namespace Constants
{
    public static class RaycastLayers
    {
        static RaycastLayers()
        {
            GroundMask = LayerMask.GetMask(RaycastLayers.Ground);
        }

        public const string Ground = "Ground";

        public static int GroundMask;
    }
}