namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BarLayoutTable
    {
        private List<BarLayoutTableRow> rows;
        private readonly List<IBarLayoutTableInfo> elements;
        private bool needsUpdate;
        private System.Windows.Controls.Orientation orientation;
        private BaseBarLayoutCalculator owner;
        private LayoutTableSizeHelper helper;

        public event EventHandler RequestMeasure;

        public BarLayoutTable(BaseBarLayoutCalculator owner);
        public virtual BarLayoutTableCell Add(IBarLayoutTableInfo element);
        protected virtual void AppendSize(ref Size target, Size source);
        public virtual void Arrange(Size finalSize);
        protected virtual BarLayoutTableRow CreateTableRow(int index);
        public void DragCell(BarLayoutTableCell currentCell, Point mousePosition, Point delta);
        public virtual void DragCellToNewRow(int newRowIndex, BarLayoutTableCell currentCell, Point relativeToTablePoint, Point delta);
        public virtual BarLayoutTableCell FindCell(Bar bar);
        public virtual BarLayoutTableCell FindCell(IBarLayoutTableInfo layoutInfo);
        public virtual BarLayoutTableCell FindCell(Point mousePosition);
        public virtual BarLayoutTableCell FindClosestCell(Point mousePosition, Func<BarLayoutTableCell, bool> predicate);
        public virtual BarLayoutTableRow FindClosestRow(Point mousePosition);
        public virtual BarLayoutTableRow FindRow(IBarLayoutTableInfo layoutInfo);
        public virtual BarLayoutTableRow FindRow(Point mousePosition);
        protected virtual BarLayoutTableRow GetClosestRow(Point mousePosition);
        protected virtual bool GetOrCreateRow(int index, out BarLayoutTableRow row, bool needsWholeRow);
        public virtual void IncrementIndexes(int startWith);
        public virtual void InsertFloatBar(Bar bar, Point point);
        public bool IsFirstRow(BarLayoutTableRow row);
        public bool IsLastRow(BarLayoutTableRow row);
        public virtual void Measure(Size constraint);
        public virtual void MoveCell(ref BarLayoutTableCell cell, int rowIndex);
        public virtual void MoveCells(ref BarLayoutTableCell first, ref BarLayoutTableCell second);
        protected virtual void OnOrientationChanged(System.Windows.Controls.Orientation oldValue);
        protected virtual void RaiseRequestMeasure();
        public virtual void Remove(IBarLayoutTableInfo element);
        protected virtual void Reorder();
        protected internal virtual void Reset();
        public void SetColumnIndent(double value);
        public void SetLastChildFill(bool value);
        public void SetRowIndent(double value);
        public virtual void UpdateIndexes();

        public List<BarLayoutTableRow> Rows { get; }

        protected List<IBarLayoutTableInfo> Elements { get; }

        public bool IgnoreElementOffset { get; set; }

        public double RowIndent { get; private set; }

        public double ColumnIndent { get; protected set; }

        public bool LastChildFill { get; protected set; }

        public Size DesiredSize { get; protected set; }

        public Size RenderSize { get; }

        public bool NeedsUpdate { get; set; }

        public BaseBarLayoutCalculator Owner { get; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        public LayoutTableSizeHelper Helper { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarLayoutTable.<>c <>9;
            public static Func<BarLayoutTableRow, int> <>9__48_0;
            public static Func<BarLayoutTableRow, object> <>9__48_1;
            public static Predicate<BarLayoutTableRow> <>9__62_0;
            public static Action<BarLayoutTableRow> <>9__71_0;
            public static Func<<>f__AnonymousType3<BarLayoutTableRow, double>, double> <>9__73_1;
            public static Func<<>f__AnonymousType3<BarLayoutTableRow, double>, BarLayoutTableRow> <>9__73_2;
            public static Action<BarLayoutTableRow> <>9__75_0;
            public static Func<BaseBarLayoutCalculator, BarContainerControlPanel> <>9__76_0;
            public static Func<BarContainerControlPanel, BarContainerControl> <>9__76_1;
            public static Func<BarContainerControl, BarContainerType> <>9__76_2;
            public static Func<BarContainerType> <>9__76_3;

            static <>c();
            internal double <GetClosestRow>b__73_1(<>f__AnonymousType3<BarLayoutTableRow, double> x);
            internal BarLayoutTableRow <GetClosestRow>b__73_2(<>f__AnonymousType3<BarLayoutTableRow, double> x);
            internal void <IncrementIndexes>b__75_0(BarLayoutTableRow x);
            internal BarContainerControlPanel <InsertFloatBar>b__76_0(BaseBarLayoutCalculator x);
            internal BarContainerControl <InsertFloatBar>b__76_1(BarContainerControlPanel x);
            internal BarContainerType <InsertFloatBar>b__76_2(BarContainerControl x);
            internal BarContainerType <InsertFloatBar>b__76_3();
            internal void <MoveCell>b__71_0(BarLayoutTableRow x);
            internal int <Reorder>b__48_0(BarLayoutTableRow x);
            internal object <Reorder>b__48_1(BarLayoutTableRow x);
            internal bool <UpdateIndexes>b__62_0(BarLayoutTableRow x);
        }
    }
}

