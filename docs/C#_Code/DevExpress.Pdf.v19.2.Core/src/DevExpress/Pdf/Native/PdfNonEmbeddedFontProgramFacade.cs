namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfNonEmbeddedFontProgramFacade : PdfFontProgramFacade
    {
        private PdfNonEmbeddedFontProgramFacade(PdfSimpleFont font) : this(null, nullable, nullable, new PdfSimpleFontCodePointMapping(null, GetUnicodeMapping((!font.Encoding.IsEmpty || !(font is PdfTrueTypeFont)) ? font.Encoding : null)))
        {
            double? nullable = null;
            nullable = null;
        }

        private PdfNonEmbeddedFontProgramFacade(PdfType0Font font, IPdfCodePointMapping mapping, PdfFontCharset charset) : base(null, nullable, nullable, mapping, charset)
        {
            double? nullable = null;
            nullable = null;
        }

        public static PdfNonEmbeddedFontProgramFacade Create(PdfSimpleFont font) => 
            new PdfNonEmbeddedFontProgramFacade(font);

        public static PdfNonEmbeddedFontProgramFacade Create(PdfType0Font font)
        {
            PdfCIDCharset predefinedCharset = PdfCIDCharset.GetPredefinedCharset(font);
            IPdfCodePointMapping mapping = (predefinedCharset == null) ? ((IPdfCodePointMapping) new PdfCompositeFontCodePointMapping(font.CidToGidMap, null)) : ((IPdfCodePointMapping) new PdfNonEmbeddedCIDFontCodePointMapping(predefinedCharset));
            return new PdfNonEmbeddedFontProgramFacade(font, mapping, (predefinedCharset == null) ? PdfFontCharset.Basic : predefinedCharset.Charset);
        }
    }
}

