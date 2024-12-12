namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementMinutesString : TimeSpanMaskFormatElementNonEditable
    {
        private readonly TimeSpanCultureInfoBase cultureInfo;

        public TimeSpanMaskFormatElementMinutesString(string mask, TimeSpanCultureInfoBase cultureInfo, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);
    }
}

