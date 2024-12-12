namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class FloatingViewFloatingResizingListener : FloatingResizingListener
    {
        private IResizingPreviewHelper resizeHelper;
        private BoundsHelper boundsHelper;
        private bool ShowPreview;

        private Size CalcResizingMinSize(FloatGroup fGroup) => 
            LayoutItemsHelper.GetResizingMinSize(fGroup);

        public override bool CanDrop(Point point, ILayoutElement element) => 
            true;

        public override void OnBegin(Point point, ILayoutElement element)
        {
            this.View.FloatGroup.ResetMaximized();
            this.ShowPreview = !this.View.Container.RedrawContentWhenResizing;
            if (this.ShowPreview)
            {
                FloatingResizingPreviewHelper helper1 = new FloatingResizingPreviewHelper(this.View);
                helper1.MinSize = this.CalcResizingMinSize(this.View.FloatGroup);
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
                this.ResizeCore(point);
            }
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            if (this.ShowPreview)
            {
                this.ResizeCore(point);
                this.resizeHelper.EndResizing();
            }
            this.RaiseDockOperationCompleted(element);
            this.View.RootUIElement.ClearValue(FrameworkElement.CursorProperty);
        }

        public override void OnInitialize(Point point, ILayoutElement element)
        {
            SizingAction sizingAction = HitTestTypeExtensions.ToSizingAction(this.View.Adapter.CalcHitInfo(this.View, point).HitResult);
            Size resizingMaxSize = LayoutItemsHelper.GetResizingMaxSize(this.View.FloatGroup);
            this.boundsHelper = new BoundsHelper(this.View, element, this.CalcResizingMinSize(this.View.FloatGroup), resizingMaxSize, sizingAction);
            this.View.RootUIElement.SetValue(FrameworkElement.CursorProperty, this.boundsHelper.GetCursor());
            this.View.Container.Win32DragService.SizingAction = sizingAction;
        }

        private void RaiseDockOperationCompleted(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).CheckDragElement().Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Resize, item);
        }

        private void ResizeCore(Point point)
        {
            Point screenPoint = this.View.ClientToScreen(point);
            this.View.FloatGroup.DisableSizeToContent();
            this.View.SetFloatingBounds(this.boundsHelper.CalcBounds(screenPoint));
            NotificationBatch.Action(this.View.Container, this.View.FloatGroup, FloatGroup.FloatLocationProperty);
            NotificationBatch.Action(this.View.Container, this.View.FloatGroup, BaseLayoutItem.FloatSizeProperty);
        }

        public FloatingView View =>
            base.ServiceProvider as FloatingView;
    }
}

