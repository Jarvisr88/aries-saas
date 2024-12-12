namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTrueTypeFontProgramFacade : PdfFontProgramFacade
    {
        private readonly byte[] fontFileData;

        private PdfTrueTypeFontProgramFacade(PdfRectangle fontBBox, double? top, double? bottom, IPdfCodePointMapping mapping, byte[] fontFileData) : base(fontBBox, top, bottom, mapping)
        {
            this.fontFileData = fontFileData;
        }

        public static PdfTrueTypeFontProgramFacade Create(PdfSimpleFont font, byte[] trueTypeFontFile) => 
            Create(font, trueTypeFontFile, fontFile => fontFile.GetSimpleMapping(font.Encoding, font.IsSymbolic));

        public static PdfTrueTypeFontProgramFacade Create(PdfType0Font font, byte[] trueTypeFontFile) => 
            Create(font, trueTypeFontFile, fontFile => new PdfCompositeFontCodePointMapping(font.CidToGidMap, null));

        private static PdfTrueTypeFontProgramFacade Create(PdfFont font, byte[] fontFileData, Func<PdfFontFile, IPdfCodePointMapping> createMapping)
        {
            using (PdfFontFile file = new PdfFontFile(PdfFontFile.TTFVersion, fontFileData))
            {
                IPdfCodePointMapping mapping = createMapping(file);
                PdfFontDescriptor fontDescriptor = font.FontDescriptor;
                bool isSymbolic = font.IsSymbolic;
                PdfFontCmapTableDirectoryEntry cMap = file.CMap;
                if (cMap != null)
                {
                    PdfFontCmapSegmentMappingFormatEntry entry = cMap.Validate(!(font is PdfTrueTypeFont), isSymbolic);
                }
                else
                {
                    cMap = new PdfFontCmapTableDirectoryEntry(new PdfFontCmapSegmentMappingFormatEntry(isSymbolic ? PdfFontEncodingID.Symbol : PdfFontEncodingID.UGL));
                    file.AddTable(cMap);
                }
                PdfFontNameTableDirectoryEntry name = file.Name;
                if (name == null)
                {
                    file.AddTable(new PdfFontNameTableDirectoryEntry(cMap, font.RegistrationName));
                }
                else
                {
                    name.Create(cMap, font.RegistrationName);
                }
                PdfFontHheaTableDirectoryEntry hhea = file.Hhea;
                if (hhea != null)
                {
                    hhea.Validate();
                }
                PdfFontOS2TableDirectoryEntry entry5 = file.OS2;
                PdfFontHeadTableDirectoryEntry head = file.Head;
                if (entry5 != null)
                {
                    entry5.Validate(font, head, hhea);
                }
                PdfRectangle fontBBox = null;
                double? bottom = null;
                double? top = null;
                if (head != null)
                {
                    if (file.Glyf != null)
                    {
                        head.Validate(file.Glyf.Glyphs.Values);
                    }
                    double unitsPerEm = head.UnitsPerEm;
                    double num2 = 1000.0 / unitsPerEm;
                    fontBBox = new PdfRectangle(head.XMin * num2, head.YMin * num2, head.XMax * num2, head.YMax * num2);
                    if (((((double) (head.YMax - head.YMin)) / unitsPerEm) > 2.0) && (entry5 != null))
                    {
                        top = new double?((entry5.TypoAscender + entry5.TypoLineGap) * num2);
                        bottom = new double?(entry5.TypoDescender * num2);
                    }
                }
                PdfFontHmtxTableDirectoryEntry hmtx = file.Hmtx;
                if (hmtx == null)
                {
                    PdfFontHmtxTableDirectoryEntry local1 = hmtx;
                }
                else
                {
                    hmtx.Validate(file);
                }
                return new PdfTrueTypeFontProgramFacade(fontBBox, top, bottom, mapping, file.GetData());
            }
        }

        public byte[] FontFileData =>
            this.fontFileData;
    }
}

