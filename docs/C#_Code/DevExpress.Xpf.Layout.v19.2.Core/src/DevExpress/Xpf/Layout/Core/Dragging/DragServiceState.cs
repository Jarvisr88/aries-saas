namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class DragServiceState : IDragServiceState
    {
        private DevExpress.Xpf.Layout.Core.OperationType _lastInitializedOperation;

        protected DragServiceState(IDragService service)
        {
            this.DragService = service;
        }

        protected bool BeginOperation(DevExpress.Xpf.Layout.Core.OperationType operationType, IView view, ILayoutElement dragItem, Point startPoint)
        {
            ILayoutElementBehavior elementBehavior = view.GetElementBehavior(dragItem);
            if ((elementBehavior != null) && (elementBehavior.AllowDragging && elementBehavior.CanDrag(operationType)))
            {
                this.DragService.SetState(operationType);
                if (this.DragService.OperationType == operationType)
                {
                    dragItem.ResetState();
                    IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(operationType);
                    if (uIServiceListener != null)
                    {
                        uIServiceListener.OnBegin(startPoint, dragItem);
                    }
                    return true;
                }
            }
            return false;
        }

        private void CancelInitializedOperation(IView view)
        {
            if (this.ShouldCancelLastInitializedOperation(this._lastInitializedOperation))
            {
                IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(this._lastInitializedOperation);
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnCancel();
                }
                this._lastInitializedOperation = DevExpress.Xpf.Layout.Core.OperationType.Regular;
            }
        }

        protected void DoCancel(IView view, DevExpress.Xpf.Layout.Core.OperationType operation)
        {
            if (view != null)
            {
                IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(operation);
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnCancel();
                }
            }
        }

        protected void DoDragging(IView view, Point point)
        {
            this.DoDragging(view, point, this.OperationType);
        }

        protected void DoDragging(IView view, Point point, DevExpress.Xpf.Layout.Core.OperationType type)
        {
            IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(type);
            if ((uIServiceListener != null) && uIServiceListener.CanDrag(point, this.DragService.DragItem))
            {
                uIServiceListener.OnDragging(point, this.DragService.DragItem);
            }
        }

        protected void DoDrop(IView view, Point point)
        {
            this.DoDrop(view, point, this.OperationType);
        }

        protected void DoDrop(IView view, Point point, DevExpress.Xpf.Layout.Core.OperationType type)
        {
            IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(type);
            if (uIServiceListener != null)
            {
                if (uIServiceListener.CanDrop(point, this.DragService.DragItem))
                {
                    uIServiceListener.OnDrop(point, this.DragService.DragItem);
                }
                else
                {
                    uIServiceListener.OnCancel();
                }
            }
        }

        protected void DoEnter(IView view, DevExpress.Xpf.Layout.Core.OperationType operation)
        {
            if (view != null)
            {
                IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(operation);
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnEnter();
                }
            }
        }

        protected void DoLeave(IView view, DevExpress.Xpf.Layout.Core.OperationType operation)
        {
            if (view != null)
            {
                IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(operation);
                if (uIServiceListener != null)
                {
                    uIServiceListener.OnLeave();
                }
            }
        }

        protected virtual DevExpress.Xpf.Layout.Core.OperationType GetBehindOperation() => 
            DevExpress.Xpf.Layout.Core.OperationType.Regular;

        protected virtual IView GetBehindView() => 
            null;

        protected bool InitializeOperation(IView view, Point point, ILayoutElement dragItem, DevExpress.Xpf.Layout.Core.OperationType type)
        {
            ILayoutElementBehavior elementBehavior = view.GetElementBehavior(dragItem);
            if ((elementBehavior == null) || (!elementBehavior.AllowDragging || !elementBehavior.CanDrag(type)))
            {
                this._lastInitializedOperation = DevExpress.Xpf.Layout.Core.OperationType.Regular;
                return false;
            }
            this.DragService.DragItem = dragItem;
            this.DragService.DragSource = view;
            this.DragService.DragOrigin = view.ClientToScreen(point);
            IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(type);
            if (uIServiceListener != null)
            {
                uIServiceListener.OnInitialize(point, dragItem);
            }
            this._lastInitializedOperation = type;
            return true;
        }

        public virtual void ProcessCancel(IView view)
        {
            this.Reset(view);
            if (this.OperationType != DevExpress.Xpf.Layout.Core.OperationType.Regular)
            {
                this.DoLeave(this.GetBehindView(), this.GetBehindOperation());
                this.DoCancel(view, this.OperationType);
            }
        }

        public virtual void ProcessComplete(IView view)
        {
        }

        public virtual void ProcessKeyDown(IView view, Key key)
        {
            if (key == Key.Escape)
            {
                this.ProcessCancel(view);
                this.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
            }
            else if ((key == Key.LeftCtrl) || (key == Key.RightCtrl))
            {
                this.DoLeave(view, this.OperationType);
                this.DoLeave(this.GetBehindView(), this.GetBehindOperation());
            }
        }

        public virtual void ProcessKeyUp(IView view, Key key)
        {
            if ((key == Key.LeftCtrl) || (key == Key.RightCtrl))
            {
                this.DoEnter(view, this.OperationType);
                this.DoEnter(this.GetBehindView(), this.GetBehindOperation());
            }
        }

        public virtual void ProcessMouseDown(IView view, Point point)
        {
            this.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
        }

        public virtual void ProcessMouseMove(IView view, Point point)
        {
            this.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
        }

        public virtual void ProcessMouseUp(IView view, Point point)
        {
            this.DragService.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
        }

        protected void Reset(IView view)
        {
            this.ResetCore();
            this.CancelInitializedOperation(view);
        }

        protected virtual void ResetCore()
        {
        }

        protected void ResetOperation(DevExpress.Xpf.Layout.Core.OperationType type, IView view)
        {
            IDragServiceListener uIServiceListener = view.GetUIServiceListener<IDragServiceListener>(type);
            if (uIServiceListener != null)
            {
                uIServiceListener.OnCancel();
            }
            this._lastInitializedOperation = DevExpress.Xpf.Layout.Core.OperationType.Regular;
        }

        protected virtual bool ShouldCancelLastInitializedOperation(DevExpress.Xpf.Layout.Core.OperationType initializedOperation) => 
            (initializedOperation != DevExpress.Xpf.Layout.Core.OperationType.Regular) && (initializedOperation != DevExpress.Xpf.Layout.Core.OperationType.ClientDragging);

        protected void TryLeaveAndEnter(IView viewFrom, DevExpress.Xpf.Layout.Core.OperationType from, IView viewTo, Point point, DevExpress.Xpf.Layout.Core.OperationType to)
        {
            IDragServiceListener uIServiceListener = null;
            if (viewFrom != null)
            {
                uIServiceListener = viewFrom.GetUIServiceListener<IDragServiceListener>(from);
            }
            IDragServiceListener uIServiceListener = null;
            bool flag = false;
            if (viewTo != null)
            {
                uIServiceListener = viewTo.GetUIServiceListener<IDragServiceListener>(to);
                flag = (uIServiceListener != null) && uIServiceListener.CanDrag(point, this.DragService.DragItem);
            }
            if ((flag || (uIServiceListener == null)) && (uIServiceListener != null))
            {
                uIServiceListener.OnLeave();
            }
            if (flag)
            {
                uIServiceListener.OnEnter();
            }
        }

        public IDragService DragService { get; private set; }

        public abstract DevExpress.Xpf.Layout.Core.OperationType OperationType { get; }
    }
}

