namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfCFFFontProgramFacade : PdfFontProgramFacade
    {
        private PdfCFFFontProgramFacade(PdfRectangle fontBBox, IPdfCodePointMapping mapping) : base(fontBBox, nullable, nullable, mapping)
        {
            double? nullable = null;
            nullable = null;
        }

        public static PdfCFFFontProgramFacade Create(PdfSimpleFont font, byte[] compactFontFile) => 
            Create(compactFontFile, font, program => program.GetSimpleMapping(font.Encoding));

        public static PdfCFFFontProgramFacade Create(PdfType0Font font, byte[] compactFontFile) => 
            Create(compactFontFile, font, program => program.GetCompositeMapping(font.CidToGidMap));

        private static PdfCFFFontProgramFacade Create(byte[] compactFontFile, PdfFont font, Func<PdfType1FontCompactFontProgram, IPdfCodePointMapping> createMapping)
        {
            if (compactFontFile == null)
            {
                return null;
            }
            PdfType1FontCompactFontProgram fontProgram = PdfType1FontCompactFontProgram.Parse(compactFontFile);
            byte[] buffer = !fontProgram.Validate() ? compactFontFile : PdfCompactFontFormatTopDictIndexWriter.Write(fontProgram);
            return new PdfCFFFontProgramFacade(ReferenceEquals(fontProgram.FontBBox, PdfType1FontCompactFontProgram.DefaultFontBBox) ? null : fontProgram.FontBBox, createMapping(fontProgram)) { 
                FontFileData = buffer,
                OriginalFontData = compactFontFile,
                Charset = fontProgram.Charset.SidToGidMapping
            };
        }

        public byte[] FontFileData { get; private set; }

        public int GlyphCount =>
            (this.Charset == null) ? 1 : (this.Charset.Count + 1);

        public IDictionary<short, short> Charset { get; private set; }

        public byte[] OriginalFontData { get; private set; }
    }
}

