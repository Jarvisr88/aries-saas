namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class RealTimeQueue : IDisposable
    {
        private readonly SynchronizationContext syncContext;
        private readonly RealTimeEventsQueue outputQuery;
        private readonly ListChangedCoreDelegate listChangedCore;
        private IBindingList source;
        private RealTimePropertyDescriptorCollection sourcePropertyDescriptorCollection;
        private readonly bool useWeakEventHandler;
        private bool needReset;
        private bool isDisposed;
        private RealTimeQueue.QueueState state;
        private readonly object syncObject;
        private ListChangedWeakEventHandler<RealTimeQueue> listChangedHandler;
        private string displayableProperties;
        private int posted;

        internal RealTimeQueue(SynchronizationContext syncContext, IBindingList source, RealTimePropertyDescriptorCollection propertyDescriptorCollection, ListChangedCoreDelegate listChanged, bool useWeakEventHandler, string displayableProperties);
        public virtual void Dispose();
        private void EnqueueOutputItem(RealTimeEventBase command);
        private RealTimeProxyForObject[] GetAllRows(RealTimePropertyDescriptorCollection pdc);
        private void PushPropertyDescriptorChanged();
        private void somethingInTheQueue();
        private void SomethingInTheQueueUI();
        private void SourceListChanged(object sender, ListChangedEventArgs e);

        private ListChangedWeakEventHandler<RealTimeQueue> ListChangedHandler { get; }

        public string DisplayableProperties { get; set; }

        internal bool IsQueueEmpty { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RealTimeQueue.<>c <>9;
            public static Action<RealTimeQueue, object, ListChangedEventArgs> <>9__13_0;

            static <>c();
            internal void <get_ListChangedHandler>b__13_0(RealTimeQueue owner, object o, ListChangedEventArgs e);
        }

        private enum QueueState
        {
            public const RealTimeQueue.QueueState Stop = RealTimeQueue.QueueState.Stop;,
            public const RealTimeQueue.QueueState PrepareToWork = RealTimeQueue.QueueState.PrepareToWork;,
            public const RealTimeQueue.QueueState Work = RealTimeQueue.QueueState.Work;
        }
    }
}

