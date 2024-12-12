namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfNonEmbeddedCIDFontCodePointMapping : IPdfCodePointMapping
    {
        private readonly PdfCIDCharset charset;

        public PdfNonEmbeddedCIDFontCodePointMapping(PdfCIDCharset charset)
        {
            this.charset = charset;
        }

        public bool UpdateCodePoints(short[] codePoints, bool useEmbeddedFontEncoding)
        {
            for (int i = 0; i < codePoints.Length; i++)
            {
                codePoints[i] = this.charset.GetUnicode(codePoints[i]);
            }
            return false;
        }
    }
}

