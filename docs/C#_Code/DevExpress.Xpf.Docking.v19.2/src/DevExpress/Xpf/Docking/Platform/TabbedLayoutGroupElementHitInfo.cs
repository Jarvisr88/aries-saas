namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class TabbedLayoutGroupElementHitInfo : DockLayoutElementHitInfo
    {
        public TabbedLayoutGroupElementHitInfo(Point point, TabbedLayoutGroupElement element) : base(point, element)
        {
        }

        public override bool InDragBounds =>
            base.InBounds;

        public override bool InReorderingBounds =>
            base.InPageHeaders;
    }
}

