namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfType3FontContentStreamParser : PdfContentStreamParser
    {
        private PdfType3FontContentStreamParser(PdfResources resources, byte[] bytes) : base(resources, bytes)
        {
        }

        public static PdfCommandList ParseGlyph(PdfResources resources, byte[] data) => 
            new PdfType3FontContentStreamParser(resources, data).Parse();

        protected override bool IsType3FontParser =>
            true;
    }
}

