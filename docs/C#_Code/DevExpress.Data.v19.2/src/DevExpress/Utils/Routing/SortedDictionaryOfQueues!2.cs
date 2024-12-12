namespace DevExpress.Utils.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class SortedDictionaryOfQueues<TKey, T>
    {
        private readonly Dictionary<TKey, Queue<T>> dictionary;
        private readonly Func<T, TKey> getKey;
        private readonly SortedSet<TKey> Keys;
        private static readonly EqualityComparer<T> Comparer;

        static SortedDictionaryOfQueues()
        {
            SortedDictionaryOfQueues<TKey, T>.Comparer = EqualityComparer<T>.Default;
        }

        public SortedDictionaryOfQueues(Func<T, TKey> getKey, IEqualityComparer<TKey> comparer = null)
        {
            this.getKey = getKey;
            IEqualityComparer<TKey> comparer1 = comparer;
            if (comparer == null)
            {
                IEqualityComparer<TKey> local1 = comparer;
                comparer1 = EqualityComparer<TKey>.Default;
            }
            this.dictionary = new Dictionary<TKey, Queue<T>>(comparer1);
            this.Keys = new SortedSet<TKey>();
        }

        public T DequeueMin()
        {
            if (this.Keys.Count == 0)
            {
                throw new InvalidOperationException();
            }
            TKey min = this.Keys.Min;
            Queue<T> queue = this.dictionary[min];
            T local2 = queue.Dequeue();
            if (queue.Count == 0)
            {
                this.dictionary.Remove(min);
                this.Keys.Remove(min);
            }
            return local2;
        }

        public void Enqueue(T item)
        {
            Queue<T> queue;
            TKey key = this.getKey(item);
            if (!this.dictionary.TryGetValue(key, out queue))
            {
                queue = new Queue<T>(5);
                this.dictionary[key] = queue;
                this.Keys.Add(key);
            }
            queue.Enqueue(item);
        }

        public void Remove(T item)
        {
            TKey key = this.getKey(item);
            Queue<T> queue = this.dictionary[key];
            if (queue.Count <= 1)
            {
                this.dictionary.Remove(key);
                this.Keys.Remove(key);
            }
            else if (SortedDictionaryOfQueues<TKey, T>.Comparer.Equals(queue.Peek(), item))
            {
                queue.Dequeue();
            }
            else
            {
                this.dictionary[key] = new Queue<T>(from x in queue.ToArray()
                    where !SortedDictionaryOfQueues<TKey, T>.Comparer.Equals(x, item)
                    select x);
            }
        }
    }
}

