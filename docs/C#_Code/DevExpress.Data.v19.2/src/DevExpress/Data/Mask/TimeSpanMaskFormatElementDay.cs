﻿namespace DevExpress.Data.Mask
{
    using System;
    using System.Runtime.InteropServices;

    public class TimeSpanMaskFormatElementDay : TimeSpanMaskFormatElementEditable
    {
        public TimeSpanMaskFormatElementDay(string mask, bool hideOnNull = false);
        public override TimeSpanMaskManagerValue ApplyElement(long result, TimeSpanMaskManagerValue editedTimeSpan);
        public override TimeSpanElementEditor CreateElementEditor(TimeSpanMaskManagerValue editedValue, bool allowInputCustomValue = false);

        public override TimeSpanMaskPart Part { get; }
    }
}

