namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;

    public class LockedPostponedAction
    {
        private readonly EndInitPostponedAction endInitPostponedAction;
        private readonly Locker locker;

        public LockedPostponedAction();
        public void PerformIfNotInProgress(Action action);
        public void PerformLockedAction(Action primaryAction);
    }
}

