namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Dragging;
    using System;
    using System.Windows;

    public class LayoutViewNonClientDraggingListener : NonClientDraggingListener
    {
        public override bool CanDrop(Point point, ILayoutElement element)
        {
            BaseLayoutItem item = ((IDockLayoutElement) element).Item;
            return (this.View.Container.ShowContentWhenDragging ? base.CanDrop(point, element) : item.AllowFloat);
        }

        public override void OnCancel()
        {
            this.View.Container.CustomizationController.HideDragCursor();
            this.ResetVisualization();
        }

        public override void OnDragging(Point point, ILayoutElement element)
        {
            this.UpdateDragCursor(element, point);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            this.View.Container.CustomizationController.HideDragCursor();
            this.ResetVisualization();
        }

        protected void ResetVisualization()
        {
            this.View.Container.CustomizationController.HideDragCursor();
            this.View.Container.CustomizationController.UpdateDragInfo(null);
        }

        protected void UpdateDragCursor(ILayoutElement element, Point point)
        {
            ICustomizationController customizationController = this.View.Container.CustomizationController;
            Point point2 = this.View.ClientToScreen(point);
            if (!customizationController.IsDragCursorVisible)
            {
                customizationController.ShowDragCursor(point2, ((IDockLayoutElement) element).Item);
            }
            else
            {
                customizationController.SetDragCursorPosition(point2);
            }
        }

        public LayoutView View =>
            base.ServiceProvider as LayoutView;
    }
}

