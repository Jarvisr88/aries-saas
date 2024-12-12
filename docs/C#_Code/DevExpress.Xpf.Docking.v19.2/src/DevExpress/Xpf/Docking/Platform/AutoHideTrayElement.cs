namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class AutoHideTrayElement : DockLayoutContainer
    {
        public AutoHideTrayElement(UIElement uiElement, UIElement view) : base(LayoutItemType.AutoHideContainer, uiElement, view)
        {
        }

        protected override UIElement CheckView(UIElement view) => 
            view;

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new AutoHideTrayElementHitInfo(pt, this);

        protected override void EnsureBoundsCore()
        {
            this.Size = (base.Element.Visibility == Visibility.Visible) ? base.Element.RenderSize : new Size(0.0, 0.0);
            base.Location = CoordinateHelper.ZeroPoint;
        }

        public override ILayoutElementBehavior GetBehavior() => 
            new AutoHideTrayElementBehavior(this);

        public AutoHideTray Tray =>
            base.Element as AutoHideTray;
    }
}

