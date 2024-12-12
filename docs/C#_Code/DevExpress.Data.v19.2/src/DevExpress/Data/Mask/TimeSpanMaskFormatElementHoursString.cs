namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementHoursString : TimeSpanMaskFormatElementNonEditable
    {
        private readonly TimeSpanCultureInfoBase cultureInfo;

        public TimeSpanMaskFormatElementHoursString(string mask, TimeSpanCultureInfoBase cultureInfo, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);
    }
}

