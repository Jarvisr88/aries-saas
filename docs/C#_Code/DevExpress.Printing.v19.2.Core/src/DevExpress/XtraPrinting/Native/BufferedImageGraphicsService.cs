namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public abstract class BufferedImageGraphicsService
    {
        protected BufferedImageGraphicsService();
        public abstract Graphics CreateGraphics(Image img);
        public abstract void OnGraphicsDispose(Image img);
        public abstract void Render(Image image);
    }
}

