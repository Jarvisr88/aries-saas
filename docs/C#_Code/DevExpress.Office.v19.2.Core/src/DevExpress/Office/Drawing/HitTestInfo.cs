namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class HitTestInfo : IDisposable
    {
        public HitTestInfo(System.Drawing.Drawing2D.GraphicsPath graphicsPath, bool hasFill)
        {
            this.GraphicsPath = graphicsPath;
            this.HasFill = hasFill;
        }

        public void Dispose()
        {
            if (this.GraphicsPath != null)
            {
                this.GraphicsPath.Dispose();
                this.GraphicsPath = null;
            }
        }

        public System.Drawing.Drawing2D.GraphicsPath GraphicsPath { get; private set; }

        public bool HasFill { get; private set; }
    }
}

