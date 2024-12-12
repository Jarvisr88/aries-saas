namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FixedItemElement : DockLayoutElement
    {
        public FixedItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.FixedItem, uiElement, view)
        {
        }

        public override bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo)
        {
            if (dragInfo.Target == null)
            {
                return false;
            }
            LayoutItemType itemType = dragInfo.Target.ItemType;
            return (LayoutItemsHelper.IsLayoutItem(dragInfo.Target) || (itemType == LayoutItemType.Group));
        }

        public override BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point) => 
            DropTypeHelper.CalcSideDropInfo(ElementHelper.GetRect(this), point, 0.25);

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new FixedItemElementHitInfo(pt, this, LayoutControllerHelper.GetLayoutController(base.Element));

        public override ILayoutElementBehavior GetBehavior() => 
            new FixedItemElementBehavior(this);
    }
}

