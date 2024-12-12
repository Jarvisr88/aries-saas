namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class BrickPaint : BrickPaintBase
    {
        public BrickPaint(GdiHashtable gdi);
        protected override void DrawBorder(IGraphics gr, RectangleF rect, Brush brush, BorderSide sides);
        public override RectangleF GetClientRect(RectangleF rect, bool rtlLayout = false);
    }
}

