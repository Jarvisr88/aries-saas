namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfFontCmapGlyphRange : IComparable<PdfFontCmapGlyphRange>
    {
        private readonly short start;
        private readonly short end;
        public short Start =>
            this.start;
        public short End =>
            this.end;
        public PdfFontCmapGlyphRange(short start, short end)
        {
            this.start = start;
            this.end = end;
        }

        int IComparable<PdfFontCmapGlyphRange>.CompareTo(PdfFontCmapGlyphRange range) => 
            this.end - range.end;
    }
}

