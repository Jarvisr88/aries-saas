namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class JBIG2HalftoneRegion : JBIG2SegmentData
    {
        private readonly int imageWidth;
        private readonly int imageHeight;
        private readonly int imageHorizontalOffset;
        private readonly int imageVerticalOffset;
        private readonly int horizontalCoordinate;
        private readonly int verticalCoordinate;
        private readonly JBIG2RegionSegmentInfo regionInfo;
        private readonly int flags;

        public JBIG2HalftoneRegion(JBIG2StreamHelper streamHelper, JBIG2SegmentHeader header, JBIG2Image image) : base(streamHelper, header, image)
        {
            this.regionInfo = new JBIG2RegionSegmentInfo(base.StreamHelper);
            this.flags = base.StreamHelper.ReadByte();
            this.imageWidth = base.StreamHelper.ReadInt32();
            this.imageHeight = base.StreamHelper.ReadInt32();
            this.imageHorizontalOffset = base.StreamHelper.ReadInt32();
            this.imageVerticalOffset = base.StreamHelper.ReadInt32();
            this.horizontalCoordinate = base.StreamHelper.ReadInt16();
            this.verticalCoordinate = base.StreamHelper.ReadInt16();
        }

        private unsafe int[] GetGrayScaleImageData()
        {
            int[] numArray = new int[this.imageWidth * this.imageHeight];
            int num = (int) Math.Ceiling(Math.Log((double) this.PatternDictionary.Patterns.Count, 2.0));
            JBIG2Image[] imageArray = new JBIG2Image[num];
            JBIG2Decoder decoder = JBIG2Decoder.Create(base.StreamHelper, 0);
            int gbTemplate = (this.flags & 6) >> 1;
            for (int i = 0; i < num; i++)
            {
                imageArray[i] = new JBIG2Image(this.imageWidth, this.imageHeight);
                int[] gbat = new int[] { 0, -1, -3, -1, 2, -2, -2, -2 };
                gbat[0] = (gbTemplate <= 1) ? 3 : 2;
                JBIG2GenericRegion.CreateDecoder(gbTemplate, imageArray[i], decoder, gbat).Decode();
            }
            int index = 1;
            while (index < num)
            {
                int num5 = 0;
                while (true)
                {
                    if (num5 >= this.imageHeight)
                    {
                        index++;
                        break;
                    }
                    int x = 0;
                    while (true)
                    {
                        if (x >= this.imageWidth)
                        {
                            num5++;
                            break;
                        }
                        imageArray[index].SetPixel(x, num5, imageArray[index - 1].GetPixel(x, num5) ^ imageArray[index].GetPixel(x, num5));
                        x++;
                    }
                }
            }
            int y = 0;
            while (y < this.imageHeight)
            {
                int x = 0;
                while (true)
                {
                    if (x >= this.imageWidth)
                    {
                        y++;
                        break;
                    }
                    int num9 = 0;
                    while (true)
                    {
                        if (num9 >= num)
                        {
                            x++;
                            break;
                        }
                        int* numPtr1 = &(numArray[x + (y * this.imageWidth)]);
                        numPtr1[0] |= imageArray[num9].GetPixel(x, y) << (((num - num9) - 1) & 0x1f);
                        num9++;
                    }
                }
            }
            return numArray;
        }

        public override void Process()
        {
            double num = ((double) (this.imageHorizontalOffset + this.horizontalCoordinate)) / 256.0;
            double num2 = ((double) (this.imageVerticalOffset - this.verticalCoordinate)) / 256.0;
            double num3 = ((double) (this.imageHorizontalOffset + this.verticalCoordinate)) / 256.0;
            double num4 = ((double) (this.imageVerticalOffset + this.horizontalCoordinate)) / 256.0;
            int[] grayScaleImageData = this.GetGrayScaleImageData();
            JBIG2PatternDictionary patternDictionary = this.PatternDictionary;
            int composeOperator = (this.flags >> 4) & 5;
            int num6 = 0;
            while (num6 < this.imageHeight)
            {
                int num7 = 0;
                while (true)
                {
                    if (num7 >= this.imageWidth)
                    {
                        num6++;
                        break;
                    }
                    int num8 = (int) (((((double) this.imageHorizontalOffset) / 256.0) + (num * num7)) + (num3 * num6));
                    int num9 = (int) (((((double) this.imageVerticalOffset) / 256.0) + (num2 * num7)) + (num4 * num6));
                    JBIG2Image image = patternDictionary.Patterns[grayScaleImageData[num7 + (num6 * this.imageWidth)]];
                    base.Image.Composite(image, num8 + this.regionInfo.X, num9 + this.regionInfo.Y, composeOperator);
                    num7++;
                }
            }
        }

        private JBIG2PatternDictionary PatternDictionary
        {
            get
            {
                JBIG2PatternDictionary dictionary2;
                using (List<int>.Enumerator enumerator = base.Header.ReferredToSegments.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            int current = enumerator.Current;
                            JBIG2PatternDictionary data = base.Image.Segments[current].Data as JBIG2PatternDictionary;
                            if (data == null)
                            {
                                continue;
                            }
                            dictionary2 = data;
                        }
                        else
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                            return null;
                        }
                        break;
                    }
                }
                return dictionary2;
            }
        }
    }
}

