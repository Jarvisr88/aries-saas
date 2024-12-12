namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class HiddenItemElement : DockLayoutElement
    {
        public HiddenItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.HiddenItem, uiElement, view)
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

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new HiddenItemElementHitInfo(pt, this, LayoutControllerHelper.GetLayoutController(base.Element));

        public override ILayoutElementBehavior GetBehavior() => 
            new HiddenItemElementBehavior(this);

        protected override BaseLayoutItem GetItem(UIElement uiElement) => 
            ((HiddenItem) uiElement).LayoutItem;
    }
}

