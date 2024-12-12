namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public abstract class ScrollInfoBase : IScrollInfo
    {
        protected readonly FrameworkElement scrollablePageView;
        private readonly IPreviewModel model;
        private const double ScrollFactor = 0.03;
        protected const double ScrollWheelFactor = 3.0;
        private ScrollViewer scrollOwner;
        private Thickness pageMargin;
        private double horizontalOffset;
        private double verticalOffset;

        public event EventHandler<EventArgs> HorizontalOffsetChanged;

        public event EventHandler<EventArgs> VerticalOffsetChanged;

        public ScrollInfoBase(FrameworkElement scrollablePageView, IPreviewModel model, Thickness pageMargin)
        {
            Guard.ArgumentNotNull(scrollablePageView, "scrollablePageView");
            this.scrollablePageView = scrollablePageView;
            this.model = model;
            if (model != null)
            {
                this.PageViewWidth = model.PageViewWidth;
                this.PageViewHeight = model.PageViewHeight;
                model.PropertyChanged += new PropertyChangedEventHandler(this.model_PropertyChanged);
            }
            this.pageMargin = pageMargin;
        }

        protected double GetStep(ScrollMode scrollMode) => 
            (this.ScrollablePageViewHeight <= this.ViewportHeight) ? ((this.model.PageCount == 1) ? 0.0 : (this.ScrollableHeight / ((double) (this.model.PageCount - 1)))) : ((scrollMode != ScrollMode.Line) ? ((scrollMode != ScrollMode.MouseWheel) ? this.ViewportHeight : (3.0 * this.ScrollLineHeight)) : this.ScrollLineHeight);

        public abstract double GetTransformX();
        public abstract double GetTransformY();
        protected abstract double GetVerticalScrollOffset(ScrollMode scrollMode, ScrollDirection scrollDirection);
        public void InvalidateScrollInfo()
        {
            if (this.scrollOwner != null)
            {
                this.scrollOwner.InvalidateScrollInfo();
            }
        }

        public bool IsHorizontalScrollDataValid() => 
            (this.model != null) && ((this.model.PageCount > 0) && ((this.ScrollableWidth > 0.0) && (this.ScrollOwner != null)));

        public bool IsVerticalScrollDataValid() => 
            (this.model != null) && ((this.model.PageCount > 0) && ((this.ScrollableHeight > 0.0) && (this.ScrollOwner != null)));

        public void LineDown()
        {
            this.SetVerticalOffset(Math.Min(this.GetVerticalScrollOffset(ScrollMode.Line, ScrollDirection.Down), this.ScrollableHeight));
            this.InvalidateScrollInfo();
        }

        public void LineLeft()
        {
            this.SetHorizontalOffset(Math.Max((double) 0.0, (double) (this.HorizontalOffset - this.ScrollLineWidth)));
            this.InvalidateScrollInfo();
        }

        public void LineRight()
        {
            this.SetHorizontalOffset(Math.Min(this.HorizontalOffset + this.ScrollLineWidth, this.ScrollableWidth));
            this.InvalidateScrollInfo();
        }

        public void LineUp()
        {
            this.SetVerticalOffset(Math.Max(this.GetVerticalScrollOffset(ScrollMode.Line, ScrollDirection.Up), 0.0));
            this.InvalidateScrollInfo();
        }

        public abstract Rect MakeVisible(Visual visual, Rect rectangle);
        protected virtual void model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PageViewWidth")
            {
                this.PageViewWidth = this.model.PageViewWidth;
            }
            else if (e.PropertyName != "PageViewHeight")
            {
                if ((e.PropertyName == "PageCount") || (e.PropertyName == "Zoom"))
                {
                    this.scrollablePageView.InvalidateArrange();
                }
            }
            else
            {
                double scrollableHeight = this.ScrollableHeight;
                this.PageViewHeight = this.model.PageViewHeight;
                if (scrollableHeight != 0.0)
                {
                    double num2 = this.ScrollableHeight;
                    this.verticalOffset = (num2 / scrollableHeight) * this.verticalOffset;
                    this.scrollablePageView.InvalidateArrange();
                }
            }
        }

        public void MouseWheelDown()
        {
            this.SetVerticalOffset(Math.Min(this.GetVerticalScrollOffset(ScrollMode.MouseWheel, ScrollDirection.Down), this.ScrollableHeight));
            this.InvalidateScrollInfo();
        }

        public void MouseWheelLeft()
        {
            this.SetHorizontalOffset(Math.Min((double) 0.0, (double) (this.HorizontalOffset - (3.0 * this.ScrollLineWidth))));
            this.InvalidateScrollInfo();
        }

        public void MouseWheelRight()
        {
            this.SetHorizontalOffset(Math.Min((double) (this.HorizontalOffset + this.ScrollLineWidth), (double) (3.0 * this.ScrollableWidth)));
            this.InvalidateScrollInfo();
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(Math.Max(this.GetVerticalScrollOffset(ScrollMode.MouseWheel, ScrollDirection.Up), 0.0));
            this.InvalidateScrollInfo();
        }

        private void OnHorizontalOffsetChanged()
        {
            if (this.HorizontalOffsetChanged != null)
            {
                this.HorizontalOffsetChanged(this, EventArgs.Empty);
            }
        }

        private void OnVerticalOffsetChanged()
        {
            if (this.VerticalOffsetChanged != null)
            {
                this.VerticalOffsetChanged(this, EventArgs.Empty);
            }
        }

        public void PageDown()
        {
            this.SetVerticalOffset(Math.Min(this.GetVerticalScrollOffset(ScrollMode.Page, ScrollDirection.Down), this.ScrollableHeight));
            this.InvalidateScrollInfo();
        }

        public void PageLeft()
        {
            this.SetHorizontalOffset(Math.Min((double) 0.0, (double) (this.HorizontalOffset - this.ViewportWidth)));
            this.InvalidateScrollInfo();
        }

        public void PageRight()
        {
            this.SetHorizontalOffset(Math.Min(this.HorizontalOffset + this.ViewportWidth, this.ScrollableWidth));
            this.InvalidateScrollInfo();
        }

        public void PageUp()
        {
            this.SetVerticalOffset(Math.Max(this.GetVerticalScrollOffset(ScrollMode.Page, ScrollDirection.Up), 0.0));
            this.InvalidateScrollInfo();
        }

        public abstract void SetCurrentPageIndex();
        public void SetHorizontalOffset(double offset)
        {
            if (this.horizontalOffset != offset)
            {
                this.horizontalOffset = offset;
                this.OnHorizontalOffsetChanged();
                this.scrollablePageView.InvalidateArrange();
            }
        }

        public virtual void SetVerticalOffset(double offset)
        {
            if (this.VerticalOffset != offset)
            {
                this.VerticalOffset = offset;
                this.OnVerticalOffsetChanged();
                this.scrollablePageView.InvalidateArrange();
            }
        }

        protected void UpdateScrollablePageViewLocalVerticalOffset()
        {
            this.ScrollablePageViewLocalVerticalOffset = ((this.model == null) || (!(this.model is IDocumentPreviewModel) || (this.model.PageCount == 0))) ? 0.0 : (this.verticalOffset - ((this.ScrollableHeight / ((double) this.model.PageCount)) * ((IDocumentPreviewModel) this.model).CurrentPageIndex));
        }

        public void ValidateScrollData()
        {
            this.horizontalOffset = Math.Max(0.0, Math.Min(this.horizontalOffset, this.ScrollableWidth));
            this.verticalOffset = Math.Max(0.0, Math.Min(this.verticalOffset, this.ScrollableHeight));
        }

        internal double PageViewWidth { get; private set; }

        internal double PageViewHeight { get; private set; }

        public virtual Point PageWithMarginPosition { get; set; }

        public Thickness PageMargin
        {
            get => 
                this.pageMargin;
            set
            {
                if (this.pageMargin != value)
                {
                    this.pageMargin = value;
                    this.InvalidateScrollInfo();
                }
            }
        }

        protected double ScrollablePageViewWidth =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : ((this.PageViewWidth + this.pageMargin.Left) + this.pageMargin.Right);

        protected abstract double ScrollablePageViewHeight { get; }

        private double ScrollableWidth =>
            Math.Max((double) 0.0, (double) (this.ExtentWidth - this.ViewportWidth));

        private double ScrollableHeight =>
            Math.Max((double) 0.0, (double) (this.ExtentHeight - this.ViewportHeight));

        private double ScrollLineWidth =>
            (this.scrollOwner != null) ? (0.03 * this.ViewportWidth) : 0.0;

        protected double ScrollLineHeight =>
            0.03 * this.ViewportHeight;

        protected double ScrollablePageViewLocalVerticalOffset { get; private set; }

        private double HorizontalRelativityFactor =>
            (this.ScrollablePageViewWidth - this.ViewportWidth) / this.ScrollableWidth;

        private double VerticalRelativityFactor
        {
            get
            {
                double num = this.ScrollableHeight / ((double) this.model.PageCount);
                return ((this.ScrollablePageViewHeight - this.ViewportHeight) / num);
            }
        }

        public bool CanHorizontallyScroll
        {
            get => 
                (this.model != null) && (this.ScrollOwner != null);
            set
            {
            }
        }

        public bool CanVerticallyScroll
        {
            get => 
                (this.model != null) && (this.ScrollOwner != null);
            set
            {
            }
        }

        public abstract double ExtentHeight { get; }

        public virtual double ExtentWidth =>
            ((this.model == null) || (this.model.PageCount == 0)) ? 0.0 : this.ScrollablePageViewWidth;

        public double HorizontalOffset =>
            this.horizontalOffset;

        public ScrollViewer ScrollOwner
        {
            get => 
                this.scrollOwner;
            set => 
                this.scrollOwner = value;
        }

        public double VerticalOffset
        {
            get => 
                this.verticalOffset;
            protected set
            {
                this.verticalOffset = value;
                this.UpdateScrollablePageViewLocalVerticalOffset();
            }
        }

        public double ViewportHeight =>
            this.scrollablePageView.ActualHeight;

        public double ViewportWidth =>
            this.scrollablePageView.ActualWidth;

        protected enum ScrollDirection
        {
            Up,
            Down
        }

        protected enum ScrollMode
        {
            Line,
            MouseWheel,
            Page
        }
    }
}

