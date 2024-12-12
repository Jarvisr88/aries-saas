namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class LayoutSplitterElementHitInfo : BaseCustomizationFormElementHitInfo
    {
        public LayoutSplitterElementHitInfo(Point pt, LayoutSplitterElement element, ILayoutController controller) : base(pt, element, controller)
        {
        }
    }
}

