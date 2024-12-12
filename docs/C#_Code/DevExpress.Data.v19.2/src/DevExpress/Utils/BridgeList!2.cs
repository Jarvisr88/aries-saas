namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BridgeList<T, Key> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        protected readonly IList<Key> keys;

        protected BridgeList(IList<Key> keys)
        {
            Guard.ArgumentNotNull(keys, "keys");
            this.keys = keys;
        }

        protected virtual void AddCore(T item)
        {
            throw new NotImplementedException();
        }

        protected virtual int AddObjectCore(object item)
        {
            throw new NotImplementedException();
        }

        protected virtual void ClearCore()
        {
        }

        private void CopyTo(Array array, int arrayIndex)
        {
            if ((array != null) && (array.Rank != 1))
            {
                throw new ArgumentException("array.Rank");
            }
            try
            {
                Array.Copy(this.keys.ToArray<Key>(), 0, array, arrayIndex, this.keys.Count);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("Invalid array type");
            }
        }

        protected abstract T GetItemByKey(Key key, int index);
        protected virtual void InsertCore(int index, T item)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsReadOnlyCore() => 
            true;

        protected virtual void RemoveAtCore(int index)
        {
            throw new NotImplementedException();
        }

        protected virtual bool RemoveCore(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            this.AddCore(item);
        }

        void ICollection<T>.Clear()
        {
            this.ClearCore();
        }

        bool ICollection<T>.Contains(T item) => 
            this.List.IndexOf(item) >= 0;

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item) => 
            this.RemoveCore(item);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            new ListEnumerator<T>(this);

        int IList<T>.IndexOf(T item)
        {
            for (int i = 0; i < this.keys.Count; i++)
            {
                if (Equals(item, this.List[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        void IList<T>.Insert(int index, T item)
        {
            this.InsertCore(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            this.RemoveAtCore(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.List.GetEnumerator();

        int IList.Add(object value) => 
            this.AddObjectCore(value);

        void IList.Clear()
        {
            this.List.Clear();
        }

        bool IList.Contains(object value) => 
            this.List.Contains((T) value);

        int IList.IndexOf(object value) => 
            this.List.IndexOf((T) value);

        void IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAtCore(index);
        }

        private IList<T> List =>
            this;

        T IList<T>.this[int index]
        {
            get => 
                this.GetItemByKey(this.keys[index], index);
            set
            {
                throw new NotImplementedException();
            }
        }

        int ICollection<T>.Count =>
            this.keys.Count;

        bool ICollection<T>.IsReadOnly =>
            this.IsReadOnlyCore();

        bool IList.IsFixedSize =>
            this.IsFixedSize;

        protected virtual bool IsFixedSize =>
            true;

        bool IList.IsReadOnly =>
            this.List.IsReadOnly;

        object IList.this[int index]
        {
            get => 
                this.List[index];
            set
            {
                throw new NotImplementedException();
            }
        }

        int ICollection.Count =>
            this.List.Count;

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot =>
            this;
    }
}

