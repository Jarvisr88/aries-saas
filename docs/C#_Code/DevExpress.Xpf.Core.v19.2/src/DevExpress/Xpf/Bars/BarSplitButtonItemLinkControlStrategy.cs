namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class BarSplitButtonItemLinkControlStrategy : BaseIPopupOwnerStrategy<IBarSplitButtonItemLinkControl>
    {
        public BarSplitButtonItemLinkControlStrategy(IBarItemLinkControl instance);
        protected override void AfterClosePopup();
        protected override void BeforeOpenPopup();
        protected override bool GetActAsDropDown();
        protected override bool GetActualIsArrowEnabled();
        protected override void GetArrowAlignment(out Dock arrowAlignment, out bool showContentInArrow);
        public override INavigationOwner GetBoundOwner();
        public virtual Visual GetVisualChild();
        protected override bool IsMouseOverArrow(IBarSplitButtonItemLinkControl linkControl);
        protected override bool IsMouseOverContent(IBarSplitButtonItemLinkControl linkControl);
        public override bool OnMouseLeftButtonDown(MouseButtonEventArgs args);
        public override void UpdateActualIsContentEnabled();
        public virtual void UpdateActualIsHoverEnabled();
        protected override void UpdateActualPropertiesOverride();
        public override void UpdateIsArrowHighlighted();

        protected override bool IsArrowPressed { get; set; }

        protected override bool IsArrowHighlighted { get; set; }

        protected override bool ShowArrowHotBorder { get; set; }

        public BarSplitButtonItemLink SplitButtonLink { get; }

        public BarSplitButtonItem SplitButtonItem { get; }

        protected IBarSplitButtonItemLinkControl SplitInstance { get; }

        public virtual bool HasVisualChild { get; }
    }
}

