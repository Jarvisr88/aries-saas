namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class ResizingDragServiceState : DragServiceState
    {
        public ResizingDragServiceState(IDragService service) : base(service)
        {
        }

        public override void ProcessMouseMove(IView view, Point point)
        {
            base.DoDragging(view, point);
        }

        public override void ProcessMouseUp(IView view, Point point)
        {
            base.DoDrop(view, point);
            base.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
        }

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.Resizing;
    }
}

