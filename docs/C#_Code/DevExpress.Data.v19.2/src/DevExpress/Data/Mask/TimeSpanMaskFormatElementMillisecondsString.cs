namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementMillisecondsString : TimeSpanMaskFormatElementNonEditable
    {
        private readonly TimeSpanCultureInfoBase cultureInfo;

        public TimeSpanMaskFormatElementMillisecondsString(string mask, TimeSpanCultureInfoBase cultureInfo, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);
    }
}

