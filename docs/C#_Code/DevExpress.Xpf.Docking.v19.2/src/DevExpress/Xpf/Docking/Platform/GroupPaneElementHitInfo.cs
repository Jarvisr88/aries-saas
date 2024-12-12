namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class GroupPaneElementHitInfo : DockLayoutElementHitInfo
    {
        public GroupPaneElementHitInfo(Point point, GroupPaneElement element) : base(point, element)
        {
        }

        public override bool InMenuBounds =>
            base.InBounds;

        public override bool InDragBounds =>
            base.InDragBounds || (base.IsCustomization && base.InBounds);
    }
}

