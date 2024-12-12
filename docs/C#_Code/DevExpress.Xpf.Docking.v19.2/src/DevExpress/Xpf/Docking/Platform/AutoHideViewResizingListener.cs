namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class AutoHideViewResizingListener : ResizingListener
    {
        private BoundsHelper boundsHelper;
        private IResizingPreviewHelper resizeHelper;
        private bool ShowPreview;
        private readonly Size minSize = new Size(50.0, 25.0);

        public override bool CanDrag(Point point, ILayoutElement element) => 
            this.View.Container.GetAutoHideResizeBounds().Contains(this.View.ClientToScreen(point));

        public override bool CanDrop(Point point, ILayoutElement element) => 
            true;

        public override void OnBegin(Point point, ILayoutElement element)
        {
            this.boundsHelper = new BoundsHelper(this.View, element, this.minSize);
            this.View.RootUIElement.SetValue(FrameworkElement.CursorProperty, this.boundsHelper.GetCursor());
            this.ShowPreview = !this.View.Container.RedrawContentWhenResizing;
            if (this.ShowPreview)
            {
                AutoHideResizingPreviewHelper helper1 = new AutoHideResizingPreviewHelper(this.View);
                helper1.MinSize = this.minSize;
                helper1.BoundsHelper = this.boundsHelper;
                this.resizeHelper = helper1;
                this.resizeHelper.InitResizing(point, element);
            }
        }

        public override void OnCancel()
        {
            if (this.ShowPreview)
            {
                this.resizeHelper.EndResizing();
            }
            this.View.RootUIElement.ClearValue(FrameworkElement.CursorProperty);
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            if (this.ShowPreview)
            {
                this.resizeHelper.Resize(point);
            }
            else
            {
                Point screenPoint = this.View.ClientToScreen(point);
                this.View.SetPanelBounds(this.boundsHelper.CalcBounds(screenPoint));
            }
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            if (this.ShowPreview)
            {
                Point screenPoint = this.View.ClientToScreen(point);
                this.View.SetPanelBounds(this.boundsHelper.CalcBounds(screenPoint));
                this.resizeHelper.EndResizing();
            }
            this.RaiseDockOperationCompleted(element);
            this.View.RootUIElement.ClearValue(FrameworkElement.CursorProperty);
        }

        public override void OnInitialize(Point point, ILayoutElement element)
        {
            base.OnInitialize(point, element);
            this.View.RootUIElement.SetValue(FrameworkElement.CursorProperty, new BoundsHelper(this.View, element, this.minSize).GetCursor());
        }

        private void RaiseDockOperationCompleted(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Resize, item);
        }

        public AutoHideView View =>
            base.ServiceProvider as AutoHideView;
    }
}

