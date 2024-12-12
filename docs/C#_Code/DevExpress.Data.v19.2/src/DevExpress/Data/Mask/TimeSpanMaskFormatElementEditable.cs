namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class TimeSpanMaskFormatElementEditable : TimeSpanMaskFormatElementNonEditable
    {
        protected TimeSpanMaskFormatElementEditable(string mask, bool hideOnNull = false);
        public abstract TimeSpanMaskManagerValue ApplyElement(long result, TimeSpanMaskManagerValue editedTimeSpan);
        public abstract TimeSpanElementEditor CreateElementEditor(TimeSpanMaskManagerValue editedValue, bool allowInputCustomValue = false);

        public abstract TimeSpanMaskPart Part { get; }

        protected internal bool IsHeadFormat { get; set; }
    }
}

