namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class ReorderingDragServiceState : DragServiceState
    {
        public ReorderingDragServiceState(IDragService service) : base(service)
        {
        }

        public override void ProcessComplete(IView view)
        {
            this.ProcessComplete(view, this.OperationType);
        }

        private void ProcessComplete(IView view, DevExpress.Xpf.Layout.Core.OperationType operation)
        {
            if (view != null)
            {
                IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(operation);
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnComplete();
                }
            }
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
            DevExpress.Xpf.Layout.Core.OperationType.Reordering;
    }
}

