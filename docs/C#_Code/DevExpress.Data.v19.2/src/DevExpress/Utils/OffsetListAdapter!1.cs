namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class OffsetListAdapter<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        private IList<T> source;
        private int offset;
        private int count;

        public OffsetListAdapter(IList<T> source, int offset, int count)
        {
            if (!(source is IList) || !((IList) source).IsFixedSize)
            {
                throw new NotSupportedException();
            }
            if ((offset < 0) || ((offset + count) > source.Count))
            {
                throw new ArgumentException();
            }
            this.source = source;
            this.offset = offset;
            this.count = count;
        }

        public int Add(object value)
        {
            throw new NotSupportedException();
        }

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item) => 
            ((IList) this.List).Contains(item);

        public bool Contains(object value)
        {
            int index = this.source.IndexOf((T) value);
            return this.ContainsIndex(index);
        }

        private bool ContainsIndex(int index) => 
            (index >= 0) && ((index >= this.offset) && (index < (this.offset + this.count)));

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<T> GetEnumerator() => 
            new ListEnumerator<T>(this);

        public int IndexOf(T item) => 
            ((IList) this.List).IndexOf(item);

        public int IndexOf(object value)
        {
            int index = this.source.IndexOf((T) value);
            return (this.ContainsIndex(index) ? (index - this.offset) : -1);
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void Remove(object value)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.List.GetEnumerator();

        private IList<T> List =>
            this;

        public T this[int index]
        {
            get => 
                this.source[index + this.offset];
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count =>
            this.count;

        public bool IsReadOnly =>
            true;

        public bool IsFixedSize =>
            true;

        object IList.this[int index]
        {
            get => 
                this.List[index];
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool IsSynchronized =>
            false;

        public object SyncRoot =>
            null;
    }
}

