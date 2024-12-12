namespace DevExpress.Mvvm.Native
{
    using System;

    internal class CollectionBinderTwoWay : IDisposable
    {
        private readonly CollectionBinderOneWay sourceToTarget;
        private readonly CollectionBinderOneWay targetToSource;

        public CollectionBinderTwoWay(object source, object target, CollectionBinder.SyncDelegate fromSourceToTarget, CollectionBinder.SyncDelegate fromTargetToSource, bool weakSource, bool weakTarget)
        {
            CollectionLocker doNotProcessSourceCollectionChanged = new CollectionLocker();
            CollectionLocker doNotProcessTargetCollectionChanged = new CollectionLocker();
            this.sourceToTarget = new CollectionBinderOneWay(source, target, fromSourceToTarget, weakSource, weakTarget, doNotProcessSourceCollectionChanged, doNotProcessTargetCollectionChanged);
            this.targetToSource = new CollectionBinderOneWay(target, source, fromTargetToSource, weakTarget, weakSource, doNotProcessTargetCollectionChanged, doNotProcessSourceCollectionChanged);
        }

        public void Dispose()
        {
            this.sourceToTarget.Dispose();
            this.targetToSource.Dispose();
        }
    }
}

