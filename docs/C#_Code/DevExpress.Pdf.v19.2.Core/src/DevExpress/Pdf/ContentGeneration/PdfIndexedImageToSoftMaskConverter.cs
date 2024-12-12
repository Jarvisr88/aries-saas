namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfIndexedImageToSoftMaskConverter : PdfImageToXObjectConverter
    {
        private readonly int bitsPerComponent;
        private readonly byte[] imageData;

        public PdfIndexedImageToSoftMaskConverter(Bitmap bmp) : base(bmp)
        {
            PixelFormat pixelFormat = bmp.PixelFormat;
            this.bitsPerComponent = (pixelFormat == PixelFormat.Format1bppIndexed) ? 1 : ((pixelFormat == PixelFormat.Format4bppIndexed) ? 4 : 8);
            this.imageData = this.ExtractImageData(bmp);
        }

        private byte[] ExtractImageData(Bitmap bmp)
        {
            byte[] buffer3;
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Bmp);
                int num = this.bitsPerComponent * base.Width;
                int count = num / 8;
                if ((num % 8) != 0)
                {
                    count++;
                }
                int height = base.Height;
                byte[] data = new byte[base.Width * height];
                int num4 = count % 4;
                int num5 = (num4 == 0) ? count : ((count + 4) - num4);
                long num6 = stream.Length - num5;
                byte[] buffer = new byte[count];
                Color[] entries = bmp.Palette.Entries;
                int num7 = 8 / this.bitsPerComponent;
                byte num8 = (byte) ((1 << (this.bitsPerComponent & 0x1f)) - 1);
                int num9 = 0;
                int num10 = 0;
                while (true)
                {
                    if (num9 >= height)
                    {
                        buffer3 = PdfFlateEncoder.Encode(data);
                        break;
                    }
                    stream.Position = num6;
                    stream.Read(buffer, 0, count);
                    int index = 0;
                    while (true)
                    {
                        if (index >= count)
                        {
                            num9++;
                            num6 -= num5;
                            break;
                        }
                        int num12 = Math.Min(num7, base.Width - (index * num7));
                        byte num13 = buffer[index];
                        int num14 = 0;
                        int num15 = 8 - this.bitsPerComponent;
                        while (true)
                        {
                            if (num14 >= num12)
                            {
                                index++;
                                break;
                            }
                            data[num10++] = entries[(num13 >> (num15 & 0x1f)) & num8].A;
                            num14++;
                            num15 -= this.bitsPerComponent;
                        }
                    }
                }
            }
            return buffer3;
        }

        public override PdfImage GetXObject()
        {
            PdfDeviceColorSpace colorSpace = new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray);
            PdfFilter[] filters = new PdfFilter[] { new PdfFlateDecodeFilter(null) };
            return new PdfImage(base.Width, base.Height, colorSpace, 8, colorSpace.CreateDefaultDecodeArray(8), new PdfArrayCompressedData(filters, this.imageData), null);
        }

        public override int ImageDataLength =>
            this.imageData.Length;
    }
}

