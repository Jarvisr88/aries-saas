namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfSimpleFontCodePointMapping : IPdfCodePointMapping
    {
        private readonly short[] glyphIndicesMapping;
        private readonly short[] unicodeMapping;

        public PdfSimpleFontCodePointMapping(short[] glyphIndicesMapping, short[] unicodeMapping)
        {
            this.glyphIndicesMapping = glyphIndicesMapping;
            this.unicodeMapping = unicodeMapping;
        }

        private static void MapCodes(short[] codePoints, short[] mappingTable)
        {
            int length = codePoints.Length;
            int num2 = mappingTable.Length;
            for (int i = 0; i < length; i++)
            {
                short index = codePoints[i];
                codePoints[i] = ((index < 0) || (index >= num2)) ? index : mappingTable[index];
            }
        }

        public bool UpdateCodePoints(short[] codePoints, bool useEmbeddedFontEncoding)
        {
            if ((this.glyphIndicesMapping != null) & useEmbeddedFontEncoding)
            {
                MapCodes(codePoints, this.glyphIndicesMapping);
                return true;
            }
            if (this.unicodeMapping != null)
            {
                MapCodes(codePoints, this.unicodeMapping);
            }
            return false;
        }
    }
}

