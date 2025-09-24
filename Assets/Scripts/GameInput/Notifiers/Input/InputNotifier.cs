using System;
using UnityEngine;

namespace GameInput
{
    public class InputNotifier : IInputNotifier
    {
        public event Action<Vector2> LookIsInteracted;
        public event Action<Vector2> MoveIsPressed;

        public void NotifyLookIsInteracted(Vector2 direction)
        {
            LookIsInteracted?.Invoke(direction);
        }

        public void NotifyMoveIsPressed(Vector2 direction)
        {
            MoveIsPressed?.Invoke(direction);
        }
    }
}