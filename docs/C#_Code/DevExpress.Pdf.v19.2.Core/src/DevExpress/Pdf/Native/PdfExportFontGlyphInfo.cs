namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfExportFontGlyphInfo
    {
        public string Unicode { get; }
        public float Width { get; }
        public PdfExportFontGlyphInfo(string unicode, float width)
        {
            this.<Unicode>k__BackingField = unicode;
            this.<Width>k__BackingField = width;
        }
    }
}

