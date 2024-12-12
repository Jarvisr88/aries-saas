namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    internal class ScrollInfo : ScrollInfoBase
    {
        private const double epsilon = 1E-12;
        private readonly Thickness scrollingPlay;
        private bool changingCurrentPageIndex;
        private readonly IDocumentPreviewModel model;

        public ScrollInfo(FrameworkElement scrollablePageView, IDocumentPreviewModel model, Thickness pageMargin) : base(scrollablePageView, model, pageMargin)
        {
            this.scrollingPlay = new Thickness(0.0, 100.0, 0.0, 100.0);
            this.model = model;
        }

        private double GetLogicalHorizontalOffset(double offset) => 
            offset * this.HorizontalRelativityFactor;

        private double GetLogicalVerticalOffset(double offset)
        {
            double num = this.ScrollableHeight / ((double) this.model.PageCount);
            return Math.Min(Math.Max((double) 0.0, (double) (((offset - (num * this.GetVisiblePageIndex(offset))) * this.VerticalRelativityFactor) - this.ScrollingPlay.Top)), ((base.PageViewHeight + base.PageMargin.Top) + base.PageMargin.Bottom) - base.ViewportHeight);
        }

        private double GetPageOffset(int pageIndex) => 
            (this.ScrollableHeight / ((double) this.model.PageCount)) * pageIndex;

        private int GetRealPageIndex(double offset)
        {
            double num = this.ScrollableHeight / ((double) this.model.PageCount);
            return (int) Math.Floor((double) ((offset / num) + 1E-12));
        }

        public override double GetTransformX() => 
            (base.ScrollablePageViewWidth > base.ViewportWidth) ? -this.GetLogicalHorizontalOffset(base.HorizontalOffset) : 0.0;

        public override double GetTransformY() => 
            (((this.ScrollablePageViewHeight - this.ScrollingPlay.Top) - this.ScrollingPlay.Bottom) > base.ViewportHeight) ? -this.GetLogicalVerticalOffset(base.VerticalOffset) : 0.0;

        protected override double GetVerticalScrollOffset(ScrollInfoBase.ScrollMode scrollMode, ScrollInfoBase.ScrollDirection scrollDirection)
        {
            double step = base.GetStep(scrollMode);
            if (this.ScrollablePageViewHeight <= base.ViewportHeight)
            {
                return ((scrollDirection == ScrollInfoBase.ScrollDirection.Down) ? (base.VerticalOffset + step) : (base.VerticalOffset - step));
            }
            int realPageIndex = this.GetRealPageIndex(base.VerticalOffset);
            double num3 = this.ScrollableHeight / ((double) this.model.PageCount);
            double num4 = base.VerticalOffset - (num3 * realPageIndex);
            return ((scrollDirection != ScrollInfoBase.ScrollDirection.Down) ? ((base.VerticalOffset == (realPageIndex * num3)) ? ((realPageIndex * num3) - 1.0) : (((num4 - step) < 0.0) ? (realPageIndex * num3) : (base.VerticalOffset - step))) : ((base.VerticalOffset != (((realPageIndex + 1) * num3) - 1.0)) ? (((num4 + step) <= num3) ? (base.VerticalOffset + step) : (((realPageIndex + 1) * num3) - 1.0)) : ((realPageIndex + 1) * num3)));
        }

        private int GetVisiblePageIndex(double offset) => 
            Math.Min(this.GetRealPageIndex(offset), this.model.PageCount - 1);

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
            if (this.ScrollablePageViewHeight > base.ViewportHeight)
            {
                if (relativeElementRect.Height >= base.ViewportHeight)
                {
                    this.SetVerticalOffset(relativeElementRect.Top + ((this.ScrollableHeight / ((double) this.model.PageCount)) * this.model.CurrentPageIndex));
                }
                else
                {
                    double num3 = Math.Min(Math.Max((double) 0.0, (double) (((((relativeElementRect.Top + (0.5 * relativeElementRect.Height)) - (0.5 * base.ViewportHeight)) + this.ScrollingPlay.Top) + base.PageMargin.Top) / this.VerticalRelativityFactor)), (this.ScrollableHeight / ((double) this.model.PageCount)) - 1.0);
                    this.SetVerticalOffset(num3 + ((this.ScrollableHeight / ((double) this.model.PageCount)) * this.model.CurrentPageIndex));
                }
            }
            if (base.ScrollablePageViewWidth > base.ViewportWidth)
            {
                if (relativeElementRect.Width >= base.ViewportWidth)
                {
                    base.SetHorizontalOffset(relativeElementRect.Left);
                }
                else
                {
                    double offset = (((relativeElementRect.Left + (0.5 * relativeElementRect.Width)) - (0.5 * base.ViewportWidth)) + base.PageMargin.Left) / this.HorizontalRelativityFactor;
                    base.SetHorizontalOffset(offset);
                }
            }
            return relativeElementRect;
        }

        protected override void model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.model_PropertyChanged(sender, e);
            if ((e.PropertyName == "PageCount") && (this.model.PageCount == 0))
            {
                base.UpdateScrollablePageViewLocalVerticalOffset();
            }
            else if ((e.PropertyName == "CurrentPageIndex") && (!this.changingCurrentPageIndex && ((this.model.CurrentPageIndex >= 0) && (this.model.PageCount > 0))))
            {
                base.VerticalOffset = this.GetPageOffset(this.model.CurrentPageIndex);
                base.scrollablePageView.InvalidateArrange();
            }
        }

        public override void SetCurrentPageIndex()
        {
            try
            {
                this.changingCurrentPageIndex = true;
                base.VerticalOffset = this.GetPageOffset(this.model.CurrentPageIndex) + base.ScrollablePageViewLocalVerticalOffset;
                if (base.VerticalOffset < 0.0)
                {
                    base.VerticalOffset = 0.0;
                }
                this.model.CurrentPageIndex = this.GetVisiblePageIndex(base.VerticalOffset);
                base.UpdateScrollablePageViewLocalVerticalOffset();
            }
            finally
            {
                this.changingCurrentPageIndex = false;
            }
        }

        public override void SetVerticalOffset(double offset)
        {
            this.SetVerticalOffset((!double.IsPositiveInfinity(offset) || (this.model.PageCount <= 0)) ? (double.IsNegativeInfinity(offset) ? 0.0 : offset) : this.GetPageOffset(this.model.PageCount - 1));
        }

        private Thickness ScrollingPlay
        {
            get
            {
                if (base.ViewportHeight < this.PageViewHeightWithMargins)
                {
                    return this.scrollingPlay;
                }
                return new Thickness();
            }
        }

        protected override double ScrollablePageViewHeight =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : ((this.PageViewHeightWithMargins + this.ScrollingPlay.Top) + this.ScrollingPlay.Bottom);

        private double PageViewHeightWithMargins =>
            (base.PageViewHeight + base.PageMargin.Top) + base.PageMargin.Bottom;

        private double ScrollableWidth =>
            Math.Max((double) 0.0, (double) (this.ExtentWidth - base.ViewportWidth));

        private double ScrollableHeight =>
            Math.Max((double) 0.0, (double) (this.ExtentHeight - base.ViewportHeight));

        private double HorizontalRelativityFactor =>
            (base.ScrollablePageViewWidth - base.ViewportWidth) / this.ScrollableWidth;

        private double VerticalRelativityFactor
        {
            get
            {
                double num = this.ScrollableHeight / ((double) this.model.PageCount);
                return ((this.ScrollablePageViewHeight - base.ViewportHeight) / num);
            }
        }

        public override double ExtentHeight =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : (Math.Max(this.ScrollablePageViewHeight, base.ViewportHeight) * this.model.PageCount);
    }
}

