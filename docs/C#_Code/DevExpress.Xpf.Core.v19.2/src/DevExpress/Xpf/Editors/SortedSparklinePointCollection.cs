namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;

    public class SortedSparklinePointCollection : SparklinePointCollection
    {
        private IComparer<SparklinePoint> comparer;

        public SortedSparklinePointCollection(IComparer<SparklinePoint> comparer)
        {
            this.comparer = comparer;
        }

        public override void Add(SparklinePoint item)
        {
            int index = base.innerList.BinarySearch(item, this.comparer);
            if (index < 0)
            {
                index = ~index;
            }
            base.innerList.Insert(index, item);
            base.RaiseCollectionChanged();
        }

        public override void AddRange(IEnumerable<SparklinePoint> collection)
        {
            base.innerList.AddRange(collection);
            base.innerList.Sort(this.comparer);
            base.RaiseCollectionChanged();
        }

        public int BinarySearch(SparklinePoint point) => 
            base.innerList.BinarySearch(point, this.comparer);

        public override bool Remove(SparklinePoint item)
        {
            bool flag = base.innerList.Remove(item);
            base.RaiseCollectionChanged();
            return flag;
        }

        public void Sort(IComparer<SparklinePoint> comparer)
        {
            this.comparer = comparer;
            base.innerList.Sort(comparer);
            base.RaiseCollectionChanged();
        }

        public void Update(SparklinePoint oldItem, SparklinePoint newItem)
        {
            base.innerList.Remove(oldItem);
            this.Add(newItem);
        }
    }
}

