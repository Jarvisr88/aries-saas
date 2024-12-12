namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Bars;
    using System;

    public class SearchPanelOptionsMenuInfo : MenuInfoBase
    {
        private readonly SearchPanel owner;

        public SearchPanelOptionsMenuInfo(SearchPanelOptionsPopupMenu menu, SearchPanel owner);
        protected override void CreateItems();

        public override bool CanCreateItems { get; }

        public override BarManagerMenuController MenuController { get; }

        public SearchPanelOptionsPopupMenu Menu { get; }
    }
}

