namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementSecondsString : TimeSpanMaskFormatElementNonEditable
    {
        private readonly TimeSpanCultureInfoBase cultureInfo;

        public TimeSpanMaskFormatElementSecondsString(string mask, TimeSpanCultureInfoBase cultureInfo, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);
    }
}

