namespace DevExpress.Export.Xl
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlCellPosition
    {
        private static readonly XlCellPosition invalidValue;
        private static readonly XlCellPosition topLeft;
        private readonly int column;
        private readonly int row;
        public static XlCellPosition InvalidValue =>
            invalidValue;
        public static XlCellPosition TopLeft =>
            topLeft;
        public static int MaxColumnCount =>
            0x4000;
        public static int MaxRowCount =>
            0x100000;
        public XlCellPosition(int column, int row, XlPositionType columnType, XlPositionType rowType)
        {
            if (column >= MaxColumnCount)
            {
                throw new ArgumentException($"Column index exceeds the maximum number of columns (16384). The actual value is {column}");
            }
            if (row >= MaxRowCount)
            {
                throw new ArgumentException($"Row index exceeds the maximum number of rows (1048576). The actual value is {row}");
            }
            this.column = (column < 0) ? -1 : column;
            this.row = (row < 0) ? -1 : row;
            if (columnType == XlPositionType.Absolute)
            {
                this.column |= 0x100000;
            }
            if (rowType == XlPositionType.Absolute)
            {
                this.row |= 0x1000000;
            }
        }

        public XlCellPosition(int column, int row) : this(column, row, XlPositionType.Relative, XlPositionType.Relative)
        {
        }

        public XlPositionType ColumnType =>
            ((XlPositionType) (this.column >> 20)) & XlPositionType.Absolute;
        public XlPositionType RowType =>
            ((XlPositionType) (this.row >> 0x18)) & XlPositionType.Absolute;
        public int Column =>
            (this.column < 0) ? -1 : (this.column & 0xfffff);
        public int Row =>
            (this.row < 0) ? -1 : (this.row & 0xffffff);
        public bool IsValid =>
            (this.column >= 0) || (this.row >= 0);
        public bool IsColumn =>
            (this.column >= 0) && (this.row < 0);
        public bool IsRow =>
            (this.row >= 0) && (this.column < 0);
        public bool IsColumnOrRow =>
            this.IsColumn || this.IsRow;
        public bool IsAbsolute =>
            !this.IsColumn ? (!this.IsRow ? ((this.ColumnType == XlPositionType.Absolute) && (this.RowType == XlPositionType.Absolute)) : (this.RowType == XlPositionType.Absolute)) : (this.ColumnType == XlPositionType.Absolute);
        public bool IsRelative =>
            !this.IsColumn ? (!this.IsRow ? ((this.ColumnType == XlPositionType.Relative) && (this.RowType == XlPositionType.Relative)) : (this.RowType == XlPositionType.Relative)) : (this.ColumnType == XlPositionType.Relative);
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (!this.IsRow)
            {
                int num = this.Column + 1;
                if (this.ColumnType == XlPositionType.Absolute)
                {
                    builder.Append('$');
                }
                int num2 = num % 0x1a;
                num2 ??= 0x1a;
                if (num > 0x2be)
                {
                    int num3 = (num - num2) % 0x2a4;
                    num3 ??= 0x2a4;
                    builder.Append((char) (0x40 + (((num - num3) - num2) / 0x2a4)));
                    builder.Append((char) (0x40 + (num3 / 0x1a)));
                }
                else if (num > 0x1a)
                {
                    builder.Append((char) (0x40 + ((num - num2) / 0x1a)));
                }
                builder.Append((char) (0x40 + num2));
            }
            if (!this.IsColumn)
            {
                int num4 = this.Row + 1;
                if (this.RowType == XlPositionType.Absolute)
                {
                    builder.Append('$');
                }
                builder.Append(num4);
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is XlCellPosition))
            {
                return false;
            }
            XlCellPosition position = (XlCellPosition) obj;
            return ((this.column == position.column) && (this.row == position.row));
        }

        internal bool EqualsPosition(XlCellPosition otherPosition) => 
            (this.Column == otherPosition.Column) && (this.Row == otherPosition.Row);

        public override int GetHashCode() => 
            this.column ^ this.row;

        public XlCellPosition AsAbsolute() => 
            new XlCellPosition(this.Column, this.Row, XlPositionType.Absolute, XlPositionType.Absolute);

        public XlCellPosition AsRelative() => 
            new XlCellPosition(this.Column, this.Row, XlPositionType.Relative, XlPositionType.Relative);

        internal XlCellPosition Offset(int columnOffset, int rowOffset)
        {
            int column = this.Column;
            int row = this.Row;
            if ((columnOffset != 0) && ((this.ColumnType == XlPositionType.Relative) && !this.IsRow))
            {
                column += columnOffset;
            }
            if ((rowOffset != 0) && ((this.RowType == XlPositionType.Relative) && !this.IsColumn))
            {
                row += rowOffset;
            }
            return new XlCellPosition(column, row, this.ColumnType, this.RowType);
        }

        static XlCellPosition()
        {
            invalidValue = new XlCellPosition(-1, -1);
            topLeft = new XlCellPosition(0, 0);
        }
    }
}

