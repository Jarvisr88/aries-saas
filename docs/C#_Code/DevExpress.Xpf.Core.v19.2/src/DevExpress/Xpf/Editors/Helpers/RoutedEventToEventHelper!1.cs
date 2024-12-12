namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class RoutedEventToEventHelper<T> : ITargetChangedHelper<T>
    {
        [CompilerGenerated]
        private TargetChangedEventHandler<T> TargetChanged;

        public event TargetChangedEventHandler<T> TargetChanged
        {
            [CompilerGenerated] add
            {
                TargetChangedEventHandler<T> targetChanged = this.TargetChanged;
                while (true)
                {
                    TargetChangedEventHandler<T> comparand = targetChanged;
                    TargetChangedEventHandler<T> handler3 = comparand + value;
                    targetChanged = Interlocked.CompareExchange<TargetChangedEventHandler<T>>(ref this.TargetChanged, handler3, comparand);
                    if (ReferenceEquals(targetChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                TargetChangedEventHandler<T> targetChanged = this.TargetChanged;
                while (true)
                {
                    TargetChangedEventHandler<T> comparand = targetChanged;
                    TargetChangedEventHandler<T> handler3 = comparand - value;
                    targetChanged = Interlocked.CompareExchange<TargetChangedEventHandler<T>>(ref this.TargetChanged, handler3, comparand);
                    if (ReferenceEquals(targetChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public RoutedEventToEventHelper(FrameworkElement element, RoutedEvent routedEvent, Func<RoutedEventArgs, T> getValueHandler)
        {
            this.Element = element;
            this.Event = routedEvent;
            this.GetValueHandler = getValueHandler;
            this.Subscribe();
        }

        public void RaiseTargetChanged(T value)
        {
            if (this.TargetChanged != null)
            {
                this.TargetChanged(this, new TargetChangedEventArgs<T>(value));
            }
        }

        private void Subscribe()
        {
            this.Element.AddHandler(this.Event, (d, e) => base.RaiseTargetChanged(base.GetValueHandler(e)), false);
        }

        public FrameworkElement Element { get; private set; }

        public RoutedEvent Event { get; private set; }

        private Func<RoutedEventArgs, T> GetValueHandler { get; set; }
    }
}

