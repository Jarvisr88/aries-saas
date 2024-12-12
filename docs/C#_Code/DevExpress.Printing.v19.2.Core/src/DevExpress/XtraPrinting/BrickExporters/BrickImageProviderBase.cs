namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public abstract class BrickImageProviderBase
    {
        protected BrickImageProviderBase();
        public abstract Image CreateContentImage(PrintingSystemBase ps, RectangleF rect, bool patchTransparentBackground, float resolution, ImageFormat imageFormat, Action<Graphics> customization);
        public abstract Image CreateContentMetafileImage(PrintingSystemBase ps, RectangleF rect, bool patchTransparentBackground);
        public abstract Image CreateImage(PrintingSystemBase ps, RectangleF rect, float resolution, ImageFormat imageFormat, Action<Graphics> customization);
        public abstract Image CreateMetafileImage(PrintingSystemBase ps, RectangleF rect);
    }
}

