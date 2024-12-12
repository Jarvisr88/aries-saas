namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Camera;
    using System;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows.Interop;
    using System.Windows.Media;

    [SecuritySafeCritical]
    public class CameraDevice : CameraDeviceBase
    {
        public CameraDevice(CameraDeviceInfo deviceInfo) : base(deviceInfo)
        {
        }

        protected override void CreateFrameCore(IntPtr section, int width, int height, IntPtr stride)
        {
            System.Windows.Media.PixelFormat wpfPixelFormat = this.GetWpfPixelFormat(base.CurrentPixelFormat);
            this.BitmapSource = Imaging.CreateBitmapSourceFromMemorySection(section, width, height, wpfPixelFormat, (width * wpfPixelFormat.BitsPerPixel) / 8, 0) as InteropBitmap;
        }

        protected override void FreeFrame()
        {
            this.BitmapSource = null;
        }

        private System.Windows.Media.PixelFormat GetWpfPixelFormat(System.Drawing.Imaging.PixelFormat format) => 
            (format == System.Drawing.Imaging.PixelFormat.Format24bppRgb) ? PixelFormats.Bgr24 : PixelFormats.Bgr32;

        public InteropBitmap BitmapSource { get; private set; }
    }
}

