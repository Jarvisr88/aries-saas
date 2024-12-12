namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class ScrollablePanel : psvPanel, IScrollInfo
    {
        private const double LineSize = 16.0;
        private const double WheelSize = 48.0;
        private ScrollData scrollData = new ScrollData();

        private static double ComputeScrollOffset(double topView, double bottomView, double topChild, double bottomChild)
        {
            bool flag = (topChild < topView) && (bottomChild < bottomView);
            bool flag2 = (bottomChild > bottomView) && (topChild > topView);
            bool flag3 = (bottomChild - topChild) > (bottomView - topView);
            if (!flag && !flag2)
            {
                return topView;
            }
            if ((flag && !flag3) || (flag2 & flag3))
            {
                return topChild;
            }
            return (bottomChild - (bottomView - topView));
        }

        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + 16.0);
        }

        public void LineLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - 16.0);
        }

        public void LineRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + 16.0);
        }

        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - 16.0);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || ((visual == null) || !base.IsAncestorOf(visual)))
            {
                return Rect.Empty;
            }
            rectangle = visual.TransformToAncestor(this).TransformBounds(rectangle);
            return this.MakeVisibleCore(rectangle);
        }

        private unsafe Rect MakeVisibleCore(Rect rectangle)
        {
            Rect rect = new Rect(this.HorizontalOffset, this.VerticalOffset, this.ViewportWidth, this.ViewportHeight);
            Rect* rectPtr1 = &rectangle;
            rectPtr1.X += rect.X;
            Rect* rectPtr2 = &rectangle;
            rectPtr2.Y += rect.Y;
            rect.X = ComputeScrollOffset(rect.Left, rect.Right, rectangle.Left, rectangle.Right);
            rect.Y = ComputeScrollOffset(rect.Top, rect.Bottom, rectangle.Top, rectangle.Bottom);
            this.SetHorizontalOffset(rect.X);
            this.SetVerticalOffset(rect.Y);
            rectangle.Intersect(rect);
            Rect* rectPtr3 = &rectangle;
            rectPtr3.X -= rect.X;
            Rect* rectPtr4 = &rectangle;
            rectPtr4.Y -= rect.Y;
            return rectangle;
        }

        public void MouseWheelDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + 48.0);
        }

        public void MouseWheelLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - 48.0);
        }

        public void MouseWheelRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + 48.0);
        }

        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - 48.0);
        }

        public void PageDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.ViewportHeight);
        }

        public void PageLeft()
        {
            this.SetHorizontalOffset(this.HorizontalOffset - this.ViewportWidth);
        }

        public void PageRight()
        {
            this.SetHorizontalOffset(this.HorizontalOffset + this.ViewportWidth);
        }

        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.ViewportHeight);
        }

        public void SetHorizontalOffset(double offset)
        {
            offset = Math.Max(0.0, Math.Min(offset, this.ExtentWidth - this.ViewportWidth));
            if (offset != this.scrollData._offset.X)
            {
                this.scrollData._offset.X = offset;
                base.InvalidateArrange();
            }
        }

        public void SetVerticalOffset(double offset)
        {
            offset = Math.Max(0.0, Math.Min(offset, this.ExtentHeight - this.ViewportHeight));
            if (offset != this.scrollData._offset.Y)
            {
                this.scrollData._offset.Y = offset;
                base.InvalidateArrange();
            }
        }

        protected void VerifyScrollData(Size viewport, Size extent)
        {
            if (double.IsInfinity(viewport.Width))
            {
                viewport.Width = extent.Width;
            }
            if (double.IsInfinity(viewport.Height))
            {
                viewport.Height = extent.Height;
            }
            this.scrollData._extent = extent;
            this.scrollData._viewport = viewport;
            this.scrollData._offset.X = Math.Max(0.0, Math.Min(this.scrollData._offset.X, this.ExtentWidth - this.ViewportWidth));
            this.scrollData._offset.Y = Math.Max(0.0, Math.Min(this.scrollData._offset.Y, this.ExtentHeight - this.ViewportHeight));
            if (this.ScrollOwner != null)
            {
                this.ScrollOwner.InvalidateScrollInfo();
            }
        }

        protected Size Viewport =>
            this.scrollData._viewport;

        public bool CanHorizontallyScroll { get; set; }

        public bool CanVerticallyScroll { get; set; }

        public double ExtentHeight =>
            this.scrollData._extent.Height;

        public double ExtentWidth =>
            this.scrollData._extent.Width;

        public double HorizontalOffset =>
            this.scrollData._offset.X;

        public ScrollViewer ScrollOwner { get; set; }

        public double VerticalOffset =>
            this.scrollData._offset.Y;

        public double ViewportHeight =>
            this.scrollData._viewport.Height;

        public double ViewportWidth =>
            this.scrollData._viewport.Width;

        private class ScrollData
        {
            internal Point _offset;
            internal Size _viewport;
            internal Size _extent;
        }
    }
}

