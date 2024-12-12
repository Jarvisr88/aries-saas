namespace DevExpress.Xpf.Layout.Core.Platform
{
    using System;

    [Flags]
    public enum MouseButtons : long
    {
        None = 0L,
        Left = 0x100000L,
        Right = 0x200000L,
        Middle = 0x400000L,
        XButton1 = 0x800000L,
        XButton2 = 0x1000000L
    }
}

