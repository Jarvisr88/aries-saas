namespace DevExpress.Office.Utils
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;

    public class HdcZoomModifier : IDisposable
    {
        private Graphics gr;
        private Win32.SIZE oldWindowExtent;

        public HdcZoomModifier(Graphics gr, float zoomFactor)
        {
            this.gr = gr;
            this.ZoomHDC(zoomFactor);
        }

        public void Dispose()
        {
            this.RestoreHDC();
        }

        protected internal virtual void RestoreHDC()
        {
            IntPtr hdc = this.gr.GetHdc();
            try
            {
                Win32.SIZE lpSize = new Win32.SIZE(0, 0);
                Win32.SetWindowExtEx(hdc, this.oldWindowExtent.Width, this.oldWindowExtent.Height, ref lpSize);
            }
            finally
            {
                this.gr.ReleaseHdc();
            }
        }

        protected internal virtual void ZoomHDC(float zoomFactor)
        {
            IntPtr hdc = this.gr.GetHdc();
            try
            {
                Win32.SIZE lpSize = new Win32.SIZE(0, 0);
                Win32.GetWindowExtEx(hdc, out this.oldWindowExtent);
                Win32.SetWindowExtEx(hdc, (int) Math.Round((double) (((float) this.oldWindowExtent.Width) / zoomFactor)), (int) Math.Round((double) (((float) this.oldWindowExtent.Height) / zoomFactor)), ref lpSize);
            }
            finally
            {
                this.gr.ReleaseHdc();
            }
        }
    }
}

