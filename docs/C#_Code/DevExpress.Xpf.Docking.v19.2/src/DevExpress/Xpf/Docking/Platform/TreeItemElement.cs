namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class TreeItemElement : DockLayoutElement
    {
        public TreeItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.TreeItem, uiElement, view)
        {
        }

        public override bool AcceptDragSource(DockLayoutElementDragInfo dragInfo) => 
            !ReferenceEquals(dragInfo.Item, dragInfo.Target) ? base.AcceptDragSource(dragInfo) : false;

        public override bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo)
        {
            if (dragInfo.Target == null)
            {
                return false;
            }
            if ((dragInfo.Item is LayoutGroup) && LayoutItemsHelper.IsParent(dragInfo.Target, dragInfo.Item))
            {
                return false;
            }
            LayoutItemType itemType = dragInfo.Target.ItemType;
            return (LayoutItemsHelper.IsLayoutItem(dragInfo.Target) || (itemType == LayoutItemType.Group));
        }

        public override BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point)
        {
            LayoutItemType itemType = base.Item.ItemType;
            if (itemType == LayoutItemType.Group)
            {
                return DropTypeHelper.CalcCenterDropInfo(ElementHelper.GetRect(this), point, 0.4);
            }
            if (itemType != LayoutItemType.ControlItem)
            {
                switch (itemType)
                {
                    case LayoutItemType.FixedItem:
                    case LayoutItemType.LayoutSplitter:
                    case LayoutItemType.EmptySpaceItem:
                    case LayoutItemType.Separator:
                    case LayoutItemType.Label:
                        break;

                    default:
                        return base.CalcDropInfo(item, point);
                }
            }
            return DropTypeHelper.CalcCenterDropInfo(ElementHelper.GetRect(this), point, 0.0);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new TreeItemElementHitInfo(pt, this, LayoutControllerHelper.GetLayoutController(base.Element));

        public override ILayoutElementBehavior GetBehavior() => 
            new TreeItemElementBehavior(this);

        protected override BaseLayoutItem GetItem(UIElement uiElement) => 
            ((TreeItem) uiElement).LayoutItem;
    }
}

