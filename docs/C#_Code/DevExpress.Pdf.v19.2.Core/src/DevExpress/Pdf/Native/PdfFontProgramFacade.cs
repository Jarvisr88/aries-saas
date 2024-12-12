namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfFontProgramFacade
    {
        private readonly IPdfCodePointMapping mapping;
        private readonly PdfRectangle fontBBox;
        private readonly double? top;
        private readonly double? bottom;
        private readonly PdfFontCharset charset;

        protected PdfFontProgramFacade(PdfRectangle fontBBox, double? top, double? bottom, IPdfCodePointMapping mapping) : this(fontBBox, top, bottom, mapping, PdfFontCharset.Basic)
        {
        }

        protected PdfFontProgramFacade(PdfRectangle fontBBox, double? top, double? bottom, IPdfCodePointMapping mapping, PdfFontCharset charset)
        {
            this.fontBBox = fontBBox;
            this.mapping = mapping;
            this.top = top;
            this.bottom = bottom;
            this.charset = charset;
        }

        public static short[] GetUnicodeMapping(PdfSimpleFontEncoding encoding)
        {
            short[] numArray = new short[0x100];
            for (short i = 0; i < 0x100; i = (short) (i + 1))
            {
                short num2;
                if (encoding == null)
                {
                    num2 = i;
                }
                else if (!PdfUnicodeConverter.TryGetGlyphCode(encoding.GetGlyphName((byte) i), out num2))
                {
                    num2 = 0x20;
                }
                numArray[i] = num2;
            }
            return numArray;
        }

        public bool UpdateCodePoints(short[] codePoints, bool useEmbeddedFontEncoding) => 
            (this.mapping != null) && this.mapping.UpdateCodePoints(codePoints, useEmbeddedFontEncoding);

        public PdfRectangle FontBBox =>
            this.fontBBox;

        public double? Top =>
            this.top;

        public double? Bottom =>
            this.bottom;

        public PdfFontCharset FontCharset =>
            this.charset;
    }
}

