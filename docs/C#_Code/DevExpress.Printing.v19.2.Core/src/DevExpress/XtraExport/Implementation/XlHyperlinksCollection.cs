namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal class XlHyperlinksCollection : IList<XlHyperlink>, ICollection<XlHyperlink>, IEnumerable<XlHyperlink>, IEnumerable
    {
        private readonly int maxCount;
        private readonly List<XlHyperlink> innerList = new List<XlHyperlink>();
        private bool maxCountAchieved;

        public XlHyperlinksCollection(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public void Add(XlHyperlink item)
        {
            if (!this.maxCountAchieved)
            {
                this.CheckNumberOfHyperlinks();
                this.innerList.Add(item);
            }
        }

        private void CheckNumberOfHyperlinks()
        {
            if (this.innerList.Count >= this.maxCount)
            {
                this.maxCountAchieved = true;
                throw new InvalidOperationException($"Maximum number of hyperlinks per worksheet is {this.maxCount}");
            }
        }

        public void Clear()
        {
            this.innerList.Clear();
            this.maxCountAchieved = false;
        }

        public bool Contains(XlHyperlink item) => 
            this.innerList.Contains(item);

        public void CopyTo(XlHyperlink[] array, int arrayIndex)
        {
            this.innerList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<XlHyperlink> GetEnumerator() => 
            this.innerList.GetEnumerator();

        public int IndexOf(XlHyperlink item) => 
            this.innerList.IndexOf(item);

        public void Insert(int index, XlHyperlink item)
        {
            if (!this.maxCountAchieved)
            {
                this.CheckNumberOfHyperlinks();
                this.innerList.Insert(index, item);
            }
        }

        public bool Remove(XlHyperlink item)
        {
            bool flag = this.innerList.Remove(item);
            if (flag)
            {
                this.maxCountAchieved = false;
            }
            return flag;
        }

        public void RemoveAt(int index)
        {
            this.innerList.RemoveAt(index);
            this.maxCountAchieved = false;
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.innerList.GetEnumerator();

        public XlHyperlink this[int index]
        {
            get => 
                this.innerList[index];
            set => 
                this.innerList[index] = value;
        }

        public int Count =>
            this.innerList.Count;

        public bool IsReadOnly =>
            this.innerList.IsReadOnly;
    }
}

