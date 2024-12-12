namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal class ExcelFilteringSettings : EndUserFilteringSettings
    {
        public ExcelFilteringSettings(IEndUserFilteringMetadataProvider typeMetadataProvider, IEndUserFilteringMetadataProvider customMetadataProvider);
        protected sealed override ILazyMetricAttributesFactory GetLazyMetricAttributesFactory();
    }
}

