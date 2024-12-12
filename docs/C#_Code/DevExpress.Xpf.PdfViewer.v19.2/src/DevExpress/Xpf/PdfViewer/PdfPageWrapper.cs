namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfPageWrapper : PageWrapper
    {
        public PdfPageWrapper(PdfPageViewModel page) : base(page)
        {
        }

        public PdfPageWrapper(IEnumerable<PdfPageViewModel> pages) : base(pages)
        {
        }

        protected internal override double CalcFirstPageLeftOffset() => 
            !base.IsCoverPage ? (!base.IsColumnMode ? base.CalcFirstPageLeftOffset() : ((this.PageWrapperTwoPageCenter - (base.IsVertical ? base.Pages.First<IPage>().PageSize.Width : base.Pages.First<IPage>().PageSize.Height)) * base.ZoomFactor)) : ((this.PageWrapperTwoPageCenter * base.ZoomFactor) + base.HorizontalPageSpacing);

        protected override Size CalcPageSize()
        {
            Size size = base.CalcPageSize();
            return new Size(this.PageWrapperWidth.IsZero() ? size.Width : this.PageWrapperWidth, size.Height);
        }

        protected override double CalcPageTopOffset(IPage page) => 
            ((base.PageSize.Height - (base.IsVertical ? page.PageSize.Height : page.PageSize.Width)) / 2.0) * base.ZoomFactor;

        protected override unsafe Size CalcRenderSize()
        {
            Size size = base.CalcRenderSize();
            if (base.IsColumnMode && (base.Pages.Count<IPage>() == 1))
            {
                Size* sizePtr1 = &size;
                sizePtr1.Width += base.HorizontalPageSpacing;
            }
            return (!size.Width.LessThan(((this.PageWrapperWidth * base.ZoomFactor) + (base.IsCoverPage ? (this.PageWrapperMargin / 2.0) : this.PageWrapperMargin))) ? size : new Size((this.PageWrapperWidth * base.ZoomFactor) + (base.IsCoverPage ? (this.PageWrapperMargin / 2.0) : this.PageWrapperMargin), size.Height));
        }

        public PdfPoint CalcTopLeftAngle(double angle, int pageIndex) => 
            ((PdfPageViewModel) base.Pages.Single<IPage>(x => (x.PageIndex == pageIndex))).CalcTopLeftPoint(angle);

        internal void InitializeInternal()
        {
            this.Initialize();
        }

        public DevExpress.Xpf.PdfViewer.Internal.RenderItem RenderItem { get; internal set; }

        internal double PageWrapperWidth { get; set; }

        internal double PageWrapperTwoPageCenter { get; set; }

        internal double PageWrapperMargin { get; set; }
    }
}

