namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class PageDraggingImplementer
    {
        private const string CS_PAGEDRAGGINGIMPLEMENTER_ID = "CS_PAGEDRAGGINGIMPLEMENTER_ID";
        private bool isDraggingMode;
        private Point? startDragPoint;
        private Point cursorPos;

        public PageDraggingImplementer(IPreviewModel model, IScrollInfo scrollableOwner, DevExpress.Xpf.Printing.Native.PageDraggingType pageDraggingType)
        {
            Guard.ArgumentNotNull(model, "model");
            Guard.ArgumentNotNull(scrollableOwner, "scrollableOwner");
            this.Model = model;
            this.ScrollableOwner = scrollableOwner;
            this.PageDraggingType = pageDraggingType;
            Point point = new Point();
            this.DragOffset = point;
        }

        public void HandleKeyDown(Key pressedKey)
        {
            if (!this.IsPageDraggingEnabled && (pressedKey == Key.Space))
            {
                this.TryBlockCursorService();
                this.isDraggingMode = true;
                this.TrySetCursor(PreviewCustomCursors.HandCursor);
                this.TrySetCursorPosition(this.cursorPos);
                ((UIElement) this.ScrollableOwner).CaptureMouse();
            }
        }

        public void HandleKeyUp(Key pressedKey)
        {
            if (pressedKey == Key.Space)
            {
                this.isDraggingMode = false;
            }
            if ((this.startDragPoint == null) && (pressedKey == Key.Space))
            {
                this.TrySetCursor(Cursors.Arrow);
                this.TryUnblockCursorService();
                ((UIElement) this.ScrollableOwner).ReleaseMouseCapture();
            }
        }

        public void HandleMouseDown(Point position)
        {
            if (this.IsPageDraggingEnabled)
            {
                this.TrySetCursor(PreviewCustomCursors.HandDragCursor);
                this.startDragPoint = new Point?(position);
            }
        }

        public void HandleMouseMove(Point position)
        {
            this.cursorPos = position;
            if (this.IsPageDraggingEnabled)
            {
                this.TrySetCursorPosition(this.cursorPos);
            }
            if (this.startDragPoint != null)
            {
                Point cursorPos = this.cursorPos;
                double num = cursorPos.X - this.startDragPoint.Value.X;
                double num2 = cursorPos.Y - this.startDragPoint.Value.Y;
                this.startDragPoint = new Point?(cursorPos);
                DevExpress.Xpf.Printing.Native.PageDraggingType pageDraggingType = this.PageDraggingType;
                if (pageDraggingType == DevExpress.Xpf.Printing.Native.PageDraggingType.DragViaScrollViewer)
                {
                    this.ScrollViewer.ScrollToHorizontalOffset(this.ScrollViewer.HorizontalOffset - num);
                    this.ScrollViewer.ScrollToVerticalOffset(this.ScrollViewer.VerticalOffset - num2);
                    this.ScrollViewer.UpdateLayout();
                }
                else
                {
                    if (pageDraggingType != DevExpress.Xpf.Printing.Native.PageDraggingType.DragViaTransform)
                    {
                        throw new ArgumentException("PageDraggingType");
                    }
                    this.DragOffset = new Point(this.DragOffset.X + num, this.DragOffset.Y + num2);
                    ((ScrollablePageView) this.ScrollableOwner).UpdatePagePosition();
                }
            }
        }

        public void HandleMouseUp(out bool handled)
        {
            this.startDragPoint = null;
            if (this.isDraggingMode)
            {
                handled = true;
                this.TrySetCursor(PreviewCustomCursors.HandCursor);
            }
            else
            {
                handled = false;
                this.TrySetCursor(Cursors.Arrow);
                this.TryUnblockCursorService();
                ((UIElement) this.ScrollableOwner).ReleaseMouseCapture();
            }
        }

        private void TryBlockCursorService()
        {
            if ((this.Model != null) && (this.Model.CursorService != null))
            {
                this.Model.CursorService.HideCustomCursor();
                this.Model.CursorService.BlockService("CS_PAGEDRAGGINGIMPLEMENTER_ID");
            }
        }

        private void TrySetCursor(CustomCursor cursorType)
        {
            if ((this.Model != null) && (this.Model.CursorService != null))
            {
                this.Model.CursorService.SetCursor((FrameworkElement) this.ScrollableOwner, Cursors.None, "CS_PAGEDRAGGINGIMPLEMENTER_ID");
                this.Model.CursorService.SetCursor(this.ScrollViewer, cursorType, "CS_PAGEDRAGGINGIMPLEMENTER_ID");
            }
        }

        private void TrySetCursor(Cursor cursor)
        {
            if ((this.Model != null) && (this.Model.CursorService != null))
            {
                this.Model.CursorService.SetCursor((FrameworkElement) this.ScrollableOwner, cursor, "CS_PAGEDRAGGINGIMPLEMENTER_ID");
                this.Model.CursorService.SetCursor(this.ScrollViewer, cursor, "CS_PAGEDRAGGINGIMPLEMENTER_ID");
            }
        }

        private void TrySetCursorPosition(Point position)
        {
            if ((this.Model != null) && (this.Model.CursorService != null))
            {
                this.Model.CursorService.SetCursorPosition(position, (FrameworkElement) this.ScrollableOwner, "CS_PAGEDRAGGINGIMPLEMENTER_ID");
            }
        }

        private void TryUnblockCursorService()
        {
            if ((this.Model != null) && (this.Model.CursorService != null))
            {
                this.Model.CursorService.UnblockService("CS_PAGEDRAGGINGIMPLEMENTER_ID");
            }
        }

        public bool IsPageDraggingEnabled =>
            this.isDraggingMode || (this.startDragPoint != null);

        public IPreviewModel Model { get; private set; }

        public Point DragOffset { get; private set; }

        public DevExpress.Xpf.Printing.Native.PageDraggingType PageDraggingType { get; private set; }

        private IScrollInfo ScrollableOwner { get; set; }

        private System.Windows.Controls.ScrollViewer ScrollViewer =>
            this.ScrollableOwner.ScrollOwner;
    }
}

