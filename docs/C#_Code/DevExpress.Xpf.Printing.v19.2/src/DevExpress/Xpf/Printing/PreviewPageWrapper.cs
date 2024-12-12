namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    internal class PreviewPageWrapper : PageWrapper
    {
        public PreviewPageWrapper(IPage page) : base(page)
        {
        }

        public PreviewPageWrapper(IEnumerable<IPage> pages) : base(pages)
        {
        }

        protected override double CalcPageTopOffset(IPage page) => 
            ((base.PageSize.Height - (base.IsVertical ? page.PageSize.Height : page.PageSize.Width)) / 2.0) * base.ZoomFactor;

        internal override Rect GetWrapModePageRect(IPage page) => 
            base.GetPageRectCore(page);
    }
}

