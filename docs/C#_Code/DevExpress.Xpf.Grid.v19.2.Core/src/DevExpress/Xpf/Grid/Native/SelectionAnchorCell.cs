namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public class SelectionAnchorCell : CellBase
    {
        public SelectionAnchorCell(DataViewBase view, int rowHandle, ColumnBase column) : base(rowHandle, column)
        {
            this.View = view;
        }

        public DataViewBase View { get; private set; }

        public double CoordinateX { get; set; }

        public double OffsetX { get; set; }

        public int RowVisibleIndex { get; set; }

        internal bool IsLastRow { get; set; }
    }
}

