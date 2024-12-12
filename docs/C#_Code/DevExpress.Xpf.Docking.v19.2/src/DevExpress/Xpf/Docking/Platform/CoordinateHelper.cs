namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class CoordinateHelper
    {
        public static readonly Point ZeroPoint = new Point(0.0, 0.0);

        public static unsafe Rect GetAvailableAdornerRect(DockLayoutManager container, Rect bounds)
        {
            Point point;
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(container);
            if (topContainerWithAdornerLayer == null)
            {
                return bounds;
            }
            Rect rect = new Rect(topContainerWithAdornerLayer.TranslatePoint(ZeroPoint, container), topContainerWithAdornerLayer.RenderSize);
            DXWindow window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(container) as DXWindow;
            if ((window != null) && window.IsAeroMode)
            {
                Rect clientArea = window.ClientArea;
                point = topContainerWithAdornerLayer.TranslatePoint(clientArea.Location, container);
                rect = new Rect(point, clientArea.Size);
            }
            MatrixTransform transform = topContainerWithAdornerLayer.TransformToVisual(container) as MatrixTransform;
            if ((transform != null) && !transform.Matrix.IsIdentity)
            {
                Matrix matrix = transform.Matrix;
                transform = new MatrixTransform(Math.Abs(matrix.M11), matrix.M12, matrix.M21, Math.Abs(matrix.M22), 0.0, 0.0);
                if (!transform.Matrix.IsIdentity)
                {
                    rect = transform.TransformBounds(rect);
                    rect.Location = point;
                }
            }
            if (((FrameworkElement) topContainerWithAdornerLayer).FlowDirection != container.FlowDirection)
            {
                Rect* rectPtr1 = &rect;
                rectPtr1.X -= rect.Width;
            }
            return rect;
        }

        public static Point GetCenter(Rect rect) => 
            new Point(rect.Left + (rect.Width * 0.5), rect.Top + (rect.Height * 0.5));

        public static Rect GetContainerRect(DockLayoutManager container) => 
            new Rect(new Point(0.0, 0.0), container.RenderSize);

        public static Point PointFromScreen(DockLayoutManager container, UIElement element, Point screenPoint) => 
            container.TranslatePoint(screenPoint, element);

        public static Point PointToScreen(DockLayoutManager container, UIElement element, Point elementPoint) => 
            element.TranslatePoint(elementPoint, container);
    }
}

