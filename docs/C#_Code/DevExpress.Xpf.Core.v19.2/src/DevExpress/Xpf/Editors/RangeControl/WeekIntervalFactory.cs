namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Globalization;

    public class WeekIntervalFactory : DateTimeIntervalFactory
    {
        protected override string FormatTextInternal(object current, string format)
        {
            int weekNumber = this.GetWeekNumber(base.ToDateTime(current));
            return string.Format(format, weekNumber, current);
        }

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
            return this.SnapInternal(dt).AddDays(7.0);
        }

        private int GetWeekNumber(DateTime current)
        {
            DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
            return currentInfo.Calendar.GetWeekOfYear(current, currentInfo.CalendarWeekRule, this.FirstDayOfWeek);
        }

        protected override DateTime SnapInternal(DateTime value)
        {
            int num = (int) (this.FirstDayOfWeek - value.DayOfWeek);
            return value.Date.AddDays((num > 0) ? ((double) (num - 7)) : ((double) num));
        }

        protected override string DefaultShortTextFormat =>
            "W{0}";

        protected override string DefaultTextFormat =>
            "Week {0}";

        protected override string DefaultLongTextFormat =>
            "Week {0}, {1:MMMM}";

        protected override string DefaultLongestTextFormat =>
            this.DefaultLongTextFormat;

        private DayOfWeek FirstDayOfWeek =>
            DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
    }
}

