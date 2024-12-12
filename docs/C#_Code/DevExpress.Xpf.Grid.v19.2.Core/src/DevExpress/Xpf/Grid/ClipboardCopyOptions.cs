namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum ClipboardCopyOptions
    {
        None = 0,
        Csv = 1,
        Excel = 2,
        Html = 4,
        Rtf = 8,
        Txt = 0x10,
        All = 0x1f
    }
}

