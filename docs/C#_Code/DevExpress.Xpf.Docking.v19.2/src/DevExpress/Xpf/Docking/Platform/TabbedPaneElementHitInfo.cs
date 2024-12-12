namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class TabbedPaneElementHitInfo : DockLayoutElementHitInfo
    {
        public TabbedPaneElementHitInfo(Point point, TabbedPaneElement element) : base(point, element)
        {
        }

        public override bool InDragBounds =>
            false;

        public override bool InReorderingBounds =>
            base.InPageHeaders;
    }
}

