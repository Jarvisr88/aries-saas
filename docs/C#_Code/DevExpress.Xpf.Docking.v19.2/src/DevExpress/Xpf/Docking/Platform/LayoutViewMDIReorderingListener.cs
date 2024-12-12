namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class LayoutViewMDIReorderingListener : ReorderingListener
    {
        private MDILocationHelper helper;
        private MDIDocumentElement document;

        public override bool CanDrop(Point point, ILayoutElement element) => 
            true;

        public override void OnBegin(Point point, ILayoutElement element)
        {
            this.document = (MDIDocumentElement) element;
            this.helper = new MDILocationHelper(this.View, this.document);
        }

        public override void OnCancel()
        {
            this.helper = null;
            this.document = null;
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            Point screenPoint = this.View.ClientToScreen(point);
            Point point3 = this.helper.CalcLocation(screenPoint);
            DocumentPanel.SetMDILocation(this.document.Item, point3);
            if (((DocumentPanel) this.document.Item).IsMinimized)
            {
                ((DocumentPanel) this.document.Item).MinimizeLocation = new Point?(point3);
            }
            NotificationBatch.Action(this.View.Container, this.document.Item, DocumentPanel.MDILocationProperty);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            this.RaiseDockOperationCompleted(element);
            this.helper = null;
            this.document = null;
        }

        private void RaiseDockOperationCompleted(ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            this.View.Container.RaiseDockOperationCompletedEvent(DockOperation.Move, item);
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

