namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class XlSubtotalFunction : IXlFormulaParameter
    {
        public static XlSubtotalFunction Create(XlCellRange range, XlSummary summary, bool ignoreHidden)
        {
            XlSubtotalFunction function = new XlSubtotalFunction {
                Ranges = new List<XlCellRange>()
            };
            function.Ranges.Add(range);
            function.Summary = summary;
            function.IgnoreHidden = ignoreHidden;
            return function;
        }

        public static XlSubtotalFunction Create(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden) => 
            new XlSubtotalFunction { 
                Ranges = new List<XlCellRange>(ranges),
                Summary = summary,
                IgnoreHidden = ignoreHidden
            };

        public string ToString(CultureInfo culture)
        {
            int summary = (int) this.Summary;
            if (this.IgnoreHidden)
            {
                summary += 100;
            }
            StringBuilder builder = new StringBuilder();
            int num2 = 0;
            int num3 = 0;
            foreach (XlCellRange range in this.Ranges)
            {
                if (num3 == 0)
                {
                    if (num2 > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append("SUBTOTAL(");
                    builder.Append(summary.ToString());
                }
                builder.Append(",");
                builder.Append(range.ToString());
                num3++;
                if (num3 >= 0xfe)
                {
                    builder.Append(")");
                    num2++;
                    num3 = 0;
                }
            }
            if (num3 > 0)
            {
                builder.Append(")");
            }
            return ((num2 != 0) ? ((this.Summary != XlSummary.Average) ? ((this.Summary != XlSummary.Min) ? ((this.Summary != XlSummary.Max) ? $"SUM({builder.ToString()})" : $"MAX({builder.ToString()})") : $"MIN({builder.ToString()})") : $"AVERAGE({builder.ToString()})") : builder.ToString());
        }

        public IList<XlCellRange> Ranges { get; set; }

        public XlSummary Summary { get; set; }

        public bool IgnoreHidden { get; set; }
    }
}

