namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BarSubItemLinkControlStrategy : BaseIPopupOwnerStrategy<IBarSubItemLinkControl>
    {
        public BarSubItemLinkControlStrategy(IBarItemLinkControl instance);
        protected override void AfterClosePopup();
        protected virtual void AssignPopupContentControlLinksHolder();
        protected override void BeforeOpenPopup();
        public override bool CanStartDragCore(object sender, MouseButtonEventArgs e);
        protected override bool GetActAsDropDown();
        protected override void GetArrowAlignment(out Dock arrowAlignment, out bool showContentInArrow);
        protected override bool IsMouseOverArrow(IBarSubItemLinkControl linkControl);
        protected override bool IsMouseOverContent(IBarSubItemLinkControl linkControl);
        public override void OnClear();
        public override void OnLinkBaseChanged(BarItemLinkBase oldValue, BarItemLinkBase newValue);
        public override void OnPopupChanged(IPopupControl oldValue, IPopupControl newValue);
        protected override void OnPopupControlClosed(object sender, EventArgs e);
        protected override void OnPopupControlOpened(object sender, EventArgs e);
        protected virtual void RaiseCloseUp();
        protected virtual void RaiseGetItemData();
        protected virtual void RaisePopup();

        protected DevExpress.Xpf.Bars.PopupMenuBase PopupMenuBase { get; }

        protected BarSubItem SubItem { get; }

        public BarSubItemLink SubItemLink { get; }

        protected SubMenuBarControl ItemsOwner { get; }

        public override bool ShouldDeactivateMenuOnAccessKey { get; }

        protected override bool IsArrowPressed { get; set; }

        protected override bool IsArrowHighlighted { get; set; }

        protected override bool ShowArrowHotBorder { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarSubItemLinkControlStrategy.<>c <>9;
            public static Func<PopupMenuBase, PopupMenuBarControl> <>9__8_0;

            static <>c();
            internal PopupMenuBarControl <get_ItemsOwner>b__8_0(PopupMenuBase x);
        }
    }
}

