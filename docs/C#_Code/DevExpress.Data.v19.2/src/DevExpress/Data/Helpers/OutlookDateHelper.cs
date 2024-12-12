namespace DevExpress.Data.Helpers
{
    using System;
    using System.Globalization;

    public abstract class OutlookDateHelper
    {
        protected OutlookDateHelper();
        private int GetMonthDelta(DateTime d1, DateTime d2);
        public OutlookInterval? GetOutlookInterval(DateTime? ndate);
        public int GetWeekNumber(TimeSpan weekSpan, bool alwaysAdd);
        public static DateTime GetWeekStart(DateTime sortTime);
        public static DateTime GetWeekStart(DateTime sortTime, DateTimeFormatInfo dateFormatInfo);

        protected abstract DateTime SortZeroTime { get; }

        protected abstract DateTime SortStartWeek { get; }
    }
}

