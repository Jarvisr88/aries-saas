namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class RegularDragServiceState : DragServiceState
    {
        internal bool isDragging;
        internal bool isResizing;
        internal bool isReordering;
        internal bool isFloating;

        public RegularDragServiceState(IDragService service) : base(service)
        {
        }

        public override void ProcessMouseDown(IView view, Point point)
        {
            LayoutElementHitInfo info = view.Adapter.CalcHitInfo(view, point);
            ILayoutElement dragItem = view.GetDragItem(info.Element);
            if ((dragItem != null) && !info.InControlBox)
            {
                if (info.InReorderingBounds)
                {
                    this.isReordering = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.Reordering);
                    if (this.isReordering)
                    {
                        return;
                    }
                }
                if (info.InDragBounds)
                {
                    this.isFloating = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.Floating);
                    if (this.isFloating)
                    {
                        return;
                    }
                    this.isDragging = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving);
                    if (this.isDragging)
                    {
                        return;
                    }
                    this.isDragging = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging);
                    if (this.isDragging)
                    {
                        return;
                    }
                }
                if (info.InResizeBounds)
                {
                    if (view.Type == HostType.Floating)
                    {
                        this.isResizing = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing);
                        if (this.isResizing)
                        {
                            return;
                        }
                    }
                    this.isResizing = base.InitializeOperation(view, point, dragItem, DevExpress.Xpf.Layout.Core.OperationType.Resizing);
                    bool isResizing = this.isResizing;
                }
            }
        }

        public override void ProcessMouseMove(IView view, Point point)
        {
            IView dragSource = base.DragService.DragSource;
            if (dragSource != null)
            {
                ILayoutElement dragItem = base.DragService.DragItem;
                Point dragOrigin = base.DragService.DragOrigin;
                Point screenPoint = view.ClientToScreen(point);
                Point startPoint = dragSource.ScreenToClient(screenPoint);
                if (DragServiceHelper.IsOutOfArea(screenPoint, dragOrigin, (this.isDragging && (dragSource.Type == HostType.Floating)) ? 1 : 5))
                {
                    if (this.isFloating && base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.Floating, dragSource, dragItem, screenPoint))
                    {
                        return;
                    }
                    if (this.isDragging)
                    {
                        if ((dragSource.Type == HostType.Floating) && base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving, dragSource, dragItem, screenPoint))
                        {
                            return;
                        }
                        if (base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.ClientDragging, dragSource, dragItem, startPoint))
                        {
                            return;
                        }
                    }
                    if (this.isReordering && base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.Reordering, dragSource, dragItem, startPoint))
                    {
                        return;
                    }
                }
                if (this.isResizing && ((dragSource.Type != HostType.Floating) || !base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing, dragSource, dragItem, screenPoint)))
                {
                    base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.Resizing, dragSource, dragItem, startPoint);
                }
            }
        }

        public override void ProcessMouseUp(IView view, Point point)
        {
            IView dragSource = base.DragService.DragSource;
            if (this.isResizing && ((dragSource != null) && (dragSource.Type == HostType.Floating)))
            {
                base.ResetOperation(DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing, view);
            }
            base.Reset(view);
        }

        protected override void ResetCore()
        {
            base.DragService.Reset();
            bool flag1 = this.isFloating = false;
            bool flag2 = this.isReordering = flag1;
            this.isDragging = this.isResizing = flag2;
            base.ResetCore();
        }

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.Regular;
    }
}

