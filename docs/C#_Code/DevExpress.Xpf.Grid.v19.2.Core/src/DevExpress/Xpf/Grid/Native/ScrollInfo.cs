namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public abstract class ScrollInfo : DependencyObject, IScrollInfo
    {
        private IScrollInfoOwner owner;
        private ScrollInfoBase verticalScrollInfo;
        private ScrollInfoBase horizontalScrollInfo;
        private ScrollViewer scrollViewer;
        private bool canHorizontallyScroll = true;

        public ScrollInfo(IScrollInfoOwner owner)
        {
            this.owner = owner;
        }

        public virtual void ClearScrollInfo()
        {
            this.horizontalScrollInfo = null;
            this.verticalScrollInfo = null;
        }

        protected abstract ScrollInfoBase CreateHorizontalScrollInfo();
        protected abstract ScrollInfoBase CreateVerticalScrollInfo();
        public virtual void OnScrollInfoChanged()
        {
            if (this.scrollViewer != null)
            {
                this.scrollViewer.InvalidateScrollInfo();
                this.Owner.IsTouchScrollBarsMode = ScrollBarExtensions.GetScrollBarMode(this.scrollViewer) == ScrollBarMode.TouchOverlap;
            }
        }

        public void SetHorizontalOffsetForce(double value)
        {
            this.HorizontalScrollInfo.SetOffsetForce(value, false);
        }

        public void SetVerticalOffsetForce(double value)
        {
            this.VerticalScrollInfo.SetOffsetForce(value, false);
        }

        void IScrollInfo.LineDown()
        {
            this.VerticalScrollInfo.LineDown();
        }

        void IScrollInfo.LineLeft()
        {
            this.HorizontalScrollInfo.LineUp();
        }

        void IScrollInfo.LineRight()
        {
            this.HorizontalScrollInfo.LineDown();
        }

        void IScrollInfo.LineUp()
        {
            this.VerticalScrollInfo.LineUp();
        }

        unsafe Rect IScrollInfo.MakeVisible(Visual visual, Rect rectangle)
        {
            Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(visual as UIElement, this.Owner.ScrollContentPresenter);
            Rect* rectPtr1 = &relativeElementRect;
            rectPtr1.Y -= this.owner.ScrollInsideActiveEditorIfNeeded(visual, rectangle);
            if (relativeElementRect.Bottom > this.Owner.ScrollContentPresenter.ActualHeight)
            {
                Rect* rectPtr2 = &relativeElementRect;
                rectPtr2.Y -= relativeElementRect.Bottom - this.Owner.ScrollContentPresenter.ActualHeight;
            }
            if (relativeElementRect.Top < 0.0)
            {
                Rect* rectPtr3 = &relativeElementRect;
                rectPtr3.Y -= relativeElementRect.Top;
            }
            if (relativeElementRect.Right > this.Owner.ScrollContentPresenter.ActualWidth)
            {
                Rect* rectPtr4 = &relativeElementRect;
                rectPtr4.Width -= Math.Min(relativeElementRect.Width, relativeElementRect.Right - this.Owner.ScrollContentPresenter.ActualWidth);
            }
            if (relativeElementRect.Left < 0.0)
            {
                Rect* rectPtr5 = &relativeElementRect;
                rectPtr5.X -= relativeElementRect.Left;
            }
            FrameworkElement element = FocusRectPresenter.FindScrollHost(visual, FocusRectPresenter.IsHorizontalScrollHostProperty);
            if (element != null)
            {
                Rect rect2 = LayoutHelper.GetRelativeElementRect(element, this.owner.ScrollContentPresenter);
                Rect* rectPtr6 = &relativeElementRect;
                rectPtr6.X += rect2.Left;
            }
            return relativeElementRect;
        }

        void IScrollInfo.MouseWheelDown()
        {
            this.VerticalScrollInfo.MouseWheelDown();
        }

        void IScrollInfo.MouseWheelLeft()
        {
            this.HorizontalScrollInfo.MouseWheelUp();
        }

        void IScrollInfo.MouseWheelRight()
        {
            this.HorizontalScrollInfo.MouseWheelDown();
        }

        void IScrollInfo.MouseWheelUp()
        {
            this.VerticalScrollInfo.MouseWheelUp();
        }

        void IScrollInfo.PageDown()
        {
            this.VerticalScrollInfo.PageDown();
        }

        void IScrollInfo.PageLeft()
        {
            this.HorizontalScrollInfo.PageUp();
        }

        void IScrollInfo.PageRight()
        {
            this.HorizontalScrollInfo.PageDown();
        }

        void IScrollInfo.PageUp()
        {
            this.VerticalScrollInfo.PageUp();
        }

        void IScrollInfo.SetHorizontalOffset(double offset)
        {
            this.HorizontalScrollInfo.SetOffset(offset);
        }

        void IScrollInfo.SetVerticalOffset(double offset)
        {
            this.VerticalScrollInfo.SetOffset(offset);
        }

        public void UpdateIsHorizontalScrollBarVisible()
        {
            if (this.scrollViewer != null)
            {
                this.Owner.IsHorizontalScrollBarVisible = this.scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible;
            }
        }

        protected IScrollInfoOwner Owner =>
            this.owner;

        public abstract ScrollByItemInfo DefineSizeScrollInfo { get; }

        public abstract ScrollByPixelInfo SecondarySizeScrollInfo { get; }

        public virtual ScrollInfoBase HorizontalScrollInfo
        {
            get
            {
                this.horizontalScrollInfo ??= this.CreateHorizontalScrollInfo();
                return this.horizontalScrollInfo;
            }
        }

        public virtual ScrollInfoBase VerticalScrollInfo
        {
            get
            {
                this.verticalScrollInfo ??= this.CreateVerticalScrollInfo();
                return this.verticalScrollInfo;
            }
        }

        bool IScrollInfo.CanHorizontallyScroll
        {
            get => 
                this.canHorizontallyScroll;
            set => 
                this.canHorizontallyScroll = value;
        }

        bool IScrollInfo.CanVerticallyScroll
        {
            get => 
                true;
            set
            {
            }
        }

        double IScrollInfo.ExtentHeight =>
            this.VerticalScrollInfo.Extent;

        double IScrollInfo.ExtentWidth =>
            this.HorizontalScrollInfo.Extent;

        double IScrollInfo.VerticalOffset =>
            this.VerticalScrollInfo.Offset;

        double IScrollInfo.HorizontalOffset =>
            this.HorizontalScrollInfo.Offset;

        double IScrollInfo.ViewportHeight =>
            this.VerticalScrollInfo.Viewport;

        double IScrollInfo.ViewportWidth =>
            this.HorizontalScrollInfo.Viewport;

        ScrollViewer IScrollInfo.ScrollOwner
        {
            get => 
                this.scrollViewer;
            set => 
                this.scrollViewer = value;
        }
    }
}

