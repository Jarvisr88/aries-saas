namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class PostponedAction
    {
        private readonly Func<bool> postponeDelegate;
        private Action action;
        public EventHandler ActionPerformed;

        public PostponedAction(Func<bool> postponeDelegate);
        public void Perform();
        public void PerformForce();
        public void PerformForce(Action action);
        public void PerformPostpone(Action action);
        private void RaiseActionPerformed();
    }
}

