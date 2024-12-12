namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DockPaneElement : DockLayoutContainer
    {
        public DockPaneElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Panel, uiElement, view)
        {
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo)
        {
            BaseLayoutItem item = dragInfo.Item;
            LayoutItemType itemType = item.ItemType;
            return (!(item is FloatGroup) ? ((itemType == LayoutItemType.Panel) || (itemType == LayoutItemType.TabPanelGroup)) : !((FloatGroup) item).IsDocumentHost);
        }

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.PinButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.MaximizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.MinimizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.ExpandButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.CollapseButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.HideButton, hitType, LayoutElementHitTest.ControlBox);
        }

        public override ILayoutElementBehavior GetBehavior() => 
            new DockPaneElementBehavior(this);

        public override ILayoutElement GetDragItem() => 
            !(base.Item.Parent is TabbedGroup) ? this : base.Container;
    }
}

