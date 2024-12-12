namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    [Flags]
    public enum AnimationMask
    {
        None = 0,
        Background = 1,
        Foreground = 2,
        ValuePosition = 4,
        IconOpacity = 8,
        FontSize = 0x10,
        FontStyle = 0x20,
        FontFamily = 0x40,
        FontStretch = 0x80,
        FontWeight = 0x100,
        Icon = 0x200,
        All = 0x3ff
    }
}

