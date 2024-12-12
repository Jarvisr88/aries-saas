namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration.TiffParsing;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfTiffImageToXObjectConverter : PdfImageToXObjectConverter
    {
        private readonly byte[] imageData;
        private readonly PdfFilter filter;
        private readonly PdfColorSpace colorSpace;
        private readonly int bitsPerComponent;
        private readonly PdfRange[] decode;

        private PdfTiffImageToXObjectConverter(int width, int height, byte[] imageData, PdfFilter filter, PdfColorSpace colorSpace, int bitsPerComponent, PdfRange[] decode) : base(width, height)
        {
            this.imageData = imageData;
            this.filter = filter;
            this.colorSpace = colorSpace;
            this.bitsPerComponent = bitsPerComponent;
            this.decode = decode;
        }

        public static PdfTiffImageToXObjectConverter Create(Stream stream)
        {
            TiffParser parser = new TiffParser(stream);
            CCITTFilterParameters parameters = CCITTFilterParameters.Create(parser);
            if ((parameters != null) && (parser.GetStripCount() == 1))
            {
                byte[] imageData = parser.GetImageData();
                if (imageData != null)
                {
                    PdfRange[] decode = new PdfRange[] { new PdfRange(0.0, 1.0) };
                    return new PdfTiffImageToXObjectConverter(parameters.Columns, parameters.Rows, imageData, new PdfCCITTFaxDecodeFilter(parameters), new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray), 1, decode);
                }
            }
            return CreateFromLZW(parser);
        }

        public static PdfTiffImageToXObjectConverter Create(Bitmap image, EncoderValue compression)
        {
            PdfTiffImageToXObjectConverter converter;
            using (MemoryStream stream = new MemoryStream())
            {
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, (long) compression);
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                int index = 0;
                while (true)
                {
                    if (index >= imageEncoders.Length)
                    {
                        converter = Create(stream);
                        break;
                    }
                    ImageCodecInfo encoder = imageEncoders[index];
                    if (encoder.FormatDescription.Equals("TIFF"))
                    {
                        image.Save(stream, encoder, encoderParams);
                    }
                    index++;
                }
            }
            return converter;
        }

        private static PdfTiffImageToXObjectConverter CreateFromLZW(TiffParser tiffParser)
        {
            int? nullable2;
            bool flag1;
            int num1;
            int? nullable1;
            ITiffValue firstValue = tiffParser.GetFirstValue(TiffTag.Compression);
            if (firstValue != null)
            {
                flag1 = firstValue.AsInt() != 5;
            }
            else
            {
                ITiffValue local1 = firstValue;
                flag1 = true;
            }
            if (flag1)
            {
                return null;
            }
            ITiffValue value2 = tiffParser.GetFirstValue(TiffTag.BitsPerSample);
            if (value2 != null)
            {
                num1 = value2.AsInt();
            }
            else
            {
                ITiffValue local2 = value2;
                num1 = 1;
            }
            int bitsPerComponent = num1;
            ITiffValue value3 = tiffParser.GetFirstValue(TiffTag.PhotometricInterpretation);
            if (value3 != null)
            {
                nullable1 = new int?(value3.AsInt());
            }
            else
            {
                ITiffValue local3 = value3;
                nullable2 = null;
                nullable1 = nullable2;
            }
            int? nullable = nullable1;
            if (nullable != null)
            {
                PdfColorSpace space;
                PdfRange[] rangeArray;
                int num8;
                int num9;
                bool flag2;
                switch (nullable.GetValueOrDefault())
                {
                    case 0:
                        rangeArray = new PdfRange[] { new PdfRange(1.0, 0.0) };
                        space = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
                        break;

                    case 1:
                        rangeArray = new PdfRange[] { new PdfRange(0.0, 1.0) };
                        space = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
                        break;

                    case 2:
                    {
                        int? nullable4;
                        ITiffValue value4 = tiffParser.GetFirstValue(TiffTag.SamplesPerPixel);
                        if (value4 != null)
                        {
                            nullable4 = new int?(value4.AsInt());
                        }
                        else
                        {
                            ITiffValue local4 = value4;
                            nullable2 = null;
                            nullable4 = nullable2;
                        }
                        int? nullable3 = nullable4;
                        if ((nullable3 != null) && (nullable3.Value != 3))
                        {
                            return null;
                        }
                        space = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB);
                        rangeArray = new PdfRange[] { new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0), new PdfRange(0.0, 1.0) };
                        break;
                    }
                    case 3:
                    {
                        ITiffValue[] directoryEntryValue = tiffParser.GetDirectoryEntryValue(TiffTag.ColorMap);
                        int num5 = 1 << (bitsPerComponent & 0x1f);
                        byte[] lookupTable = new byte[num5 * 3];
                        int index = 0;
                        int num7 = 0;
                        while (true)
                        {
                            if (index >= num5)
                            {
                                space = new PdfIndexedColorSpace(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB), num5 - 1, lookupTable);
                                rangeArray = new PdfRange[] { new PdfRange(0.0, (double) (num5 - 1)) };
                                break;
                            }
                            lookupTable[num7++] = (byte) (directoryEntryValue[index].AsInt() >> 8);
                            lookupTable[num7++] = (byte) (directoryEntryValue[index + num5].AsInt() >> 8);
                            lookupTable[num7++] = (byte) (directoryEntryValue[index + (num5 * 2)].AsInt() >> 8);
                            index++;
                        }
                        break;
                    }
                    default:
                        goto TR_0001;
                }
                ITiffValue value5 = tiffParser.GetFirstValue(TiffTag.ImageWidth);
                if (value5 != null)
                {
                    num8 = value5.AsInt();
                }
                else
                {
                    ITiffValue local5 = value5;
                    num8 = 1;
                }
                int columns = num8;
                ITiffValue value6 = tiffParser.GetFirstValue(TiffTag.ImageLength);
                if (value6 != null)
                {
                    num9 = value6.AsInt();
                }
                else
                {
                    ITiffValue local6 = value6;
                    num9 = 1;
                }
                int height = num9;
                ITiffValue value7 = tiffParser.GetFirstValue(TiffTag.Predictor);
                if (value7 != null)
                {
                    flag2 = value7.AsInt() == 2;
                }
                else
                {
                    ITiffValue local7 = value7;
                    flag2 = false;
                }
                PdfLZWDecodeFilter filter = new PdfLZWDecodeFilter(true, flag2 ? PdfFlateLZWFilterPredictor.TiffPredictor : PdfFlateLZWFilterPredictor.NoPrediction, space.ComponentsCount, bitsPerComponent, columns);
                if (tiffParser.GetStripCount() == 1)
                {
                    byte[] imageData = tiffParser.GetImageData();
                    if (imageData != null)
                    {
                        return new PdfTiffImageToXObjectConverter(columns, height, imageData, filter, space, bitsPerComponent, rangeArray);
                    }
                }
                return null;
            }
        TR_0001:
            return null;
        }

        public override PdfImage GetXObject()
        {
            PdfFilter[] filters = new PdfFilter[] { this.filter };
            return new PdfImage(base.Width, base.Height, this.colorSpace, this.bitsPerComponent, this.decode, new PdfArrayCompressedData(filters, this.imageData), null);
        }

        public override int ImageDataLength =>
            this.imageData.Length;
    }
}

