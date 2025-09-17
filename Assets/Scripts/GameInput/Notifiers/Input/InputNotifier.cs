using System;
using UnityEngine;

namespace GameInput
{
    public class InputNotifier : IInputNotifier
    {
        public event Action<Vector2> MouseIsMoved;
        public event Action<Vector2> MoveIsPressed;

        public void NotifyMouseIsMoved(Vector2 direction)
        {
            MouseIsMoved?.Invoke(direction);
        }

        public void NotifyMoveIsPressed(Vector2 direction)
        {
            MoveIsPressed?.Invoke(direction);
        }
    }
}