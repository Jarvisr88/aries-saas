﻿namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementSeconds : TimeSpanMaskFormatElementEditable
    {
        public TimeSpanMaskFormatElementSeconds(string mask, bool hideOnNull = false);
        public override TimeSpanMaskManagerValue ApplyElement(long result, TimeSpanMaskManagerValue editedTimeSpan);
        public override TimeSpanElementEditor CreateElementEditor(TimeSpanMaskManagerValue editedValue, bool allowInputCustomValue = false);
        public override string Format(TimeSpanMaskManagerValue formatted);

        public override TimeSpanMaskPart Part { get; }
    }
}

