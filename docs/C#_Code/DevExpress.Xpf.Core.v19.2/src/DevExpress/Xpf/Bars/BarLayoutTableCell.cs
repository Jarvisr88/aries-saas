namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarLayoutTableCell
    {
        private readonly BarLayoutTableRow row;
        private readonly BarLayoutTable table;
        private IBarLayoutTableInfo element;
        private bool forcedCanReduce;

        public BarLayoutTableCell(BarLayoutTableRow row, BarLayoutTable table);
        public virtual void Arrange(Rect finalRect);
        private Size CreateSize(double define, double secondary);
        private bool GetCanReduce();
        public Dock? GetDock(Point mousePosition);
        protected virtual void LayoutPropertyChanged(object sender, EventArgs e);
        private Size MakeValid(Size sz);
        public virtual void Measure();
        protected virtual void OnElementChanged(IBarLayoutTableInfo oldValue, IBarLayoutTableInfo newValue);
        public virtual void UpdateIndex(int rowIndex, int columnIndex);

        protected bool IsFirst { get; }

        protected bool IsLast { get; }

        protected double ColumnIndent { get; }

        protected virtual double DockPercent { get; }

        public BarLayoutTable Table { get; }

        public BarLayoutTableRow Row { get; }

        public bool IsVisible { get; }

        public IBarLayoutTableInfo Element { get; set; }

        public Size DesiredSize { get; private set; }

        public Size RenderSize { get; private set; }

        public Rect RenderRect { get; private set; }

        public bool CanReduce { get; }

        public bool CanExpand { get; private set; }

        protected double ActualOffset { get; set; }

        protected double Offset { get; }

        public double Constraint { get; set; }

        public Size MinDesiredSize { get; }

        public bool UseWholeRow { get; }

        public bool Fill { get; }

        public BarLayoutTableCell NextSibling { get; }

        public BarLayoutTableCell PreviousSibling { get; }

        public Size InfiniteSize { get; private set; }
    }
}

