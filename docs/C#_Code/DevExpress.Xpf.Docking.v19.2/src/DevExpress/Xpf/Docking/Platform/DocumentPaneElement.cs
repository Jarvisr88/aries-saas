namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DocumentPaneElement : DockLayoutContainer
    {
        private bool isTabContainerCore;
        private DevExpress.Xpf.Docking.VisualElements.DocumentPane pane;
        private TabHeadersPanel headersPanelCore;
        private UIElement selectedPageCore;

        public DocumentPaneElement(UIElement uiElement, UIElement view) : base(LayoutItemType.DocumentPanelGroup, uiElement, view)
        {
            this.isTabContainerCore = (this.DocumentPane != null) && (this.DocumentPane.PartContent.PartItemsContainer is DocumentTabContainer);
        }

        public override bool AcceptFill(DockLayoutElementDragInfo dragInfo) => 
            this.IsTabContainer ? dragInfo.Item.GetAllowDockToDocumentGroup() : (dragInfo.Item.ItemType == LayoutItemType.Document);

        public override bool AcceptReordering(DockLayoutElementDragInfo dragInfo) => 
            this.IsTabContainer;

        protected override void CheckAndSetHitTests(LayoutElementHitInfo hitInfo, HitTestType hitType)
        {
            base.CheckAndSetHitTests(hitInfo, hitType);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollPrevButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.ScrollNextButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.DropDownButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.RestoreButton, hitType, LayoutElementHitTest.ControlBox);
            hitInfo.CheckAndSetHitTest(HitTestType.PageHeaders, hitType, LayoutElementHitTest.Bounds);
        }

        protected override LayoutElementHitInfo CreateHitInfo(Point pt) => 
            new DocumentPaneElementHitInfo(pt, this);

        public override Rect GetHeadersPanelBounds() => 
            new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, this.HeadersPanel), this.HeadersPanel.RenderSize);

        public override Rect GetSelectedPageBounds() => 
            new Rect(base.TranslateToView(CoordinateHelper.ZeroPoint, this.SelectedPage), this.SelectedPage.RenderSize);

        protected DevExpress.Xpf.Docking.VisualElements.DocumentPane DocumentPane
        {
            get
            {
                this.pane ??= ((base.Element is DevExpress.Xpf.Docking.VisualElements.DocumentPane) ? ((DevExpress.Xpf.Docking.VisualElements.DocumentPane) base.Element) : LayoutItemsHelper.GetVisualChild<DevExpress.Xpf.Docking.VisualElements.DocumentPane>(base.Element));
                return this.pane;
            }
        }

        protected TabHeadersPanel HeadersPanel
        {
            get
            {
                this.headersPanelCore ??= (this.DocumentPane.PartContent.PartItemsContainer as DocumentTabContainer).PartHeadersPanel;
                return this.headersPanelCore;
            }
        }

        protected UIElement SelectedPage
        {
            get
            {
                this.selectedPageCore ??= (this.DocumentPane.PartContent.PartItemsContainer as DocumentTabContainer).PartSelectedPage;
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
                DocumentTabContainer partItemsContainer = this.DocumentPane.PartContent.PartItemsContainer as DocumentTabContainer;
                return ((partItemsContainer.CaptionLocation != CaptionLocation.Default) ? partItemsContainer.CaptionLocation.ToDock(Dock.Top) : Dock.Top);
            }
        }

        public bool HasItems
        {
            get
            {
                Func<LayoutGroup, bool> evaluator = <>c.<>9__24_0;
                if (<>c.<>9__24_0 == null)
                {
                    Func<LayoutGroup, bool> local1 = <>c.<>9__24_0;
                    evaluator = <>c.<>9__24_0 = x => x.HasItems;
                }
                return (base.Item as LayoutGroup).Return<LayoutGroup, bool>(evaluator, (<>c.<>9__24_1 ??= () => false));
            }
        }

        public override bool HasHeadersPanel =>
            this.IsTabContainer && (this.HeadersPanel != null);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPaneElement.<>c <>9 = new DocumentPaneElement.<>c();
            public static Func<LayoutGroup, bool> <>9__24_0;
            public static Func<bool> <>9__24_1;

            internal bool <get_HasItems>b__24_0(LayoutGroup x) => 
                x.HasItems;

            internal bool <get_HasItems>b__24_1() => 
                false;
        }
    }
}

