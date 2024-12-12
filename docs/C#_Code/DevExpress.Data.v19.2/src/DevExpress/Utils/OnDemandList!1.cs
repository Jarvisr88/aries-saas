namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class OnDemandList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        private T[] store;

        protected OnDemandList(int itemsCount)
        {
            this.store = new T[itemsCount];
        }

        protected abstract T CreateItem(int listIndex);
        public T GetRealValue(int index) => 
            this.store[index];

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Contains(T item) => 
            this.List.IndexOf(item) >= 0;

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            new ListEnumerator<T>(this);

        int IList<T>.IndexOf(T item)
        {
            for (int i = 0; i < this.store.Length; i++)
            {
                if (Equals(item, this.store[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.List.GetEnumerator();

        int IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Clear()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private IList<T> List =>
            this;

        T IList<T>.this[int index]
        {
            get
            {
                if (this.store[index] == null)
                {
                    this.store[index] = this.CreateItem(index);
                }
                return this.store[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int ICollection<T>.Count =>
            this.store.Length;

        bool ICollection<T>.IsReadOnly =>
            true;

        bool IList.IsFixedSize =>
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

