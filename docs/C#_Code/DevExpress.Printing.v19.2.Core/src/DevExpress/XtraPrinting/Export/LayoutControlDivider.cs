namespace DevExpress.XtraPrinting.Export
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class LayoutControlDivider
    {
        private List<LayoutControlCollection> parts;

        public LayoutControlDivider(LayoutControlCollection layoutControls) : this(layoutControls, 0f)
        {
        }

        public LayoutControlDivider(LayoutControlCollection layoutControls, float minDelta)
        {
            if ((layoutControls != null) && (layoutControls.Count != 0))
            {
                CoordCalculator yCalc = new CoordCalculator(layoutControls.Count);
                for (int i = 0; i < layoutControls.Count; i++)
                {
                    RectangleF boundsF = layoutControls[i].BoundsF;
                    yCalc.AddArea(boundsF.Top, boundsF.Bottom, i);
                }
                yCalc.SortMergeCoords();
                this.parts = FillParts(layoutControls, yCalc, minDelta);
            }
        }

        private static bool CanSeparate(float minDelta, CoordInfo currentCoord, CoordInfo nextCoord) => 
            (minDelta == 0f) || ((nextCoord == null) || ((nextCoord.coord - currentCoord.coord) >= minDelta));

        private static List<LayoutControlCollection> FillParts(LayoutControlCollection layoutControls, CoordCalculator yCalc, float minDelta)
        {
            List<LayoutControlCollection> list = new List<LayoutControlCollection>();
            LayoutControlCollection item = new LayoutControlCollection();
            List<int> list2 = new List<int>();
            List<CoordInfo> coords = yCalc.Coords;
            for (int i = 0; i < coords.Count; i++)
            {
                CoordInfo currentCoord = coords[i];
                if (currentCoord.isStart)
                {
                    item.Add(layoutControls[currentCoord.index]);
                    list2.Add(currentCoord.index);
                }
                else
                {
                    list2.Remove(currentCoord.index);
                    if ((list2.Count == 0) && CanSeparate(minDelta, currentCoord, (i == (coords.Count - 1)) ? null : coords[i + 1]))
                    {
                        list.Add(item);
                        item = new LayoutControlCollection();
                    }
                }
            }
            return list;
        }

        private static int Round(float value) => 
            (int) Math.Round((double) value, MidpointRounding.AwayFromZero);

        public List<LayoutControlCollection> Parts =>
            this.parts;

        private class CoordCalculator
        {
            private List<CoordInfo> coords;

            public CoordCalculator(int size)
            {
                this.coords = new List<CoordInfo>(size);
            }

            public virtual void AddArea(float start, float end, int i)
            {
                this.AddValue(start, i, true);
                this.AddValue(end, i, false);
            }

            private void AddValue(float value, int index, bool isStart)
            {
                CoordInfo item = new CoordInfo();
                item.coord = value;
                item.index = index;
                item.isStart = isStart;
                this.coords.Add(item);
            }

            private static bool ShouldAdvancePosition(CoordInfo coordInfo, float prevCoord, int prevPosition) => 
                ((coordInfo.coord - prevCoord) > 0.5) && (LayoutControlDivider.Round(coordInfo.coord) != prevPosition);

            public void SortMergeCoords()
            {
                this.coords.Sort();
                int num = 0;
                int num2 = 0;
                float prevCoord = 0f;
                int prevPosition = 0;
                if (!ShouldAdvancePosition(this.coords[0], prevCoord, prevPosition))
                {
                    prevCoord = this.coords[0].coord;
                    prevPosition = LayoutControlDivider.Round(this.coords[0].coord);
                    num2++;
                }
                while (num2 < this.coords.Count)
                {
                    CoordInfo coordInfo = this.coords[num2];
                    if (!ShouldAdvancePosition(coordInfo, prevCoord, prevPosition))
                    {
                        coordInfo.coord = prevCoord;
                    }
                    else
                    {
                        prevCoord = coordInfo.coord;
                        prevPosition = LayoutControlDivider.Round(coordInfo.coord);
                        num++;
                    }
                    num2++;
                }
                this.coords.Sort(new LayoutControlDivider.CoordComparer());
            }

            public List<CoordInfo> Coords =>
                this.coords;
        }

        private class CoordComparer : IComparer<CoordInfo>
        {
            int IComparer<CoordInfo>.Compare(CoordInfo first, CoordInfo second)
            {
                int num = first.CompareTo(second);
                return ((num == 0) ? first.isStart.CompareTo(second.isStart) : num);
            }
        }
    }
}

