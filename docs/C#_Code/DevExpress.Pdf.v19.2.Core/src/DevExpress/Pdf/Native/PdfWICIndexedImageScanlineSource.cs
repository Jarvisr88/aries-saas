namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop.WIC;
    using System;
    using System.Security;

    public class PdfWICIndexedImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private const int alphaMask = -16777216;
        private static readonly Guid WICPixelFormat24bppRGB = new Guid(0x6fddc324, 0x4e03, 0x4bfe, 0xb1, 0x85, 0x3d, 0x77, 0x76, 0x8d, 0xc9, 13);
        private readonly int width;
        private readonly PdfWICBitmapSource bitmapSource;
        private WICFormatConverter converter;
        private WICPalette palette;
        private int currentY;

        [SecuritySafeCritical]
        public PdfWICIndexedImageScanlineSource(WICImagingFactory factory, IPdfImageScanlineSource source, int width, int height, int bitsPerComponent, byte[] lookupTable)
        {
            this.width = width;
            this.converter = factory.CreateFormatConverter();
            int[] palette = new int[0x100];
            int num = 1 << (bitsPerComponent & 0x1f);
            int num2 = 0xff / (num - 1);
            int num3 = Math.Min(lookupTable.Length / 3, num);
            int num4 = 0;
            int num5 = 0;
            while (num4 < num3)
            {
                int num6 = -16777216;
                int num7 = 2;
                while (true)
                {
                    if (num7 < 0)
                    {
                        palette[num4 * num2] = num6;
                        num4++;
                        break;
                    }
                    num6 |= lookupTable[num5++] << ((num7 * 8) & 0x1f);
                    num7--;
                }
            }
            this.bitmapSource = new PdfWICBitmapSource(source, width, height, palette);
            this.palette = factory.CreatePalette();
            this.palette.InitializeCustom(palette, palette.Length);
            Guid guid = WICPixelFormat24bppRGB;
            this.converter.Initialize(this.bitmapSource, guid);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        [SecuritySafeCritical]
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.palette.Dispose();
                this.converter.Dispose();
                this.bitmapSource.Dispose();
            }
        }

        [SecuritySafeCritical]
        public void FillNextScanline(byte[] scanlineData)
        {
            int currentY = this.currentY;
            this.currentY = currentY + 1;
            this.converter.CopyPixels(new WICRect(0, currentY, this.width, 1), this.width * 3, scanlineData);
        }

        ~PdfWICIndexedImageScanlineSource()
        {
            this.Dispose(false);
        }

        public int ComponentsCount =>
            3;

        public bool HasAlpha =>
            false;
    }
}

