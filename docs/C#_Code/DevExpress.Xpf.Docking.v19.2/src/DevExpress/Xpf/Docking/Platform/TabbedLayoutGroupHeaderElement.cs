namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class TabbedLayoutGroupHeaderElement : DockLayoutElement
    {
        public TabbedLayoutGroupHeaderElement(UIElement uiElement, UIElement view) : base(LayoutItemType.TabItem, uiElement, view)
        {
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new TabbedLayoutGroupHeaderElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new TabbedLayoutGroupHeaderElementBehavior(this);

        public override bool IsPageHeader =>
            true;

        public override bool IsTabHeader =>
            true;
    }
}

