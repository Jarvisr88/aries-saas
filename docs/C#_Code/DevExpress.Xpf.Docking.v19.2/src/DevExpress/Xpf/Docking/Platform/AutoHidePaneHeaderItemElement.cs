namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class AutoHidePaneHeaderItemElement : DockLayoutContainer
    {
        public AutoHidePaneHeaderItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.AutoHidePanel, uiElement, view)
        {
        }

        protected override UIElement CheckView(UIElement view) => 
            view;

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new AutoHidePaneHeaderItemElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new AutoHidePaneHeaderItemElementBehavior(this);

        public override bool IsPageHeader =>
            true;

        public AutoHideTray Tray =>
            base.View as AutoHideTray;
    }
}

