namespace DevExpress.Xpf.Core.Localization
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;

    public class TabControlLocalizer : XtraLocalizer<TabControlStringId>
    {
        static TabControlLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<TabControlStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<TabControlStringId> CreateDefaultLocalizer() => 
            new TabControlLocalizer();

        public override XtraLocalizer<TabControlStringId> CreateResXLocalizer() => 
            new TabControlResLocalizer();

        public static string GetString(TabControlStringId id) => 
            Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(TabControlStringId.MenuCmd_ScrollPrev, "Scroll to previos header");
            this.AddString(TabControlStringId.MenuCmd_ScrollPrevDescription, "Scroll to previos header");
            this.AddString(TabControlStringId.MenuCmd_ScrollNext, "Scroll to next header");
            this.AddString(TabControlStringId.MenuCmd_ScrollNextDescription, "Scroll to next header");
            this.AddString(TabControlStringId.MenuCmd_ScrollToSelectedTabItem, "Scroll to selected header");
            this.AddString(TabControlStringId.MenuCmd_ScrollToSelectedTabItemDescription, "Scroll to selected header");
            this.AddString(TabControlStringId.MenuCmd_ScrollFirst, "Scroll to first header");
            this.AddString(TabControlStringId.MenuCmd_ScrollFirstDescription, "Scroll to first header");
            this.AddString(TabControlStringId.MenuCmd_ScrollLast, "Scroll to last header");
            this.AddString(TabControlStringId.MenuCmd_ScrollLastDescription, "Scroll to last header");
            this.AddString(TabControlStringId.MenuCmd_SelectPrev, "Select previos tab");
            this.AddString(TabControlStringId.MenuCmd_SelectPrevDescription, "Select previos tab");
            this.AddString(TabControlStringId.MenuCmd_SelectNext, "Select next tab");
            this.AddString(TabControlStringId.MenuCmd_SelectNextDescription, "Select next tab");
            this.AddString(TabControlStringId.MenuCmd_HideSelectedItem, "Hide selected tab");
            this.AddString(TabControlStringId.MenuCmd_HideSelectedItemDescription, "Hide selected tab");
        }

        public static XtraLocalizer<TabControlStringId> Active
        {
            get => 
                XtraLocalizer<TabControlStringId>.Active;
            set => 
                XtraLocalizer<TabControlStringId>.Active = value;
        }
    }
}

