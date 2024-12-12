namespace DevExpress.XtraExport.Csv
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlNumFmtHours : XlNumFmtTimeBase
    {
        private bool is12HourTime;

        public XlNumFmtHours(int count, bool elapsed, bool is12HourTime) : base(count, elapsed)
        {
            this.is12HourTime = is12HourTime;
        }

        protected override void FormatCore(XlVariantValue value, CultureInfo culture, XlNumFmtResult result)
        {
            long num = base.Elapsed ? ((long) TimeSpan.FromDays(value.NumericValue).TotalHours) : ((long) value.DateTimeValue.Hour);
            if (this.Is12HourTime)
            {
                num = num % ((long) 12);
                num ??= 12;
            }
            result.Text = result.Text + num.ToString(new string('0', base.Count), culture);
        }

        public override XlNumFmtDesignator Designator =>
            XlNumFmtDesignator.Hour;

        public bool Is12HourTime
        {
            get => 
                this.is12HourTime;
            set => 
                this.is12HourTime = value;
        }
    }
}

