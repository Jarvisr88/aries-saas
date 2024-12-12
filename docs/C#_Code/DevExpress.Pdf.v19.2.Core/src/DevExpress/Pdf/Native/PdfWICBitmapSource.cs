namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.Common.WIC;
    using DevExpress.DirectX.NativeInterop.CCW;
    using DevExpress.DirectX.NativeInterop.WIC;
    using DevExpress.DirectX.NativeInterop.WIC.CCW;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PdfWICBitmapSource : ComCallableWrapperBase, IWICBitmapSourceCCW, IUnknownCCW, IComCallableWrapper<IWICBitmapSourceCCW>, IDisposable
    {
        private readonly IPdfImageScanlineSource source;
        private readonly Guid pixelFormat;
        private readonly int width;
        private readonly int height;
        private readonly byte[] scanline;
        private readonly int[] palette;
        private readonly int horizontalOffset;

        public PdfWICBitmapSource(IPdfImageScanlineSource source, Guid pixelFormat, int width, int height) : this(source, pixelFormat, width, height, width, 0, 0)
        {
        }

        public PdfWICBitmapSource(IPdfImageScanlineSource source, int width, int height, int[] palette) : this(source, WICPixelFormats.PixelFormat8bppIndexed, width, height)
        {
            this.palette = palette;
        }

        public PdfWICBitmapSource(IPdfImageScanlineSource source, Guid pixelFormat, int width, int height, int sourceWidth, int horizontalOffset, int verticalOffset)
        {
            this.source = source;
            this.width = width;
            this.height = height;
            this.pixelFormat = pixelFormat;
            int componentsCount = source.ComponentsCount;
            this.horizontalOffset = horizontalOffset * componentsCount;
            this.scanline = new byte[sourceWidth * componentsCount];
            for (int i = 0; i < verticalOffset; i++)
            {
                source.FillNextScanline(this.scanline);
            }
        }

        [SecuritySafeCritical]
        public int CopyPalette(IntPtr palette)
        {
            using (WICPalette palette2 = WICPalette.FromNativeObject(palette))
            {
                palette2.InitializeCustom(this.palette, this.palette.Length);
            }
            return 0;
        }

        [SecuritySafeCritical]
        public int CopyPixels(ref WICRect prc, int stride, int bufferSize, IntPtr buffer)
        {
            IntPtr destination = buffer;
            int length = this.width * this.source.ComponentsCount;
            if (bufferSize < (length * prc.Height))
            {
                return -2147024809;
            }
            for (int i = 0; i < prc.Height; i++)
            {
                this.source.FillNextScanline(this.scanline);
                Marshal.Copy(this.scanline, this.horizontalOffset, destination, length);
                destination += stride;
            }
            return 0;
        }

        protected override void FreeResources()
        {
            base.FreeResources();
            this.source.Dispose();
        }

        public int GetPixelFormat(out Guid pixelFormat)
        {
            pixelFormat = this.pixelFormat;
            return 0;
        }

        public int GetResolution(out double dpiX, out double dpiY)
        {
            dpiX = 96.0;
            dpiY = 96.0;
            return 0;
        }

        public int GetSize(out int width, out int height)
        {
            width = this.width;
            height = this.height;
            return 0;
        }

        public IntPtr NativeObject =>
            base.QueryInterface<IWICBitmapSourceCCW>();
    }
}

