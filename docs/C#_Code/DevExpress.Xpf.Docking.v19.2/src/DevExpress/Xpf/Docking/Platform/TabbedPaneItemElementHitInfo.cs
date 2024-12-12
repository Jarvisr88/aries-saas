namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class TabbedPaneItemElementHitInfo : DockLayoutElementHitInfo
    {
        public TabbedPaneItemElementHitInfo(Point point, TabbedPaneItemElement element) : base(point, element)
        {
        }

        public override bool InDragBounds =>
            false;

        public override bool InReorderingBounds =>
            base.InBounds;
    }
}

