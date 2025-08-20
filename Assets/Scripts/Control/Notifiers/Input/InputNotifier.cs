using System;

namespace Notifiers
{
    public class InputNotifier : IInputNotifier
    {
        public event Action ForwardIsPressed;
        public event Action BackwardIsPressed;
        public event Action LeftIsPressed;
        public event Action RightIsPressed;
        
        public void NotifyForwardIsPressed()
        {
            ForwardIsPressed?.Invoke();
        }

        public void NotifyBackwardIsPressed()
        {
            BackwardIsPressed?.Invoke();
        }

        public void NotifyLeftIsPressed()
        {
            LeftIsPressed?.Invoke();
        }

        public void NotifyRightIsPressed()
        {
            RightIsPressed?.Invoke();
        }
    }
}