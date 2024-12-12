namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class AutoHidePaneHeaderItemElementHitInfo : DockLayoutElementHitInfo
    {
        public AutoHidePaneHeaderItemElementHitInfo(Point point, AutoHidePaneHeaderItemElement element) : base(point, element)
        {
        }

        public override bool InReorderingBounds =>
            base.IsDragging ? base.InBounds : base.InHeader;
    }
}

