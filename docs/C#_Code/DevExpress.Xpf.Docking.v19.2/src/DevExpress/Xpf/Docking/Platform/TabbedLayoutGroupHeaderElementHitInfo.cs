namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class TabbedLayoutGroupHeaderElementHitInfo : DockLayoutElementHitInfo
    {
        public TabbedLayoutGroupHeaderElementHitInfo(Point point, TabbedLayoutGroupHeaderElement element) : base(point, element)
        {
        }

        public override bool InDragBounds =>
            false;

        public override bool InReorderingBounds =>
            base.InBounds;
    }
}

