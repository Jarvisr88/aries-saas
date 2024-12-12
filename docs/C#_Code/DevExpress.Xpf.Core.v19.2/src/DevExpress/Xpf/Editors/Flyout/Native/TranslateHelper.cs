namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class TranslateHelper
    {
        public static Rect ToScreen(Visual baseElement, Rect rect) => 
            (baseElement != null) ? new Rect(baseElement.PointToScreen(rect.TopLeft), baseElement.PointToScreen(rect.BottomRight)) : rect;

        public static Rect TranslateBounds(UIElement baseElement, UIElement element)
        {
            if ((baseElement == null) || ((element == null) || (!((FrameworkElement) element).IsInVisualTree() || !((FrameworkElement) baseElement).IsInVisualTree())))
            {
                return new Rect();
            }
            Rect rect = new Rect(0.0, 0.0, element.RenderSize.Width, element.RenderSize.Height);
            if (element.RenderTransform != null)
            {
                rect = element.RenderTransform.TransformBounds(rect);
                rect.X = 0.0;
                rect.Y = 0.0;
            }
            return element.TransformToVisual(baseElement).TransformBounds(rect);
        }
    }
}

