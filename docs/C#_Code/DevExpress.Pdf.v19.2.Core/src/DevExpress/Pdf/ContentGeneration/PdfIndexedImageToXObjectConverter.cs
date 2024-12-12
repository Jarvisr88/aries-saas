namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class PdfIndexedImageToXObjectConverter : PdfImageToXObjectConverter
    {
        private readonly int bitsPerComponent;
        private readonly byte[] imageData;
        private readonly byte[] lookupTable;
        private readonly PdfDeviceColorSpaceKind baseColorSpaceKind;
        private readonly PdfRange[] colorKeyMask;
        private readonly int maxValue;

        public PdfIndexedImageToXObjectConverter(Bitmap bmp) : base(bmp)
        {
            PixelFormat pixelFormat = bmp.PixelFormat;
            this.bitsPerComponent = (pixelFormat == PixelFormat.Format1bppIndexed) ? 1 : ((pixelFormat == PixelFormat.Format4bppIndexed) ? 4 : 8);
            ColorPalette palette = bmp.Palette;
            int flags = palette.Flags;
            Color[] entries = palette.Entries;
            this.maxValue = entries.Length;
            if ((flags & 2) != 0)
            {
                this.baseColorSpaceKind = PdfDeviceColorSpaceKind.Gray;
                this.lookupTable = new byte[this.maxValue];
                for (int i = 0; i < this.maxValue; i++)
                {
                    Color color2 = entries[i];
                    this.lookupTable[i] = color2.R;
                    if (color2.A == 0)
                    {
                        this.colorKeyMask ??= CreateColorKeyMask(i);
                    }
                }
            }
            else
            {
                this.baseColorSpaceKind = PdfDeviceColorSpaceKind.RGB;
                this.lookupTable = new byte[this.maxValue * 3];
                int index = 0;
                int num3 = 0;
                while (index < this.maxValue)
                {
                    Color color = entries[index];
                    this.lookupTable[num3++] = color.R;
                    this.lookupTable[num3++] = color.G;
                    this.lookupTable[num3++] = color.B;
                    if (color.A == 0)
                    {
                        this.colorKeyMask ??= CreateColorKeyMask(index);
                    }
                    index++;
                }
            }
            this.imageData = this.ExtractImageData(bmp);
        }

        private static PdfRange[] CreateColorKeyMask(int index) => 
            new PdfRange[] { new PdfRange((double) index, (double) index) };

        private byte[] ExtractImageData(Bitmap bmp)
        {
            byte[] buffer2;
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
                byte[] data = new byte[count * height];
                int num4 = count % 4;
                int num5 = (num4 == 0) ? count : ((count + 4) - num4);
                long num6 = stream.Length - num5;
                int num7 = 0;
                int offset = 0;
                while (true)
                {
                    if (num7 >= height)
                    {
                        buffer2 = PdfFlateEncoder.Encode(data);
                        break;
                    }
                    stream.Position = num6;
                    stream.Read(data, offset, count);
                    num7++;
                    offset += count;
                    num6 -= num5;
                }
            }
            return buffer2;
        }

        public override PdfImage GetXObject()
        {
            PdfIndexedColorSpace colorSpace = new PdfIndexedColorSpace(new PdfDeviceColorSpace(this.baseColorSpaceKind), this.maxValue - 1, this.lookupTable);
            PdfFilter[] filters = new PdfFilter[] { new PdfFlateDecodeFilter(null) };
            return new PdfImage(base.Width, base.Height, colorSpace, this.bitsPerComponent, colorSpace.CreateDefaultDecodeArray(this.bitsPerComponent), new PdfArrayCompressedData(filters, this.imageData), null) { ColorKeyMask = this.colorKeyMask };
        }

        public override int ImageDataLength =>
            this.imageData.Length;
    }
}

