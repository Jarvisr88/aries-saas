namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomNotification
    {
        private CustomNotifier notifier;

        public CustomNotification(object viewModel, CustomNotifier notifier)
        {
            this.ViewModel = viewModel;
            this.notifier = notifier;
        }

        internal void Activate()
        {
            this.notifier.Activate(this);
        }

        internal void Dismiss()
        {
            this.notifier.Dismiss(this);
        }

        public void Hide()
        {
            this.notifier.Hide(this);
        }

        internal void ResetTimer()
        {
            this.notifier.ResetTimer(this);
        }

        internal void StopTimer()
        {
            this.notifier.StopTimer(this);
        }

        internal void TimeOut()
        {
            this.notifier.TimeOut(this);
        }

        public object ViewModel { get; set; }
    }
}

