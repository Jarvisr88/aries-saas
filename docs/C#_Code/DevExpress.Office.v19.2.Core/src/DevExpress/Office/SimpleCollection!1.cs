namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class SimpleCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable, DevExpress.Office.IReadOnlyList<T>, DevExpress.Office.IReadOnlyCollection<T>
    {
        private List<T> innerList;

        protected SimpleCollection()
        {
            this.innerList = new List<T>();
        }

        public virtual int Add(T item)
        {
            int index = this.AddWithoutNotification(item);
            this.OnItemInserted(index, item);
            return index;
        }

        protected virtual int AddWithoutNotification(T item)
        {
            Guard.ArgumentNotNull(item, "Item");
            this.InnerList.Add(item);
            return (this.Count - 1);
        }

        public virtual void Clear()
        {
            if (this.Count > 0)
            {
                this.InnerList.Clear();
                this.OnModified();
            }
        }

        public virtual bool Contains(T item) => 
            this.innerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        public void ForEach(Action<T> action)
        {
            this.innerList.ForEach(action);
        }

        public virtual IEnumerator<T> GetEnumerator() => 
            this.InnerList.GetEnumerator();

        public virtual int IndexOf(T item) => 
            this.innerList.IndexOf(item);

        public virtual void Insert(int index, T item)
        {
            this.innerList.Insert(index, item);
            this.OnItemInserted(index, item);
        }

        protected virtual void OnItemInserted(int index, T item)
        {
            this.OnModified();
        }

        protected virtual void OnItemRemoved(int index, T item)
        {
            this.OnModified();
        }

        protected virtual void OnModified()
        {
        }

        public virtual bool Remove(T item)
        {
            int index = this.innerList.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            this.RemoveAt(index);
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            T item = this[index];
            this.innerList.RemoveAt(index);
            this.OnItemRemoved(index, item);
        }

        public virtual void Sort(int index, int count, IComparer<T> comparer)
        {
            this.innerList.Sort(index, count, comparer);
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public int Count =>
            this.innerList.Count;

        public int Capacity
        {
            get => 
                this.innerList.Capacity;
            set => 
                this.innerList.Capacity = value;
        }

        public T this[int index] =>
            this.innerList[index];

        public List<T> InnerList =>
            this.innerList;

        public T First
        {
            get
            {
                if (this.Count > 0)
                {
                    return this.InnerList[0];
                }
                return default(T);
            }
        }

        public T Last
        {
            get
            {
                if (this.Count > 0)
                {
                    return this.InnerList[this.Count - 1];
                }
                return default(T);
            }
        }

        public bool IsReadOnly =>
            false;
    }
}

