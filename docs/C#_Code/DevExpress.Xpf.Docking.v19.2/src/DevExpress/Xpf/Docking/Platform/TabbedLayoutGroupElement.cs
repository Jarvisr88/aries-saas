namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class TabbedLayoutGroupElement : DockLayoutContainer
    {
        private bool isTabContainerCore;
        private TabbedLayoutGroupPane pane;

        public TabbedLayoutGroupElement(UIElement uiElement, UIElement view) : base(LayoutItemType.Group, uiElement, view)
        {
            this.isTabContainerCore = (this.TabbedPane != null) && (this.TabbedPane.PartHeadersPanel != null);
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo)
        {
            BaseLayoutItem item = dragInfo.Item;
            LayoutGroup group = item as LayoutGroup;
            return ((dragInfo.DragSource is TabbedLayoutGroupHeaderElement) || (LayoutItemsHelper.IsLayoutItem(item) || ((group != null) && group.IsControlItemsHost)));
        }

        public override BaseDropInfo CalcDropInfo(BaseLayoutItem item, Point point)
        {
            TabHeaderInsertHelper helper = new TabHeaderInsertHelper(this, point, true);
            return ((helper.TabIndex == -1) ? base.CalcDropInfo(item, point) : new TabbedLayoutGroupElementDropInfo(this, point, helper.TabIndex));
        }

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.PageHeaders, hitType, LayoutElementHitTest.Bounds);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollNextButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollPrevButton, hitType, LayoutElementHitTest.ControlBox);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new TabbedLayoutGroupElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new TabbedLayoutGroupElementBehaviour(this);

        public override Rect GetHeadersPanelBounds()
        {
            UIElement partHeadersPanel = this.TabbedPane.PartHeadersPanel;
            return new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, partHeadersPanel), partHeadersPanel.RenderSize);
        }

        public override Rect GetSelectedPageBounds()
        {
            UIElement partSelectedPage = this.TabbedPane.PartSelectedPage;
            return new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, partSelectedPage), partSelectedPage.RenderSize);
        }

        protected TabbedLayoutGroupPane TabbedPane
        {
            get
            {
                this.pane ??= ((base.Element is TabbedLayoutGroupPane) ? ((TabbedLayoutGroupPane) base.Element) : LayoutItemsHelper.GetVisualChild<TabbedLayoutGroupPane>(base.Element));
                return this.pane;
            }
        }

        public override bool IsTabContainer =>
            this.isTabContainerCore;

        public override bool IsHorizontalHeaders =>
            this.TabbedPane.PartHeadersPanel.Orientation == Orientation.Horizontal;

        public override Dock TabHeaderLocation =>
            (this.TabbedPane.CaptionLocation != CaptionLocation.Default) ? this.TabbedPane.CaptionLocation.ToDock(Dock.Top) : Dock.Top;

        private class TabbedLayoutGroupElementDropInfo : BaseDropInfo
        {
            private bool isHorzontal;
            private bool isVertical;
            private DropType type;
            private int insertIndex;

            public TabbedLayoutGroupElementDropInfo(TabbedLayoutGroupElement group, Point pt, int insertIndex) : base(group.Bounds, pt)
            {
                this.isHorzontal = group.IsHorizontalHeaders;
                this.isVertical = !this.isHorzontal;
                this.type = DropType.Center;
                this.insertIndex = insertIndex;
            }

            public override DropType Type =>
                this.type;

            public override bool Horizontal =>
                this.isHorzontal;

            public override bool Vertical =>
                this.isVertical;

            public override Rect DropRect =>
                Rect.Empty;

            public override int InsertIndex =>
                this.insertIndex;
        }
    }
}

