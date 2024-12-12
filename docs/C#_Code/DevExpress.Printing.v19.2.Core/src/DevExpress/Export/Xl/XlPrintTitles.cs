namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class XlPrintTitles
    {
        private readonly IXlSheet sheet;

        public XlPrintTitles(IXlSheet sheet)
        {
            this.sheet = sheet;
        }

        private void AppendRange(StringBuilder sb, XlCellRange intervalRange)
        {
            if (intervalRange != null)
            {
                XlCellRange range = new XlCellRange(intervalRange.TopLeft, intervalRange.BottomRight) {
                    SheetName = this.sheet.Name
                };
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.Append(range.ToString(true));
            }
        }

        internal bool IsValid() => 
            ((this.Rows == null) || (!this.Rows.TopLeft.IsRow || !this.Rows.BottomRight.IsRow)) ? ((this.Columns != null) && (this.Columns.TopLeft.IsColumn && this.Columns.BottomRight.IsColumn)) : true;

        public void SetColumns(int startIndex, int endIndex)
        {
            this.Columns = new XlCellRange(new XlCellPosition(startIndex, -1, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(endIndex, -1, XlPositionType.Absolute, XlPositionType.Absolute));
        }

        public void SetRows(int startIndex, int endIndex)
        {
            this.Rows = new XlCellRange(new XlCellPosition(-1, startIndex, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(-1, endIndex, XlPositionType.Absolute, XlPositionType.Absolute));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            this.AppendRange(sb, this.Columns);
            this.AppendRange(sb, this.Rows);
            return sb.ToString();
        }

        public XlCellRange Rows { get; set; }

        public XlCellRange Columns { get; set; }
    }
}

