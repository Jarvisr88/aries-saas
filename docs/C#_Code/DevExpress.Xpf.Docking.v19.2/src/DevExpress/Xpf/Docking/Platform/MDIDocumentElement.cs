namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class MDIDocumentElement : DockLayoutContainer
    {
        public MDIDocumentElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Document, uiElement, view)
        {
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo) => 
            dragInfo.Item.ItemType == LayoutItemType.Document;

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.MaximizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.MinimizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new MDIDocumentElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new MDIDocumentElementBehavior(this);
    }
}

