namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public class ExcelFilteringUIServiceProvider : BaseFilteringUIServiceProvider
    {
        protected virtual ICustomFunctionsQueryFactory GetCustomFunctionsQueryFactory() => 
            DefaultCustomFunctionsQueryFactory.Instance;

        protected virtual ICustomUIFilterDialogTypesResolver GetCustomUIFilterDialogTypesResolver() => 
            DefaultCustomUIFilterDialogTypesResolver.Instance;

        protected virtual ICustomUIFilterFactory GetCustomUIFilterFactory() => 
            DefaultCustomUIFilterFactory.Instance;

        protected virtual ICustomUIFiltersFactory GetCustomUIFiltersFactory() => 
            DefaultCustomUIFiltersFactory.Instance;

        protected virtual ICustomUIFiltersOptionsFactory GetCustomUIFiltersOptionsFactory() => 
            DefaultCustomUIFiltersOptionsFactory.Instance;

        protected virtual ICustomUIFiltersPropertiesFactory GetCustomUIFiltersPropertiesFactory() => 
            DefaultCustomUIFiltersPropertiesFactory.Instance;

        protected virtual ICustomUIFilterTypesResolver GetCustomUIFilterTypesResolver() => 
            DefaultCustomUIFilterTypesResolver.Instance;

        protected virtual ICustomUIFilterValuesFactory GetCustomUIFilterValuesFactory() => 
            DefaultCustomUIFilterValuesFactory.Instance;

        protected override IEndUserFilteringSettingsFactory GetEndUserFilteringSettingsFactory() => 
            ExcelFilteringSettingsFactory.Instance;

        protected override IMetricAttributesQueryFactory GetMetricAttributesQueryFactory() => 
            ExcelMetricAttributesQueryFactory.Instance;

        protected override void RegisterServices()
        {
            base.RegisterServices();
            base.RegisterService<ICustomFunctionsQueryFactory>(this.GetCustomFunctionsQueryFactory());
            base.RegisterService<ICustomUIFiltersPropertiesFactory>(this.GetCustomUIFiltersPropertiesFactory());
            base.RegisterService<ICustomUIFilterTypesResolver>(this.GetCustomUIFilterTypesResolver());
            base.RegisterService<ICustomUIFilterDialogTypesResolver>(this.GetCustomUIFilterDialogTypesResolver());
            base.RegisterService<ICustomUIFiltersOptionsFactory>(this.GetCustomUIFiltersOptionsFactory());
            base.RegisterService<ICustomUIFiltersFactory>(this.GetCustomUIFiltersFactory());
            base.RegisterService<ICustomUIFilterFactory>(this.GetCustomUIFilterFactory());
            base.RegisterService<ICustomUIFilterValuesFactory>(this.GetCustomUIFilterValuesFactory());
        }
    }
}

