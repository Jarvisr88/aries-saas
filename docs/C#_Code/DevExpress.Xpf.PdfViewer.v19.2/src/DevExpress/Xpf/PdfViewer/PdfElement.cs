namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class PdfElement
    {
        protected PdfElement()
        {
        }

        public abstract void Render(DrawingContext dc, Size renderSize);
    }
}

