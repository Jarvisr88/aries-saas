namespace DevExpress.Utils.Text
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    public class HdcDpiModifier : IDisposable
    {
        private readonly Graphics gr;
        private readonly Size viewPort;
        private readonly int dpi;
        private Win32Util.SIZE oldWindowExt;
        private Win32Util.SIZE oldViewportExt;
        private int oldMapMode;

        public HdcDpiModifier(Graphics gr, Size viewPort, int dpi)
        {
            this.gr = gr;
            this.viewPort = viewPort;
            this.dpi = dpi;
            this.ApplyHDCDpi();
        }

        [SecuritySafeCritical]
        protected virtual void ApplyHDCDpi()
        {
            int num = (int) Math.Round((double) this.gr.DpiX);
            int num2 = (int) Math.Round((double) this.gr.DpiY);
            IntPtr hdc = this.gr.GetHdc();
            try
            {
                this.oldMapMode = Win32Util.Win32API.GetMapMode(hdc);
                Win32Util.Win32API.SetMapMode(hdc, 8);
                Win32Util.Win32API.SetWindowExtEx(hdc, (this.viewPort.Width * this.Dpi) / num, (this.viewPort.Height * this.Dpi) / num2, ref this.oldWindowExt);
                Win32Util.Win32API.SetViewportExtEx(hdc, this.viewPort.Width, this.viewPort.Height, ref this.oldViewportExt);
            }
            finally
            {
                this.gr.ReleaseHdc(hdc);
            }
        }

        public void Dispose()
        {
            this.RestoreHDCDpi();
        }

        [SecuritySafeCritical]
        protected virtual void RestoreHDCDpi()
        {
            IntPtr hdc = this.gr.GetHdc();
            try
            {
                Win32Util.Win32API.SetViewportExtEx(hdc, this.oldViewportExt.cx, this.oldViewportExt.cy, ref this.oldViewportExt);
                Win32Util.Win32API.SetWindowExtEx(hdc, this.oldWindowExt.cx, this.oldWindowExt.cy, ref this.oldWindowExt);
                Win32Util.Win32API.SetMapMode(hdc, this.oldMapMode);
            }
            finally
            {
                this.gr.ReleaseHdc(hdc);
            }
        }

        protected virtual int Dpi =>
            this.dpi;
    }
}

