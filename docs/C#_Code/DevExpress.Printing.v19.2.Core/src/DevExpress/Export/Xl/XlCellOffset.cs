namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlCellOffset
    {
        private int row;
        private int column;
        private XlCellOffsetType rowType;
        private XlCellOffsetType columnType;
        public int Row
        {
            get => 
                this.row;
            set => 
                this.row = value;
        }
        public int Column
        {
            get => 
                this.column;
            set => 
                this.column = value;
        }
        public XlCellOffsetType RowType
        {
            get => 
                this.rowType;
            set => 
                this.rowType = value;
        }
        public XlCellOffsetType ColumnType
        {
            get => 
                this.columnType;
            set => 
                this.columnType = value;
        }
        public XlCellOffset(int column, int row)
        {
            this.column = column;
            this.row = row;
            this.columnType = XlCellOffsetType.Position;
            this.rowType = XlCellOffsetType.Position;
        }

        public XlCellOffset(int column, int row, XlCellOffsetType columnType, XlCellOffsetType rowType) : this(column, row)
        {
            this.columnType = columnType;
            this.rowType = rowType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is XlCellOffset))
            {
                return false;
            }
            XlCellOffset offset = (XlCellOffset) obj;
            return ((this.column == offset.column) && ((this.row == offset.row) && ((this.columnType == offset.columnType) && (this.rowType == offset.rowType))));
        }

        public override int GetHashCode() => 
            this.column ^ this.row;

        public XlCellPosition ToCellPosition(IXlExpressionContext context)
        {
            int num = (context == null) ? 1 : context.CurrentCell.Row;
            int num2 = (context == null) ? 0 : context.CurrentCell.Column;
            int column = this.Column;
            int row = this.Row;
            if (this.ColumnType == XlCellOffsetType.Offset)
            {
                column += num2;
            }
            if (this.RowType == XlCellOffsetType.Offset)
            {
                row += num;
            }
            if (context != null)
            {
                if (column < 0)
                {
                    column += context.MaxColumnCount;
                }
                if (column >= context.MaxColumnCount)
                {
                    column -= context.MaxColumnCount;
                }
                if (row < 0)
                {
                    row += context.MaxRowCount;
                }
                if (row >= context.MaxRowCount)
                {
                    row -= context.MaxRowCount;
                }
            }
            return new XlCellPosition(column, row, (this.ColumnType == XlCellOffsetType.Offset) ? XlPositionType.Relative : XlPositionType.Absolute, (this.RowType == XlCellOffsetType.Offset) ? XlPositionType.Relative : XlPositionType.Absolute);
        }
    }
}

