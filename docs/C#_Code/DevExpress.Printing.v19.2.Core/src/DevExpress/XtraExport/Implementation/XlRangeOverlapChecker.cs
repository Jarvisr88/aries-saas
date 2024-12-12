namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public class XlRangeOverlapChecker
    {
        private const int rowDistance = 0x10;
        private const int columnDistance = 0x40;
        private const int lastClusterIndex = -1;
        private readonly Dictionary<int, RangeCluster> clusters = new Dictionary<int, RangeCluster>();

        public void Clear()
        {
            this.clusters.Clear();
        }

        private RangeCluster FindCluster(int index) => 
            !this.clusters.ContainsKey(index) ? null : this.clusters[index];

        private RangeCluster GetCluster(int index)
        {
            if (this.clusters.ContainsKey(index))
            {
                return this.clusters[index];
            }
            RangeCluster cluster = new RowsCluster();
            this.clusters.Add(index, cluster);
            return cluster;
        }

        private int GetIndexOfCluster(XlCellRange range)
        {
            int num = range.FirstRow / 0x10;
            return ((num == (range.LastRow / 0x10)) ? num : -1);
        }

        public bool IsNotOverlapped(XlCellRange range)
        {
            bool flag;
            int indexOfCluster = this.GetIndexOfCluster(range);
            RangeCluster cluster = this.GetCluster(indexOfCluster);
            if (indexOfCluster != -1)
            {
                flag = cluster.IsNotOverlapped(range);
                if (flag)
                {
                    if (this.GetCluster(-1).IsNotOverlapped(range))
                    {
                        cluster.Add(range);
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            else
            {
                flag = cluster.IsNotOverlapped(range);
                if (flag)
                {
                    int num3 = range.LastRow / 0x10;
                    int key = range.FirstRow / 0x10;
                    while (true)
                    {
                        if (key <= num3)
                        {
                            RangeCluster cluster2;
                            if (!this.clusters.TryGetValue(key, out cluster2) || cluster2.IsNotOverlapped(range))
                            {
                                key++;
                                continue;
                            }
                            flag = false;
                        }
                        if (flag)
                        {
                            cluster.Add(range);
                        }
                        break;
                    }
                }
            }
            return flag;
        }

        public void Remove(XlCellRange range)
        {
            int indexOfCluster = this.GetIndexOfCluster(range);
            RangeCluster cluster = this.FindCluster(indexOfCluster);
            if (cluster != null)
            {
                cluster.Remove(range);
            }
        }

        private class ColumnsCluster : XlRangeOverlapChecker.RangeCluster
        {
            private List<XlCellRange> innerList = new List<XlCellRange>();

            public override void Add(XlCellRange range)
            {
                this.innerList.Add(range);
            }

            public override bool IsNotOverlapped(XlCellRange range)
            {
                int count = this.innerList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (range.HasCommonCells(this.innerList[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            public override void Remove(XlCellRange range)
            {
                this.innerList.Remove(range);
            }
        }

        private abstract class RangeCluster
        {
            protected RangeCluster()
            {
            }

            public abstract void Add(XlCellRange range);
            public abstract bool IsNotOverlapped(XlCellRange range);
            public abstract void Remove(XlCellRange range);
        }

        private class RowsCluster : XlRangeOverlapChecker.RangeCluster
        {
            private Dictionary<int, XlRangeOverlapChecker.RangeCluster> clusters = new Dictionary<int, XlRangeOverlapChecker.RangeCluster>();

            public override void Add(XlCellRange range)
            {
                int indexOfCluster = this.GetIndexOfCluster(range);
                this.GetCluster(indexOfCluster).Add(range);
            }

            private XlRangeOverlapChecker.RangeCluster FindCluster(int index) => 
                !this.clusters.ContainsKey(index) ? null : this.clusters[index];

            private XlRangeOverlapChecker.RangeCluster GetCluster(int index)
            {
                if (this.clusters.ContainsKey(index))
                {
                    return this.clusters[index];
                }
                XlRangeOverlapChecker.RangeCluster cluster = new XlRangeOverlapChecker.ColumnsCluster();
                this.clusters.Add(index, cluster);
                return cluster;
            }

            private int GetIndexOfCluster(XlCellRange range)
            {
                int num = range.FirstColumn / 0x40;
                return ((num == (range.LastColumn / 0x40)) ? num : -1);
            }

            public override bool IsNotOverlapped(XlCellRange range)
            {
                bool flag;
                int indexOfCluster = this.GetIndexOfCluster(range);
                XlRangeOverlapChecker.RangeCluster cluster = this.GetCluster(indexOfCluster);
                if (indexOfCluster != -1)
                {
                    flag = cluster.IsNotOverlapped(range);
                    if (flag && !this.GetCluster(-1).IsNotOverlapped(range))
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = cluster.IsNotOverlapped(range);
                    if (flag)
                    {
                        int num3 = range.LastColumn / 0x40;
                        for (int i = range.FirstColumn / 0x40; i <= num3; i++)
                        {
                            XlRangeOverlapChecker.RangeCluster cluster2;
                            if (this.clusters.TryGetValue(i, out cluster2) && !cluster2.IsNotOverlapped(range))
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                return flag;
            }

            public override void Remove(XlCellRange range)
            {
                int indexOfCluster = this.GetIndexOfCluster(range);
                XlRangeOverlapChecker.RangeCluster cluster = this.FindCluster(indexOfCluster);
                if (cluster != null)
                {
                    cluster.Remove(range);
                }
            }
        }
    }
}

