namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BarLayoutTableRow
    {
        private readonly BarLayoutTable table;
        private List<BarLayoutTableCell> cells;

        public BarLayoutTableRow(BarLayoutTable table);
        public virtual BarLayoutTableCell Add(IBarLayoutTableInfo element);
        protected virtual void AppendSize(ref Size target, Size source);
        public virtual void Arrange(Rect finalRect);
        public virtual bool Contains(IBarLayoutTableInfo element);
        protected virtual BarLayoutTableCell CreateTableCell(IBarLayoutTableInfo element);
        public virtual void DragCell(BarLayoutTableCell currentCell, Point relativeToTablePoint, Point delta, bool skipHeightCheck = false);
        private void DragCellFromAnotherRow(BarLayoutTableCell currentCell, Point relativeToSelfPoint, Point delta);
        protected virtual void DragCellInsideRow(BarLayoutTableCell currentCell, Point relativeToSelfPoint, Point delta);
        public virtual BarLayoutTableCell FindCell(Bar bar);
        public virtual BarLayoutTableCell FindCell(IBarLayoutTableInfo layoutInfo);
        public virtual BarLayoutTableCell FindCell(Point mousePosition, bool positionInTableCoords);
        public virtual BarLayoutTableCell FindClosestCell(Point mousePosition, Func<BarLayoutTableCell, bool> predicate);
        public double GetLeftAvailableSize(BarLayoutTableCell currentCell);
        private IEnumerable<BarLayoutTableCell> GetLeftCells(BarLayoutTableCell currentCell, bool includeSelf = false);
        public BarLayoutTableCell GetNextSibling(BarLayoutTableCell cell);
        public BarLayoutTableCell GetPreviousSibling(BarLayoutTableCell cell);
        protected Point GetRelativePosition(Point mousePosition);
        public double GetRightAvailableSize(BarLayoutTableCell currentCell);
        private IEnumerable<BarLayoutTableCell> GetRightCells(BarLayoutTableCell currentCell, bool includeSelf = false);
        protected Point GetTablePosition(Point relativePosition);
        public virtual void IncrementIndexes();
        public bool IsFirstColumn(BarLayoutTableCell cell);
        public bool IsLastColumn(BarLayoutTableCell cell);
        public virtual void Measure(Size constraint);
        protected virtual bool MoveCellsInsideRow(BarLayoutTableCell currentCell, Point relativeToSelfPoint, Point delta);
        public virtual void Remove(IBarLayoutTableInfo element);
        protected virtual void Reorder();
        public virtual void UpdateIndexes();

        protected List<BarLayoutTableCell> Cells { get; }

        protected bool IsFirst { get; }

        protected bool IsLast { get; }

        protected double RowIndent { get; }

        public BarLayoutTable Table { get; }

        public LayoutTableSizeHelper Helper { get; }

        public Size DesiredSize { get; private set; }

        public Size RenderSize { get; private set; }

        public Rect RenderRect { get; private set; }

        public int Index { get; set; }

        public bool CanAddItems { get; }

        public object WholeRowItemIndex { get; }

        public bool IsEmpty { get; }

        public bool HasReducedCells { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarLayoutTableRow.<>c <>9;
            public static Func<BarLayoutTableCell, bool> <>9__31_0;
            public static Func<BarLayoutTableCell, int> <>9__33_0;
            public static Func<int> <>9__33_1;
            public static Func<BarLayoutTableCell, bool> <>9__37_0;
            public static Func<BarLayoutTableCell, bool> <>9__41_0;
            public static Func<BarLayoutTableCell, int> <>9__41_1;
            public static Func<BarLayoutTableCell, int> <>9__41_2;
            public static Func<BarLayoutTableCell, bool> <>9__43_4;
            public static Func<BarLayoutTableCell, bool> <>9__43_5;
            public static Func<IBarLayoutTableInfo, Bar> <>9__50_0;
            public static Func<BarLayoutTableCell, double> <>9__54_0;
            public static Func<BarLayoutTableCell, bool> <>9__55_0;
            public static Func<BarLayoutTableCell, bool> <>9__56_0;
            public static Func<BaseBarLayoutCalculator, BarContainerControlPanel> <>9__62_0;
            public static Func<BarContainerControlPanel, BarContainerControl> <>9__62_1;
            public static Func<BarContainerControl, BarContainerType> <>9__62_2;
            public static Func<BarContainerType> <>9__62_3;
            public static Func<BarLayoutTableCell, bool> <>9__62_5;
            public static Action<BarLayoutTableCell> <>9__63_0;
            public static Func<BarLayoutTableCell, int> <>9__63_4;
            public static Func<int> <>9__63_5;
            public static Func<double> <>9__63_11;
            public static Func<BarLayoutTableCell, double> <>9__65_3;

            static <>c();
            internal BarContainerControlPanel <DragCell>b__62_0(BaseBarLayoutCalculator x);
            internal BarContainerControl <DragCell>b__62_1(BarContainerControlPanel x);
            internal BarContainerType <DragCell>b__62_2(BarContainerControl x);
            internal BarContainerType <DragCell>b__62_3();
            internal bool <DragCell>b__62_5(BarLayoutTableCell x);
            internal void <DragCellFromAnotherRow>b__63_0(BarLayoutTableCell x);
            internal double <DragCellFromAnotherRow>b__63_11();
            internal int <DragCellFromAnotherRow>b__63_4(BarLayoutTableCell x);
            internal int <DragCellFromAnotherRow>b__63_5();
            internal Bar <FindCell>b__50_0(IBarLayoutTableInfo x);
            internal bool <get_CanAddItems>b__31_0(BarLayoutTableCell x);
            internal bool <get_HasReducedCells>b__37_0(BarLayoutTableCell x);
            internal int <get_WholeRowItemIndex>b__33_0(BarLayoutTableCell x);
            internal int <get_WholeRowItemIndex>b__33_1();
            internal bool <GetLeftCells>b__56_0(BarLayoutTableCell x);
            internal double <GetRightAvailableSize>b__54_0(BarLayoutTableCell x);
            internal bool <GetRightCells>b__55_0(BarLayoutTableCell x);
            internal bool <Measure>b__43_4(BarLayoutTableCell x);
            internal bool <Measure>b__43_5(BarLayoutTableCell x);
            internal double <MoveCellsInsideRow>b__65_3(BarLayoutTableCell x);
            internal bool <Reorder>b__41_0(BarLayoutTableCell x);
            internal int <Reorder>b__41_1(BarLayoutTableCell x);
            internal int <Reorder>b__41_2(BarLayoutTableCell x);
        }
    }
}

