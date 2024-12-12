namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FloatingDragServiceState : DragServiceState
    {
        public FloatingDragServiceState(IDragService service) : base(service)
        {
        }

        public override void ProcessMouseMove(IView view, Point point)
        {
            Point startPoint = view.ClientToScreen(point);
            IView view2 = view.Adapter.GetView(base.DragService.DragItem);
            if ((view2 == null) || (view2.Type != HostType.Floating))
            {
                base.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
            }
            else
            {
                base.DragService.DragSource = view2;
                view2.Adapter.UIInteractionService.Activate(view2);
                base.BeginOperation(DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving, view2, base.DragService.DragItem, startPoint);
            }
        }

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.Floating;
    }
}

