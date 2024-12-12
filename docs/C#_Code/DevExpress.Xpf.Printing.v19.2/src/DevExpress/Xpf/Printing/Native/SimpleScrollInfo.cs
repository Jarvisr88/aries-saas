namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class SimpleScrollInfo : ScrollInfoBase
    {
        private IPreviewModel model;

        public SimpleScrollInfo(FrameworkElement scrollablePageView, IPreviewModel model, Thickness pageMargin) : base(scrollablePageView, model, pageMargin)
        {
            this.model = model;
        }

        public override double GetTransformX() => 
            (base.ScrollablePageViewWidth > base.ViewportWidth) ? -base.HorizontalOffset : 0.0;

        public override double GetTransformY() => 
            (this.ScrollablePageViewHeight > base.ViewportHeight) ? -base.VerticalOffset : 0.0;

        protected override double GetVerticalScrollOffset(ScrollInfoBase.ScrollMode scrollMode, ScrollInfoBase.ScrollDirection scrollDirection)
        {
            double step = base.GetStep(scrollMode);
            return ((scrollDirection != ScrollInfoBase.ScrollDirection.Down) ? (base.VerticalOffset - step) : (base.VerticalOffset + step));
        }

        private bool IsVisible(Rect rectangle)
        {
            if ((this.ScrollablePageViewHeight < base.ViewportHeight) && (base.ScrollablePageViewWidth < base.ViewportWidth))
            {
                return true;
            }
            Rect rect = new Rect(base.HorizontalOffset, base.VerticalOffset, base.ViewportWidth, base.ViewportHeight);
            return (rect.Contains(rectangle.TopLeft()) && rect.Contains(rectangle.BottomRight()));
        }

        public override Rect MakeVisible(Visual visual, Rect rectangle) => 
            this.MakeVisibleCore((UIElement) visual, rectangle);

        private Rect MakeVisibleCore(UIElement element, Rect rectangle)
        {
            if ((this.model.PageContent == null) || !this.model.PageContent.IsInVisualTree())
            {
                return Rect.Empty;
            }
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(element, this.model.PageContent);
            double num = this.model.Zoom / 100.0;
            relativeElementRect = new Rect(relativeElementRect.X * num, relativeElementRect.Y * num, relativeElementRect.Width * num, relativeElementRect.Height * num);
            if (!this.IsVisible(relativeElementRect))
            {
                if (this.ScrollablePageViewHeight > base.ViewportHeight)
                {
                    double offset = 0.0;
                    offset = (relativeElementRect.Height >= base.ViewportHeight) ? relativeElementRect.Top : Math.Max((double) 0.0, (double) (((relativeElementRect.Top + (0.5 * relativeElementRect.Height)) - (0.5 * base.ViewportHeight)) + base.PageMargin.Top));
                    this.SetVerticalOffset(offset);
                }
                if (base.ScrollablePageViewWidth > base.ViewportWidth)
                {
                    double offset = 0.0;
                    offset = (relativeElementRect.Width >= base.ViewportWidth) ? relativeElementRect.Left : Math.Max((double) 0.0, (double) (((relativeElementRect.Left + (0.5 * relativeElementRect.Width)) - (0.5 * base.ViewportWidth)) + base.PageMargin.Left));
                    base.SetHorizontalOffset(offset);
                }
            }
            return relativeElementRect;
        }

        public override void SetCurrentPageIndex()
        {
        }

        protected override double ScrollablePageViewHeight =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : ((base.PageViewHeight + base.PageMargin.Top) + base.PageMargin.Bottom);

        public override double ExtentHeight =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : Math.Max(this.ScrollablePageViewHeight, base.ViewportHeight);
    }
}

