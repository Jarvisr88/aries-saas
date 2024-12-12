namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class ThumbnailPageWrapper : PageWrapper
    {
        public ThumbnailPageWrapper(IPage page) : base(page)
        {
        }

        public ThumbnailPageWrapper(IEnumerable<IPage> pages) : base(pages)
        {
        }

        protected override Size CalcRenderSize()
        {
            Size size = base.CalcRenderSize();
            return new Size(size.Width, size.Height + this.LabelHeight);
        }

        protected virtual double LabelHeight =>
            10.0;
    }
}

