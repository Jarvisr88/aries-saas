namespace DevExpress.Office.Drawing
{
    using System;

    [Flags]
    public enum TextMetricsPitchAndFamily : byte
    {
        FixedPitch = 1,
        Vector = 2,
        TrueType = 4,
        Device = 8
    }
}

