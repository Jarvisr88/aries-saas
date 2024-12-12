namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LimitedDeque<T> : IEnumerable<T>, IEnumerable
    {
        public const int DefaultCapacity = 0x100;
        private object lockObject;
        private T[] buffer;
        private int position;
        private int count;

        public LimitedDeque(int capacity = 0x100)
        {
            this.lockObject = new object();
            if (capacity <= 0)
            {
                throw new ArgumentException("capacity");
            }
            this.position = -1;
            this.count = 0;
            this.buffer = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator() => 
            new LimitedDequeEnumerator<T>(this.buffer, this.position, this.count);

        public T Pop()
        {
            T local2;
            object lockObject = this.lockObject;
            lock (lockObject)
            {
                if ((this.position < 0) || (this.count <= 0))
                {
                    local2 = default(T);
                    local2 = local2;
                }
                else
                {
                    T local = this.buffer[this.position];
                    this.buffer[this.position] = default(T);
                    this.count--;
                    this.position--;
                    if (this.position < 0)
                    {
                        this.position = this.buffer.Length - 1;
                    }
                    local2 = local;
                }
            }
            return local2;
        }

        public T Push(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if ((this.position < 0) || ((this.buffer[this.position] == null) || !this.buffer[this.position].Equals(value)))
            {
                object lockObject = this.lockObject;
                lock (lockObject)
                {
                    this.position++;
                    this.count = (this.count < this.buffer.Length) ? (this.count + 1) : this.count;
                    if (this.position == this.buffer.Length)
                    {
                        this.position = 0;
                    }
                    this.buffer[this.position] = value;
                }
            }
            return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            new LimitedDequeEnumerator<T>(this.buffer, this.position, this.count);

        public bool Empty
        {
            get
            {
                object lockObject = this.lockObject;
                lock (lockObject)
                {
                    return (this.count == 0);
                }
            }
        }

        public int Position =>
            this.position;

        public int Capacity =>
            this.buffer.Length;

        public int Count =>
            this.count;
    }
}

