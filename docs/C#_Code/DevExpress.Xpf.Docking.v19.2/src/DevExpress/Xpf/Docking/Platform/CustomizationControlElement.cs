namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class CustomizationControlElement : DockLayoutContainer
    {
        public CustomizationControlElement(UIElement uiElement, UIElement view) : base(LayoutItemType.CustomizationControl, uiElement, view)
        {
        }

        public override BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point) => 
            null;

        public override ILayoutElementBehavior GetBehavior() => 
            null;
    }
}

