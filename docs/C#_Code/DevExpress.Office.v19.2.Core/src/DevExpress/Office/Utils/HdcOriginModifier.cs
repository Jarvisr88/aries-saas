namespace DevExpress.Office.Utils
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;

    public class HdcOriginModifier : IDisposable
    {
        private Graphics gr;
        private Win32.POINT oldOrigin;

        public HdcOriginModifier(Graphics gr, Point newOrigin, float zoomFactor) : this(gr, newOrigin, zoomFactor, Mode.Replace)
        {
        }

        public HdcOriginModifier(Graphics gr, Point newOrigin, float zoomFactor, Mode mode)
        {
            this.gr = gr;
            this.SetHDCOrigin(newOrigin, zoomFactor, mode);
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
                Win32.POINT lpPoint = new Win32.POINT();
                Win32.SetWindowOrgEx(hdc, this.oldOrigin.X, this.oldOrigin.Y, ref lpPoint);
            }
            finally
            {
                this.gr.ReleaseHdc();
            }
        }

        protected internal virtual void SetHDCOrigin(Point newOrigin, float zoomFactor, Mode mode)
        {
            IntPtr hdc = this.gr.GetHdc();
            try
            {
                Win32.POINT lpPoint = new Win32.POINT();
                Win32.GetWindowOrgEx(hdc, out this.oldOrigin);
                int x = -((int) Math.Round((double) (((float) newOrigin.X) / zoomFactor)));
                int y = -((int) Math.Round((double) (((float) newOrigin.Y) / zoomFactor)));
                if (mode == Mode.Combine)
                {
                    x += (int) Math.Round((double) (((float) this.oldOrigin.X) / zoomFactor));
                    y += (int) Math.Round((double) (((float) this.oldOrigin.Y) / zoomFactor));
                }
                Win32.SetWindowOrgEx(hdc, x, y, ref lpPoint);
            }
            finally
            {
                this.gr.ReleaseHdc();
            }
        }

        public enum Mode
        {
            Replace,
            Combine
        }
    }
}

