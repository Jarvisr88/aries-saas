namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementLiteral : TimeSpanMaskFormatElement
    {
        public TimeSpanMaskFormatElementLiteral(string literal, bool hideOnNull = false);
        public override string Format(TimeSpanMaskManagerValue formatted);

        public string Literal { get; private set; }
    }
}

