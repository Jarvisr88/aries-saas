namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DocumentElement : DockLayoutContainer
    {
        public DocumentElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Document, uiElement, view)
        {
        }

        public override bool AcceptDockSource(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo) => 
            !(base.Parent is FloatPanePresenterElement) ? base.AcceptDockSource(dragInfo, hitInfo) : false;

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo) => 
            !(base.Parent is FloatPanePresenterElement) ? (dragInfo.Item.ItemType == LayoutItemType.Document) : false;

        public override ILayoutElementBehavior GetBehavior() => 
            new DocumentElementBehavior(this);
    }
}

