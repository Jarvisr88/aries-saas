namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    internal static class GraphicsExtension
    {
        public static Metafile CreateMetafile(this Graphics graphics, Rectangle bounds, MetafileFrameUnit unit, EmfType emfType)
        {
            Metafile metafile;
            IntPtr hdc = graphics.GetHdc();
            try
            {
                metafile = new Metafile(hdc, bounds, unit, emfType);
            }
            finally
            {
                graphics.ReleaseHdc();
            }
            return metafile;
        }
    }
}

