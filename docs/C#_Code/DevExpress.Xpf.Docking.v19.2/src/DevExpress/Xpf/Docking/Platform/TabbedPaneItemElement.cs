namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class TabbedPaneItemElement : DockLayoutElement
    {
        public TabbedPaneItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Panel, uiElement, view)
        {
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new TabbedPaneItemElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new TabbedPaneItemElementBehavior(this);

        public override bool IsPageHeader =>
            true;

        public override bool IsTabHeader =>
            true;
    }
}

