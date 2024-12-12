namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class DoubleClickImplementer
    {
        private object owner;
        private DispatcherTimer doubleClickTimer;
        private bool isTimerWorking;

        public DoubleClickImplementer(object owner);
        protected virtual void OnDoubleClickTimerTick(object sender, EventArgs e);
        protected virtual void OnMouseDoubleClickCore(MouseButtonEventArgs e, MouseButtonEventHandler mouseDoubleClickHandlerOverride, MouseButtonEventHandler mouseDoubleClickEvent);
        public void OnMouseLeftButtonUpDoubleClickForce(MouseButtonEventArgs e, MouseButtonEventHandler mouseDoubleClickHandlerOverride, MouseButtonEventHandler mouseDoubleClickEvent);
        private void RaiseMouseDoubleClick(MouseButtonEventArgs e, MouseButtonEventHandler mouseDoubleClickEvent);
        public void SetDoubleClickTimerInterval(TimeSpan interval);
        private void StartTimer();
        private void StopTimer();
    }
}

