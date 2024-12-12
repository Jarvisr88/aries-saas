namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class BrickPagePairCollection : CollectionBase, IEnumerable
    {
        private List<BPPIdx> indices;

        public int Add(BrickPagePair pair)
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                int index = this.IndexOf(pair);
                if (index < 0)
                {
                    index = base.InnerList.Add(pair);
                    this.IndicesAdd(pair, index);
                }
                return index;
            }
        }

        public bool Contains(BrickPagePair pair)
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                return (this.IndexOf(pair) >= 0);
            }
        }

        public Brick[] GetBricks(PageList pages, int pageIndex)
        {
            Brick[] brickArray;
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                List<Brick> list2 = new List<Brick>();
                int num = 0;
                while (true)
                {
                    if (num >= base.Count)
                    {
                        brickArray = list2.ToArray();
                        break;
                    }
                    if (this[num].PageIndex == pageIndex)
                    {
                        list2.Add(this[num].GetBrick(pages));
                    }
                    num++;
                }
            }
            return brickArray;
        }

        public int IndexOf(BrickPagePair pair)
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                int num = this.Indices.BinarySearch(new BPPIdx(pair, -1));
                return ((num >= 0) ? this.indices[num].Idx : -1);
            }
        }

        private void IndicesAdd(BrickPagePair bpp, int idx)
        {
            BPPIdx item = new BPPIdx(bpp, idx);
            int num = this.Indices.BinarySearch(item);
            if (num < 0)
            {
                this.indices.Insert(~num, item);
            }
        }

        protected override void OnClear()
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                base.OnClear();
                this.indices = null;
            }
        }

        public void Remove(BrickPagePair pair)
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                if (this.Contains(pair))
                {
                    base.InnerList.Remove(pair);
                    this.indices = null;
                }
            }
        }

        public void Sort(IComparer sortComparer)
        {
            ArrayList innerList = base.InnerList;
            lock (innerList)
            {
                if (sortComparer != null)
                {
                    base.InnerList.Sort(sortComparer);
                    this.indices = null;
                }
            }
        }

        private List<BPPIdx> Indices
        {
            get
            {
                if (this.indices == null)
                {
                    this.indices = new List<BPPIdx>();
                    int idx = 0;
                    while (true)
                    {
                        if (idx >= base.Count)
                        {
                            this.indices.Sort();
                            break;
                        }
                        this.indices.Add(new BPPIdx(this[idx], idx));
                        idx++;
                    }
                }
                return this.indices;
            }
        }

        public BrickPagePair this[int index]
        {
            get
            {
                ArrayList innerList = base.InnerList;
                lock (innerList)
                {
                    return (BrickPagePair) base.InnerList[index];
                }
            }
        }

        private class BPPIdx : IComparable<BrickPagePairCollection.BPPIdx>
        {
            private readonly BrickPagePair bpp;
            private readonly int idx;

            public BPPIdx(BrickPagePair bpp, int idx)
            {
                this.bpp = bpp;
                this.idx = idx;
            }

            public int CompareTo(BrickPagePairCollection.BPPIdx other)
            {
                if (this.bpp.PageIndex != other.bpp.PageIndex)
                {
                    return (this.bpp.PageIndex - other.bpp.PageIndex);
                }
                if (this.bpp.BrickIndices.Length != other.bpp.BrickIndices.Length)
                {
                    return (this.bpp.BrickIndices.Length - other.bpp.BrickIndices.Length);
                }
                for (int i = 0; i < this.bpp.BrickIndices.Length; i++)
                {
                    int num2 = this.bpp.BrickIndices[i] - other.bpp.BrickIndices[i];
                    if (num2 != 0)
                    {
                        return num2;
                    }
                }
                return 0;
            }

            public int Idx =>
                this.idx;
        }
    }
}

