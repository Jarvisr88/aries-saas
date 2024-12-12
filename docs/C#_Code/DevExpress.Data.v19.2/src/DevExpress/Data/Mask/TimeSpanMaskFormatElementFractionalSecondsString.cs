namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementFractionalSecondsString : TimeSpanMaskFormatElementNonEditable
    {
        private readonly TimeSpanCultureInfoBase cultureInfo;

        public TimeSpanMaskFormatElementFractionalSecondsString(string mask, TimeSpanCultureInfoBase cultureInfo, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);
    }
}

