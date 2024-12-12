namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class EventToEventHelper<T> : ITargetChangedHelper<T>
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

        public void RaiseTargetChanged(T value)
        {
            if (this.TargetChanged != null)
            {
                this.TargetChanged(this, new TargetChangedEventArgs<T>(value));
            }
        }
    }
}

