namespace DevExpress.XtraExport.Helpers
{
    using System;

    [Flags]
    public enum ClipboardDataFormat
    {
        Csv = 1,
        Html = 2,
        Text = 4,
        UnicodeText = 8,
        RichText = 0x10,
        Excel = 0x20,
        AllSupported = 0x3f
    }
}

