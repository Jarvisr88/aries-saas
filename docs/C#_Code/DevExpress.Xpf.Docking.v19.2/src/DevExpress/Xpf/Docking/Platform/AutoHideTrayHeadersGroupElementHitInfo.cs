namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class AutoHideTrayHeadersGroupElementHitInfo : DockLayoutElementHitInfo
    {
        public AutoHideTrayHeadersGroupElementHitInfo(Point point, AutoHideTrayHeadersGroupElement element) : base(point, element)
        {
        }

        public override bool InReorderingBounds =>
            base.InBounds;
    }
}

