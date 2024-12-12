namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ReflectionPathInfo : HitTestSupportedPathInfo
    {
        public ReflectionPathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Func<HitTestInfoCollection> hitTestInfoProvider, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, hitTestInfoProvider, boundsInLayoutUnits)
        {
        }
    }
}

