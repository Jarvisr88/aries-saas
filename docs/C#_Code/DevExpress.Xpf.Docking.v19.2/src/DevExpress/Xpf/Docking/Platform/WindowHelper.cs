namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media;

    public static class WindowHelper
    {
        internal const int MagicPosition = -32000;

        public static void BindFlowDirection(FrameworkElement window, object source)
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(FrameworkElement.FlowDirectionProperty);
            binding.Source = source;
            window.SetBinding(FrameworkElement.FlowDirectionProperty, binding);
        }

        public static void BindFlowDirectionIfNeeded(FrameworkElement window, object source)
        {
            if (!BindingHelper.IsDataBound(window, FrameworkElement.FlowDirectionProperty))
            {
                BindFlowDirection(window, source);
            }
        }

        public static void BringToFront(DependencyObject window)
        {
            HwndSource hWndSource = PresentationSource.FromDependencyObject(window) as HwndSource;
            if (hWndSource != null)
            {
                SetForegroundWindow(hWndSource);
            }
        }

        public static Rect CheckScreenBounds(DockLayoutManager manager, Rect screenBounds)
        {
            if (manager != null)
            {
                Rect workingArea = GetWorkingArea(manager, screenBounds);
                Rect rect = screenBounds;
                if (manager.FlowDirection == FlowDirection.RightToLeft)
                {
                    rect = new Rect(-rect.X - rect.Width, rect.Y, rect.Width, rect.Height);
                }
                if (!IsZero(workingArea))
                {
                    bool flag = IntersectsWith(workingArea, rect);
                    if (flag)
                    {
                        Size size = new Size();
                        if (GetWorkingArea(manager, new Rect(screenBounds.Location, size)).Top <= (rect.Top + 12.0))
                        {
                            return screenBounds;
                        }
                    }
                    if (!flag)
                    {
                        workingArea = GetWorkingArea(manager, new Rect(0.0, 0.0, 1.0, 1.0));
                    }
                    double x = Math.Min(Math.Max(workingArea.Left, rect.Left), Math.Max(workingArea.Left, workingArea.Right - screenBounds.Width));
                    screenBounds = new Rect(x, Math.Min(Math.Max(screenBounds.Top, workingArea.Top), Math.Max(workingArea.Top, workingArea.Bottom - screenBounds.Height)), screenBounds.Width, screenBounds.Height);
                    if (manager.FlowDirection == FlowDirection.RightToLeft)
                    {
                        screenBounds = new Rect(-screenBounds.X - rect.Width, screenBounds.Y, rect.Width, rect.Height);
                    }
                }
            }
            return screenBounds;
        }

        internal static void DragSize(this Window window, SizingAction sizingAction)
        {
            HwndSource source = PresentationSource.FromDependencyObject(window) as HwndSource;
            if (source != null)
            {
                IntPtr handle = source.Handle;
                NativeHelper.SendMessageSafe(handle, 0x112, (IntPtr) (0xf000 + sizingAction), IntPtr.Zero);
                NativeHelper.SendMessageSafe(handle, 0x202, IntPtr.Zero, IntPtr.Zero);
            }
        }

        internal static Thickness GetActualThreshold(DockLayoutManager container) => 
            IsAeroMode(container) ? new Thickness(3.0, 3.0, 14.0, 14.0) : new Thickness(15.0);

        private static unsafe Rect GetAdornerWorkingArea(DockLayoutManager manager)
        {
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(manager);
            if (topContainerWithAdornerLayer == null)
            {
                return new Rect(CoordinateHelper.ZeroPoint, manager.RenderSize);
            }
            Point location = topContainerWithAdornerLayer.TranslatePoint(CoordinateHelper.ZeroPoint, manager);
            if (FrameworkElement.GetFlowDirection(topContainerWithAdornerLayer) != manager.FlowDirection)
            {
                Point* pointPtr1 = &location;
                pointPtr1.X -= topContainerWithAdornerLayer.RenderSize.Width;
            }
            return new Rect(location, topContainerWithAdornerLayer.RenderSize);
        }

        private static unsafe Rect GetAdornerWorkingArea(DockLayoutManager manager, bool checkAeroMode)
        {
            Point point;
            if (!checkAeroMode)
            {
                return GetAdornerWorkingArea(manager);
            }
            UIElement topContainerWithAdornerLayer = LayoutHelper.GetTopContainerWithAdornerLayer(manager);
            if (topContainerWithAdornerLayer == null)
            {
                return new Rect(CoordinateHelper.ZeroPoint, manager.RenderSize);
            }
            Rect rect = new Rect(topContainerWithAdornerLayer.TranslatePoint(CoordinateHelper.ZeroPoint, manager), topContainerWithAdornerLayer.RenderSize);
            DXWindow window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(manager) as DXWindow;
            if ((window != null) && window.IsAeroMode)
            {
                Rect clientArea = window.ClientArea;
                point = topContainerWithAdornerLayer.TranslatePoint(clientArea.Location, manager);
                rect = new Rect(point, clientArea.Size);
            }
            MatrixTransform transform = topContainerWithAdornerLayer.TransformToVisual(manager) as MatrixTransform;
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
            if (FrameworkElement.GetFlowDirection(topContainerWithAdornerLayer) != manager.FlowDirection)
            {
                Rect* rectPtr1 = &rect;
                rectPtr1.X -= rect.Width;
            }
            return rect;
        }

        private static Matrix GetDisplayMatrix(UIElement element, bool inverse = false)
        {
            PresentationSource source = PresentationSource.FromVisual(element);
            return ((source == null) ? Matrix.Identity : (inverse ? source.CompositionTarget.TransformToDevice : source.CompositionTarget.TransformFromDevice));
        }

        public static Rect GetMaximizeBounds(DockLayoutManager manager, Rect restoreBounds) => 
            ((manager == null) || !ScreenHelper.IsAttachedToPresentationSource(manager)) ? restoreBounds : GetWorkingArea(manager, restoreBounds, true);

        public static Point GetScreenLocation(UIElement element)
        {
            Point point = PointToScreen(element, CoordinateHelper.ZeroPoint);
            if (!IsXBAP)
            {
                point = GetDisplayMatrix(element, false).Transform(point);
            }
            return point;
        }

        private static Rect GetScreenWorkingArea(DockLayoutManager manager, Rect restoreBounds)
        {
            int num = (manager.FlowDirection == FlowDirection.RightToLeft) ? -1 : 1;
            Point point = manager.PointToScreen(CoordinateHelper.ZeroPoint);
            Rect rect = new Rect(GetDisplayMatrix(manager, true).Transform(restoreBounds.Location), restoreBounds.Size);
            if (manager.FlowDirection == FlowDirection.RightToLeft)
            {
                rect.X = -rect.X - rect.Width;
            }
            Rect workingArea = GetWorkingArea(Rect.Offset(rect, point.X, point.Y));
            Point point2 = manager.PointFromScreen(workingArea.Location);
            return new Rect(new Point(num * point2.X, point2.Y), (Size) GetDisplayMatrix(manager, false).Transform((Vector) workingArea.Size));
        }

        [SecuritySafeCritical]
        private static Rect GetWorkingArea(Rect box)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rcWork = new DevExpress.Xpf.Core.NativeMethods.RECT(0, 0, 0, 0);
            DevExpress.Xpf.Core.NativeMethods.RECT rect2 = new DevExpress.Xpf.Core.NativeMethods.RECT((int) box.Left, (int) box.Top, (int) box.Right, (int) box.Bottom);
            IntPtr handle = DevExpress.Xpf.Core.NativeMethods.MonitorFromRect(ref rect2, 2);
            if (handle != IntPtr.Zero)
            {
                DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX info = new DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX();
                DevExpress.Xpf.Core.NativeMethods.GetMonitorInfo(new HandleRef(null, handle), info);
                rcWork = info.rcWork;
            }
            return new Rect((double) rcWork.left, (double) rcWork.top, (double) (rcWork.right - rcWork.left), (double) (rcWork.bottom - rcWork.top));
        }

        private static Rect GetWorkingArea(DockLayoutManager manager, Rect screenBounds) => 
            (manager.GetRealFloatingMode() == DevExpress.Xpf.Core.FloatingMode.Adorner) ? GetAdornerWorkingArea(manager) : GetScreenWorkingArea(manager, screenBounds);

        private static Rect GetWorkingArea(DockLayoutManager manager, Rect screenBounds, bool checkAeroMode) => 
            (!checkAeroMode || (manager.GetRealFloatingMode() != DevExpress.Xpf.Core.FloatingMode.Adorner)) ? GetWorkingArea(manager, screenBounds) : GetAdornerWorkingArea(manager, checkAeroMode);

        private static bool IntersectsWith(Rect r, Rect rect) => 
            !r.IsEmpty && (!rect.IsEmpty && ((rect.Left <= r.Right) && ((rect.Right >= r.Left) && ((rect.Top <= r.Bottom) && (rect.Bottom >= r.Top)))));

        public static bool IsAeroMode(DependencyObject dObj)
        {
            DXWindow window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(dObj) as DXWindow;
            return ((window != null) && window.IsAeroMode);
        }

        private static bool IsZero(Rect r) => 
            r == new Rect(0.0, 0.0, 0.0, 0.0);

        private static Point PointToScreen(UIElement element, Point p) => 
            IsXBAP ? element.MapPoint(p, element.GetRootVisual()) : element.PointToScreen(p);

        private static void SetForegroundWindow(HwndSource hWndSource)
        {
            if (hWndSource != null)
            {
                NativeHelper.SetForegroundWindowSafe(new HandleRef(null, hWndSource.Handle));
            }
        }

        public static bool IsXBAP =>
            BrowserInteropHelper.IsBrowserHosted;
    }
}

