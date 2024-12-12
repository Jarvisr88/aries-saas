namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FloatingView : LayoutView
    {
        private readonly Locker floatingLocker;

        public FloatingView(IUIElement viewUIElement) : base(viewUIElement)
        {
            this.floatingLocker = new Locker();
        }

        internal void BeginFloating()
        {
            this.floatingLocker.Lock();
        }

        protected override bool CanSuspendFloatingMoving(ILayoutElement dragItem)
        {
            if (base.Container.EnableNativeDragging)
            {
                return false;
            }
            BaseLayoutItem item = ((IDockLayoutElement) dragItem).CheckDragElement().Item;
            return base.Container.RaiseDockOperationStartingEvent(DockOperation.Move, item, null);
        }

        protected override bool CanUseCustomServiceListener(object key)
        {
            bool flag = Equals(key, typeof(IUIInteractionServiceListener)) || Equals(key, DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving);
            if (base.CanUseCustomServiceListener(key))
            {
                return true;
            }
            if (!flag)
            {
                return false;
            }
            Func<DockLayoutManager, bool> evaluator = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<DockLayoutManager, bool> local1 = <>c.<>9__19_0;
                evaluator = <>c.<>9__19_0 = x => x.EnableNativeDragging;
            }
            return base.Container.Return<DockLayoutManager, bool>(evaluator, (<>c.<>9__19_1 ??= () => false));
        }

        protected override UIElement CheckRootUIElement(IUIElement viewUIElement) => 
            ((FloatPanePresenter) viewUIElement).Element;

        internal void EndFloating()
        {
            this.floatingLocker.Unlock();
        }

        protected internal void EnterReordering()
        {
            base.RootUIElement.Opacity = 0.9;
        }

        protected internal override AdornerWindow GetAdornerWindow()
        {
            FloatingWindowPresenter rootKey = this.RootKey as FloatingWindowPresenter;
            return rootKey?.EnsureAdornerWindow();
        }

        protected override ServiceListener GetCustomUIServiceListener<ServiceListener>(object key) where ServiceListener: class, IUIServiceListener => 
            !Equals(key, typeof(IUIInteractionServiceListener)) ? (!Equals(key, DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving) ? base.GetCustomUIServiceListener<ServiceListener>(key) : (new FloatingViewNativeFloatingMovingListener() as ServiceListener)) : (new FloatingViewNativeInteractionListener() as ServiceListener);

        protected internal void LeaveReordering()
        {
            UIElement rootUIElement = base.RootUIElement;
            if (rootUIElement == null)
            {
                UIElement local1 = rootUIElement;
            }
            else
            {
                rootUIElement.ClearValue(UIElement.OpacityProperty);
            }
        }

        protected override void RegisterListeners()
        {
            base.RegisterListeners();
            base.RegisterUIServiceListener(new FloatingViewFloatingMovingListener());
            base.RegisterUIServiceListener(new FloatingViewFloatingResizingListener());
            base.RegisterUIServiceListener(new FloatingViewUIInteractionListener());
        }

        protected override void ReleaseCaptureCore()
        {
            base.ReleaseCaptureCore();
            base.ResizingWindowHelper.Reset();
        }

        protected override ILayoutElementFactory ResolveDefaultFactory() => 
            new FloatLayoutElementFactory();

        public override Point ScreenToClient(Point screenPoint)
        {
            if (!base.IsDisposing)
            {
                return base.ScreenToClient(screenPoint);
            }
            return new Point();
        }

        protected internal void SetFloatingBounds(Rect screenRect)
        {
            this.FloatGroup.FloatLocation = screenRect.Location();
            this.FloatGroup.FitSizeToContent(screenRect.Size());
            base.RootUIElement.UpdateLayout();
        }

        protected internal void SetFloatLocation(Point screenLocation)
        {
            if (!base.Container.RaiseDockItemDraggingEvent(this.FloatGroup, screenLocation))
            {
                this.FloatGroup.FloatLocation = screenLocation;
            }
        }

        public DevExpress.Xpf.Docking.FloatGroup FloatGroup =>
            base.RootGroup as DevExpress.Xpf.Docking.FloatGroup;

        public override HostType Type =>
            HostType.Floating;

        internal bool IsFloating =>
            (bool) this.floatingLocker;

        protected override bool CanCaptureMouse =>
            !this.IsFloating || !base.Container.EnableNativeDragging;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingView.<>c <>9 = new FloatingView.<>c();
            public static Func<DockLayoutManager, bool> <>9__19_0;
            public static Func<bool> <>9__19_1;

            internal bool <CanUseCustomServiceListener>b__19_0(DockLayoutManager x) => 
                x.EnableNativeDragging;

            internal bool <CanUseCustomServiceListener>b__19_1() => 
                false;
        }
    }
}

