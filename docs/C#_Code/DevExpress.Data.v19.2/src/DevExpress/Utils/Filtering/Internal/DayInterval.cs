namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Day:{Begin.Value.ToShortDateString(),nq}")]
    public sealed class DayInterval : DateInterval
    {
        public DayInterval(DateTime dateTime);
        public DayInterval(int year, int month, int day);
    }
}

