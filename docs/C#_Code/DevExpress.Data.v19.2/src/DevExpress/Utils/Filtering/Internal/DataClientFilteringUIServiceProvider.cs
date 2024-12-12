namespace DevExpress.Utils.Filtering.Internal
{
    public class DataClientFilteringUIServiceProvider : FilteringUIServiceProviderBase
    {
        protected override IEndUserFilteringSettingsFactory GetEndUserFilteringSettingsFactory() => 
            ExcelFilteringSettingsFactory.Instance;

        protected override IMetricAttributesQueryFactory GetMetricAttributesQueryFactory() => 
            ExcelMetricAttributesQueryFactory.Instance;
    }
}

