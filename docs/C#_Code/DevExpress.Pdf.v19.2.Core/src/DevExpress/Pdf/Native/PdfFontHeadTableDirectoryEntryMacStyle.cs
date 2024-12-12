namespace DevExpress.Pdf.Native
{
    using System;

    [Flags]
    public enum PdfFontHeadTableDirectoryEntryMacStyle
    {
        Empty = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4,
        Outline = 8,
        Shadow = 0x10,
        Condensed = 0x20,
        Extended = 0x40
    }
}

