namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DeviceCaps : IDisposable
    {
        private const int HORZRES = 8;
        private const int VERTRES = 10;
        private const int PHYSICALWIDTH = 110;
        private const int PHYSICALHEIGHT = 0x6f;
        private const int PHYSICALOFFSETX = 0x70;
        private const int PHYSICALOFFSETY = 0x71;
        private Graphics graph;
        private IntPtr hdc;
        private float dpiX;
        private float dpiY;

        private DeviceCaps(Graphics graph)
        {
            this.graph = graph;
            this.dpiX = graph.DpiX;
            this.dpiY = graph.DpiY;
            this.hdc = graph.GetHdc();
        }

        public static int CompareMargins(Margins m1, Margins m2) => 
            ((m1.Top < m2.Top) || ((m1.Right < m2.Right) || ((m1.Bottom < m2.Bottom) || (m1.Left < m2.Left)))) ? -1 : 1;

        public void Dispose()
        {
            this.graph.ReleaseHdc(this.hdc);
        }

        [SuppressUnmanagedCodeSecurity, DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [SecuritySafeCritical]
        private Size GetHVRes() => 
            new Size(GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 8), this.dpiX, 100f), GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 10), this.dpiY, 100f));

        public static Margins GetMinMargins(Graphics graph)
        {
            try
            {
                return GetMinMarginsUsingDeviceCaps(graph);
            }
            catch
            {
                return XtraPageSettingsBase.EmptyMargins;
            }
        }

        private static Margins GetMinMarginsUsingDeviceCaps(Graphics graph)
        {
            using (DeviceCaps caps = new DeviceCaps(graph))
            {
                Size hVRes = caps.GetHVRes();
                Size physSize = caps.GetPhysSize();
                Point physOffset = caps.GetPhysOffset();
                return new Margins(physOffset.X, Math.Max(0, (physSize.Width - physOffset.X) - hVRes.Width), physOffset.Y, Math.Max(0, (physSize.Height - physOffset.Y) - hVRes.Height));
            }
        }

        [SecuritySafeCritical]
        private Point GetPhysOffset() => 
            new Point(GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 0x70), this.dpiX, 100f), GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 0x71), this.dpiY, 100f));

        [SecuritySafeCritical]
        private Size GetPhysSize() => 
            new Size(GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 110), this.dpiX, 100f), GraphicsUnitConverter.Convert(GetDeviceCaps(this.hdc, 0x6f), this.dpiY, 100f));
    }
}

