namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class GroupPaneElement : DockLayoutContainer
    {
        public GroupPaneElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Group, uiElement, view)
        {
        }

        public override bool AcceptDockSource(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo) => 
            base.AcceptDockSource(dragInfo, hitInfo) && ((LayoutGroup) base.Item).AcceptDock;

        public override bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo)
        {
            if (dragInfo.Target == null)
            {
                return false;
            }
            LayoutItemType itemType = dragInfo.Target.ItemType;
            return (LayoutItemsHelper.IsLayoutItem(dragInfo.Target) || (itemType == LayoutItemType.Group));
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo)
        {
            LayoutItemType itemType = dragInfo.Item.ItemType;
            return (!base.Item.IsControlItemsHost || ((itemType != LayoutItemType.Panel) && ((itemType != LayoutItemType.Document) && ((itemType != LayoutItemType.TabPanelGroup) && (itemType != LayoutItemType.FloatGroup)))));
        }

        public override BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point) => 
            ((item.ItemType == LayoutItemType.Panel) || (!item.IsControlItemsHost && !LayoutItemsHelper.IsLayoutItem(item))) ? base.CalcDropInfo(item, point) : (((base.Items.Count == 0) || (!((LayoutGroup) base.Item).HasVisibleItems || !((LayoutGroup) base.Item).HasNotCollapsedItems)) ? DropTypeHelper.CalcCenterDropInfo(base.Bounds, point, 0.6) : new GroupPaneElementDropInfo(this, point));

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            hitInfo.CheckAndSetHitTest(HitTestType.ExpandButton, hitType, LayoutElementHitTest.ControlBox);
            base.CheckAndSetHitTests(hitInfo, hitType);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new GroupPaneElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new GroupPaneElementBehavior(this);

        private class GroupPaneElementDropInfo : BaseDropInfo
        {
            private bool isHorzontal;
            private bool isVertical;
            private DropType type;

            public GroupPaneElementDropInfo(GroupPaneElement group, Point pt) : base(group.Bounds, pt)
            {
                this.type = DropType.None;
                Rect bounds = GetBounds(group.Items[0]);
                for (int i = 1; i < group.Items.Count; i++)
                {
                    bounds.Union(GetBounds(group.Items[i]));
                }
                LayoutGroup item = (LayoutGroup) group.Item;
                if ((item.GroupBorderStyle == GroupBorderStyle.NoBorder) && !item.IsLayoutRoot)
                {
                    RectHelper.Inflate(ref bounds, (double) MovingHelper.NoBorderMarginHorizontal, (double) MovingHelper.NoBorderMarginVertical);
                }
                if (pt.X > bounds.Right)
                {
                    this.type = DropType.Right;
                    this.isHorzontal = true;
                }
                else if (pt.X < bounds.Left)
                {
                    this.type = DropType.Left;
                    this.isHorzontal = true;
                }
                else if (pt.Y > bounds.Bottom)
                {
                    this.type = DropType.Bottom;
                    this.isVertical = true;
                }
                else if (pt.Y < bounds.Top)
                {
                    this.type = DropType.Top;
                    this.isVertical = true;
                }
            }

            private static Rect GetBounds(ILayoutElement e) => 
                new Rect(e.Location, e.Size);

            public override DropType Type =>
                this.type;

            public override bool Horizontal =>
                this.isHorzontal;

            public override bool Vertical =>
                this.isVertical;

            public override Rect DropRect =>
                Rect.Empty;
        }
    }
}

