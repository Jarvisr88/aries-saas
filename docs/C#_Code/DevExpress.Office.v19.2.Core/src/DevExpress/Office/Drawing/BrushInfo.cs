namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BrushInfo
    {
        public BrushInfo()
        {
        }

        public BrushInfo(System.Drawing.Brush brush, bool isPermanent)
        {
            this.Brush = brush;
            this.IsPermanent = isPermanent;
        }

        public System.Drawing.Brush Brush { get; private set; }

        public bool IsPermanent { get; private set; }
    }
}

