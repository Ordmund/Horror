using System;

namespace Player
{
    [Flags]
    public enum CharacterState
    {
        Idle = 0,
        Moving = 1 << 0,
        Running = 1 << 1,
        Crouching = 1 << 2,
        Sliding = 1 << 3,
        Jumping = 1 << 4,
        Turning = 1 << 5
    }
}