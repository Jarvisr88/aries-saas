namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ExtremePointIndexes
    {
        private readonly List<int> minIndexes = new List<int>();
        private readonly List<int> maxIndexes = new List<int>();

        public ExtremePointIndexes(IList<SparklinePoint> sortedItems)
        {
            this.Start = this.End = -1;
            if (sortedItems != null)
            {
                this.Fill(sortedItems);
            }
        }

        private void Fill(IList<SparklinePoint> sortedItems)
        {
            for (int i = 0; i < sortedItems.Count; i++)
            {
                if (SparklineMathUtils.IsValidDouble(sortedItems[i].Value))
                {
                    if (this.Start < 0)
                    {
                        this.Start = i;
                    }
                    if (this.Start >= 0)
                    {
                        this.End = i;
                    }
                    if (this.minIndexes.Count == 0)
                    {
                        this.minIndexes.Add(i);
                    }
                    else if (sortedItems[i].Value < sortedItems[this.minIndexes[0]].Value)
                    {
                        this.minIndexes.Clear();
                        this.minIndexes.Add(i);
                    }
                    else if (sortedItems[i].Value == sortedItems[this.minIndexes[0]].Value)
                    {
                        this.minIndexes.Add(i);
                    }
                    if (this.maxIndexes.Count == 0)
                    {
                        this.maxIndexes.Add(i);
                    }
                    else if (sortedItems[i].Value > sortedItems[this.maxIndexes[0]].Value)
                    {
                        this.maxIndexes.Clear();
                        this.maxIndexes.Add(i);
                    }
                    else if (sortedItems[i].Value == sortedItems[this.maxIndexes[0]].Value)
                    {
                        this.maxIndexes.Add(i);
                    }
                }
            }
        }

        public bool IsEndPoint(int pointIndex) => 
            pointIndex == this.End;

        public bool IsMaxPoint(int pointIndex) => 
            this.maxIndexes.BinarySearch(pointIndex) >= 0;

        public bool IsMinPoint(int pointIndex) => 
            this.minIndexes.BinarySearch(pointIndex) >= 0;

        public bool IsStartPoint(int pointIndex) => 
            pointIndex == this.Start;

        public int Min =>
            (this.minIndexes.Count > 0) ? this.minIndexes[0] : -1;

        public int Max =>
            (this.maxIndexes.Count > 0) ? this.maxIndexes[0] : -1;

        public int Start { get; set; }

        public int End { get; set; }

        public bool IsEmpty =>
            (this.Start == -1) && (this.End == -1);
    }
}

