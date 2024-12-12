namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Resources;

    public class FilteringResXLocalizer : XtraResXLocalizer<FilteringLocalizerStringId>
    {
        private const string baseName = "DevExpress.Data.EndUserFiltering.LocalizationRes";

        public FilteringResXLocalizer();
        protected override ResourceManager CreateResourceManagerCore();
    }
}

