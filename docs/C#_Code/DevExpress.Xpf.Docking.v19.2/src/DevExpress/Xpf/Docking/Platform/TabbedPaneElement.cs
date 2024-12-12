namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class TabbedPaneElement : DockLayoutContainer
    {
        private bool isTabContainerCore;
        private DevExpress.Xpf.Docking.VisualElements.TabbedPane pane;
        private TabHeadersPanel headersPanelCore;
        private UIElement selectedPageCore;

        public TabbedPaneElement(UIElement uiElement, UIElement view) : base(LayoutItemType.TabPanelGroup, uiElement, view)
        {
            this.isTabContainerCore = ((this.TabbedPane != null) && (this.TabbedPane.PartContent != null)) && (this.TabbedPane.PartContent.PartItemsContainer is PanelTabContainer);
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo) => 
            (dragInfo.Item.ItemType == LayoutItemType.Panel) || (dragInfo.Item.ItemType == LayoutItemType.TabPanelGroup);

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.PageHeaders, hitType, LayoutElementHitTest.Bounds);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollPrevButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollNextButton, hitType, LayoutElementHitTest.ControlBox);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new TabbedPaneElementHitInfo(pt, this);

        public override ILayoutElementBehavior GetBehavior() => 
            new TabbedPaneElementBehavior(this);

        public override Rect GetHeadersPanelBounds() => 
            new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, this.HeadersPanel), this.HeadersPanel.RenderSize);

        public override Rect GetSelectedPageBounds() => 
            new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, this.SelectedPage), this.SelectedPage.RenderSize);

        protected DevExpress.Xpf.Docking.VisualElements.TabbedPane TabbedPane
        {
            get
            {
                this.pane ??= ((base.Element is DevExpress.Xpf.Docking.VisualElements.TabbedPane) ? ((DevExpress.Xpf.Docking.VisualElements.TabbedPane) base.Element) : LayoutItemsHelper.GetVisualChild<DevExpress.Xpf.Docking.VisualElements.TabbedPane>(base.Element));
                return this.pane;
            }
        }

        protected TabHeadersPanel HeadersPanel
        {
            get
            {
                this.headersPanelCore ??= (this.TabbedPane.PartContent.PartItemsContainer as PanelTabContainer).PartHeadersPanel;
                return this.headersPanelCore;
            }
        }

        protected UIElement SelectedPage
        {
            get
            {
                this.selectedPageCore ??= (this.TabbedPane.PartContent.PartItemsContainer as PanelTabContainer).PartSelectedPage;
                return this.selectedPageCore;
            }
        }

        public override bool IsTabContainer =>
            this.isTabContainerCore;

        public override bool IsHorizontalHeaders =>
            this.HeadersPanel.Orientation == Orientation.Horizontal;

        public override Dock TabHeaderLocation
        {
            get
            {
                PanelTabContainer partItemsContainer = this.TabbedPane.PartContent.PartItemsContainer as PanelTabContainer;
                return ((partItemsContainer.CaptionLocation != CaptionLocation.Default) ? partItemsContainer.CaptionLocation.ToDock(Dock.Top) : Dock.Bottom);
            }
        }

        public override bool HasHeadersPanel =>
            this.IsTabContainer && (this.HeadersPanel != null);
    }
}

