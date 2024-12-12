namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    internal class FakeScrollInfo : IScrollInfo
    {
        public void LineDown()
        {
        }

        public void LineLeft()
        {
        }

        public void LineRight()
        {
        }

        public void LineUp()
        {
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) => 
            Rect.Empty;

        public void MouseWheelDown()
        {
        }

        public void MouseWheelLeft()
        {
        }

        public void MouseWheelRight()
        {
        }

        public void MouseWheelUp()
        {
        }

        public void PageDown()
        {
        }

        public void PageLeft()
        {
        }

        public void PageRight()
        {
        }

        public void PageUp()
        {
        }

        public void SetHorizontalOffset(double offset)
        {
        }

        public void SetVerticalOffset(double offset)
        {
        }

        public bool CanHorizontallyScroll { get; set; }

        public bool CanVerticallyScroll { get; set; }

        public double ExtentHeight =>
            0.0;

        public double ExtentWidth =>
            0.0;

        public double HorizontalOffset =>
            0.0;

        public ScrollViewer ScrollOwner { get; set; }

        public double VerticalOffset =>
            0.0;

        public double ViewportHeight =>
            0.0;

        public double ViewportWidth =>
            0.0;
    }
}

