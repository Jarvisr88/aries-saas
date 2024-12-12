namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementNonEditable : TimeSpanMaskFormatElement
    {
        public TimeSpanMaskFormatElementNonEditable(string mask, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);

        public string Mask { get; private set; }
    }
}

