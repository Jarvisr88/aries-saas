namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Reflection;

    public class List<T, U> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection where U: struct, IConvertToInt<U>
    {
        private static readonly U indexConverter;
        private readonly List<T> innerList;

        public List()
        {
            this.innerList = new List<T>();
        }

        public List(IEnumerable<T> collection)
        {
            this.innerList = new List<T>(collection);
        }

        public List(int capacity)
        {
            this.innerList = new List<T>(capacity);
        }

        public void Add(T item)
        {
            this.InnerList.Add(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            this.InnerList.AddRange(collection);
        }

        public ReadOnlyCollection<T> AsReadOnly() => 
            this.InnerList.AsReadOnly();

        public U BinarySearch(T item) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.BinarySearch(item));

        public U BinarySearch(T item, IComparer<T> comparer) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.BinarySearch(item, comparer));

        public U BinarySearch(U index, int count, T item, IComparer<T> comparer) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.BinarySearch(index.ToInt(), count, item, comparer));

        public virtual void Clear()
        {
            this.InnerList.Clear();
        }

        public bool Contains(T item) => 
            this.InnerList.Contains(item);

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => 
            this.InnerList.ConvertAll<TOutput>(converter);

        public void CopyTo(T[] array)
        {
            this.InnerList.CopyTo(array);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.InnerList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(U index, T[] array, int arrayIndex, int count)
        {
            this.InnerList.CopyTo(index.ToInt(), array, arrayIndex, count);
        }

        public bool Exists(Predicate<T> match) => 
            this.InnerList.Exists(match);

        public T Find(Predicate<T> match) => 
            this.InnerList.Find(match);

        public List<T> FindAll(Predicate<T> match) => 
            this.InnerList.FindAll(match);

        public U FindIndex(Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindIndex(match));

        public U FindIndex(U startIndex, Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindIndex(startIndex.ToInt(), match));

        public U FindIndex(U startIndex, int count, Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindIndex(startIndex.ToInt(), count, match));

        public T FindLast(Predicate<T> match) => 
            this.InnerList.FindLast(match);

        public U FindLastIndex(Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindLastIndex(match));

        public U FindLastIndex(U startIndex, Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindLastIndex(startIndex.ToInt(), match));

        public U FindLastIndex(U startIndex, int count, Predicate<T> match) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.FindLastIndex(startIndex.ToInt(), count, match));

        public void ForEach(Action<T> action)
        {
            this.InnerList.ForEach(action);
        }

        public List<T>.Enumerator GetEnumerator() => 
            this.InnerList.GetEnumerator();

        public List<T> GetRange(U index, int count) => 
            this.InnerList.GetRange(index.ToInt(), count);

        public U IndexOf(T item) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.IndexOf(item));

        public U IndexOf(T item, U index) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.IndexOf(item, index.ToInt()));

        public U IndexOf(T item, U index, int count) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.IndexOf(item, index.ToInt(), count));

        public void Insert(U index, T item)
        {
            this.InnerList.Insert(index.ToInt(), item);
        }

        public void InsertRange(U index, IEnumerable<T> collection)
        {
            this.InnerList.InsertRange(index.ToInt(), collection);
        }

        public U LastIndexOf(T item) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.LastIndexOf(item));

        public U LastIndexOf(T item, U index) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.LastIndexOf(item, index.ToInt()));

        public U LastIndexOf(T item, U index, int count) => 
            List<T, U>.indexConverter.FromInt(this.InnerList.LastIndexOf(item, index.ToInt(), count));

        public bool Remove(T item) => 
            this.InnerList.Remove(item);

        public int RemoveAll(Predicate<T> match) => 
            this.InnerList.RemoveAll(match);

        public void RemoveAt(U index)
        {
            this.InnerList.RemoveAt(index.ToInt());
        }

        public void RemoveRange(U index, int count)
        {
            this.InnerList.RemoveRange(index.ToInt(), count);
        }

        public void Reverse()
        {
            this.InnerList.Reverse();
        }

        public void Reverse(U index, int count)
        {
            this.InnerList.Reverse(index.ToInt(), count);
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }

        public void Sort(IComparer<T> comparer)
        {
            this.InnerList.Sort(comparer);
        }

        public void Sort(Comparison<T> comparison)
        {
            this.InnerList.Sort(comparison);
        }

        public void Sort(U index, int count, IComparer<T> comparer)
        {
            this.InnerList.Sort(index.ToInt(), count, comparer);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.InnerList.GetEnumerator();

        int IList<T>.IndexOf(T item) => 
            this.InnerList.IndexOf(item);

        void IList<T>.Insert(int index, T item)
        {
            this.InnerList.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            this.InnerList.RemoveAt(index);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.InnerList.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InnerList.GetEnumerator();

        int IList.Add(object item) => 
            this.InnerList.Add(item);

        bool IList.Contains(object item) => 
            this.InnerList.Contains(item);

        int IList.IndexOf(object item) => 
            this.InnerList.IndexOf(item);

        void IList.Insert(int index, object item)
        {
            this.InnerList.Insert(index, item);
        }

        void IList.Remove(object item)
        {
            this.InnerList.Remove(item);
        }

        void IList.RemoveAt(int index)
        {
            this.InnerList.RemoveAt(index);
        }

        public T[] ToArray() => 
            this.InnerList.ToArray();

        public void TrimExcess()
        {
            this.InnerList.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match) => 
            this.InnerList.TrueForAll(match);

        public List<T> InnerList =>
            this.innerList;

        public int Capacity
        {
            get => 
                this.InnerList.Capacity;
            set => 
                this.InnerList.Capacity = value;
        }

        public int Count =>
            this.InnerList.Count;

        public T this[U index]
        {
            get => 
                this.InnerList[index.ToInt()];
            set => 
                this.InnerList[index.ToInt()] = value;
        }

        bool ICollection<T>.IsReadOnly =>
            this.InnerList.IsReadOnly;

        bool ICollection.IsSynchronized =>
            ((ICollection) this.InnerList).IsSynchronized;

        object ICollection.SyncRoot =>
            ((ICollection) this.InnerList).SyncRoot;

        bool IList.IsFixedSize =>
            ((IList) this.InnerList).IsFixedSize;

        bool IList.IsReadOnly =>
            ((IList) this.InnerList).IsReadOnly;

        object IList.this[int index]
        {
            get => 
                this.InnerList[index];
            set => 
                this.InnerList[index] = (T) value;
        }

        T IList<T>.this[int index]
        {
            get => 
                this.InnerList[index];
            set => 
                this.InnerList[index] = value;
        }

        public T First
        {
            [DebuggerStepThrough]
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
            [DebuggerStepThrough]
            get
            {
                if (this.Count > 0)
                {
                    return this.InnerList[this.Count - 1];
                }
                return default(T);
            }
        }
    }
}

