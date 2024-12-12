namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    [TargetType(typeof(Window)), TargetType(typeof(UserControl))]
    public class DispatcherService : ServiceBase, IDispatcherService
    {
        public static readonly DependencyProperty DelayProperty;
        private TimeSpan delay;

        static DispatcherService()
        {
            DelayProperty = DependencyProperty.Register("Delay", typeof(TimeSpan), typeof(DispatcherService), new PropertyMetadata(TimeSpan.Zero, (d, e) => ((DispatcherService) d).OnDelayChanged()));
        }

        public DispatcherService()
        {
            this.DispatcherPriority = System.Windows.Threading.DispatcherPriority.Normal;
        }

        public Task BeginInvoke(Action action) => 
            this.InvokeAsyncCore(action);

        public void Invoke(Action action)
        {
            this.InvokeCore(action);
        }

        private Task InvokeAsyncCore(Action action)
        {
            if (this.delay == TimeSpan.Zero)
            {
                return base.Dispatcher.BeginInvoke(action, this.DispatcherPriority, new object[0]).Task;
            }
            TaskCompletionSource<object> source = new TaskCompletionSource<object>();
            DispatcherTimer timer = new DispatcherTimer(this.DispatcherPriority, base.Dispatcher);
            EventHandler onTimerTick = null;
            onTimerTick = delegate (object s, EventArgs e) {
                timer.Tick -= onTimerTick;
                timer.Stop();
                action();
                source.SetResult(null);
            };
            timer.Tick += onTimerTick;
            timer.Interval = this.delay;
            timer.Start();
            return source.Task;
        }

        private void InvokeCore(Action action)
        {
            base.Dispatcher.Invoke(action);
        }

        private void OnDelayChanged()
        {
            this.delay = this.Delay;
        }

        public TimeSpan Delay
        {
            get => 
                (TimeSpan) base.GetValue(DelayProperty);
            set => 
                base.SetValue(DelayProperty, value);
        }

        public System.Windows.Threading.DispatcherPriority DispatcherPriority { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DispatcherService.<>c <>9 = new DispatcherService.<>c();

            internal void <.cctor>b__15_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DispatcherService) d).OnDelayChanged();
            }
        }
    }
}

