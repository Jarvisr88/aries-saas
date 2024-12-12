namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MegaTable
    {
        private int tableRight;
        private int tableBottom;
        private List<int> colWidths = new List<int>();
        private List<int> rowHeights = new List<int>();
        private ObjectInfo[] fObjects = new ObjectInfo[0];
        private LayoutControlCollection layoutControls;
        private bool isEmpty;
        private bool highGridDensity;

        public unsafe MegaTable(LayoutControlCollection layoutControls, bool highGridDensity, bool correctControlBounds, int maxRowHeight = -1)
        {
            if ((layoutControls == null) || (layoutControls.Count == 0))
            {
                this.isEmpty = true;
            }
            else
            {
                this.highGridDensity = highGridDensity;
                this.layoutControls = layoutControls;
                DescLayoutControlComparer comparer = highGridDensity ? new DescLayoutControlComparer() : new DescLayoutControlComparerWithMinTopLeftCalculator(layoutControls[0]);
                layoutControls.Sort(comparer);
                RectangleF[] rects = new RectangleF[layoutControls.Count];
                double num = Math.Ceiling((double) GraphicsUnitConverter.Convert((float) 1f, (float) 25.4f, (float) 96f));
                for (int i = 0; i < layoutControls.Count; i++)
                {
                    rects[i] = layoutControls[i].BoundsF;
                }
                if (correctControlBounds)
                {
                    CorrectControlBounds(rects);
                }
                if (!highGridDensity)
                {
                    DescLayoutControlComparerWithMinTopLeftCalculator calculator = (DescLayoutControlComparerWithMinTopLeftCalculator) comparer;
                    for (int j = 0; j < layoutControls.Count; j++)
                    {
                        RectangleF ef = RectHelper.OffsetRectF(rects[j], (float) -calculator.MinLeft, (float) -calculator.MinTop);
                        if (j > 0)
                        {
                            float num4 = ef.X - rects[j - 1].X;
                            if ((num4 < 0f) && (Math.Ceiling((double) Math.Abs(num4)) < num))
                            {
                                RectangleF* efPtr1 = &ef;
                                efPtr1.X -= num4;
                                RectangleF* efPtr2 = &ef;
                                efPtr2.Width += num4;
                            }
                        }
                        rects[j] = ef;
                    }
                }
                this.DivideAreaByRectanglesAndCalculateSpans(layoutControls, rects, maxRowHeight);
            }
        }

        private static bool Contains(RectangleF rectangle, PointF point) => 
            ((rectangle.Left >= point.X) || ((point.X >= rectangle.Right) || ((rectangle.Top > point.Y) || (point.Y >= rectangle.Bottom)))) ? ((rectangle.Top < point.Y) && ((point.Y < rectangle.Bottom) && ((rectangle.Left <= point.X) && (point.X < rectangle.Right)))) : true;

        private static void CorrectControlBounds(RectangleF[] rects)
        {
            int index = 0;
            while (index < rects.Length)
            {
                RectangleF rectangle = rects[index];
                int num4 = index + 1;
                while (true)
                {
                    if (num4 < rects.Length)
                    {
                        RectangleF ef2 = rects[num4];
                        if (rectangle.Right > ef2.X)
                        {
                            if (Contains(rectangle, ef2.Location))
                            {
                                MoveControlBounds(rects, num4, rectangle.Right - ef2.Left, rectangle.Bottom - ef2.Top);
                            }
                            num4++;
                            continue;
                        }
                    }
                    index++;
                    break;
                }
            }
        }

        private void DivideAreaByRectanglesAndCalculateSpans(LayoutControlCollection layoutControls, RectangleF[] rects, int maxRowHeight)
        {
            RectangleDividerF rf = new RectangleDividerF(rects, maxRowHeight, !this.highGridDensity);
            this.tableRight = rf.Right;
            this.tableBottom = rf.Bottom;
            this.colWidths = rf.ColWidths;
            this.rowHeights = rf.RowHeights;
            this.ZeroColumnNeeded = rf.ZeroColumnNeeded;
            this.ZeroRowNeeded = rf.ZeroRowNeeded;
            this.fObjects = new ObjectInfo[rects.Length];
            for (int i = 0; i < rects.Length; i++)
            {
                RectangleDividerF.Range range = rf.XRanges[i];
                RectangleDividerF.Range range2 = rf.YRanges[i];
                int colSpan = range.end - range.start;
                int rowSpan = range2.end - range2.start;
                this.fObjects[i] = new ObjectInfo(range.start, range2.start, colSpan, rowSpan, layoutControls[i]);
            }
        }

        public static void Dump(RectangleF[] rects)
        {
            SafeBinaryFormatter.Serialize(rects);
        }

        private static MoveAction GetMoveAction(float xDelta, float yDelta) => 
            ((xDelta <= 0f) || ((yDelta != 0f) && (xDelta > yDelta))) ? MoveAction.MoveRows : MoveAction.MoveColumns;

        private static void MoveControlBounds(RectangleF[] rects, int index, float xDelta, float yDelta)
        {
            if (GetMoveAction(xDelta, yDelta) == MoveAction.MoveColumns)
            {
                MoveControlColumnBounds(rects, index, xDelta);
            }
            else
            {
                MoveControlRowBounds(rects, index, yDelta);
            }
        }

        private static void MoveControlColumnBounds(RectangleF[] rects, int index, float xDelta)
        {
            for (int i = index; i < rects.Length; i++)
            {
                RectangleF ef = rects[i];
                rects[i] = new RectangleF(ef.Left + xDelta, ef.Top, ef.Width, ef.Height);
            }
        }

        private static void MoveControlRowBounds(RectangleF[] rects, int index, float yDelta)
        {
            float top = rects[index].Top;
            for (int i = 0; i < rects.Length; i++)
            {
                RectangleF ef = rects[i];
                if (ef.Top >= top)
                {
                    rects[i] = new RectangleF(ef.Left, ef.Top + yDelta, ef.Width, ef.Height);
                }
            }
        }

        public ObjectInfo[] Objects =>
            this.fObjects;

        public int Width =>
            this.tableRight;

        public int Height =>
            this.tableBottom;

        public List<int> ColWidths =>
            this.colWidths;

        public List<int> RowHeights =>
            this.rowHeights;

        public bool ZeroColumnNeeded { get; private set; }

        public bool ZeroRowNeeded { get; private set; }

        public virtual int RowCount =>
            this.rowHeights.Count;

        public virtual int ColumnCount =>
            this.colWidths.Count;

        public bool IsEmpty =>
            this.isEmpty;

        private class DescLayoutControlComparer : IComparer<ILayoutControl>
        {
            int IComparer<ILayoutControl>.Compare(ILayoutControl xControl, ILayoutControl yControl)
            {
                int left = xControl.Left;
                int top = xControl.Top;
                int num3 = yControl.Left;
                int num4 = yControl.Top;
                return ((left > num3) ? 1 : ((left < num3) ? -1 : ((top > num4) ? 1 : ((top < num4) ? -1 : 0))));
            }
        }

        private class DescLayoutControlComparerWithMinTopLeftCalculator : MegaTable.DescLayoutControlComparer, IComparer<ILayoutControl>
        {
            private int minLeft;
            private int minTop;

            public DescLayoutControlComparerWithMinTopLeftCalculator()
            {
                this.minLeft = 0x7fffffff;
                this.minTop = 0x7fffffff;
            }

            public DescLayoutControlComparerWithMinTopLeftCalculator(ILayoutControl startControl)
            {
                this.minLeft = 0x7fffffff;
                this.minTop = 0x7fffffff;
                this.minLeft = startControl.Left;
                this.minTop = startControl.Top;
            }

            int IComparer<ILayoutControl>.Compare(ILayoutControl xControl, ILayoutControl yControl)
            {
                int left = xControl.Left;
                int top = xControl.Top;
                int num3 = yControl.Left;
                int num4 = yControl.Top;
                if (left > num3)
                {
                    this.minLeft = Math.Min(this.minLeft, num3);
                    this.minTop = Math.Min(this.minTop, Math.Min(top, num4));
                    return 1;
                }
                if (left < num3)
                {
                    this.minLeft = Math.Min(this.minLeft, left);
                    this.minTop = Math.Min(this.minTop, Math.Min(top, num4));
                    return -1;
                }
                if (top > num4)
                {
                    this.minTop = Math.Min(this.minTop, num4);
                    return 1;
                }
                if (top >= num4)
                {
                    return 0;
                }
                this.minTop = Math.Min(this.minTop, top);
                return -1;
            }

            public int MinLeft =>
                this.minLeft;

            public int MinTop =>
                this.minTop;
        }

        private enum MoveAction
        {
            MoveColumns,
            MoveRows
        }
    }
}

