namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXArranger : Decorator
    {
        private UIElement topElement;
        private Point lastOffset;

        public DXArranger()
        {
            base.LayoutUpdated += new EventHandler(this.DXArranger_LayoutUpdated);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (!base.SnapsToDevicePixels)
            {
                return base.ArrangeOverride(arrangeSize);
            }
            this.lastOffset = this.GetOffset();
            this.Child.Arrange(new Rect(this.lastOffset.X, this.lastOffset.Y, Floor(arrangeSize.Width, 4), Floor(arrangeSize.Height, 4)));
            return arrangeSize;
        }

        private double CalculateOffset(double value)
        {
            double num = value - Floor(value, 4);
            return ((num >= 0.5) ? (1.0 - num) : -num);
        }

        public static double Ceiling(double value) => 
            Math.Ceiling((double) (value * ScreenHelper.ScaleX)) / ScreenHelper.ScaleX;

        public static Size Ceiling(Size value) => 
            new Size(Ceiling(value.Width), Ceiling(value.Height));

        private void DXArranger_LayoutUpdated(object sender, EventArgs e)
        {
            if (base.SnapsToDevicePixels && (base.IsVisible && (this.lastOffset != this.GetOffset())))
            {
                base.InvalidateArrange();
            }
        }

        public static double Floor(double value, int digits = 4) => 
            Math.Floor(Math.Round((double) (value * ScreenHelper.ScaleX), digits)) / ScreenHelper.ScaleX;

        private Point GetOffset()
        {
            if (!base.IsVisible || ((this.topElement == null) || !LayoutHelper.IsChildElement(this.topElement, this)))
            {
                return new Point();
            }
            try
            {
                Rect relativeElementRect = LayoutHelper.GetRelativeElementRect(this, this.topElement);
                return new Point(this.CalculateOffset(relativeElementRect.Left), this.CalculateOffset(relativeElementRect.Top));
            }
            catch (InvalidOperationException)
            {
                return new Point();
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (!base.SnapsToDevicePixels)
            {
                return base.MeasureOverride(constraint);
            }
            base.MeasureOverride(new Size(Floor(constraint.Width, 4), Floor(constraint.Height, 4)));
            return new Size(Ceiling(this.Child.DesiredSize.Width), Ceiling(this.Child.DesiredSize.Height));
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, UIElement.IsVisibleProperty))
            {
                this.topElement = LayoutHelper.GetTopLevelVisual(this);
            }
        }

        public static double Round(double value) => 
            Math.Round((double) (value * ScreenHelper.ScaleX)) / ScreenHelper.ScaleX;

        public static Size Round(Size value) => 
            new Size(Round(value.Width), Round(value.Height));

        public static double Round(double value, int digits) => 
            Math.Round((double) (value * ScreenHelper.ScaleX), digits) / ScreenHelper.ScaleX;
    }
}

