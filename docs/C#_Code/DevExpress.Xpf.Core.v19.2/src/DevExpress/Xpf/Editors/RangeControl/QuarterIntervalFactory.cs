namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;

    public class QuarterIntervalFactory : DateTimeIntervalFactory
    {
        protected override string FormatTextInternal(object current, string format) => 
            string.Format(format, this.GetQuarter((DateTime) this.Snap(current)) + 1, current);

        public override string GetLongestText(object current)
        {
            string longestTextFormat = this.LongestTextFormat;
            string format = longestTextFormat;
            if (longestTextFormat == null)
            {
                string local1 = longestTextFormat;
                format = this.DefaultLongestTextFormat;
            }
            return this.FormatTextInternal(current, format);
        }

        public override object GetNextValue(object value)
        {
            DateTime dt = base.ToDateTime(value);
            return this.SnapInternal(dt).AddMonths(3);
        }

        private int GetQuarter(DateTime value) => 
            (value.Month - 1) / 3;

        protected override DateTime SnapInternal(DateTime value)
        {
            int quarter = this.GetQuarter(value);
            return new DateTime(value.Year, (3 * quarter) + 1, 1, 0, 0, 0);
        }

        protected override string DefaultShortTextFormat =>
            "Q{0}";

        protected override string DefaultTextFormat =>
            "Q{0}, {1:yyyy}";

        protected override string DefaultLongTextFormat =>
            "Q{0}, {1:yyyy}";

        protected override string DefaultLongestTextFormat =>
            "Q{0}, {1:yyyy}";

        public override string LabelTextFormat =>
            "{0:MMM} {0:yyyy}";
    }
}

