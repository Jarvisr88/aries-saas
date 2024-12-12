namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlAnchorPoint
    {
        private readonly int column;
        private readonly int row;
        private readonly int columnOffset;
        private readonly int rowOffset;
        private readonly int cellWidth;
        private readonly int cellHeight;
        public XlAnchorPoint(int column, int row) : this(column, row, 0, 0, 0, 0)
        {
        }

        public XlAnchorPoint(int column, int row, int columnOffsetInPixels, int rowOffsetInPixels) : this(column, row, columnOffsetInPixels, rowOffsetInPixels, 0, 0)
        {
        }

        internal XlAnchorPoint(int column, int row, int columnOffsetInPixels, int rowOffsetInPixels, int cellWidthInPixels, int cellHeightInPixels)
        {
            Guard.ArgumentNonNegative(cellWidthInPixels, "cellWidthInPixels");
            Guard.ArgumentNonNegative(cellHeightInPixels, "cellHeightInPixels");
            this.column = column;
            this.row = row;
            this.columnOffset = columnOffsetInPixels;
            this.rowOffset = rowOffsetInPixels;
            this.cellWidth = cellWidthInPixels;
            this.cellHeight = cellHeightInPixels;
        }

        public int Column =>
            this.column;
        public int Row =>
            this.row;
        public int ColumnOffsetInPixels =>
            this.columnOffset;
        public int RowOffsetInPixels =>
            this.rowOffset;
        internal float RelativeColumnOffset =>
            (this.cellWidth != 0) ? ((float) ((this.columnOffset * 1.0) / ((double) this.cellWidth))) : 0f;
        internal float RelativeRowOffset =>
            (this.cellHeight != 0) ? ((float) ((this.rowOffset * 1.0) / ((double) this.cellHeight))) : 0f;
        public override bool Equals(object obj)
        {
            if (!(obj is XlAnchorPoint))
            {
                return false;
            }
            XlAnchorPoint point = (XlAnchorPoint) obj;
            return ((this.column == point.column) && ((this.row == point.row) && ((this.columnOffset == point.columnOffset) && ((this.rowOffset == point.rowOffset) && ((this.cellWidth == point.cellWidth) && (this.cellHeight == point.cellHeight))))));
        }

        public override int GetHashCode() => 
            ((((this.column ^ this.row) ^ this.columnOffset) ^ this.rowOffset) ^ this.cellWidth) ^ this.cellHeight;
    }
}

