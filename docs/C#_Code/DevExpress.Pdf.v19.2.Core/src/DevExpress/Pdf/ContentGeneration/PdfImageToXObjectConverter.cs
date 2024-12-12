namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public abstract class PdfImageToXObjectConverter
    {
        private const int jpegHighestQuality = 100;
        private const int nonRGBImageFlags = 0x160;
        protected const PixelFormat CMYK32bppPixelFormat = ((PixelFormat) 0x200f);
        private readonly int width;
        private readonly int height;

        protected PdfImageToXObjectConverter(Image image) : this(image.Width, image.Height)
        {
        }

        protected PdfImageToXObjectConverter(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public static PdfImageToXObjectConverter Create(Bitmap bmp, bool convertToJpeg, long jpegQuality, bool extractSMask)
        {
            PdfImageToXObjectConverter converter;
            if (bmp.RawFormat.Equals(ImageFormat.Jpeg))
            {
                converter = (bmp.PixelFormat != ((PixelFormat) 0x200f)) ? (((bmp.Flags & 0x160) != 0) ? PdfImageToDCTImageToXObjectConverter.ConvertToJpeg(bmp, (long) 100, false) : new PdfImageToDCTImageToXObjectConverter(GetJpegImageData(bmp), bmp.Width, bmp.Height)) : PdfImageToDCTImageToXObjectConverter.CreateFromYCCKJpeg(GetJpegImageData(bmp), bmp.Width, bmp.Height);
            }
            else if (!bmp.RawFormat.Equals(ImageFormat.Tiff))
            {
                converter = CreateConverter(bmp, convertToJpeg, jpegQuality, extractSMask);
            }
            else
            {
                int index = Array.IndexOf<int>(bmp.PropertyIdList, 0x103);
                if (index != -1)
                {
                    EncoderValue? nullable;
                    switch (BitConverter.ToInt16(bmp.PropertyItems[index].Value, 0))
                    {
                        case 3:
                            nullable = new EncoderValue?(EncoderValue.CompressionCCITT3);
                            break;

                        case 4:
                            nullable = new EncoderValue?(EncoderValue.CompressionCCITT4);
                            break;

                        case 5:
                            nullable = new EncoderValue?(EncoderValue.CompressionLZW);
                            break;

                        default:
                            nullable = null;
                            break;
                    }
                    if (nullable != null)
                    {
                        converter = PdfTiffImageToXObjectConverter.Create(bmp, nullable.Value);
                        if (converter != null)
                        {
                            return converter;
                        }
                    }
                }
                converter = CreateConverter(bmp, convertToJpeg, jpegQuality, extractSMask);
            }
            if (!convertToJpeg || (jpegQuality == 100))
            {
                return converter;
            }
            PdfImageToXObjectConverter converter2 = PdfImageToDCTImageToXObjectConverter.ConvertToJpeg(bmp, jpegQuality, extractSMask);
            return ((converter2.ImageDataLength < converter.ImageDataLength) ? converter2 : converter);
        }

        public static PdfImageToXObjectConverter Create(PdfRecognizedImageInfo imageInfo, Stream imageStream, bool convertToJpeg, long jpegQuality, bool extractSMask)
        {
            if (imageInfo.Type == PdfRecognizedImageFormat.Tiff)
            {
                PdfTiffImageToXObjectConverter converter = PdfTiffImageToXObjectConverter.Create(imageStream);
                if (converter != null)
                {
                    return converter;
                }
            }
            if (!convertToJpeg || (jpegQuality == 100))
            {
                PdfRecognizedImageFormat type = imageInfo.Type;
                if (type == PdfRecognizedImageFormat.RGBJpeg)
                {
                    return new PdfImageToDCTImageToXObjectConverter(GetImageData(imageStream), imageInfo.JpegWidth, imageInfo.JpegHeight);
                }
                if (type == PdfRecognizedImageFormat.YCCKJpeg)
                {
                    return PdfImageToDCTImageToXObjectConverter.CreateFromYCCKJpeg(GetImageData(imageStream), imageInfo.JpegWidth, imageInfo.JpegHeight);
                }
            }
            using (Bitmap bitmap = (Bitmap) Image.FromStream(imageStream))
            {
                return Create(bitmap, convertToJpeg, jpegQuality, extractSMask);
            }
        }

        private static PdfImageToXObjectConverter CreateConverter(Bitmap bmp, bool convertToJpeg, long jpegQuality, bool extractSMask)
        {
            PdfImageToXObjectConverter converter;
            if (convertToJpeg && (jpegQuality == 100))
            {
                return PdfImageToDCTImageToXObjectConverter.ConvertToJpeg(bmp, (long) 100, extractSMask);
            }
            PixelFormat pixelFormat = bmp.PixelFormat;
            if ((pixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
            {
                return new PdfIndexedImageToXObjectConverter(bmp);
            }
            if (pixelFormat == PixelFormat.Format24bppRgb)
            {
                return new Pdf24BppBitmapToXObjectConverter(bmp);
            }
            if (pixelFormat == PixelFormat.Format32bppArgb)
            {
                return new Pdf32BppBitmapToXObjectConverter(bmp, extractSMask);
            }
            if ((pixelFormat & PixelFormat.Alpha) != PixelFormat.Undefined)
            {
                using (Bitmap bitmap = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format32bppArgb))
                {
                    converter = new Pdf32BppBitmapToXObjectConverter(bitmap, extractSMask);
                }
            }
            else
            {
                using (Bitmap bitmap2 = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb))
                {
                    converter = new Pdf24BppBitmapToXObjectConverter(bitmap2);
                }
            }
            return converter;
        }

        private static byte[] GetImageData(Stream imageStream)
        {
            int length = (int) imageStream.Length;
            byte[] buffer = new byte[length];
            imageStream.Read(buffer, 0, length);
            return buffer;
        }

        protected static byte[] GetJpegImageData(Bitmap image)
        {
            byte[] buffer;
            try
            {
                buffer = SaveBitmapToByteArray(delegate (Stream stream) {
                    image.Save(stream, ImageFormat.Jpeg);
                });
            }
            catch (ExternalException)
            {
                using (Bitmap tempImage = new Bitmap(image))
                {
                    buffer = SaveBitmapToByteArray(delegate (Stream stream) {
                        tempImage.Save(stream, ImageFormat.Jpeg);
                    });
                }
            }
            return buffer;
        }

        public abstract PdfImage GetXObject();
        protected static byte[] SaveBitmapToByteArray(Action<Stream> save)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                save(stream);
                return stream.ToArray();
            }
        }

        public int Width =>
            this.width;

        public int Height =>
            this.height;

        public abstract int ImageDataLength { get; }
    }
}

