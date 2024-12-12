namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class SoftEdgePathInfo : BitmapBasedPathInfo
    {
        public SoftEdgePathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
        }

        public float BlurRadiusInLayoutUnits { get; set; }
    }
}

