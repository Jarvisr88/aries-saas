namespace DevExpress.ReportServer.IndexedCache
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class IndexedCacheItem<T> : IDisposable
    {
        private readonly HashSet<Action<int[]>> itemCachedCallbacks;

        public IndexedCacheItem(T value)
        {
            this.State = IndexedCacheItemState.NotRequested;
            this.Value = value;
            this.itemCachedCallbacks = new HashSet<Action<int[]>>();
        }

        public void AddItemCachedCallback(Action<int[]> callback)
        {
            if (!this.itemCachedCallbacks.Contains(callback))
            {
                this.itemCachedCallbacks.Add(callback);
            }
        }

        public void Dispose()
        {
            this.DisposeValue();
            this.itemCachedCallbacks.Clear();
        }

        private void DisposeValue()
        {
            IDisposable disposable = this.Value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public Action<int[]>[] GetItemCachedCallbacks()
        {
            Action<int[]>[] array = new Action<int[]>[this.itemCachedCallbacks.Count];
            this.itemCachedCallbacks.CopyTo(array);
            return array;
        }

        public void SetRealValue(T value)
        {
            this.DisposeValue();
            this.Value = value;
            this.State = IndexedCacheItemState.Cached;
            this.itemCachedCallbacks.Clear();
        }

        public T Value { get; private set; }

        public IndexedCacheItemState State { get; set; }
    }
}

