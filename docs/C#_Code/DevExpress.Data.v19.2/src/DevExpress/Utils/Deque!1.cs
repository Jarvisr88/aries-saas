namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class Deque<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private T[] arr;
        private int size;
        private int head;
        private int tail;

        public Deque()
        {
            this.arr = new T[0];
        }

        public void Clear()
        {
            this.arr = new T[0];
            this.size = 0;
            this.head = 0;
            this.tail = 0;
        }

        public bool Contains(T item)
        {
            bool flag;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            using (IEnumerator<T> enumerator = this.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (!comparer.Equals(item, current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (this.head < this.tail)
            {
                Array.Copy(this.arr, this.head, array, arrayIndex, this.tail - this.head);
            }
            else
            {
                int length = this.arr.Length - this.head;
                Array.Copy(this.arr, this.head, array, arrayIndex, length);
                Array.Copy(this.arr, 0, array, arrayIndex + length, this.tail);
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__17))]
        public IEnumerator<T> GetEnumerator()
        {
            int head;
            int head;
            int num2;
            if (this.head >= this.tail)
            {
                head = this.head;
            }
            else
            {
                head = this.head;
                goto TR_0003;
            }
        Label_PostSwitchInIterator:;
            if (head < this.arr.Length)
            {
                yield return this.arr[head];
                num2 = head;
                head = num2 + 1;
                goto Label_PostSwitchInIterator;
            }
            for (int i = 0; i < this.tail; i = num2 + 1)
            {
                yield return this.arr[i];
                num2 = i;
            }
        TR_0003:
            while (head < this.tail)
            {
                yield return this.arr[head];
                num2 = head;
                head = num2 + 1;
            }
            goto TR_0003;
        }

        public T PopBack()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }
            this.tail = (this.tail != 0) ? (this.tail - 1) : (this.arr.Length - 1);
            T local = this.arr[this.tail];
            this.arr[this.tail] = default(T);
            this.size--;
            return local;
        }

        public T PopFront()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException();
            }
            T local = this.arr[this.head];
            this.arr[this.head] = default(T);
            this.head++;
            if (this.head == this.arr.Length)
            {
                this.head = 0;
            }
            this.size--;
            return local;
        }

        public void PushBack(T item)
        {
            if (this.size == this.arr.Length)
            {
                this.SetCapacity(Math.Max(4, this.arr.Length * 2));
            }
            int tail = this.tail;
            this.tail = tail + 1;
            this.arr[tail] = item;
            if (this.tail == this.arr.Length)
            {
                this.tail = 0;
            }
            this.size++;
        }

        public void PushFront(T item)
        {
            if (this.size == this.arr.Length)
            {
                this.SetCapacity(Math.Max(4, this.arr.Length * 2));
            }
            this.head = (this.head != 0) ? (this.head - 1) : (this.arr.Length - 1);
            this.arr[this.head] = item;
            this.size++;
        }

        private void SetCapacity(int capacity)
        {
            T[] array = new T[capacity];
            this.CopyTo(array, 0);
            this.head = 0;
            this.tail = (this.size == capacity) ? 0 : this.size;
            this.arr = array;
        }

        void ICollection<T>.Add(T item)
        {
            this.PushBack(item);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Count =>
            this.size;

        public T this[int index]
        {
            get
            {
                if ((index < 0) || (index > (this.size - 1)))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return this.arr[(this.head + index) % this.arr.Length];
            }
            set
            {
                if ((index < 0) || (index > (this.size - 1)))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                this.arr[(this.head + index) % this.arr.Length] = value;
            }
        }

        bool ICollection<T>.IsReadOnly =>
            false;

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__17 : IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public Deque<T> <>4__this;
            private int <i>5__1;
            private int <j>5__2;
            private int <k>5__3;

            [DebuggerHidden]
            public <GetEnumerator>d__17(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num2;
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        if (this.<>4__this.head >= this.<>4__this.tail)
                        {
                            this.<j>5__2 = this.<>4__this.head;
                        }
                        else
                        {
                            this.<i>5__1 = this.<>4__this.head;
                            goto TR_0003;
                        }
                        break;

                    case 1:
                        this.<>1__state = -1;
                        num2 = this.<i>5__1;
                        this.<i>5__1 = num2 + 1;
                        goto TR_0003;

                    case 2:
                        this.<>1__state = -1;
                        num2 = this.<j>5__2;
                        this.<j>5__2 = num2 + 1;
                        break;

                    case 3:
                        this.<>1__state = -1;
                        num2 = this.<k>5__3;
                        this.<k>5__3 = num2 + 1;
                        goto TR_0006;

                    default:
                        return false;
                }
                if (this.<j>5__2 < this.<>4__this.arr.Length)
                {
                    this.<>2__current = this.<>4__this.arr[this.<j>5__2];
                    this.<>1__state = 2;
                    return true;
                }
                this.<k>5__3 = 0;
                goto TR_0006;
            TR_0001:
                return false;
            TR_0003:
                if (this.<i>5__1 < this.<>4__this.tail)
                {
                    this.<>2__current = this.<>4__this.arr[this.<i>5__1];
                    this.<>1__state = 1;
                    return true;
                }
                goto TR_0001;
            TR_0006:
                if (this.<k>5__3 < this.<>4__this.tail)
                {
                    this.<>2__current = this.<>4__this.arr[this.<k>5__3];
                    this.<>1__state = 3;
                    return true;
                }
                goto TR_0001;
            }

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

