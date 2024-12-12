namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;

    public class SearchPanelLocalizer : DXLocalizer<SearchPanelStringId>
    {
        static SearchPanelLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<SearchPanelStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<SearchPanelStringId> CreateDefaultLocalizer() => 
            new SearchPanelResXLocalizer();

        public override XtraLocalizer<SearchPanelStringId> CreateResXLocalizer() => 
            new SearchPanelResXLocalizer();

        public static string GetString(SearchPanelStringId id) => 
            XtraLocalizer<SearchPanelStringId>.Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(SearchPanelStringId.ButtonTooltip_FindPrev, "Find Previous");
            this.AddString(SearchPanelStringId.ButtonTooltip_FindNext, "Find Next");
            this.AddString(SearchPanelStringId.ButtonTooltip_Close, "Close");
            this.AddString(SearchPanelStringId.ButtonTooltip_SearchOptions, "Search Options");
            this.AddString(SearchPanelStringId.ButtonText_Replace, "Replace");
            this.AddString(SearchPanelStringId.ButtonText_ReplaceAll, "Replace All");
            this.AddString(SearchPanelStringId.LabelText_Find, "Find");
            this.AddString(SearchPanelStringId.LabelText_Replace, "Replace with");
            this.AddString(SearchPanelStringId.MenuCheckItem_CaseSensative, "Case sensitive");
            this.AddString(SearchPanelStringId.MenuCheckItem_UseRegularExpression, "Regular expression");
            this.AddString(SearchPanelStringId.MenuCheckItem_WholeWord, "Whole word");
        }
    }
}

