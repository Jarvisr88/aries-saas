namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class DrawingPathInfo : BitmapBasedPathInfo
    {
        public DrawingPathInfo(GraphicsPath graphicsPath, Func<Image> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
        }
    }
}

