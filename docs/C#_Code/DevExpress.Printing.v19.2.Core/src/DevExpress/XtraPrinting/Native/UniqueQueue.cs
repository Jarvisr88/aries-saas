namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;

    public class UniqueQueue
    {
        private readonly Queue<object> queue = new Queue<object>();
        private readonly Dictionary<object, int> count = new Dictionary<object, int>();

        public void Clear()
        {
            this.queue.Clear();
            this.count.Clear();
        }

        public object Pop()
        {
            int num;
            object key = this.queue.Dequeue();
            if (this.count.TryGetValue(key, out num))
            {
                if (num == 0)
                {
                    return this.Pop();
                }
                if (num == 1)
                {
                    this.count.Remove(key);
                    return key;
                }
                object obj3 = key;
                this.count[obj3] -= 1;
            }
            return this.Pop();
        }

        public void Push(object key)
        {
            int num;
            this.queue.Enqueue(key);
            if (this.count.TryGetValue(key, out num))
            {
                this.count[key] = num + 1;
            }
            else
            {
                this.count[key] = 1;
            }
        }

        public int Count =>
            this.count.Count;
    }
}

