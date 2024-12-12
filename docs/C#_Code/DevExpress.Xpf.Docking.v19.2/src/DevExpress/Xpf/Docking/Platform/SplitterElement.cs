namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class SplitterElement : DockLayoutElement
    {
        public SplitterElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Splitter, uiElement, view)
        {
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new SplitterElementHitInfo(pt, this);

        protected override BaseLayoutItem GetItem(UIElement uiElement) => 
            ((Splitter) uiElement).LayoutGroup;

        public override bool AllowActivate =>
            false;
    }
}

