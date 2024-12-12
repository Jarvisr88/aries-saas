namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JBIG2PatternDictionary : JBIG2SegmentData
    {
        private readonly IList<JBIG2Image> patterns;

        public JBIG2PatternDictionary(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            int num = base.StreamHelper.ReadByte();
            if (num == 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int width = base.StreamHelper.ReadByte();
            int height = base.StreamHelper.ReadByte();
            int num4 = base.StreamHelper.ReadInt32() + 1;
            JBIG2Image image2 = new JBIG2Image(width * num4, height);
            int[] gbat = new int[] { 0, 0, -3, -1, 2, -2, -2, -2 };
            gbat[0] = -width;
            JBIG2GenericRegion.CreateDecoder((num & 6) >> 1, image2, JBIG2Decoder.Create(base.StreamHelper, 0), gbat).Decode();
            this.patterns = new JBIG2Image[num4];
            int num5 = 0;
            while (num5 < num4)
            {
                JBIG2Image image3 = new JBIG2Image(width, height);
                int num6 = num5 * width;
                int y = 0;
                while (true)
                {
                    if (y >= height)
                    {
                        this.patterns[num5] = image3;
                        num5++;
                        break;
                    }
                    int x = 0;
                    while (true)
                    {
                        if (x >= width)
                        {
                            y++;
                            break;
                        }
                        image3.SetPixel(x, y, image2.GetPixel(x + num6, y));
                        x++;
                    }
                }
            }
        }

        public IList<JBIG2Image> Patterns =>
            this.patterns;
    }
}

