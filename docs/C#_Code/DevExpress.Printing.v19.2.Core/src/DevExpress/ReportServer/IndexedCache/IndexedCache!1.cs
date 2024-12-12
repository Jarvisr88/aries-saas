namespace DevExpress.ReportServer.IndexedCache
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal abstract class IndexedCache<T> : ICache<T>, IDisposable
    {
        protected readonly IList<IndexedCacheItem<T>> cache;

        public IndexedCache()
        {
            this.cache = new List<IndexedCacheItem<T>>();
            this.IsDisposed = false;
        }

        public void Clear()
        {
            foreach (IndexedCacheItem<T> item in this.cache)
            {
                item.Dispose();
            }
            this.cache.Clear();
        }

        protected abstract T CreateFakeValue(int index, int count);
        public void Dispose()
        {
            this.Clear();
            this.IsDisposed = true;
        }

        public bool EnlargeCapacity(int capacity)
        {
            if (this.cache.Count >= capacity)
            {
                return false;
            }
            for (int i = this.cache.Count; i < capacity; i++)
            {
                this.cache.Add(new IndexedCacheItem<T>(this.CreateFakeValue(i, capacity)));
            }
            return true;
        }

        public void Ensure(int[] indexes, Action<int[]> listener)
        {
            lock (cache)
            {
                int[] numArray = this.EnsureIndexes(indexes);
                if (numArray.Length != 0)
                {
                    foreach (int num2 in numArray)
                    {
                        this.cache[num2].State = IndexedCacheItemState.Requested;
                        this.cache[num2].AddItemCachedCallback(listener);
                    }
                }
                this.StartRequestIfNeeded();
            }
        }

        protected int[] EnsureIndexes(int[] indexes)
        {
            List<int> list = new List<int>();
            foreach (int num2 in indexes)
            {
                if ((num2 < this.cache.Count) && (this.cache[num2].State == IndexedCacheItemState.NotRequested))
                {
                    list.Add(num2);
                }
            }
            return list.ToArray();
        }

        public bool IsElementCached(int index) => 
            this.cache[index].State == IndexedCacheItemState.Cached;

        protected void OnRequestCompleted(Dictionary<int, T> result)
        {
            HashSet<Action<int[]>> set = new HashSet<Action<int[]>>();
            int[] array = new int[result.Keys.Count];
            result.Keys.CopyTo(array, 0);
            foreach (KeyValuePair<int, T> pair in result)
            {
                if (pair.Key < this.cache.Count)
                {
                    IndexedCacheItem<T> item = this.cache[pair.Key];
                    Action<int[]>[] itemCachedCallbacks = item.GetItemCachedCallbacks();
                    int index = 0;
                    while (true)
                    {
                        if (index >= itemCachedCallbacks.Length)
                        {
                            item.SetRealValue(pair.Value);
                            break;
                        }
                        Action<int[]> action = itemCachedCallbacks[index];
                        if (!set.Contains(action))
                        {
                            set.Add(action);
                        }
                        index++;
                    }
                }
            }
            foreach (Action<int[]> action2 in set)
            {
                action2(array);
            }
            this.StartRequestIfNeeded();
        }

        public void RemoveAt(int index)
        {
            this.cache.RemoveAt(index);
        }

        protected abstract void StartRequestIfNeeded();

        protected bool IsDisposed { get; private set; }

        public int Capacity =>
            this.cache.Count;

        public T this[int index]
        {
            get => 
                this.cache[index].Value;
            set
            {
            }
        }
    }
}

