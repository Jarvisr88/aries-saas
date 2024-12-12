namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfCache<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> objectStorage;
        private IList<TKey> queue;
        private long size;
        private long limit;

        protected PdfCache(long limit)
        {
            this.objectStorage = new Dictionary<TKey, TValue>();
            this.queue = new List<TKey>();
            this.limit = limit;
        }

        protected void AddValue(TKey key, TValue value)
        {
            this.objectStorage.Add(key, value);
            this.size += this.GetSizeOfValue(value);
            this.queue.Add(key);
        }

        protected void CheckCapacity()
        {
            if (this.limit > 0L)
            {
                while (this.size > this.limit)
                {
                    TKey key = this.queue[0];
                    TValue local2 = this.objectStorage[key];
                    this.size -= this.GetSizeOfValue(local2);
                    this.queue.RemoveAt(0);
                    this.objectStorage.Remove(key);
                    this.DisposeValue(local2);
                }
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<TKey, TValue> pair in this.objectStorage)
            {
                this.DisposeValue(pair.Value);
            }
            this.objectStorage.Clear();
            this.queue.Clear();
            this.size = 0L;
        }

        public void Clear(TKey key)
        {
            if (this.objectStorage.ContainsKey(key))
            {
                this.DisposeValue(this.objectStorage[key]);
                this.objectStorage.Remove(key);
                this.queue.Remove(key);
            }
        }

        private void DisposeValue(TValue value)
        {
            IDisposable disposable = value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        protected abstract long GetSizeOfValue(TValue value);
        protected void Remove(TKey key)
        {
            this.size -= this.GetSizeOfValue(this.objectStorage[key]);
            TValue local = default(TValue);
            this.objectStorage[key] = local;
            this.objectStorage.Remove(key);
        }

        protected void UpdateQueue(TKey key)
        {
            this.queue.Remove(key);
            this.queue.Add(key);
        }

        protected void UpdateValue(TKey key, TValue oldValue, TValue value)
        {
            this.size -= this.GetSizeOfValue(oldValue);
            this.objectStorage[key] = value;
            this.DisposeValue(oldValue);
            this.size += this.GetSizeOfValue(value);
            this.UpdateQueue(key);
        }

        protected long Limit
        {
            get => 
                this.limit;
            set
            {
                this.limit = value;
                this.CheckCapacity();
            }
        }

        protected IDictionary<TKey, TValue> ObjectStorage =>
            this.objectStorage;
    }
}

