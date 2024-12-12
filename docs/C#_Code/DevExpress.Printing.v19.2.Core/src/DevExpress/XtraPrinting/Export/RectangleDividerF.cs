namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class RectangleDividerF
    {
        private List<int> columnWidths;
        private List<int> rowHeights;
        private bool zeroColumnNeeded;
        private bool zeroRowNeeded;
        private Range[] xRanges;
        private Range[] yRanges;
        private int right;
        private int bottom;

        public RectangleDividerF(RectangleF[] areas, int maxRowHeight, bool onlyTopLeft)
        {
            int length = areas.Length;
            this.xRanges = new Range[length];
            this.yRanges = new Range[length];
            RangeCalculator calculator = CreateRangeCalculator(onlyTopLeft, length, this.xRanges, -1);
            RangeCalculator calculator2 = CreateRangeCalculator(onlyTopLeft, length, this.yRanges, maxRowHeight);
            for (int i = 0; i < areas.Length; i++)
            {
                RectangleF ef = areas[i];
                calculator.AddArea(ef.Left, ef.Right, i);
                calculator2.AddArea(ef.Top, ef.Bottom, i);
            }
            this.zeroColumnNeeded = calculator.CalcRanges();
            this.columnWidths = calculator.Lengths;
            this.right = calculator.FarBound;
            this.zeroRowNeeded = calculator2.CalcRanges();
            this.rowHeights = calculator2.Lengths;
            this.bottom = calculator2.FarBound;
            if (onlyTopLeft)
            {
                List<float> bounds = ((RangeCalculatorTopLeftOnly) calculator).Bounds;
                List<float> list2 = ((RangeCalculatorTopLeftOnly) calculator2).Bounds;
                for (int j = 0; j < areas.Length; j++)
                {
                    this.xRanges[j].end = FindBound(bounds, areas[j].Right, this.xRanges[j].start) + 1;
                    this.yRanges[j].end = FindBound(list2, areas[j].Bottom, this.yRanges[j].start) + 1;
                }
            }
        }

        private static RangeCalculator CreateRangeCalculator(bool onlyTopLeft, int size, Range[] ranges, int maxLength) => 
            onlyTopLeft ? new RangeCalculatorTopLeftOnly(size, ranges, maxLength) : new RangeCalculator(size, ranges, maxLength);

        private static int FindBound(List<float> bounds, float areaBound, int start)
        {
            int num = bounds.BinarySearch(start, bounds.Count - start, (float) Round(areaBound), null);
            num = (num < 0) ? ~num : num;
            return (((num <= 0) || ((areaBound - bounds[num - 1]) >= 1f)) ? num : (num - 1));
        }

        private static int Round(float value) => 
            (int) Math.Round((double) value, MidpointRounding.AwayFromZero);

        public int Right =>
            this.right;

        public int Bottom =>
            this.bottom;

        public List<int> ColWidths =>
            this.columnWidths;

        public List<int> RowHeights =>
            this.rowHeights;

        public Range[] XRanges =>
            this.xRanges;

        public Range[] YRanges =>
            this.yRanges;

        public bool ZeroColumnNeeded =>
            this.zeroColumnNeeded;

        public bool ZeroRowNeeded =>
            this.zeroRowNeeded;

        [StructLayout(LayoutKind.Sequential)]
        public struct Range
        {
            public int start;
            public int end;
        }

        private class RangeCalculator
        {
            protected RectangleDividerF.Range[] ranges;
            private List<CoordInfo> coords;
            private int maxLength;
            private List<int> lengths;

            public RangeCalculator(int size, RectangleDividerF.Range[] ranges, int maxLength)
            {
                this.ranges = ranges;
                this.maxLength = maxLength;
                this.coords = new List<CoordInfo>(size);
                this.lengths = new List<int>();
            }

            public virtual void AddArea(float start, float end, int i)
            {
                this.AddValue(start, i, true);
                this.AddValue(end, i, false);
            }

            protected virtual void AddPosition(float position, int prevPosition)
            {
                int num = RectangleDividerF.Round(position);
                this.lengths.Add(num - prevPosition);
            }

            protected void AddValue(float value, int index, bool isStart)
            {
                CoordInfo item = new CoordInfo();
                item.coord = value;
                item.index = index;
                item.isStart = isStart;
                this.coords.Add(item);
            }

            protected virtual void AssignRange(CoordInfo coordInfo, int current)
            {
                if (coordInfo.isStart)
                {
                    this.ranges[coordInfo.index].start = current;
                }
                else
                {
                    this.ranges[coordInfo.index].end = current;
                }
            }

            public virtual bool CalcRanges()
            {
                this.lengths.Clear();
                this.coords.Sort();
                int num = 0;
                float prevCoord = 0f;
                int prevPosition = 0;
                bool flag = false;
                if (!ShouldAddPosition(this.coords[0].coord, prevCoord, prevPosition))
                {
                    this.ranges[this.coords[0].index].start = 0;
                    prevCoord = this.coords[0].coord;
                    prevPosition = RectangleDividerF.Round(this.coords[0].coord);
                    flag = true;
                    num++;
                }
                while (num < this.coords.Count)
                {
                    CoordInfo coordInfo = this.coords[num];
                    if (ShouldAddPosition(coordInfo.coord, prevCoord, prevPosition))
                    {
                        this.ProcessCoord(coordInfo.coord, prevCoord, prevPosition);
                        prevCoord = coordInfo.coord;
                        prevPosition = RectangleDividerF.Round(prevCoord);
                    }
                    this.AssignRange(coordInfo, this.lengths.Count);
                    num++;
                }
                return flag;
            }

            private void ProcessCoord(float currentCoord, float prevCoord, int prevPosition)
            {
                int num = RectangleDividerF.Round(currentCoord);
                if ((this.maxLength <= 0) || ((num - prevPosition) <= this.maxLength))
                {
                    this.AddPosition(currentCoord, prevPosition);
                }
                else
                {
                    float position = prevCoord + this.maxLength;
                    this.AddPosition(position, prevPosition);
                    this.ProcessCoord(currentCoord, position, RectangleDividerF.Round(position));
                }
            }

            private static bool ShouldAddPosition(float coord, float prevCoord, int prevPosition) => 
                ((coord - prevCoord) > 0.5) && (RectangleDividerF.Round(coord) != prevPosition);

            public List<int> Lengths =>
                this.lengths;

            public int FarBound =>
                ((IEnumerable<int>) this.lengths).Sum();
        }

        private class RangeCalculatorTopLeftOnly : RectangleDividerF.RangeCalculator
        {
            private float maxBound;
            private List<float> bounds;

            public RangeCalculatorTopLeftOnly(int size, RectangleDividerF.Range[] ranges, int maxLength) : base(size, ranges, maxLength)
            {
                this.maxBound = float.MinValue;
                this.bounds = new List<float>();
            }

            public override void AddArea(float start, float end, int i)
            {
                base.AddValue(start, i, true);
                this.maxBound = Math.Max(this.maxBound, end);
            }

            protected override void AddPosition(float position, int prevPosition)
            {
                base.AddPosition(position, prevPosition);
                this.bounds.Add(position);
            }

            protected override void AssignRange(CoordInfo coordInfo, int current)
            {
                if (coordInfo.isStart)
                {
                    base.ranges[coordInfo.index].start = current;
                }
            }

            public override bool CalcRanges()
            {
                base.AddValue(this.maxBound, -1, false);
                return base.CalcRanges();
            }

            public List<float> Bounds =>
                this.bounds;
        }
    }
}

