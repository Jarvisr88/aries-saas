namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class LayoutViewMDIResizingListener : ResizingListener
    {
        private static readonly Size DefaultMinSize = new Size(82.0, 42.0);
        private bool ShowPreview;
        private MDIBoundsHelper boundsHelper;
        private MDIDocumentElement document;
        private IResizingPreviewHelper resizeHelper;

        public override bool CanDrop(Point point, ILayoutElement element) => 
            true;

        public override void OnBegin(Point point, ILayoutElement element)
        {
            this.document = (MDIDocumentElement) element;
            Size[] minSizes = new Size[] { DefaultMinSize, this.document.Item.ActualMinSize };
            Size minSize = MathHelper.MeasureMinSize(minSizes);
            Size actualMaxSize = this.document.Item.ActualMaxSize;
            this.boundsHelper = new MDIBoundsHelper(this.View, this.document, minSize, actualMaxSize);
            this.View.RootUIElement.SetValue(FrameworkElement.CursorProperty, this.boundsHelper.GetCursor());
            this.ShowPreview = !this.View.Container.RedrawContentWhenResizing;
            if (this.ShowPreview)
            {
                MDIDocumentResizingPreviewHelper helper1 = new MDIDocumentResizingPreviewHelper(this.View);
                helper1.MinSize = minSize;
                helper1.MaxSize = actualMaxSize;
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
            this.Reset();
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
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Resize, item);
            this.Reset();
        }

        private void Reset()
        {
            this.View.RootUIElement.ClearValue(FrameworkElement.CursorProperty);
            this.boundsHelper = null;
            this.document = null;
        }

        private void ResizeCore(Point point)
        {
            Point screenPoint = this.View.ClientToScreen(point);
            Rect rect = this.boundsHelper.CalcBounds(screenPoint);
            DocumentPanel.SetMDISize(this.document.Item, rect.Size());
            DocumentPanel.SetMDILocation(this.document.Item, rect.Location());
            NotificationBatch.Action(this.View.Container, this.document.Item, DocumentPanel.MDILocationProperty);
            NotificationBatch.Action(this.View.Container, this.document.Item, DocumentPanel.MDISizeProperty);
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

