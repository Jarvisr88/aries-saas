namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Month:{Begin.Value.ToShortDateString(),nq}")]
    public sealed class MonthInterval : DateInterval
    {
        public MonthInterval(DateTime dateTime);
        public MonthInterval(int year, int month);
    }
}

