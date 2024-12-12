namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class LimitedDequeEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private T[] buffer;
        private int position;
        private int capacity;
        private int count;
        private int index;

        public LimitedDequeEnumerator(T[] buffer, int position, int count)
        {
            this.index = -1;
            this.capacity = buffer.Length;
            this.buffer = buffer;
            this.count = count;
            position = (position - count) + 1;
            this.position = (position < 0) ? (this.capacity + position) : position;
        }

        public void Dispose()
        {
            this.buffer = null;
        }

        public bool MoveNext()
        {
            this.index++;
            return ((this.count > 0) && ((this.index < this.buffer.Length) && (this.index < this.count)));
        }

        public void Reset()
        {
            this.index = -1;
        }

        public int CurrentPosition =>
            (this.index + this.position) % this.capacity;

        public T Current =>
            this.buffer[this.CurrentPosition];

        object IEnumerator.Current =>
            this.buffer[this.CurrentPosition];
    }
}

