namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfImageToDCTImageToXObjectConverter : PdfDeviceImageToXObjectConverter
    {
        private readonly byte[] imageData;
        private readonly PdfImage sMask;

        public PdfImageToDCTImageToXObjectConverter(byte[] imageData, int width, int height) : this(width, height, imageData, null)
        {
        }

        private PdfImageToDCTImageToXObjectConverter(int width, int height, byte[] imageData, PdfImage sMask) : this(width, height, imageData, sMask, PdfDeviceColorSpaceKind.RGB, PdfDeviceImageToXObjectConverter.RgbDecode)
        {
        }

        private PdfImageToDCTImageToXObjectConverter(int width, int height, byte[] imageData, PdfImage sMask, PdfDeviceColorSpaceKind colorSpaceKind, IList<PdfRange> decode) : base(width, height, colorSpaceKind, decode)
        {
            this.imageData = imageData;
            this.sMask = sMask;
        }

        public static PdfImageToDCTImageToXObjectConverter ConvertToJpeg(Bitmap image, long jpegImageQuality, bool extractSMask)
        {
            byte[] jpegImageData;
            EncoderParameters encoderParms = new EncoderParameters(1);
            encoderParms.Param[0] = new EncoderParameter(Encoder.Quality, jpegImageQuality);
            ImageCodecInfo jpegCodec = null;
            ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
            int index = 0;
            while (true)
            {
                if (index < imageDecoders.Length)
                {
                    ImageCodecInfo info = imageDecoders[index];
                    if (!(info.FormatID == ImageFormat.Jpeg.Guid))
                    {
                        index++;
                        continue;
                    }
                    jpegCodec = info;
                }
                if (jpegCodec == null)
                {
                    jpegImageData = GetJpegImageData(image);
                }
                else
                {
                    try
                    {
                        jpegImageData = SaveBitmapToByteArray(delegate (Stream stream) {
                            image.Save(stream, jpegCodec, encoderParms);
                        });
                    }
                    catch (ExternalException)
                    {
                        using (Bitmap tempImage = new Bitmap(image))
                        {
                            jpegImageData = SaveBitmapToByteArray(delegate (Stream stream) {
                                tempImage.Save(stream, jpegCodec, encoderParms);
                            });
                        }
                    }
                }
                break;
            }
            PdfImage sMask = null;
            if (extractSMask)
            {
                if (image.PixelFormat.HasFlag(PixelFormat.Indexed))
                {
                    Func<Color, bool> predicate = <>c.<>9__2_2;
                    if (<>c.<>9__2_2 == null)
                    {
                        Func<Color, bool> local2 = <>c.<>9__2_2;
                        predicate = <>c.<>9__2_2 = c => c.A != 0xff;
                    }
                    if (image.Palette.Entries.Any<Color>(predicate))
                    {
                        sMask = new PdfIndexedImageToSoftMaskConverter(image).GetXObject();
                    }
                }
                else if ((image.PixelFormat != PixelFormat.Format24bppRgb) && ((image.PixelFormat & PixelFormat.Alpha) != PixelFormat.Undefined))
                {
                    using (Bitmap bitmap = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format32bppArgb))
                    {
                        int width = bitmap.Width;
                        int height = bitmap.Height;
                        using (PdfImageStreamFlateEncoder encoder = new PdfImageStreamFlateEncoder(width))
                        {
                            using (PdfImageConverterImageDataReader reader = PdfImageConverterImageDataReader.Create(bitmap, 4))
                            {
                                bool flag = false;
                                int count = width * 4;
                                byte[] buffer2 = new byte[count];
                                int num5 = 0;
                                while (true)
                                {
                                    if (num5 >= height)
                                    {
                                        if (flag)
                                        {
                                            sMask = CreateSoftMaskImage(width, height, encoder.GetEncodedData());
                                        }
                                        break;
                                    }
                                    reader.ReadNextRow(buffer2, count);
                                    int num6 = 0;
                                    int num7 = 0;
                                    while (true)
                                    {
                                        if (num6 >= width)
                                        {
                                            encoder.EndRow();
                                            num5++;
                                            break;
                                        }
                                        num7 += 3;
                                        byte num8 = buffer2[num7++];
                                        encoder.Add(num8);
                                        flag |= num8 != 0xff;
                                        num6++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (((image.PixelFormat != ((PixelFormat) 0x200f)) || !image.RawFormat.Equals(ImageFormat.Jpeg)) ? new PdfImageToDCTImageToXObjectConverter(image.Width, image.Height, jpegImageData, sMask) : CreateFromYCCKJpeg(jpegImageData, image.Width, image.Height, sMask));
        }

        public static PdfImageToDCTImageToXObjectConverter CreateFromYCCKJpeg(byte[] jpegImageData, int width, int height) => 
            CreateFromYCCKJpeg(jpegImageData, width, height, null);

        private static PdfImageToDCTImageToXObjectConverter CreateFromYCCKJpeg(byte[] jpegImageData, int width, int height, PdfImage sMask)
        {
            PdfRange[] decode = new PdfRange[] { new PdfRange(1.0, 0.0), new PdfRange(1.0, 0.0), new PdfRange(1.0, 0.0), new PdfRange(1.0, 0.0) };
            return new PdfImageToDCTImageToXObjectConverter(width, height, jpegImageData, sMask, PdfDeviceColorSpaceKind.CMYK, decode);
        }

        protected override PdfImage GetSoftMask() => 
            this.sMask;

        protected override PdfFilter Filter =>
            new PdfDCTDecodeFilter(null);

        protected override byte[] ImageData =>
            this.imageData;

        public override int ImageDataLength =>
            this.imageData.Length;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfImageToDCTImageToXObjectConverter.<>c <>9 = new PdfImageToDCTImageToXObjectConverter.<>c();
            public static Func<Color, bool> <>9__2_2;

            internal bool <ConvertToJpeg>b__2_2(Color c) => 
                c.A != 0xff;
        }
    }
}

