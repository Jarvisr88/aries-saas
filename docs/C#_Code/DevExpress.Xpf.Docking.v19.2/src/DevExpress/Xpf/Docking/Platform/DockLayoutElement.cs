namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class DockLayoutElement : BaseLayoutElement, IDockLayoutElement, ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>, IDragSource, IDropTarget, ISelectionKey
    {
        private readonly LayoutItemType itemTypeCore;
        private object viewKey;

        public DockLayoutElement(LayoutItemType itemType, UIElement uiElement, UIElement view)
        {
            this.itemTypeCore = itemType;
            this.Element = uiElement;
            this.viewKey = view;
            this.View = this.CheckView(view);
            this.Item = this.GetItem(uiElement);
        }

        public virtual bool AcceptDockSource(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo) => 
            true;

        public virtual bool AcceptDockTarget(DockLayoutElementDragInfo dragInfo, DockHintHitInfo hitInfo) => 
            true;

        public virtual bool AcceptDragSource(DockLayoutElementDragInfo dragInfo) => 
            true;

        public virtual bool AcceptDropTarget(DockLayoutElementDragInfo dragInfo) => 
            true;

        public virtual bool AcceptFill(DockLayoutElementDragInfo dragInfo) => 
            true;

        public virtual bool AcceptReordering(DockLayoutElementDragInfo dragInfo) => 
            true;

        public virtual BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point) => 
            DropTypeHelper.CalcCenterDropInfo(new Rect(base.Location, base.Size), point, 0.0);

        protected override void CalcHitInfoCore(LayoutElementHitInfo hitInfo)
        {
            HitTestResult hitResult = HitTestHelper.GetHitResult(this.View, hitInfo.HitPoint);
            if ((hitResult != null) && (hitResult.VisualHit != null))
            {
                hitInfo.Tag = hitResult;
                HitTestType hitTestType = HitTestHelper.GetHitTestType(hitResult);
                if (hitTestType != HitTestType.Undefined)
                {
                    this.CheckAndSetHitTests(hitInfo, hitTestType);
                }
            }
        }

        protected virtual void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            hitInfo.CheckAndSetHitTest(HitTestType.Border, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.Header, hitType, LayoutElementHitTest.Header);
            hitInfo.CheckAndSetHitTest(HitTestType.Label, hitType, LayoutElementHitTest.Header);
            hitInfo.CheckAndSetHitTest(HitTestType.Content, hitType, LayoutElementHitTest.Content);
            hitInfo.CheckAndSetHitTest(HitTestType.ControlBox, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.CloseButton, hitType, LayoutElementHitTest.ControlBox);
        }

        public virtual IDockLayoutElement CheckDragElement() => 
            this;

        protected virtual UIElement CheckView(UIElement view)
        {
            FloatPanePresenter presenter = view as FloatPanePresenter;
            return ((presenter == null) ? view : presenter.Element);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new DockLayoutElementHitInfo(pt, this);

        protected override void EnsureBoundsCore()
        {
            base.Size = this.Element.RenderSize;
            base.Location = this.TranslateToView(CoordinateHelper.ZeroPoint, null);
        }

        public virtual ILayoutElementBehavior GetBehavior() => 
            new DockLayoutElementBehavior(this);

        public virtual ILayoutElement GetDragItem() => 
            this;

        protected virtual BaseLayoutItem GetItem(UIElement uiElement) => 
            DockLayoutManager.GetLayoutItem(uiElement);

        protected override bool HitEquals(object prevHitResult, object hitResult) => 
            HitTestHelper.HitTestTypeEquals(prevHitResult, hitResult);

        protected override bool HitTestCore(Point pt) => 
            base.HitTestCore(pt) && HitTestHelper.CheckVisualHitTest(this, pt);

        protected override object InitHotState() => 
            ControlBox.GetHotButton(this.Element);

        protected override object InitPressedState() => 
            ControlBox.GetPressedButton(this.Element);

        public override void ResetState()
        {
            base.ResetState();
            if (this.Element != null)
            {
                this.Element.ClearValue(ControlBox.PressedButtonProperty);
                this.Element.ClearValue(ControlBox.HotButtonProperty);
            }
        }

        protected Point TranslateToView(Point point, UIElement element = null)
        {
            element ??= this.Element;
            return element.TranslatePoint(point, this.View);
        }

        object ISelectionKey.Item =>
            this.Item;

        object ISelectionKey.ElementKey =>
            this.Element;

        object ISelectionKey.ViewKey =>
            this.viewKey;

        public UIElement View { get; private set; }

        public UIElement Element { get; private set; }

        public BaseLayoutItem Item { get; private set; }

        public LayoutItemType Type =>
            this.itemTypeCore;

        public virtual bool IsPageHeader =>
            false;

        public override bool IsActive =>
            (this.Item != null) && this.Item.IsActive;

        public override bool IsEnabled =>
            (this.Item != null) && this.Item.IsEnabled;

        public virtual bool AllowActivate =>
            (this.Item != null) && this.Item.AllowActivate;
    }
}

