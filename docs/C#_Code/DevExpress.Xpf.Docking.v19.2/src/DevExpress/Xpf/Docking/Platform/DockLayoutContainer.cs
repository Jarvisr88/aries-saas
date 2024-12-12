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
    using System.Windows.Controls;
    using System.Windows.Media;

    public class DockLayoutContainer : BaseLayoutContainer, IDockLayoutContainer, IDockLayoutElement, ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>, IDragSource, IDropTarget, ILayoutContainer, ISelectionKey
    {
        private readonly LayoutItemType itemTypeCore;
        private object viewKey;

        public DockLayoutContainer(LayoutItemType itemType, UIElement uiElement, UIElement view)
        {
            this.itemTypeCore = itemType;
            this.Element = uiElement;
            this.viewKey = view;
            this.View = this.CheckView(view);
            BaseLayoutItem item1 = uiElement as BaseLayoutItem;
            BaseLayoutItem layoutItem = item1;
            if (item1 == null)
            {
                BaseLayoutItem local1 = item1;
                layoutItem = DockLayoutManager.GetLayoutItem(uiElement);
            }
            this.Item = layoutItem;
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
            false;

        public virtual bool AcceptReordering(DockLayoutElementDragInfo dragInfo) => 
            true;

        public virtual BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point) => 
            DropTypeHelper.CalcCenterDropInfo(new Rect(base.Location, base.Size), point, 0.0);

        protected override void CalcHitInfoCore(LayoutElementHitInfo hitInfo)
        {
            HitTestResult hitResult = this.GetHitResult(hitInfo);
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
            hitInfo.CheckAndSetHitTest(HitTestType.CloseButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeE, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeN, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeS, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeW, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeNE, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeNW, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeSE, hitType, LayoutElementHitTest.Border);
            hitInfo.CheckAndSetHitTest(HitTestType.SizeSW, hitType, LayoutElementHitTest.Border);
        }

        public virtual IDockLayoutElement CheckDragElement() => 
            this;

        protected virtual UIElement CheckView(UIElement view)
        {
            FloatPanePresenter presenter = view as FloatPanePresenter;
            return ((presenter == null) ? view : presenter.Element);
        }

        protected virtual bool CheckVisualHitTestCore(Point pt) => 
            HitTestHelper.CheckVisualHitTest(this, pt);

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

        public virtual Rect GetHeadersPanelBounds() => 
            Rect.Empty;

        protected virtual HitTestResult GetHitResult(LayoutElementHitInfo hitInfo) => 
            HitTestHelper.GetHitResult(this.Element, DockLayoutElementHelper.GetElementPoint(hitInfo));

        public virtual Rect GetSelectedPageBounds() => 
            Rect.Empty;

        protected override bool HitEquals(object prevHitResult, object hitResult) => 
            HitTestHelper.HitTestTypeEquals(prevHitResult, hitResult);

        protected override bool HitTestCore(Point pt) => 
            base.HitTestCore(pt) && this.CheckVisualHitTestCore(pt);

        protected override object InitHotState() => 
            ControlBox.GetHotButton(this.Element);

        protected override object InitPressedState() => 
            ControlBox.GetPressedButton(this.Element);

        public sealed override void ResetState()
        {
            base.ResetState();
            this.ResetStateCore();
        }

        protected virtual void ResetStateCore()
        {
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

        public virtual bool IsTabContainer =>
            false;

        public virtual bool HasHeadersPanel =>
            this.IsTabContainer;

        public virtual bool IsHorizontalHeaders =>
            false;

        public virtual Dock TabHeaderLocation =>
            Dock.Top;

        public override bool IsActive =>
            (this.Item != null) && this.Item.IsActive;

        public override bool IsEnabled =>
            (this.Item == null) || this.Item.IsEnabled;

        public virtual bool AllowActivate =>
            (this.Item != null) && this.Item.AllowActivate;
    }
}

