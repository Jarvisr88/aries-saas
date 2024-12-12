namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public class ImageGraphics : GdiGraphics
    {
        private Image img;

        public ImageGraphics(Image img, PrintingSystemBase ps);
        protected internal ImageGraphics(Image img, PrintingSystemBase ps, bool enableBufferedGraphics);
        public override void Dispose();
        public override void DrawImage(Image image, Point position);
        public override void DrawImage(Image image, RectangleF bounds);
        public static Graphics GetActualGraphics(Image img, PrintingSystemBase ps, bool enableBufferedGraphics);

        internal Image Img { get; }
    }
}

