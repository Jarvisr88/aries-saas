namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ListWrapper<TBase, T> : IList<TBase>, ICollection<TBase>, IEnumerable<TBase>, IEnumerable where T: TBase
    {
        private IList<T> list;

        public ListWrapper(IList<T> list)
        {
            this.list = list;
        }

        public void Add(TBase item)
        {
            this.list.Add((T) item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(TBase item) => 
            this.list.Contains((T) item);

        public void CopyTo(TBase[] array, int arrayIndex)
        {
            for (int i = 0; i < this.list.Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<TBase> GetEnumerator() => 
            this.list.Cast<TBase>().GetEnumerator();

        public int IndexOf(TBase item) => 
            this.list.IndexOf((T) item);

        public void Insert(int index, TBase item)
        {
            this.list.Insert(index, (T) item);
        }

        public bool Remove(TBase item) => 
            this.list.Remove((T) item);

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.list.Cast<TBase>().GetEnumerator();

        public TBase this[int index]
        {
            get => 
                this.list[index];
            set => 
                this[index] = (TBase) ((T) value);
        }

        public int Count =>
            this.list.Count;

        bool ICollection<TBase>.IsReadOnly =>
            this.list.IsReadOnly;
    }
}

