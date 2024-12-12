namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class SplitterElementHitInfo : DockLayoutElementHitInfo
    {
        public SplitterElementHitInfo(Point pt, SplitterElement element) : base(pt, element)
        {
        }

        public override bool InMenuBounds =>
            base.InBounds;
    }
}

