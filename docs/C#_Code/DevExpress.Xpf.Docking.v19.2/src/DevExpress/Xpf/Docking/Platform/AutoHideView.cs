namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class AutoHideView : LayoutView
    {
        public AutoHideView(IUIElement viewUIElement) : base(viewUIElement)
        {
        }

        protected override bool CanUseCustomServiceListener(object key) => 
            false;

        protected internal override DockingHintAdorner CreateDockingHintAdorner(UIElement adornedElement) => 
            new AutoHideDockHintAdorner(adornedElement);

        protected internal override void Initialize(IUIElement viewUIElement)
        {
            base.Initialize(viewUIElement);
            this.SubscribeMouseEvents(this.Tray.Panel);
        }

        protected override void OnDispose()
        {
            this.UnSubscribeMouseEvents(this.Tray);
            this.UnSubscribeMouseEvents(this.Tray.Panel);
            base.OnDispose();
        }

        protected override void RegisterListeners()
        {
            base.RegisterListeners();
            base.RegisterUIServiceListener(new AutoHideViewRegularDragListener());
            base.RegisterUIServiceListener(new AutoHideViewFloatingDragListener());
            base.RegisterUIServiceListener(new AutoHideViewUIInteractionListener());
            base.RegisterUIServiceListener(new AutoHideViewResizingListener());
            base.RegisterUIServiceListener(new AutoHideViewReorderingListener());
            base.RegisterUIServiceListener(new AutoHideViewActionListener());
        }

        protected override void ReleaseCaptureCore()
        {
            base.ReleaseCaptureCore();
            this.Tray.UnlockAutoHide();
            base.ResizingWindowHelper.Reset();
        }

        protected override ILayoutElementFactory ResolveDefaultFactory() => 
            new AutoHideLayoutElementFactory();

        protected override void SetCaptureCore()
        {
            this.Tray.LockAutoHide();
            base.SetCaptureCore();
        }

        protected internal void SetPanelBounds(Rect bounds)
        {
            bool flag = AutoHideTray.GetOrientation(this.Tray) == Orientation.Vertical;
            this.Tray.DoResizePanel(flag ? bounds.Width : bounds.Height);
        }

        public AutoHideTray Tray =>
            this.RootKey as AutoHideTray;

        public override HostType Type =>
            HostType.AutoHide;
    }
}

