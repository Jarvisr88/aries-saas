namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class EndInitPostponedAction
    {
        private bool performaActionOnEndInit;
        private readonly Func<bool> isLoadingDelegate;
        private Action action;

        public EndInitPostponedAction(Func<bool> isLoadingDelegate);
        public void PerformActionOnEndInitIfNeeded();
        public void PerformActionOnEndInitIfNeeded(Action action);
        public void PerformIfNotLoading(Action action, Action actionWhenLoading = null);
    }
}

