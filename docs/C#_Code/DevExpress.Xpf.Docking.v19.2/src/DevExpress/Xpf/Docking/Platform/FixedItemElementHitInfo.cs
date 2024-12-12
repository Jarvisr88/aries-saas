namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class FixedItemElementHitInfo : BaseCustomizationFormElementHitInfo
    {
        public FixedItemElementHitInfo(Point pt, FixedItemElement element, ILayoutController controller) : base(pt, element, controller)
        {
        }
    }
}

