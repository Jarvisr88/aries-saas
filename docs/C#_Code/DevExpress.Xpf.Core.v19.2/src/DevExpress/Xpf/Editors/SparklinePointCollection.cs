namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class SparklinePointCollection : IList<SparklinePoint>, ICollection<SparklinePoint>, IEnumerable<SparklinePoint>, IEnumerable, INotifyCollectionChanged
    {
        protected readonly List<SparklinePoint> innerList = new List<SparklinePoint>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public virtual void Add(SparklinePoint item)
        {
            this.innerList.Add(item);
            this.RaiseCollectionChanged();
        }

        public virtual void AddRange(IEnumerable<SparklinePoint> items)
        {
            this.innerList.AddRange(items);
            this.RaiseCollectionChanged();
        }

        public void Clear()
        {
            this.innerList.Clear();
            this.RaiseCollectionChanged();
        }

        public bool Contains(SparklinePoint item) => 
            this.innerList.Contains(item);

        public void CopyTo(SparklinePoint[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SparklinePoint> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(SparklinePoint item) => 
            this.innerList.IndexOf(item);

        public void Insert(int index, SparklinePoint item)
        {
            this.innerList.Insert(index, item);
            this.RaiseCollectionChanged();
        }

        protected void RaiseCollectionChanged()
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, null);
            }
        }

        public virtual bool Remove(SparklinePoint item)
        {
            bool flag = this.innerList.Remove(item);
            this.RaiseCollectionChanged();
            return flag;
        }

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
            this.RaiseCollectionChanged();
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public SparklineScaleType ArgumentScaleType =>
            (this.Count > 0) ? this[0].ArgumentScaleType : SparklineScaleType.Unknown;

        public SparklineScaleType ValueScaleType =>
            (this.Count > 0) ? this[0].ValueScaleType : SparklineScaleType.Unknown;

        public int Count =>
            this.innerList.Count;

        public bool IsReadOnly =>
            false;

        public SparklinePoint this[int index]
        {
            get => 
                this.innerList[index];
            set
            {
                this.innerList[index] = value;
                this.RaiseCollectionChanged();
            }
        }
    }
}

