namespace DevExpress.Printing.Native.StreamingPagination
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class BucketQueueExt
    {
        private static readonly Pair<int, int>[] bucketSizes = new Pair<int, int>[] { new Pair<int, int>(1, 1), new Pair<int, int>(5, 2), new Pair<int, int>(15, 5), new Pair<int, int>(0x23, 10), new Pair<int, int>(0x4b, 20), new Pair<int, int>(0x7fffffff, 50) };

        [IteratorStateMachine(typeof(<DequeueItems>d__3))]
        public static IEnumerable<T> DequeueItems<T>(this BucketQueue<T> queue)
        {
            <DequeueItems>d__3<T> d__1 = new <DequeueItems>d__3<T>(-2);
            d__1.<>3__queue = queue;
            return d__1;
        }

        public static void Enqueue<T>(this BucketQueue<T> queue, IEnumerable<T> items)
        {
            items.ForEach<T>(item => queue.Enqueue(item));
        }

        public static int GetEffectiveBucketSize(int enqueuedItemsCount, int currentBucketSize)
        {
            foreach (Pair<int, int> pair in bucketSizes)
            {
                if (enqueuedItemsCount < pair.First)
                {
                    return Math.Max(currentBucketSize, pair.Second);
                }
            }
            return Math.Max(currentBucketSize, bucketSizes.Last<Pair<int, int>>().Second);
        }

        [CompilerGenerated]
        private sealed class <DequeueItems>d__3<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private BucketQueue<T> queue;
            public BucketQueue<T> <>3__queue;
            private T <item>5__1;

            [DebuggerHidden]
            public <DequeueItems>d__3(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    this.<item>5__1 = default(T);
                }
                try
                {
                    this.<item>5__1 = this.queue.Dequeue();
                }
                catch
                {
                    return false;
                }
                this.<>2__current = this.<item>5__1;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                BucketQueueExt.<DequeueItems>d__3<T> d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new BucketQueueExt.<DequeueItems>d__3<T>(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = (BucketQueueExt.<DequeueItems>d__3<T>) this;
                }
                d__.queue = this.<>3__queue;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

