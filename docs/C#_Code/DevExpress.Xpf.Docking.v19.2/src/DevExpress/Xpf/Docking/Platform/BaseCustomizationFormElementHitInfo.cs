namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BaseCustomizationFormElementHitInfo : DockLayoutElementHitInfo
    {
        public BaseCustomizationFormElementHitInfo(Point pt, DockLayoutElement element, ILayoutController controller) : base(pt, element)
        {
            this.Controller = controller;
        }

        public ILayoutController Controller { get; private set; }

        public override bool InDragBounds =>
            base.IsCustomization && base.InBounds;

        public override bool InMenuBounds =>
            base.InBounds;
    }
}

