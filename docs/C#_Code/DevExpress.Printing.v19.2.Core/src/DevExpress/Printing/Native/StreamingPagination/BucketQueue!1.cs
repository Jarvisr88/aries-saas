namespace DevExpress.Printing.Native.StreamingPagination
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class BucketQueue<T>
    {
        private const int Default_initialBucketSize = 1;
        private const int Default_maxBucketSize = 50;
        private const int Default_maxBucketCount = 0x7fffffff;
        private const int SyncTimeout = 150;
        private readonly object SyncOutgoing;
        private readonly object SyncIngoing;
        private ConcurrentQueue<Bucket<T>> bucketQueue;
        private Bucket<T> ingoingBucket;
        private Bucket<T> outgoingBucket;

        public BucketQueue() : this(1, 50, 0x7fffffff)
        {
        }

        public BucketQueue(int maxBucketSize) : this(1, maxBucketSize, 0x7fffffff)
        {
        }

        public BucketQueue(int initialBucketSize, int maxBucketSize, int maxBucketCount)
        {
            this.SyncOutgoing = new object();
            this.SyncIngoing = new object();
            this.bucketQueue = new ConcurrentQueue<Bucket<T>>();
            this.BucketSize = initialBucketSize;
            this.MaxBucketSize = maxBucketSize;
            this.MaxBucketCount = maxBucketCount;
        }

        public void Complete()
        {
            object syncIngoing = this.SyncIngoing;
            lock (syncIngoing)
            {
                this.IsCompleted = true;
                this.bucketQueue.Enqueue(this.ingoingBucket);
                this.ingoingBucket = null;
                Monitor.Pulse(this.SyncIngoing);
            }
        }

        public T Dequeue()
        {
            if ((this.outgoingBucket == null) || (this.outgoingBucket.Count == 0))
            {
                this.outgoingBucket = this.DequeueBucketInternal(true);
            }
            return this.outgoingBucket.Dequeue();
        }

        public IEnumerable<T> DequeueBucket(bool wait = false) => 
            this.DequeueBucketInternal(wait);

        private Bucket<T> DequeueBucketInternal(bool wait)
        {
            if (this.IsCompleted)
            {
                object syncIngoing = this.SyncIngoing;
                lock (syncIngoing)
                {
                    if (this.bucketQueue.Count == 0)
                    {
                        throw new BucketQueueException("IsCompleted = true, Queue Is Empty");
                    }
                }
            }
            while (!this.bucketQueue.TryDequeue(out this.outgoingBucket) || (this.outgoingBucket == null))
            {
                if (wait)
                {
                    object syncIngoing = this.SyncIngoing;
                    lock (syncIngoing)
                    {
                        Monitor.Wait(this.SyncIngoing, 150);
                    }
                }
                if (!wait || (this.IsCompleted && (this.bucketQueue.Count <= 0)))
                {
                    throw new BucketQueueException();
                }
            }
            object syncOutgoing = this.SyncOutgoing;
            lock (syncOutgoing)
            {
                Monitor.Pulse(this.SyncOutgoing);
            }
            return this.outgoingBucket;
        }

        public void Enqueue(T item)
        {
            int enqueuedItemsCount;
            if (this.IsCompleted)
            {
                throw new BucketQueueException("IsCompleted = true");
            }
            Bucket<T> ingoingBucket = this.ingoingBucket;
            if (this.ingoingBucket == null)
            {
                Bucket<T> local1 = this.ingoingBucket;
                this.BucketSize = enqueuedItemsCount = Math.Min(this.MaxBucketSize, BucketQueueExt.GetEffectiveBucketSize(this.EnqueuedItemsCount, this.BucketSize));
                ingoingBucket = new Bucket<T>(enqueuedItemsCount);
            }
            this.ingoingBucket = ingoingBucket;
            this.ingoingBucket.Enqueue(item);
            enqueuedItemsCount = this.EnqueuedItemsCount;
            this.EnqueuedItemsCount = enqueuedItemsCount + 1;
            if (this.ingoingBucket.Count >= this.BucketSize)
            {
                this.WaitForBucketQueueIfNeed();
                this.bucketQueue.Enqueue(this.ingoingBucket);
                this.ingoingBucket = null;
                object syncIngoing = this.SyncIngoing;
                lock (syncIngoing)
                {
                    Monitor.Pulse(this.SyncIngoing);
                }
            }
        }

        private void WaitForBucketQueueIfNeed()
        {
            while (!this.IsCompleted && (this.bucketQueue.Count >= this.MaxBucketCount))
            {
                object syncOutgoing = this.SyncOutgoing;
                lock (syncOutgoing)
                {
                    Monitor.Wait(this.SyncOutgoing, 150);
                }
            }
        }

        public bool IsCompleted { get; private set; }

        public int BucketSize { get; private set; }

        public int MaxBucketSize { get; private set; }

        public int MaxBucketCount { get; set; }

        public int EnqueuedItemsCount { get; private set; }

        public int BucketCount =>
            this.bucketQueue.Count;

        protected class Bucket : Queue<T>
        {
            public Bucket(int capacity) : base(capacity)
            {
            }
        }
    }
}

