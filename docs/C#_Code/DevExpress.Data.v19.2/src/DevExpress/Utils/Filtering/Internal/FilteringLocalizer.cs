namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Localization;
    using System;

    public class FilteringLocalizer : XtraLocalizer<FilteringLocalizerStringId>
    {
        static FilteringLocalizer();
        public static XtraLocalizer<FilteringLocalizerStringId> CreateDefaultLocalizer();
        public override XtraLocalizer<FilteringLocalizerStringId> CreateResXLocalizer();
        public static string GetString(FilteringLocalizerStringId id);
        protected override void PopulateStringTable();

        public static XtraLocalizer<FilteringLocalizerStringId> Active { get; set; }
    }
}

