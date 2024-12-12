namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class CustomUIFilterNone : CustomUIFilter
    {
        internal CustomUIFilterNone(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            null;

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue) => 
            null;

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.None;
    }
}

