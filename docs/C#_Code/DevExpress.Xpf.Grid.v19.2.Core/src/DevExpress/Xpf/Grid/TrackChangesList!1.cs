namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class TrackChangesList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly IList<T> innerList;

        public TrackChangesList(IList<T> innerList)
        {
            Guard.ArgumentNotNull(innerList, "innerList");
            if (innerList.IsReadOnly)
            {
                throw new ArgumentException();
            }
            this.innerList = innerList;
        }

        public void Add(T item)
        {
            this.SetChanged(() => ((TrackChangesList<T>) this).innerList.Add(item));
        }

        public void Clear()
        {
            this.SetChanged(new Action(this.innerList.Clear));
        }

        public bool Contains(T item) => 
            this.innerList.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(T item) => 
            this.innerList.IndexOf(item);

        public void Insert(int index, T item)
        {
            this.SetChanged(() => ((TrackChangesList<T>) this).innerList.Insert(index, item));
        }

        public bool Remove(T item)
        {
            bool result = false;
            this.SetChanged(delegate {
                result = ((TrackChangesList<T>) this).innerList.Remove(item);
            });
            return result;
        }

        public void RemoveAt(int index)
        {
            this.SetChanged(() => ((TrackChangesList<T>) this).innerList.RemoveAt(index));
        }

        private void SetChanged(Action action)
        {
            action();
            this.IsChanged = true;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public bool IsChanged { get; private set; }

        public T this[int index]
        {
            get => 
                this.innerList[index];
            set => 
                this.SetChanged(() => ((TrackChangesList<T>) this).innerList[index] = value);
        }

        public int Count =>
            this.innerList.Count;

        public bool IsReadOnly =>
            this.innerList.IsReadOnly;
    }
}

