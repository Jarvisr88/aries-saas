namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ListWrapper<TBase> : IList<TBase>, ICollection<TBase>, IEnumerable<TBase>, IEnumerable, IList, ICollection
    {
        private IList list;

        public ListWrapper(IList list)
        {
            this.list = list;
        }

        public int Add(object value) => 
            this.list.Add(value);

        public void Add(TBase item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(object value) => 
            this.list.Contains(value);

        public bool Contains(TBase item) => 
            this.list.Contains(item);

        public void CopyTo(Array array, int index)
        {
            this.list.CopyTo(array, index);
        }

        public void CopyTo(TBase[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TBase> GetEnumerator() => 
            this.list.Cast<TBase>().GetEnumerator();

        public int IndexOf(object value) => 
            this.list.IndexOf(value);

        public int IndexOf(TBase item) => 
            this.list.IndexOf(item);

        public void Insert(int index, object value)
        {
            this.list.Insert(index, value);
        }

        public void Insert(int index, TBase item)
        {
            this.list.Insert(index, item);
        }

        public bool Remove(TBase item)
        {
            int index = this.list.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            this.list.RemoveAt(index);
            return true;
        }

        public void Remove(object value)
        {
            this.list.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.list.GetEnumerator();

        public TBase this[int index]
        {
            get => 
                (TBase) this.list[index];
            set => 
                this.list[index] = value;
        }

        object IList.this[int index]
        {
            get => 
                this.list[index];
            set => 
                this.list[index] = value;
        }

        public int Count =>
            this.list.Count;

        bool IList.IsFixedSize =>
            this.list.IsFixedSize;

        public bool IsReadOnly =>
            this.list.IsReadOnly;

        public bool IsSynchronized =>
            this.list.IsSynchronized;

        public object SyncRoot =>
            this.list.SyncRoot;
    }
}

