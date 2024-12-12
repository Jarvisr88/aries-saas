namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Year:{Begin.Value.ToShortDateString(),nq}")]
    public sealed class YearInterval : DateInterval
    {
        public YearInterval(DateTime dateTime);
        public YearInterval(int year);
    }
}

