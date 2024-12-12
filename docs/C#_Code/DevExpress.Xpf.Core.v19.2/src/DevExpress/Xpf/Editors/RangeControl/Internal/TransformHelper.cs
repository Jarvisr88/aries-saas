namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class TransformHelper
    {
        public static Rect GetElementBounds(FrameworkElement element, FrameworkElement relativeTo) => 
            GetTransform(element, relativeTo).TransformBounds(new Rect(new Point(0.0, 0.0), new Size(element.ActualWidth, element.ActualHeight)));

        public static double GetElementCenter(FrameworkElement element, FrameworkElement relativeTo) => 
            GetElementRelativeTopLeft(element, relativeTo).X + (GetElementWidth(element) / 2.0);

        public static Point GetElementRelativeTopLeft(FrameworkElement element, FrameworkElement relativeTo) => 
            GetTransform(element, relativeTo).Transform(new Point(0.0, 0.0));

        public static double GetElementWidth(FrameworkElement element) => 
            (element == null) ? 0.0 : element.ActualWidth;

        private static GeneralTransform GetTransform(FrameworkElement element, FrameworkElement relativeTo) => 
            element.TransformToVisual(relativeTo);
    }
}

