using System;
using UnityEngine;

namespace Notifiers
{
    public interface IInputNotifier
    {
        event Action<Vector2> MouseIsMoved;
        event Action<Vector2> MoveIsPressed;

        void NotifyMouseIsMoved(Vector2 direction);
        void NotifyMoveIsPressed(Vector2 direction);
    }
}