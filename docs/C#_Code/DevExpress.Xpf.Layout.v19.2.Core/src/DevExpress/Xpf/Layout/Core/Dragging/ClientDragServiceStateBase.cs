namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Windows;

    public abstract class ClientDragServiceStateBase : DragServiceState
    {
        private IView lastBehindView;
        private DevExpress.Xpf.Layout.Core.OperationType lastBehindViewOperation;

        public ClientDragServiceStateBase(IDragService service) : base(service)
        {
        }

        private bool CheckReordering(IView view, Point point)
        {
            if (!(view is BaseView))
            {
                return true;
            }
            IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(DevExpress.Xpf.Layout.Core.OperationType.Reordering);
            return ((uIServiceListener == null) || (uIServiceListener.CanDrag(point, base.DragService.DragItem) && ((BaseView) view).CheckReordering(point)));
        }

        protected override DevExpress.Xpf.Layout.Core.OperationType GetBehindOperation() => 
            this.lastBehindViewOperation;

        protected override IView GetBehindView() => 
            this.lastBehindView;

        protected virtual IView GetBehindView(IView view, Point screenPoint, Point clientPoint) => 
            view.Adapter.GetBehindView(view, screenPoint);

        public override void ProcessMouseMove(IView view, Point point)
        {
            base.DoDragging(view, point);
            if (!base.DragService.SuspendBehindDragging)
            {
                Point screenPoint = view.ClientToScreen(point);
                DevExpress.Xpf.Layout.Core.OperationType regular = DevExpress.Xpf.Layout.Core.OperationType.Regular;
                Point pt = new Point();
                IView behindView = this.GetBehindView(view, screenPoint, point);
                if (behindView != null)
                {
                    pt = view.Adapter.GetBehindViewPoint(view, behindView, screenPoint);
                    if (behindView.Adapter.CalcHitInfo(behindView, pt).InReorderingBounds && this.CheckReordering(behindView, pt))
                    {
                        regular = DevExpress.Xpf.Layout.Core.OperationType.Reordering;
                    }
                }
                if ((this.lastBehindView != null) && this.lastBehindView.IsDisposing)
                {
                    this.lastBehindView = null;
                }
                if (!ReferenceEquals(behindView, this.lastBehindView) || (this.lastBehindViewOperation != regular))
                {
                    base.TryLeaveAndEnter(this.lastBehindView, this.lastBehindViewOperation, behindView, pt, regular);
                    this.lastBehindView = behindView;
                    this.lastBehindViewOperation = regular;
                }
                if (behindView != null)
                {
                    base.DoDragging(behindView, pt, regular);
                }
            }
        }

        public override void ProcessMouseUp(IView view, Point point)
        {
            Point point1;
            Point screenPoint = view.ClientToScreen(point);
            IView behindView = this.GetBehindView(view, screenPoint, point);
            if (behindView != null)
            {
                point1 = view.Adapter.GetBehindViewPoint(view, behindView, screenPoint);
            }
            else
            {
                point1 = new Point();
            }
            Point point3 = point1;
            base.DoDrop(view, point);
            if ((behindView != null) && !behindView.IsDisposing)
            {
                this.DoDrop(behindView, point3, (!behindView.Adapter.CalcHitInfo(behindView, point3).InReorderingBounds || !this.CheckReordering(behindView, point3)) ? DevExpress.Xpf.Layout.Core.OperationType.Regular : DevExpress.Xpf.Layout.Core.OperationType.Reordering);
            }
            base.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
        }
    }
}

