namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FloatDocumentElement : DocumentElement
    {
        public FloatDocumentElement(UIElement uiElement, UIElement view) : base(uiElement, view)
        {
        }

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.MaximizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.MinimizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
        }

        public override ILayoutElementBehavior GetBehavior() => 
            new FloatDocumentElementBehavior(this);
    }
}

