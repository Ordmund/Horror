using System;
using UnityEngine;

namespace GameInput
{
    public interface IInputNotifier
    {
        event Action<Vector2> LookIsInteracted;
        event Action<Vector2> MoveIsPressed;

        void NotifyLookIsInteracted(Vector2 direction);
        void NotifyMoveIsPressed(Vector2 direction);
    }
}