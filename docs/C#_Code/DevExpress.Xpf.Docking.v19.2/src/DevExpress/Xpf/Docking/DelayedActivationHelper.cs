namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Threading;

    internal class DelayedActivationHelper : DependencyObject
    {
        private readonly DelayedActivationCallback callback;
        private DispatcherOperation _operation;

        public DelayedActivationHelper(DelayedActivationCallback callback)
        {
            this.callback = callback;
        }

        private void Activate(DelayedActivation delayedActivation)
        {
            if (this._operation != null)
            {
                this._operation.Abort();
            }
            if (!delayedActivation.Delayed)
            {
                this.callback(delayedActivation);
            }
            else
            {
                object[] args = new object[] { delayedActivation };
                this._operation = base.Dispatcher.BeginInvoke(this.callback, args);
            }
        }

        internal void Activate(BaseLayoutItem item, bool focus, bool delayed)
        {
            this.Activate(new DelayedActivation(item, focus, delayed));
        }

        public delegate void DelayedActivationCallback(DelayedActivation arg);
    }
}

