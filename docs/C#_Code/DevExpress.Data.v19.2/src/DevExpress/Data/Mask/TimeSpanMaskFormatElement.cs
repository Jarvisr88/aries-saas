namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class TimeSpanMaskFormatElement
    {
        public TimeSpanMaskFormatElement(bool hideOnNull);
        public abstract string Format(TimeSpanMaskManagerValue formatted);

        public bool Editable { get; }

        public bool HideOnNull { get; private set; }
    }
}

