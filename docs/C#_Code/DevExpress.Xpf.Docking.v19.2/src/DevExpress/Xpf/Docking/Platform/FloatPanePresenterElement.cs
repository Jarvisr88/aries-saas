namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class FloatPanePresenterElement : DockLayoutContainer
    {
        private IDockLayoutElement nestedElementCore;

        public FloatPanePresenterElement(UIElement uiElement, UIElement view) : base(LayoutItemType.FloatGroup, uiElement, view)
        {
        }

        public override bool AcceptDockSource(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo)
        {
            IDockLayoutElement nestedElement = this.GetNestedElement();
            return ((nestedElement != null) && nestedElement.AcceptDockSource(dragInfo, hitInfo));
        }

        public override bool AcceptDockTarget(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo)
        {
            IDockLayoutElement nestedElement = this.GetNestedElement();
            return ((nestedElement == null) ? (LayoutItemsHelper.IsDockItem(dragInfo.Target) || LayoutItemsHelper.IsEmptyLayoutGroup(dragInfo.Target)) : nestedElement.AcceptDockTarget(dragInfo, hitInfo));
        }

        public override bool AcceptDragSource(DockLayoutElementDragInfo dragInfo)
        {
            IDockLayoutElement nestedElement = this.GetNestedElement();
            return ((nestedElement != null) && nestedElement.AcceptDragSource(dragInfo));
        }

        public override bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo)
        {
            IDockLayoutElement nestedElement = this.GetNestedElement();
            return ((nestedElement != null) && nestedElement.AcceptDropTarget(dragInfo));
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo)
        {
            IDockLayoutElement nestedElement = this.GetNestedElement();
            return ((nestedElement != null) && nestedElement.AcceptFill(dragInfo));
        }

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.MaximizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.MinimizeButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
        }

        public override IDockLayoutElement CheckDragElement() => 
            !((FloatGroup) base.Item).HasSingleItem ? this : this.GetNestedElement();

        protected override void EnsureBoundsCore()
        {
            base.Location = CoordinateHelper.ZeroPoint;
            base.Size = ((FloatPanePresenter) base.Element).FloatSize;
        }

        public override ILayoutElementBehavior GetBehavior() => 
            new FloatPanePresenterElementBehavior(this);

        private UIElement GetFloatElement() => 
            ((FloatPanePresenter) base.Element).Element;

        protected override HitTestResult GetHitResult(LayoutElementHitInfo hitInfo) => 
            HitTestHelper.GetHitResult(this.GetFloatElement(), DockLayoutElementHelper.GetElementPoint(hitInfo));

        protected IDockLayoutElement GetNestedElement() => 
            !base.IsDisposing ? this.GetNestedElementCore() : this.nestedElementCore;

        private IDockLayoutElement GetNestedElementCore() => 
            (base.Nodes.Length == 1) ? ((IDockLayoutElement) base.Nodes[0]) : null;

        protected override object InitHotState() => 
            ControlBox.GetHotButton(this.GetFloatElement());

        protected override object InitPressedState() => 
            ControlBox.GetPressedButton(this.GetFloatElement());

        protected override void OnDispose()
        {
            if (base.IsDragging)
            {
                this.nestedElementCore = this.GetNestedElementCore();
            }
            base.OnDispose();
        }

        protected override void OnResetIsDragging()
        {
            if (base.IsDisposing)
            {
                this.nestedElementCore = null;
            }
            base.OnResetIsDragging();
        }

        protected override void ResetStateCore()
        {
            UIElement floatElement = this.GetFloatElement();
            if (floatElement != null)
            {
                floatElement.ClearValue(ControlBox.PressedButtonProperty);
                floatElement.ClearValue(ControlBox.HotButtonProperty);
            }
        }
    }
}

