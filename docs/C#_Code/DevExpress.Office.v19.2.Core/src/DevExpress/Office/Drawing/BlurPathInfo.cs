namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class BlurPathInfo : BitmapBasedPathInfo
    {
        public BlurPathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, boundsInLayoutUnits)
        {
        }

        public float BlurRadiusInLayoutUnits { get; set; }

        public bool IsCropped { get; set; }

        public Rectangle BoundsWithoutTransform { get; set; }
    }
}

