namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    internal class Win32DragService : DispatcherObject
    {
        private DragOperation dragOperation;
        private bool tryToSuspendDragging;
        private IDraggableWindow windowToDrag;

        internal void CancelDragging()
        {
            this.windowToDrag = null;
        }

        private bool CanDrag(IDraggableWindow window) => 
            window.Manager.ViewAdapter.DragService.DragItem != null;

        private bool CanSuspendDragging(IDraggableWindow window)
        {
            IDockLayoutElement viewElement = window.Manager.GetViewElement(window.FloatGroup.GetUIElement<FloatPanePresenter>()) as IDockLayoutElement;
            if (viewElement == null)
            {
                return false;
            }
            BaseLayoutItem item = viewElement.CheckDragElement().Item;
            return window.Manager.RaiseDockOperationStartingEvent(DockOperation.Move, item, null);
        }

        public void DoDragging()
        {
            if (this.IsDragging)
            {
                if (this.tryToSuspendDragging)
                {
                    this.dragOperation.Window.SuspendDragging = this.SuspendBehingDragging = this.CanSuspendDragging(this.dragOperation.Window);
                    this.tryToSuspendDragging = false;
                }
                FloatingView view = this.dragOperation.Window.Manager.GetView(((LayoutGroup) this.dragOperation.Window.FloatGroup)) as FloatingView;
                if (!this.SuspendBehingDragging && (view != null))
                {
                    Point mousePositionSafe = NativeHelper.GetMousePositionSafe();
                    view.OnMouseMove(new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(view.RootUIElement.PointFromScreen(mousePositionSafe), MouseButtons.Left));
                }
            }
        }

        public void DoSizing()
        {
            if (this.IsResizing)
            {
                FloatingView view = this.dragOperation.Window.Manager.GetView(((LayoutGroup) this.dragOperation.Window.FloatGroup)) as FloatingView;
                if (view != null)
                {
                    Point mousePositionSafe = NativeHelper.GetMousePositionSafe();
                    view.OnMouseMove(new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(view.RootUIElement.PointFromScreen(mousePositionSafe), MouseButtons.Left));
                }
            }
        }

        internal void EnqueueDragging(IDraggableWindow window)
        {
            this.windowToDrag = window;
        }

        public void FinishDragging()
        {
            if (this.IsDragging || this.IsResizing)
            {
                if (Mouse.LeftButton != MouseButtonState.Released)
                {
                    base.Dispatcher.BeginInvoke(new Action(this.FinishDragging), DispatcherPriority.Input, new object[0]);
                }
                else
                {
                    this.FinishDraggingCore();
                }
            }
        }

        private void FinishDraggingCore()
        {
            if (((this.IsDragging || this.IsResizing) && (this.dragOperation != null)) && (Mouse.LeftButton == MouseButtonState.Released))
            {
                this.dragOperation.Window.IsDragging = false;
                this.IsDragging = this.IsResizing = false;
                FloatingView view = this.dragOperation.Window.Manager.GetView(((LayoutGroup) this.dragOperation.Window.FloatGroup)) as FloatingView;
                if (view != null)
                {
                    Point mousePositionSafe = NativeHelper.GetMousePositionSafe();
                    view.OnMouseUp(new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(view.RootUIElement.PointFromScreen(mousePositionSafe), MouseButtons.None, MouseButtons.Left));
                    Ref.Dispose<DragOperation>(ref this.dragOperation);
                }
            }
        }

        internal void ReleaseCapture()
        {
            if (this.IsDragging || this.IsResizing)
            {
                this.IsDragging = this.IsResizing = false;
                if (this.dragOperation != null)
                {
                    this.dragOperation.Window.IsDragging = false;
                    FloatingView view = this.dragOperation.Window.Manager.GetView(((LayoutGroup) this.dragOperation.Window.FloatGroup)) as FloatingView;
                    if (view != null)
                    {
                        view.OnMouseCaptureLost();
                        Ref.Dispose<DragOperation>(ref this.dragOperation);
                    }
                }
            }
        }

        internal bool TryStartDragging(bool isFloating = true)
        {
            bool flag;
            try
            {
                flag = (this.windowToDrag != null) && this.TryStartDragging(this.windowToDrag, isFloating);
            }
            finally
            {
                this.windowToDrag = null;
            }
            return flag;
        }

        internal bool TryStartDragging(IDraggableWindow window, bool isFloating)
        {
            if ((window == null) || this.IsInEvent)
            {
                return false;
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.CanDrag(window))
                {
                    return false;
                }
                this.dragOperation = new DragOperation(window);
                DockLayoutManager container = window.Manager;
                FloatingView view = container.GetView(((LayoutGroup) window.FloatGroup)) as FloatingView;
                if (view == null)
                {
                    return false;
                }
                view.ReleaseCapture();
                window.IsDragging = this.IsDragging = true;
                this.tryToSuspendDragging = true;
                if (isFloating)
                {
                    Matrix transformFromDevice = PresentationSource.FromVisual(container).CompositionTarget.TransformFromDevice;
                    Point point = transformFromDevice.Transform(window.Window.PointToScreen(Mouse.GetPosition(window.Window)));
                    Point dragOrigin = container.ViewAdapter.DragService.DragOrigin;
                    if (double.IsNaN(dragOrigin.X) || double.IsNaN(dragOrigin.Y))
                    {
                        window.Window.Left = point.X - 15.0;
                        window.Window.Top = point.Y - 15.0;
                    }
                    else
                    {
                        Point point3 = transformFromDevice.Transform(container.PointToScreen(dragOrigin));
                        Point dragOffset = container.GetDragOffset();
                        double num = (point.X - point3.X) + dragOffset.X;
                        double num2 = (point.Y - point3.Y) + dragOffset.Y;
                        Window window1 = window.Window;
                        window1.Left += num;
                        Window window2 = window.Window;
                        window2.Top += num2;
                    }
                }
                container.StartNativeDragging();
                using (new OverrideCursor(Cursors.Arrow))
                {
                    IntPtr handle = new WindowInteropHelper(window.Window).Handle;
                    Win32.SendMessage(handle, 0x112, 0xf012, IntPtr.Zero);
                    Win32.SendMessage(handle, 0x202, 0, IntPtr.Zero);
                }
                container.CompleteNativeDragging();
            }
            return true;
        }

        internal bool TryStartSizing(IDraggableWindow floatingPaneWindow)
        {
            if (!this.IsInEvent && ((Mouse.LeftButton == MouseButtonState.Pressed) && (floatingPaneWindow.Manager.ViewAdapter.DragService.OperationType == DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing)))
            {
                FloatingView objA = floatingPaneWindow.Manager.GetView(((LayoutGroup) floatingPaneWindow.FloatGroup)) as FloatingView;
                if ((objA == null) || !ReferenceEquals(objA, floatingPaneWindow.Manager.ViewAdapter.DragService.DragSource))
                {
                    return false;
                }
                this.dragOperation = new DragOperation(floatingPaneWindow);
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    this.IsResizing = true;
                    objA.ReleaseCapture();
                    using (new OverrideCursor(this.SizingAction.ToCursor()))
                    {
                        floatingPaneWindow.Window.DragSize(this.SizingAction);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsDragging { get; private set; }

        public bool IsInEvent =>
            this.IsDragging || this.IsResizing;

        public bool IsResizing { get; private set; }

        public DevExpress.Xpf.Layout.Core.SizingAction SizingAction { get; internal set; }

        private bool SuspendBehingDragging { get; set; }

        private class DragOperation : IDisposable
        {
            public DragOperation(IDraggableWindow window)
            {
                this.Window = window;
                this.FloatGroup = this.Window.FloatGroup;
                this.Manager = this.Window.Manager;
            }

            public void Dispose()
            {
                if (!this.IsDisposing)
                {
                    this.IsDisposing = true;
                    this.Manager = null;
                    this.FloatGroup = null;
                    this.Window = null;
                }
                GC.SuppressFinalize(this);
            }

            public DevExpress.Xpf.Docking.FloatGroup FloatGroup { get; private set; }

            public bool IsDisposing { get; private set; }

            public DockLayoutManager Manager { get; private set; }

            public IDraggableWindow Window { get; private set; }
        }

        private class OverrideCursor : IDisposable
        {
            public OverrideCursor(Cursor cursor)
            {
                if (!ReferenceEquals(Mouse.OverrideCursor, cursor))
                {
                    Mouse.OverrideCursor = cursor;
                }
            }

            public void Dispose()
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(this.DisposeCore), DispatcherPriority.Input, new object[0]);
            }

            private void DisposeCore()
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}

