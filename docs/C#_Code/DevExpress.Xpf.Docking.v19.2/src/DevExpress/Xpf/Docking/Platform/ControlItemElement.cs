namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class ControlItemElement : DockLayoutContainer
    {
        public ControlItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.ControlItem, uiElement, view)
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
            DropTypeHelper.CalcSideDropInfo(base.Bounds, point, 0.25);

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new ControlItemElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new ControlItemElementBehavior(this);
    }
}

