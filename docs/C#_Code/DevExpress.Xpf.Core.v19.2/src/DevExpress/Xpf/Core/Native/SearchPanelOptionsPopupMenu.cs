namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public class SearchPanelOptionsPopupMenu : CustomizablePopupMenuBase
    {
        private readonly SearchPanel owner;

        public SearchPanelOptionsPopupMenu(SearchPanel owner);
        public BarCheckItem AddMenuCheckItem(string caption, bool initialValue);
        protected override MenuInfoBase CreateMenuInfo(UIElement placementTarget);
    }
}

