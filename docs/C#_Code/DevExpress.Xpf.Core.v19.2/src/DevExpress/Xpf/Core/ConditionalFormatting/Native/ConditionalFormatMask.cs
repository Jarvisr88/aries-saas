namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    [Flags]
    public enum ConditionalFormatMask
    {
        None = 0,
        DataBarOrIcon = 1,
        Background = 2,
        Foreground = 4,
        FontSize = 8,
        FontStyle = 0x10,
        FontFamily = 0x20,
        FontStretch = 0x40,
        FontWeight = 0x80,
        TextDecorations = 0x100
    }
}

