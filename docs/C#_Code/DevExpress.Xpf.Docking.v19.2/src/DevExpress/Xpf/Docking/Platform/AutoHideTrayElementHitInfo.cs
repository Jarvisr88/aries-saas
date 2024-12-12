namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class AutoHideTrayElementHitInfo : DockLayoutElementHitInfo
    {
        public AutoHideTrayElementHitInfo(Point point, AutoHideTrayElement element) : base(point, element)
        {
        }

        public override bool InMenuBounds =>
            base.InBounds;

        public override bool InDragBounds =>
            false;
    }
}

