namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class CustomizationViewClientDraggingListener : LayoutViewClientDraggingListener
    {
        public override bool CanDrag(Point point, ILayoutElement element) => 
            ((IDockLayoutElement) element).Item != null;

        public override bool CanDrop(Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(base.View, point, element);
            if ((!(info.DropTarget is HiddenItemElement) && !(info.DropTarget is HiddenItemsListElement)) || !info.Item.AllowHide)
            {
                return base.CanDrop(point, element);
            }
            LayoutItemType itemType = info.Item.ItemType;
            return (LayoutItemsHelper.IsLayoutItem(info.Item) || (itemType == LayoutItemType.Group));
        }

        private bool IsLayoutRoot(BaseLayoutItem item) => 
            (item is LayoutGroup) && ((LayoutGroup) item).IsLayoutRoot;

        public override unsafe void OnDragging(Point point, ILayoutElement element)
        {
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(base.View, point, element);
            ILayoutElement dropTarget = (ILayoutElement) info.DropTarget;
            Point point2 = base.View.ClientToScreen(point);
            if (!this.IsLayoutRoot(info.Target) && ((info.DropTarget is TreeItemElement) && (info.MoveType != MoveType.None)))
            {
                Rect screenRect = ElementHelper.GetScreenRect(base.View, dropTarget);
                point2 = new CursorLocationHelper(160, 40).CorrectPosition(screenRect, info.MoveType);
                if (base.View.Container.FlowDirection == FlowDirection.RightToLeft)
                {
                    Point* pointPtr1 = &point2;
                    pointPtr1.X += 160.0;
                }
            }
            base.View.Container.CustomizationController.SetDragCursorPosition(point2);
        }

        public override void OnDrop(Point point, ILayoutElement element)
        {
            base.ResetVisualization();
            DockLayoutElementDragInfo info = new DockLayoutElementDragInfo(base.View, point, element);
            if ((!(info.DropTarget is HiddenItemElement) && !(info.DropTarget is HiddenItemsListElement)) || !info.Item.AllowHide)
            {
                base.OnDrop(point, element);
            }
            else
            {
                base.View.Container.LayoutController.Hide(info.Item);
            }
        }
    }
}

