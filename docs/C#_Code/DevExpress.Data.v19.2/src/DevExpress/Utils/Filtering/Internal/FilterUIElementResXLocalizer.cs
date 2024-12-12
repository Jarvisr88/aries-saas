namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Localization;
    using System;
    using System.Resources;

    public class FilterUIElementResXLocalizer : XtraResXLocalizer<FilterUIElementLocalizerStringId>
    {
        private const string baseName = "DevExpress.Data.EndUserFiltering.UIElements.LocalizationRes";

        public FilterUIElementResXLocalizer() : base(new FilterUIElementLocalizer())
        {
        }

        protected override ResourceManager CreateResourceManagerCore() => 
            new ResourceManager("DevExpress.Data.EndUserFiltering.UIElements.LocalizationRes", typeof(FilterUIElementResXLocalizer).Assembly);
    }
}

