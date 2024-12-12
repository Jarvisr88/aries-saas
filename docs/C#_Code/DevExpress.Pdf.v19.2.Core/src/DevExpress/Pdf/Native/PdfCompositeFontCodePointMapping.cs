namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompositeFontCodePointMapping : IPdfCodePointMapping
    {
        private readonly short[] cidToGidMap;
        private readonly IDictionary<short, short> mapping;

        public PdfCompositeFontCodePointMapping(short[] cidToGidMap, IDictionary<short, short> mapping)
        {
            this.cidToGidMap = cidToGidMap;
            this.mapping = mapping;
        }

        public bool UpdateCodePoints(short[] codePoints, bool useEmbeddedFontEncoding)
        {
            if (this.cidToGidMap != null)
            {
                int length = this.cidToGidMap.Length;
                int num2 = codePoints.Length;
                for (int i = 0; i < num2; i++)
                {
                    ushort index = (ushort) codePoints[i];
                    codePoints[i] = (index >= length) ? ((short) 0) : this.cidToGidMap[index];
                }
            }
            if (this.mapping != null)
            {
                int length = codePoints.Length;
                for (int i = 0; i < length; i++)
                {
                    short num7;
                    codePoints[i] = this.mapping.TryGetValue(codePoints[i], out num7) ? num7 : ((short) 0);
                }
            }
            return true;
        }
    }
}

