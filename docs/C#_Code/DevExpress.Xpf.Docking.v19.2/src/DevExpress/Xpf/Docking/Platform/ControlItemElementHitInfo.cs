namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;

    public class ControlItemElementHitInfo : DockLayoutElementHitInfo
    {
        public ControlItemElementHitInfo(Point point, ControlItemElement element) : base(point, element)
        {
        }

        public override bool InDragBounds =>
            base.InDragBounds || (base.IsCustomization && base.InBounds);

        public override bool InMenuBounds =>
            base.InMenuBounds || (base.IsCustomization && base.InBounds);
    }
}

