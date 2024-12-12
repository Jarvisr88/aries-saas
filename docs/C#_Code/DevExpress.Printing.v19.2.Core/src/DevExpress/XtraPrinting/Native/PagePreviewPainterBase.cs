namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class PagePreviewPainterBase : IDisposable
    {
        protected const int padding = 10;
        protected const int shadowWidth = 3;
        protected Brush grayBrush;
        protected Brush whiteBrush;
        protected Brush blackBrush;
        protected Pen borderPen;

        public PagePreviewPainterBase();
        public virtual void Dispose();
        protected virtual void DrawImage(Graphics gr, int w, int h);
        protected virtual void DrawPage(Graphics gr, int w, int h);
        public void GenerateImage(PictureBox pic);
    }
}

