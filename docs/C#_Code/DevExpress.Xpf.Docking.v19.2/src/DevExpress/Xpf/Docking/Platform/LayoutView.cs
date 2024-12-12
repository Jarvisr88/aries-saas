namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class LayoutView : BaseView, IAdornerWindowClient, ICustomDragProcessor
    {
        private readonly Locker mouseCaptureLocker;
        private MouseEventSubscriber mouseEventSubscriber;
        private DevExpress.Xpf.Docking.Platform.ResizingWindowHelper resizingWindowHelper;
        private IDictionary<object, IDisposable> subscriptions;
        private VisualizerAdornerHelper visualizerAdornerHelper;

        public LayoutView(IUIElement viewUIElement) : base(null, null)
        {
            this.mouseCaptureLocker = new Locker();
            this.RootElement = viewUIElement;
            this.Initialize(viewUIElement);
            this.RegisterListeners();
        }

        protected override void BeginProcessEvent(object ea)
        {
            base.BeginProcessEvent(ea);
            this.LastProcessedEvent = ea;
        }

        private Rect CalcBounds()
        {
            Point screenLocation = WindowHelper.GetScreenLocation(this.RootUIElement);
            Size renderSize = this.RootUIElement.RenderSize;
            if (this.Container.FlowDirection == FlowDirection.RightToLeft)
            {
                screenLocation.X -= renderSize.Width;
            }
            return new Rect(screenLocation, renderSize);
        }

        protected override int CalcZOrder() => 
            ((UIElement) this.RootElement).GetVisualOrder(this.Container);

        protected override bool CanHandleMouseCaptureLost() => 
            this.mouseCaptureLocker == 0;

        protected override bool CanSuspendBehindDragging(ILayoutElement dragItem) => 
            this.RaiseStartDocking(dragItem);

        protected override bool CanSuspendClientDragging(ILayoutElement dragItem)
        {
            if (LayoutItemsHelper.IsLayoutItem(((IDockLayoutElement) dragItem).Item))
            {
                return false;
            }
            BaseLayoutItem item = ((IDockLayoutElement) dragItem).CheckDragElement().Item;
            return ((!(item.AllowFloat && item.IsItemWithRestrictedFloating()) || !this.Container.RaiseDockOperationStartingEvent(DockOperation.Move, item, null)) ? this.RaiseStartDocking(dragItem) : true);
        }

        protected override bool CanSuspendDocking(ILayoutElement dragItem) => 
            this.RaiseStartDocking(dragItem);

        protected override bool CanSuspendFloating(ILayoutElement dragItem) => 
            this.RaiseStartDocking(dragItem);

        protected override bool CanSuspendReordering(ILayoutElement dragItem)
        {
            if (!(dragItem is MDIDocumentElement))
            {
                return false;
            }
            BaseLayoutItem item = ((IDockLayoutElement) dragItem).CheckDragElement().Item;
            return this.Container.RaiseDockOperationStartingEvent(DockOperation.Move, item, null);
        }

        protected override bool CanSuspendResizing(ILayoutElement dragItem)
        {
            BaseLayoutItem item = ((IDockLayoutElement) dragItem).CheckDragElement().Item;
            return this.Container.RaiseDockOperationStartingEvent(DockOperation.Resize, item, null);
        }

        protected override bool CanUseCustomServiceListener(object key) => 
            (base.Adapter.DragService.DragItem is MDIDocumentElement) && (Equals(key, DevExpress.Xpf.Layout.Core.OperationType.Reordering) || Equals(key, DevExpress.Xpf.Layout.Core.OperationType.Resizing));

        protected override bool CheckReordering(Point point)
        {
            DockHintHitInfo hitInfo = this.AdornerHelper.GetHitInfo(point);
            return ((hitInfo == null) || !hitInfo.InHint);
        }

        protected virtual UIElement CheckRootUIElement(IUIElement viewUIElement) => 
            viewUIElement as UIElement;

        public override Point ClientToScreen(Point clientPoint) => 
            CoordinateHelper.PointToScreen(this.Container, this.RootUIElement, clientPoint);

        protected internal virtual DockingHintAdorner CreateDockingHintAdorner(UIElement adornedElement)
        {
            DockingHintAdorner adorner1 = new DockingHintAdorner(adornedElement);
            adorner1.HostType = this.Type;
            return adorner1;
        }

        protected internal virtual SelectionAdorner CreateSelectionAdorner(UIElement adornedElement)
        {
            SelectionAdorner adorner1 = new SelectionAdorner(adornedElement);
            adorner1.HostType = this.Type;
            return adorner1;
        }

        protected internal virtual ShadowResizeAdorner CreateShadowResizeAdorner(UIElement adornedElement) => 
            new ShadowResizeAdorner(adornedElement);

        protected internal virtual TabHeadersAdorner CreateTabHeadersAdorner(UIElement adornedElement) => 
            new TabHeadersAdorner(adornedElement);

        void ICustomDragProcessor.CancelDragging()
        {
            Action<DockLayoutManager> action = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Action<DockLayoutManager> local1 = <>c.<>9__88_0;
                action = <>c.<>9__88_0 = x => x.Win32DragService.CancelDragging();
            }
            this.Container.Do<DockLayoutManager>(action);
        }

        void ICustomDragProcessor.StartDragging()
        {
            if (!base.Adapter.IsInEvent)
            {
                Action<DockLayoutManager> action = <>c.<>9__89_0;
                if (<>c.<>9__89_0 == null)
                {
                    Action<DockLayoutManager> local1 = <>c.<>9__89_0;
                    action = <>c.<>9__89_0 = x => x.Win32DragService.TryStartDragging(true);
                }
                this.Container.Do<DockLayoutManager>(action);
            }
        }

        protected override void EndProcessEvent(object ea)
        {
            this.LastProcessedEvent = null;
            base.EndProcessEvent(ea);
        }

        protected internal virtual AdornerWindow GetAdornerWindow() => 
            new AdornerWindow(this, this.Container);

        protected override ServiceListener GetCustomUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener
        {
            if (Equals(key, DevExpress.Xpf.Layout.Core.OperationType.Reordering))
            {
                return (new LayoutViewMDIReorderingListener() as ServiceListener);
            }
            if (Equals(key, DevExpress.Xpf.Layout.Core.OperationType.Resizing))
            {
                return (new LayoutViewMDIResizingListener() as ServiceListener);
            }
            return default(ServiceListener);
        }

        protected override ILayoutElement GetDragItemCore(ILayoutElement element) => 
            ((IDockLayoutElement) element).GetDragItem();

        protected override ILayoutElementBehavior GetElementBehaviorCore(ILayoutElement element) => 
            (element is IDockLayoutElement) ? ((IDockLayoutElement) element).GetBehavior() : null;

        protected internal virtual void Initialize(IUIElement viewUIElement)
        {
            this.RootUIElement = this.CheckRootUIElement(viewUIElement);
            if (this.RootUIElement != null)
            {
                this.Container = DockLayoutManager.GetDockLayoutManager(this.RootUIElement);
                this.RootGroup = DockLayoutManager.GetLayoutItem(this.RootUIElement) as LayoutGroup;
                if (this.visualizerAdornerHelper != null)
                {
                    this.visualizerAdornerHelper.Dispose();
                }
                this.visualizerAdornerHelper = new VisualizerAdornerHelper(this);
                base.InvalidateZOrder();
            }
            this.resizingWindowHelper = new DevExpress.Xpf.Docking.Platform.ResizingWindowHelper(this);
            this.SubscribeMouseEvents(this.RootUIElement);
        }

        internal void OnDesignTimeEvent(object sender, RoutedEventArgs e)
        {
            if (!base.IsDisposing)
            {
                this.mouseEventSubscriber.OnDesignTimeEvent(sender, e);
            }
        }

        protected override void OnDispose()
        {
            this.UnSubscribeMouseEvents(this.RootUIElement);
            this.ReleaseCaptureCore();
            Ref.Dispose<VisualizerAdornerHelper>(ref this.visualizerAdornerHelper);
            Ref.Dispose<DevExpress.Xpf.Docking.Platform.ResizingWindowHelper>(ref this.resizingWindowHelper);
            this.Container = null;
            this.RootUIElement = null;
            this.RootElement = null;
            this.RootGroup = null;
            base.OnDispose();
        }

        private bool RaiseStartDocking(ILayoutElement dragItem)
        {
            BaseLayoutItem item = ((IDockLayoutElement) dragItem).Item;
            return this.Container.RaiseItemCancelEvent(item, DockLayoutManager.DockItemStartDockingEvent);
        }

        protected virtual void RegisterListeners()
        {
            base.RegisterUIServiceListener(new LayoutViewRegularDragListener());
            base.RegisterUIServiceListener(new LayoutViewFloatingDragListener());
            base.RegisterUIServiceListener(new LayoutViewReorderingListener());
            base.RegisterUIServiceListener(new LayoutViewClientDraggingListener());
            base.RegisterUIServiceListener(new LayoutViewNonClientDraggingListener());
            base.RegisterUIServiceListener(new LayoutViewUIInteractionListener());
            base.RegisterUIServiceListener(new LayoutViewSelectionListener());
            base.RegisterUIServiceListener(new LayoutViewActionListener());
            base.RegisterUIServiceListener(new LayoutViewContextActionServiceListener());
        }

        protected override void ReleaseCaptureCore()
        {
            using (this.mouseCaptureLocker.Lock())
            {
                this.UnsubscribeKeyboardEvent();
                if (this.RootUIElement != null)
                {
                    this.RootUIElement.ReleaseMouseCapture();
                }
            }
        }

        protected override ILayoutElementBehavior ResolveDefaultBehavior() => 
            new EmptyBehavior();

        protected override ILayoutElementFactory ResolveDefaultFactory() => 
            new LayoutElementFactory();

        internal void RootUIElementKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsDown)
            {
                base.OnKeyDown(e.Key);
            }
            if (e.IsUp)
            {
                base.OnKeyUp(e.Key);
            }
            if ((base.Adapter.DragService.OperationType != DevExpress.Xpf.Layout.Core.OperationType.Regular) && (e.Key == Key.Tab))
            {
                e.Handled = true;
            }
        }

        public override Point ScreenToClient(Point screenPoint) => 
            CoordinateHelper.PointFromScreen(this.Container, this.RootUIElement, screenPoint);

        protected override void SetCaptureCore()
        {
            if (this.CanCaptureMouse)
            {
                this.RootUIElement.CaptureMouse();
            }
            this.SubscribeKeyboardEvent();
        }

        private void SubscribeCore(object element, IDisposable subscriber)
        {
            if (element != null)
            {
                IDisposable disposable;
                this.subscriptions ??= new Dictionary<object, IDisposable>();
                if (!this.subscriptions.TryGetValue(element, out disposable))
                {
                    this.subscriptions.Add(element, subscriber);
                }
                else
                {
                    Ref.Dispose<IDisposable>(ref disposable);
                    this.subscriptions[element] = subscriber;
                }
            }
        }

        protected void SubscribeKeyboardEvent()
        {
            if (!ReferenceEquals(this.KeyboardFocusHolder, KeyHelper.FocusedElement))
            {
                if (this.KeyboardFocusHolder != null)
                {
                    this.UnSubscribeCore(this.KeyboardFocusHolder);
                }
                this.KeyboardFocusHolder = KeyHelper.FocusedElement;
                if (this.KeyboardFocusHolder != null)
                {
                    this.SubscribeCore(this.KeyboardFocusHolder, new KeyboardEventSubscriber(this.KeyboardFocusHolder, this));
                }
            }
        }

        protected virtual void SubscribeMouseEvents(UIElement element)
        {
            MouseEventSubscriber subscriber1 = new MouseEventSubscriber(element, this);
            subscriber1.Root = this.RootUIElement;
            this.mouseEventSubscriber = subscriber1;
            this.SubscribeCore(element, this.mouseEventSubscriber);
        }

        private void UnSubscribeCore(object element)
        {
            IDisposable disposable;
            if (((element != null) && (this.subscriptions != null)) && this.subscriptions.TryGetValue(element, out disposable))
            {
                Ref.Dispose<IDisposable>(ref disposable);
                this.subscriptions.Remove(element);
            }
        }

        protected void UnsubscribeKeyboardEvent()
        {
            if (this.KeyboardFocusHolder != null)
            {
                this.UnSubscribeCore(this.KeyboardFocusHolder);
                this.KeyboardFocusHolder = null;
            }
        }

        protected virtual void UnSubscribeMouseEvents(UIElement element)
        {
            this.UnSubscribeCore(element);
        }

        public VisualizerAdornerHelper AdornerHelper =>
            this.visualizerAdornerHelper;

        public DockLayoutManager Container { get; private set; }

        public override bool IsActiveAndCanProcessEvent =>
            base.IsActiveAndCanProcessEvent && (base.ZOrder != -1);

        public bool IsAdornerHelperInitialized =>
            this.visualizerAdornerHelper != null;

        public DevExpress.Xpf.Docking.Platform.ResizingWindowHelper ResizingWindowHelper =>
            this.resizingWindowHelper;

        public IUIElement RootElement { get; private set; }

        public LayoutGroup RootGroup { get; private set; }

        public override object RootKey =>
            this.RootElement;

        public UIElement RootUIElement { get; private set; }

        public override HostType Type =>
            HostType.Layout;

        protected virtual bool CanCaptureMouse =>
            true;

        protected IInputElement KeyboardFocusHolder { get; private set; }

        Rect IAdornerWindowClient.Bounds =>
            this.CalcBounds();

        internal object LastProcessedEvent { get; set; }

        bool ICustomDragProcessor.IsInEvent
        {
            get
            {
                Func<DockLayoutManager, bool> evaluator = <>c.<>9__87_0;
                if (<>c.<>9__87_0 == null)
                {
                    Func<DockLayoutManager, bool> local1 = <>c.<>9__87_0;
                    evaluator = <>c.<>9__87_0 = x => x.Win32DragService.IsInEvent;
                }
                return this.Container.Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__87_1 ??= () => false));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutView.<>c <>9 = new LayoutView.<>c();
            public static Func<DockLayoutManager, bool> <>9__87_0;
            public static Func<bool> <>9__87_1;
            public static Action<DockLayoutManager> <>9__88_0;
            public static Action<DockLayoutManager> <>9__89_0;

            internal void <DevExpress.Xpf.Docking.ICustomDragProcessor.CancelDragging>b__88_0(DockLayoutManager x)
            {
                x.Win32DragService.CancelDragging();
            }

            internal bool <DevExpress.Xpf.Docking.ICustomDragProcessor.get_IsInEvent>b__87_0(DockLayoutManager x) => 
                x.Win32DragService.IsInEvent;

            internal bool <DevExpress.Xpf.Docking.ICustomDragProcessor.get_IsInEvent>b__87_1() => 
                false;

            internal void <DevExpress.Xpf.Docking.ICustomDragProcessor.StartDragging>b__89_0(DockLayoutManager x)
            {
                x.Win32DragService.TryStartDragging(true);
            }
        }
    }
}

