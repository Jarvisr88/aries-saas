namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DocumentPaneItemElement : DockLayoutElement
    {
        public DocumentPaneItemElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Document, uiElement, view)
        {
        }

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.PinButton, hitType, LayoutElementHitTest.ControlBox);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new DocumentPaneItemElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new DocumentPaneItemElementBehavior(this);

        protected override bool HitTestBounds(Point hitPoint, Rect bounds)
        {
            RectHelper.Inflate(ref bounds, this.DragOffset);
            return base.HitTestBounds(hitPoint, bounds);
        }

        private DevExpress.Xpf.Docking.VisualElements.DocumentPaneItem DocumentPaneItem =>
            base.Element as DevExpress.Xpf.Docking.VisualElements.DocumentPaneItem;

        public Thickness DragOffset
        {
            get
            {
                DevExpress.Xpf.Docking.VisualElements.DocumentPaneItem documentPaneItem = this.DocumentPaneItem;
                if ((documentPaneItem != null) && documentPaneItem.IsSelected)
                {
                    return documentPaneItem.DragOffset;
                }
                return new Thickness();
            }
        }

        public override bool IsPageHeader =>
            true;

        public override bool IsTabHeader =>
            true;
    }
}

