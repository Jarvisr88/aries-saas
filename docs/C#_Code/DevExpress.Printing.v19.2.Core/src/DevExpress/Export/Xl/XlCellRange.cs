namespace DevExpress.Export.Xl
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    public class XlCellRange : IXlFormulaParameter, IXlCellRange
    {
        private string sheetName;
        private XlCellPosition topLeft;
        private XlCellPosition bottomRight;

        protected XlCellRange()
        {
        }

        public XlCellRange(XlCellPosition singleCell)
        {
            this.topLeft = new XlCellPosition(singleCell.Column, singleCell.Row, singleCell.ColumnType, singleCell.RowType);
            this.bottomRight = new XlCellPosition(singleCell.Column, singleCell.Row, singleCell.ColumnType, singleCell.RowType);
            this.sheetName = string.Empty;
            this.Normalize();
        }

        public XlCellRange(XlCellPosition topLeft, XlCellPosition bottomRight) : this(string.Empty, topLeft, bottomRight)
        {
        }

        public XlCellRange(string sheetName, XlCellPosition topLeft, XlCellPosition bottomRight)
        {
            if ((topLeft.IsColumn && bottomRight.IsRow) || (topLeft.IsRow && bottomRight.IsColumn))
            {
                throw new ArgumentException("Invalid or incompatible range positions.");
            }
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
            this.sheetName = sheetName;
            this.Normalize();
        }

        public XlCellRange AsAbsolute() => 
            new XlCellRange(this.topLeft.AsAbsolute(), this.bottomRight.AsAbsolute()) { SheetName = this.SheetName };

        public XlCellRange AsRelative() => 
            new XlCellRange(this.topLeft.AsRelative(), this.bottomRight.AsRelative()) { SheetName = this.SheetName };

        internal XlCellRange ChangeColumnType(XlPositionType columnType) => 
            new XlCellRange(new XlCellPosition(this.topLeft.Column, this.topLeft.Row, columnType, this.topLeft.RowType), new XlCellPosition(this.bottomRight.Column, this.bottomRight.Row, columnType, this.bottomRight.RowType)) { SheetName = this.SheetName };

        internal XlCellRange Clone() => 
            new XlCellRange(this.topLeft, this.bottomRight) { SheetName = this.SheetName };

        public static XlCellRange ColumnInterval(int left, int right) => 
            new XlCellRange(new XlCellPosition(left, -1), new XlCellPosition(right, -1));

        public static XlCellRange ColumnInterval(int left, int right, XlPositionType columnType) => 
            new XlCellRange(new XlCellPosition(left, -1, columnType, XlPositionType.Absolute), new XlCellPosition(right, -1, columnType, XlPositionType.Absolute));

        internal bool Contains(XlCellPosition position) => 
            (position.Column >= this.FirstColumn) && ((position.Column <= this.LastColumn) && ((position.Row >= this.FirstRow) && (position.Row <= this.LastRow)));

        internal bool Contains(int column, int row) => 
            (column >= this.FirstColumn) && ((column <= this.LastColumn) && ((row >= this.FirstRow) && (row <= this.LastRow)));

        public override bool Equals(object obj)
        {
            XlCellRange range = obj as XlCellRange;
            return ((range != null) ? (this.topLeft.Equals(range.topLeft) && (this.bottomRight.Equals(range.bottomRight) && (this.SheetName == range.SheetName))) : false);
        }

        public static XlCellRange FromLTRB(int left, int top, int right, int bottom) => 
            new XlCellRange(new XlCellPosition(left, top), new XlCellPosition(right, bottom));

        internal XlCellRange GetColumnRange(int column, XlPositionType columnType)
        {
            if ((column < this.topLeft.Column) || (column > this.bottomRight.Column))
            {
                return null;
            }
            return new XlCellRange(new XlCellPosition(column, this.topLeft.Row, columnType, this.topLeft.RowType), new XlCellPosition(column, this.bottomRight.Row, columnType, this.bottomRight.RowType)) { SheetName = this.SheetName };
        }

        public override int GetHashCode() => 
            (this.topLeft.GetHashCode() ^ this.bottomRight.GetHashCode()) ^ this.sheetName.GetHashCode();

        internal bool HasCommonCells(XlCellRange other) => 
            (Math.Max(this.FirstColumn, other.FirstColumn) <= Math.Min(this.LastColumn, other.LastColumn)) & (Math.Max(this.FirstRow, other.FirstRow) <= Math.Min(this.LastRow, other.LastRow));

        private static bool IsValidIndentifier(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (!IsValidIndentifierChar(value[i], i, value))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsValidIndentifierChar(char curChar, int index, string value)
        {
            if (index == 0)
            {
                if (curChar != '\\')
                {
                    if (!char.IsLetter(curChar) && (curChar != '_'))
                    {
                        return false;
                    }
                }
                else
                {
                    if (value.Length == 1)
                    {
                        return true;
                    }
                    if ((value[1] != '\\') && ((value[1] != '.') && ((value[1] != '?') && (value[1] != '_'))))
                    {
                        return false;
                    }
                }
            }
            return (char.IsLetterOrDigit(curChar) || ((curChar == '_') || ((curChar == '.') || ((curChar == '\\') || (curChar == '?')))));
        }

        private void Normalize()
        {
            if (this.topLeft.Row > this.bottomRight.Row)
            {
                int row = this.bottomRight.Row;
                XlPositionType rowType = this.bottomRight.RowType;
                int num2 = this.topLeft.Row;
                XlPositionType type2 = this.topLeft.RowType;
                this.topLeft = new XlCellPosition(this.topLeft.Column, row, this.topLeft.ColumnType, rowType);
                this.bottomRight = new XlCellPosition(this.bottomRight.Column, num2, this.bottomRight.ColumnType, type2);
            }
            if (this.topLeft.Column > this.bottomRight.Column)
            {
                int column = this.bottomRight.Column;
                XlPositionType columnType = this.bottomRight.ColumnType;
                int num4 = this.topLeft.Column;
                XlPositionType type4 = this.topLeft.ColumnType;
                this.topLeft = new XlCellPosition(column, this.topLeft.Row, columnType, this.topLeft.RowType);
                this.bottomRight = new XlCellPosition(num4, this.bottomRight.Row, type4, this.bottomRight.RowType);
            }
        }

        internal void Offset(int columnOffset, int rowOffset)
        {
            if (this.topLeft.IsRow)
            {
                if (columnOffset != 0)
                {
                    throw new InvalidOperationException("Can't offset interval row range!");
                }
                if (((this.topLeft.Row + rowOffset) < 0) || ((this.bottomRight.Row + rowOffset) >= XlCellPosition.MaxRowCount))
                {
                    throw new InvalidOperationException("Can't offset range out of worksheet bounds!");
                }
            }
            else if (!this.topLeft.IsColumn)
            {
                if (((this.topLeft.Column + columnOffset) < 0) || (((this.topLeft.Row + rowOffset) < 0) || (((this.bottomRight.Column + columnOffset) >= XlCellPosition.MaxColumnCount) || ((this.bottomRight.Row + rowOffset) >= XlCellPosition.MaxRowCount))))
                {
                    throw new InvalidOperationException("Can't offset range out of worksheet bounds!");
                }
            }
            else
            {
                if (rowOffset != 0)
                {
                    throw new InvalidOperationException("Can't offset interval column range!");
                }
                if (((this.topLeft.Column + columnOffset) < 0) || ((this.bottomRight.Column + columnOffset) >= XlCellPosition.MaxColumnCount))
                {
                    throw new InvalidOperationException("Can't offset range out of worksheet bounds!");
                }
            }
            this.topLeft = new XlCellPosition(this.topLeft.Column + columnOffset, this.topLeft.Row + rowOffset, this.topLeft.ColumnType, this.topLeft.RowType);
            this.bottomRight = new XlCellPosition(this.bottomRight.Column + columnOffset, this.bottomRight.Row + rowOffset, this.bottomRight.ColumnType, this.bottomRight.RowType);
        }

        public static XlCellRange Parse(string reference) => 
            XlRangeReferenceParser.Parse(reference);

        public static XlCellRange RowInterval(int top, int bottom) => 
            new XlCellRange(new XlCellPosition(-1, top), new XlCellPosition(-1, bottom));

        public static XlCellRange RowInterval(int top, int bottom, XlPositionType rowType) => 
            new XlCellRange(new XlCellPosition(-1, top, XlPositionType.Absolute, rowType), new XlCellPosition(-1, bottom, XlPositionType.Absolute, rowType));

        private static bool ShouldAddQuotes(string value) => 
            !IsValidIndentifier(value);

        public override string ToString() => 
            this.ToString(false);

        public string ToString(bool intervalRange)
        {
            string str = (!this.topLeft.Equals(this.bottomRight) || intervalRange) ? (this.topLeft.ToString() + ":" + this.bottomRight.ToString()) : this.topLeft.ToString();
            if (!string.IsNullOrEmpty(this.SheetName))
            {
                str = !ShouldAddQuotes(this.SheetName) ? (this.SheetName + "!" + str) : ("'" + this.SheetName.Replace("'", "''") + "'!" + str);
            }
            return str;
        }

        public string ToString(CultureInfo culture) => 
            this.ToString(false);

        public XlCellPosition TopLeft
        {
            [DebuggerStepThrough]
            get => 
                this.topLeft;
            set => 
                this.topLeft = value;
        }

        public XlCellPosition BottomRight
        {
            [DebuggerStepThrough]
            get => 
                this.bottomRight;
            set => 
                this.bottomRight = value;
        }

        public string SheetName
        {
            [DebuggerStepThrough]
            get => 
                this.sheetName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.sheetName = string.Empty;
                }
                else
                {
                    this.sheetName = value;
                }
            }
        }

        internal int RowCount =>
            (this.bottomRight.Row - this.topLeft.Row) + 1;

        internal int ColumnCount =>
            (this.bottomRight.Column - this.topLeft.Column) + 1;

        internal int FirstRow =>
            this.topLeft.IsColumn ? 0 : this.topLeft.Row;

        internal int LastRow =>
            this.bottomRight.IsColumn ? 0xfffff : this.bottomRight.Row;

        internal int FirstColumn =>
            this.topLeft.IsRow ? 0 : this.topLeft.Column;

        internal int LastColumn =>
            this.bottomRight.IsRow ? 0x3fff : this.bottomRight.Column;
    }
}

