namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public enum PdfFontFlags
    {
        FixedWidthFont = 1,
        SerifFont = 2,
        SymbolicFont = 3,
        ScriptFont = 4,
        UsesTheAdobeStandardRomanCharacterSet = 6,
        Italic = 7,
        AllCapFont = 0x11,
        SmallCapFont = 0x12,
        ForceBoldAtSmallTextSize = 0x13
    }
}

