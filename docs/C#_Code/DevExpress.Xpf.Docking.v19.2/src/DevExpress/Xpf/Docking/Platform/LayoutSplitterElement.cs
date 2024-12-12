namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class LayoutSplitterElement : DockLayoutElement
    {
        public LayoutSplitterElement(UIElement uiElement, UIElement view) : base(LayoutItemType.LayoutSplitter, uiElement, view)
        {
        }

        public override bool AcceptDragSource(DockLayoutElementDragInfo dragInfo) => 
            false;

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
            new LayoutSplitterElementHitInfo(pt, this, LayoutControllerHelper.GetLayoutController(base.Element));

        public override ILayoutElementBehavior GetBehavior() => 
            new LayoutSplitterElementBehavior(this);

        public override bool IsEnabled
        {
            get
            {
                LayoutSplitter item = base.Item as LayoutSplitter;
                return ((item != null) && (item.IsEnabled || item.GetIsCustomization()));
            }
        }
    }
}

