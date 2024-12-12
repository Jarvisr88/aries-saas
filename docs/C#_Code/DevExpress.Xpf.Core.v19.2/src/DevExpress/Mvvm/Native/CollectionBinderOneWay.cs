namespace DevExpress.Mvvm.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    internal class CollectionBinderOneWay : IDisposable
    {
        private readonly object source;
        private readonly object target;
        private readonly bool weakSource;
        private readonly bool weakTarget;
        private readonly CollectionBinder.SyncDelegate sync;
        private readonly CollectionLocker doNotProcessSourceCollectionChanged;
        private readonly CollectionLocker doNotProcessTargetCollectionChanged;
        private WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> sourceHandler;

        public CollectionBinderOneWay(object source, object target, CollectionBinder.SyncDelegate sync, bool weakSource, bool weakTarget) : this(source, target, sync, weakSource, weakTarget, new CollectionLocker(), new CollectionLocker())
        {
        }

        public CollectionBinderOneWay(object source, object target, CollectionBinder.SyncDelegate sync, bool weakSource, bool weakTarget, CollectionLocker doNotProcessSourceCollectionChanged, CollectionLocker doNotProcessTargetCollectionChanged)
        {
            this.source = weakSource ? new WeakReference(source, false) : source;
            this.target = weakTarget ? new WeakReference(target, false) : target;
            this.sync = sync;
            this.weakSource = weakSource;
            this.weakTarget = weakTarget;
            this.doNotProcessSourceCollectionChanged = doNotProcessSourceCollectionChanged;
            this.doNotProcessTargetCollectionChanged = doNotProcessTargetCollectionChanged;
            this.Subscribe();
        }

        public void Dispose()
        {
            this.Unsubscribe();
        }

        private object GetSource() => 
            (!this.weakSource || !((WeakReference) this.source).IsAlive) ? this.source : ((WeakReference) this.source).Target;

        private object GetTarget() => 
            (!this.weakTarget || !((WeakReference) this.target).IsAlive) ? this.target : ((WeakReference) this.target).Target;

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            object source = this.GetSource();
            object target = this.GetTarget();
            if ((source != null) && (target != null))
            {
                this.doNotProcessTargetCollectionChanged.DoLockedActionIfNotLocked(this.doNotProcessSourceCollectionChanged, () => this.sync(source, target, e));
            }
            else
            {
                this.Dispose();
            }
        }

        private void Subscribe()
        {
            object source = this.GetSource();
            if (source is INotifyCollectionChanged)
            {
                if (this.weakSource)
                {
                    Action<CollectionBinderOneWay, object, NotifyCollectionChangedEventArgs> onEventAction = <>c.<>9__13_0;
                    if (<>c.<>9__13_0 == null)
                    {
                        Action<CollectionBinderOneWay, object, NotifyCollectionChangedEventArgs> local1 = <>c.<>9__13_0;
                        onEventAction = <>c.<>9__13_0 = (x, s, e) => x.OnSourceCollectionChanged(s, e);
                    }
                    this.sourceHandler = new WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>(this, onEventAction, <>c.<>9__13_1 ??= delegate (WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> wh, object o) {
                        ((INotifyCollectionChanged) o).CollectionChanged -= wh.Handler;
                    }, <>c.<>9__13_2 ??= wh => new NotifyCollectionChangedEventHandler(wh.OnEvent));
                    ((INotifyCollectionChanged) source).CollectionChanged += this.sourceHandler.Handler;
                }
                else
                {
                    ((INotifyCollectionChanged) source).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
                }
            }
        }

        private void Unsubscribe()
        {
            object source = this.GetSource();
            if (source is INotifyCollectionChanged)
            {
                if (this.weakSource && (this.sourceHandler != null))
                {
                    ((INotifyCollectionChanged) source).CollectionChanged -= this.sourceHandler.Handler;
                    this.sourceHandler = null;
                }
                ((INotifyCollectionChanged) source).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionBinderOneWay.<>c <>9 = new CollectionBinderOneWay.<>c();
            public static Action<CollectionBinderOneWay, object, NotifyCollectionChangedEventArgs> <>9__13_0;
            public static Action<WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, object> <>9__13_1;
            public static Func<WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>, NotifyCollectionChangedEventHandler> <>9__13_2;

            internal void <Subscribe>b__13_0(CollectionBinderOneWay x, object s, NotifyCollectionChangedEventArgs e)
            {
                x.OnSourceCollectionChanged(s, e);
            }

            internal void <Subscribe>b__13_1(WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> wh, object o)
            {
                ((INotifyCollectionChanged) o).CollectionChanged -= wh.Handler;
            }

            internal NotifyCollectionChangedEventHandler <Subscribe>b__13_2(WeakEventHandler<CollectionBinderOneWay, NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> wh) => 
                new NotifyCollectionChangedEventHandler(wh.OnEvent);
        }
    }
}

