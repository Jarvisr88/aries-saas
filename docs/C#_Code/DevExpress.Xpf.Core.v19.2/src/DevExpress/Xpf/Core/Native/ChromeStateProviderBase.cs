namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public abstract class ChromeStateProviderBase
    {
        private FrameworkElement source;
        private RenderStrategy target;
        private string state;

        protected ChromeStateProviderBase();
        protected void ChangeState();
        public abstract string GetActualForegroundStateName(IEnumerable<string> availableStates);
        public abstract string GetBaseState(string desiredState, IEnumerable<string> allStates);
        protected abstract string GetState();
        private void OnSourceChanged(FrameworkElement oldValue);
        private void OnStateChanged(string oldValue);
        private void OnTargetChanged(RenderStrategy oldValue);
        protected abstract void Subscribe(FrameworkElement source);
        protected abstract void Unsubscribe(FrameworkElement source);
        private void UpdateTarget();

        public FrameworkElement Source { get; set; }

        public string State { get; set; }

        public RenderStrategy Target { get; set; }
    }
}

