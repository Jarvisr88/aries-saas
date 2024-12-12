namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class OuterShadowPathInfo : HitTestSupportedPathInfo
    {
        public OuterShadowPathInfo(GraphicsPath graphicsPath, Func<Bitmap> bitmapRenderer, Func<HitTestInfoCollection> hitTestInfoProvider, Rectangle boundsInLayoutUnits) : base(graphicsPath, bitmapRenderer, hitTestInfoProvider, boundsInLayoutUnits)
        {
        }

        public int BlurRadius { get; set; }
    }
}

