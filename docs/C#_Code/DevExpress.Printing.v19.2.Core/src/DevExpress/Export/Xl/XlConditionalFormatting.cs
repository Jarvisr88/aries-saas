namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public class XlConditionalFormatting
    {
        private readonly List<XlCellRange> ranges = new List<XlCellRange>();
        private readonly List<XlCondFmtRule> rules = new List<XlCondFmtRule>();

        internal XlCellPosition GetTopLeftCell()
        {
            int num = -1;
            int num2 = -1;
            foreach (XlCellRange range in this.Ranges)
            {
                num = (num != -1) ? Math.Min(num, range.TopLeft.Column) : range.TopLeft.Column;
                num2 = (num2 != -1) ? Math.Min(num2, range.TopLeft.Row) : range.TopLeft.Row;
            }
            return new XlCellPosition(num, num2);
        }

        public IList<XlCellRange> Ranges =>
            this.ranges;

        public IList<XlCondFmtRule> Rules =>
            this.rules;
    }
}

