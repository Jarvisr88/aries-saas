namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class IndexedDictionary<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private List<T> itemsArray;
        private Dictionary<T, int> indices;

        public IndexedDictionary()
        {
            this.itemsArray = new List<T>();
        }

        public int Add(T item)
        {
            this.itemsArray.Add(item);
            this.Indices[item] = this.itemsArray.Count - 1;
            return (this.itemsArray.Count - 1);
        }

        public void Clear()
        {
            this.itemsArray.Clear();
            this.ClearIndices();
        }

        private void ClearIndices()
        {
            if (this.indices != null)
            {
                this.indices.Clear();
                this.indices = null;
            }
        }

        public int IndexOf(T item)
        {
            int num;
            return (!this.Indices.TryGetValue(item, out num) ? -1 : num);
        }

        public bool Remove(T item)
        {
            if (!this.itemsArray.Remove(item))
            {
                return false;
            }
            this.ClearIndices();
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < this.itemsArray.Count)
            {
                this.itemsArray.RemoveAt(index);
                this.ClearIndices();
            }
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        bool ICollection<T>.Contains(T item) => 
            this.IndexOf(item) >= 0;

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < this.itemsArray.Count; i++)
            {
                array.SetValue(this.itemsArray[i], (int) (i + arrayIndex));
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.itemsArray.GetEnumerator();

        void IList<T>.Insert(int index, T item)
        {
            this.itemsArray.Insert(index, item);
            this.ClearIndices();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.itemsArray.GetEnumerator();

        private Dictionary<T, int> Indices
        {
            get
            {
                if (this.indices == null)
                {
                    this.indices = new Dictionary<T, int>();
                    for (int i = 0; i < this.itemsArray.Count; i++)
                    {
                        this.indices.Add(this.itemsArray[i], i);
                    }
                }
                return this.indices;
            }
        }

        public int Count =>
            this.itemsArray.Count;

        public T this[int index]
        {
            get => 
                this.itemsArray[index];
            set
            {
                this.Indices.Remove(this.itemsArray[index]);
                this.itemsArray[index] = value;
                this.Indices.Add(this.itemsArray[index], index);
            }
        }

        bool ICollection<T>.IsReadOnly =>
            false;
    }
}

