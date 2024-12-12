namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DateTimeMaskFormatElementContext
    {
        public bool YearProcessed;
        public bool MonthProcessed;
        public bool DayProcessed;
    }
}

