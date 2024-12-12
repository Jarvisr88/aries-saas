namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    internal class DragService : UIService, IDragService, IUIService, IDisposable
    {
        private IDragServiceStateFactory defaultStateFactoryCore;
        private IDragServiceState stateCore;
        private IViewAdapter adapter;
        private ILayoutElement dragItemCore;
        private IView dragSourceCore;

        public DragService() : this(new DragServiceStateFactory())
        {
        }

        public DragService(IDragServiceStateFactory factory)
        {
            this.defaultStateFactoryCore = factory;
        }

        protected override void BeginEventOverride(IView sender)
        {
            this.RethrowEvent = false;
        }

        private void BeginUpdating()
        {
            if (!base.IsDisposing)
            {
                NotificationBatch.Updating(this.GetNotificationSource());
            }
        }

        protected IView CalculateEventProcessor(Point screenPoint, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            IView receiver = this.Receiver;
            if (this.Receiver == null)
            {
                receiver = base.Sender;
                ILayoutElementBehavior elementBehavior = base.Sender.GetElementBehavior(this.DragItem);
                if (this.OperationType == DevExpress.Xpf.Layout.Core.OperationType.Reordering)
                {
                    DevExpress.Xpf.Layout.Core.OperationType type;
                    if (this.CheckBehavior(base.Sender, this.DragItem, DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging) == DevExpress.Xpf.Layout.Core.OperationType.ClientDragging)
                    {
                        type = this.CheckBehavior(base.Sender, this.DragItem, DevExpress.Xpf.Layout.Core.OperationType.Floating, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging);
                    }
                    if ((elementBehavior != null) && (elementBehavior.AllowDragging && elementBehavior.CanDrag(type)))
                    {
                        this.LeaveAndEnter(this.LastProcessor, this.OperationType, base.Sender, screenPoint, type);
                    }
                }
                if ((this.OperationType == DevExpress.Xpf.Layout.Core.OperationType.ClientDragging) || (this.OperationType == DevExpress.Xpf.Layout.Core.OperationType.Reordering))
                {
                    DevExpress.Xpf.Layout.Core.OperationType type2;
                    if (this.CheckBehavior(base.Sender, this.DragItem, DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving, DevExpress.Xpf.Layout.Core.OperationType.NonClientDragging) == DevExpress.Xpf.Layout.Core.OperationType.NonClientDragging)
                    {
                        type2 = this.CheckBehavior(base.Sender, this.DragItem, DevExpress.Xpf.Layout.Core.OperationType.Floating, DevExpress.Xpf.Layout.Core.OperationType.NonClientDragging);
                    }
                    if ((elementBehavior != null) && (elementBehavior.AllowDragging && elementBehavior.CanDrag(type2)))
                    {
                        this.LeaveAndEnter(this.LastProcessor, this.OperationType, base.Sender, screenPoint, type2);
                    }
                }
            }
            else
            {
                switch (this.OperationType)
                {
                    case DevExpress.Xpf.Layout.Core.OperationType.Resizing:
                    case DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving:
                    case DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing:
                        receiver = base.Sender;
                        break;

                    case DevExpress.Xpf.Layout.Core.OperationType.Reordering:
                    {
                        IView sender = base.Sender;
                        DevExpress.Xpf.Layout.Core.OperationType operation = this.CheckReorderingOperation(this.Receiver, this.DragItem, screenPoint);
                        ILayoutElementBehavior elementBehavior = sender.GetElementBehavior(this.DragItem);
                        if ((eventType != MouseEventType.MouseUp) && ((elementBehavior != null) && (elementBehavior.AllowDragging && elementBehavior.CanDrag(operation))))
                        {
                            IViewAdapter adapter = sender.Adapter;
                            this.LeaveAndEnter(this.LastProcessor, DevExpress.Xpf.Layout.Core.OperationType.Reordering, sender, screenPoint, operation);
                            if (sender.IsDisposing)
                            {
                                sender = adapter.GetView(this.DragItem);
                            }
                        }
                        receiver = sender;
                        break;
                    }
                    case DevExpress.Xpf.Layout.Core.OperationType.ClientDragging:
                        this.LeaveAndEnter(this.LastProcessor, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging, this.Receiver, screenPoint, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging);
                        break;

                    case DevExpress.Xpf.Layout.Core.OperationType.NonClientDragging:
                    {
                        DevExpress.Xpf.Layout.Core.OperationType operation = this.CheckReorderingOperation(this.Receiver, this.DragItem, screenPoint);
                        ILayoutElementBehavior elementBehavior = base.Sender.GetElementBehavior(this.DragItem);
                        if ((elementBehavior != null) && (elementBehavior.AllowDragging && elementBehavior.CanDrag(operation)))
                        {
                            this.LeaveAndEnter(base.Sender, DevExpress.Xpf.Layout.Core.OperationType.NonClientDragging, this.Receiver, screenPoint, operation);
                        }
                        break;
                    }
                    default:
                        break;
                }
            }
            return receiver;
        }

        private DevExpress.Xpf.Layout.Core.OperationType CheckBehavior(IView view, ILayoutElement dragItem, DevExpress.Xpf.Layout.Core.OperationType requested, DevExpress.Xpf.Layout.Core.OperationType allowed)
        {
            ILayoutElementBehavior elementBehavior = view.GetElementBehavior(dragItem);
            return (((elementBehavior == null) || !elementBehavior.CanDrag(requested)) ? allowed : requested);
        }

        private DevExpress.Xpf.Layout.Core.OperationType CheckCanSuspendOperation(DevExpress.Xpf.Layout.Core.OperationType operationType)
        {
            bool flag = false;
            switch (operationType)
            {
                case DevExpress.Xpf.Layout.Core.OperationType.Resizing:
                case DevExpress.Xpf.Layout.Core.OperationType.FloatingResizing:
                    flag = this.CheckSuspendResizing(operationType);
                    break;

                case DevExpress.Xpf.Layout.Core.OperationType.Reordering:
                    flag = this.CheckSuspendReordering(operationType);
                    break;

                case DevExpress.Xpf.Layout.Core.OperationType.Floating:
                    flag = this.CheckSuspendFloating(operationType);
                    break;

                case DevExpress.Xpf.Layout.Core.OperationType.ClientDragging:
                    flag = this.CheckSuspendClientDragging(operationType);
                    break;

                case DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving:
                    flag = this.CheckSuspendFloatingMoving(operationType);
                    break;

                default:
                    break;
            }
            if (flag)
            {
                this.State.ProcessCancel(this.DragSource);
                operationType = DevExpress.Xpf.Layout.Core.OperationType.Regular;
                this.Reset();
            }
            return operationType;
        }

        private DevExpress.Xpf.Layout.Core.OperationType CheckReorderingOperation(IView target, ILayoutElement dragItem, Point screenPoint)
        {
            Point pt = target.ScreenToClient(screenPoint);
            if (target.Adapter.CalcHitInfo(target, pt).InReorderingBounds)
            {
                ReorderingListener uIServiceListener = target.GetUIServiceListener<ReorderingListener>(DevExpress.Xpf.Layout.Core.OperationType.Reordering);
                if ((uIServiceListener != null) && !uIServiceListener.CancelReordering(screenPoint, dragItem))
                {
                    return DevExpress.Xpf.Layout.Core.OperationType.Reordering;
                }
            }
            return this.CheckBehavior(target, dragItem, DevExpress.Xpf.Layout.Core.OperationType.Floating, DevExpress.Xpf.Layout.Core.OperationType.ClientDragging);
        }

        private void CheckSuspendBehindDragging(DevExpress.Xpf.Layout.Core.OperationType prevOperationType)
        {
            if ((this.DragSource != null) && ((this.DragItem != null) && (prevOperationType == DevExpress.Xpf.Layout.Core.OperationType.Regular)))
            {
                this.SuspendBehindDragging = ((BaseView) this.DragSource).CanSuspendBehindDragging(this.DragItem);
            }
        }

        private bool CheckSuspendClientDragging(DevExpress.Xpf.Layout.Core.OperationType operationType) => 
            (this.DragSource != null) && ((this.DragItem != null) && ((BaseView) this.DragSource).CanSuspendClientDragging(this.DragItem));

        private bool CheckSuspendFloating(DevExpress.Xpf.Layout.Core.OperationType operationType) => 
            (this.DragSource != null) && ((this.DragItem != null) && ((BaseView) this.DragSource).CanSuspendFloating(this.DragItem));

        private bool CheckSuspendFloatingMoving(DevExpress.Xpf.Layout.Core.OperationType operationType) => 
            (this.DragSource != null) && ((this.DragItem != null) && ((BaseView) this.DragSource).CanSuspendFloatingMoving(this.DragItem));

        private bool CheckSuspendReordering(DevExpress.Xpf.Layout.Core.OperationType operationType) => 
            (this.DragSource != null) && ((this.DragItem != null) && ((BaseView) this.DragSource).CanSuspendReordering(this.DragItem));

        private bool CheckSuspendResizing(DevExpress.Xpf.Layout.Core.OperationType operationType) => 
            (this.DragSource != null) && ((this.DragItem != null) && ((BaseView) this.DragSource).CanSuspendResizing(this.DragItem));

        protected override void EndEventOverride()
        {
            this.RethrowEvent = false;
        }

        private void EndUpdating()
        {
            if (!base.IsDisposing)
            {
                NotificationBatch.Updated(this.GetNotificationSource());
            }
        }

        protected override Key[] GetKeys() => 
            new Key[] { Key.Escape, Key.LeftCtrl, Key.RightCtrl };

        private object GetNotificationSource()
        {
            if (this.adapter == null)
            {
                IView view = base.Sender ?? this.Receiver;
                if (view == null)
                {
                    return null;
                }
                this.adapter = view.Adapter;
            }
            return this.adapter?.NotificationSource;
        }

        protected IDragServiceStateFactory GetStateFactory() => 
            this.GetStateFactoryCore() ?? this.DefaultStateFactory;

        protected virtual IDragServiceStateFactory GetStateFactoryCore() => 
            null;

        protected void LeaveAndEnter(IView viewFrom, DevExpress.Xpf.Layout.Core.OperationType from, IView viewTo, Point screenPoint, DevExpress.Xpf.Layout.Core.OperationType to)
        {
            if (!ReferenceEquals(viewFrom, viewTo) || (from != to))
            {
                IDragServiceListener uIServiceListener;
                if (viewFrom != null)
                {
                    uIServiceListener = viewFrom.GetUIServiceListener<IDragServiceListener>(from);
                    if (uIServiceListener != null)
                    {
                        uIServiceListener.OnLeave();
                    }
                }
                if (from != to)
                {
                    this.SetState(to);
                    if (this.OperationType != to)
                    {
                        return;
                    }
                }
                if (viewTo != null)
                {
                    uIServiceListener = viewTo.GetUIServiceListener<IDragServiceListener>(to);
                    Point point = viewTo.ScreenToClient(screenPoint);
                    if ((uIServiceListener != null) && uIServiceListener.CanDrag(point, this.DragItem))
                    {
                        uIServiceListener.OnEnter();
                    }
                }
            }
        }

        protected override void OnDispose()
        {
            this.Reset();
            this.adapter = null;
            base.OnDispose();
        }

        private void OnDragItemChanged(ILayoutElement value, ILayoutElement oldValue)
        {
            if (oldValue is BaseLayoutElement)
            {
                ((BaseLayoutElement) oldValue).IsDragging = false;
            }
            if (value is BaseLayoutElement)
            {
                ((BaseLayoutElement) value).IsDragging = true;
            }
        }

        private void OnDragSourceChanged(IView value, IView oldValue)
        {
            if ((oldValue != null) && !oldValue.IsDisposing)
            {
                oldValue.ReleaseCapture();
            }
            if (value != null)
            {
                value.SetCapture();
            }
        }

        private void OnReset()
        {
            this.State.ProcessComplete(this.DragSource);
        }

        protected virtual void ProcessKeyCore(IView processor, KeyEventType eventype, Key key)
        {
            if (eventype == KeyEventType.KeyDown)
            {
                this.State.ProcessKeyDown(processor, key);
            }
            else
            {
                this.State.ProcessKeyUp(processor, key);
            }
        }

        protected override bool ProcessKeyOverride(IView view, KeyEventType eventype, Key key)
        {
            if (view.Adapter == null)
            {
                return false;
            }
            this.ProcessKeyCore(base.Sender, eventype, key);
            return true;
        }

        protected virtual void ProcessMouseCore(IView processor, Point screenPoint, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs e)
        {
            if (processor != null)
            {
                Point point = processor.ScreenToClient(screenPoint);
                bool flag = false;
                switch (eventType)
                {
                    case MouseEventType.MouseDown:
                        flag = e.Buttons != MouseButtons.Left;
                        if (!flag)
                        {
                            this.State.ProcessMouseDown(processor, point);
                        }
                        break;

                    case MouseEventType.MouseUp:
                        flag = e.Buttons != MouseButtons.None;
                        if (!flag)
                        {
                            this.State.ProcessMouseUp(processor, point);
                        }
                        break;

                    case MouseEventType.MouseMove:
                        flag = e.Buttons != MouseButtons.Left;
                        if (!flag)
                        {
                            this.State.ProcessMouseMove(processor, point);
                        }
                        break;

                    case MouseEventType.MouseCaptureLost:
                        flag = true;
                        break;

                    default:
                        break;
                }
                if (flag)
                {
                    this.State.ProcessCancel(processor);
                    this.SetState(DevExpress.Xpf.Layout.Core.OperationType.Regular);
                }
            }
        }

        protected override bool ProcessMouseOverride(IView view, MouseEventType eventType, DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs ea)
        {
            IViewAdapter adapter = view.Adapter;
            if (adapter == null)
            {
                return false;
            }
            Point screenPoint = view.ClientToScreen(ea.Point);
            this.Receiver = adapter.GetView(screenPoint);
            IView processor = this.CalculateEventProcessor(screenPoint, eventType, ea);
            this.RethrowEvent = false;
            this.ProcessMouseCore(processor, screenPoint, eventType, ea);
            if (this.RethrowEvent)
            {
                this.ProcessMouseCore(processor, screenPoint, eventType, ea);
            }
            this.LastProcessor = processor;
            return true;
        }

        public void Reset()
        {
            this.OnReset();
            this.DragSource = null;
            this.DragItem = null;
            this.DragOrigin = UIService.InvalidPoint;
            this.SuspendBehindDragging = false;
        }

        protected virtual IDragServiceStateFactory ResolveDefaultStateFactory() => 
            ServiceLocator<IDragServiceStateFactory>.Resolve();

        public void SetState(DevExpress.Xpf.Layout.Core.OperationType operationType)
        {
            DevExpress.Xpf.Layout.Core.OperationType prevOperationType = this.OperationType;
            operationType = this.CheckCanSuspendOperation(operationType);
            if (prevOperationType != operationType)
            {
                this.stateCore = this.GetStateFactory().Create(this, operationType);
                switch (operationType)
                {
                    case DevExpress.Xpf.Layout.Core.OperationType.Regular:
                        if (prevOperationType != DevExpress.Xpf.Layout.Core.OperationType.Regular)
                        {
                            this.EndUpdating();
                        }
                        this.Reset();
                        break;

                    case DevExpress.Xpf.Layout.Core.OperationType.Resizing:
                    case DevExpress.Xpf.Layout.Core.OperationType.Reordering:
                    case DevExpress.Xpf.Layout.Core.OperationType.Floating:
                    case DevExpress.Xpf.Layout.Core.OperationType.ClientDragging:
                        this.RethrowEvent = true;
                        break;

                    case DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving:
                        this.CheckSuspendBehindDragging(prevOperationType);
                        break;

                    default:
                        break;
                }
                if (prevOperationType == DevExpress.Xpf.Layout.Core.OperationType.Regular)
                {
                    this.BeginUpdating();
                }
            }
        }

        public IView Receiver { get; private set; }

        public DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            this.State.OperationType;

        public Point DragOrigin { get; set; }

        public bool SuspendBehindDragging { get; set; }

        protected internal IView LastProcessor { get; set; }

        protected internal bool RethrowEvent { get; private set; }

        protected IDragServiceStateFactory DefaultStateFactory
        {
            [DebuggerStepThrough]
            get
            {
                this.defaultStateFactoryCore ??= this.ResolveDefaultStateFactory();
                return this.defaultStateFactoryCore;
            }
        }

        protected IDragServiceState State
        {
            [DebuggerStepThrough]
            get
            {
                this.stateCore ??= this.GetStateFactory().Create(this, DevExpress.Xpf.Layout.Core.OperationType.Regular);
                return this.stateCore;
            }
        }

        public ILayoutElement DragItem
        {
            get => 
                this.dragItemCore;
            set
            {
                if (!ReferenceEquals(this.dragItemCore, value))
                {
                    ILayoutElement dragItemCore = this.dragItemCore;
                    this.dragItemCore = value;
                    this.OnDragItemChanged(value, dragItemCore);
                }
            }
        }

        public IView DragSource
        {
            get => 
                this.dragSourceCore;
            set
            {
                if (!ReferenceEquals(this.dragSourceCore, value))
                {
                    IView dragSourceCore = this.dragSourceCore;
                    this.dragSourceCore = value;
                    this.OnDragSourceChanged(value, dragSourceCore);
                }
            }
        }
    }
}

