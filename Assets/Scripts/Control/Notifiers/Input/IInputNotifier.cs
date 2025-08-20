using System;

namespace Notifiers
{
    public interface IInputNotifier
    {
        event Action ForwardIsPressed;
        event Action BackwardIsPressed;
        event Action LeftIsPressed;
        event Action RightIsPressed;

        void NotifyForwardIsPressed();
        void NotifyBackwardIsPressed();
        void NotifyLeftIsPressed();
        void NotifyRightIsPressed();
    }
}