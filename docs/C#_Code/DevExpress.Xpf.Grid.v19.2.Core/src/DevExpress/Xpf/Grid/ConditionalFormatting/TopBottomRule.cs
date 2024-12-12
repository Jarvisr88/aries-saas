namespace DevExpress.Xpf.Grid.ConditionalFormatting
{
    using System;
    using System.ComponentModel;

    [Obsolete("Use the Xpf.Core.ConditionalFormatting.TopBottomRule instead"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public enum TopBottomRule
    {
        TopItems,
        TopPercent,
        BottomItems,
        BottomPercent,
        AboveAverage,
        BelowAverage
    }
}

