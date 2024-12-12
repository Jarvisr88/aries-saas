namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IEndUserFilteringCriteriaAwareViewModel
    {
        bool TryParse(IEndUserFilteringMetric metric, CriteriaOperator criteria);
    }
}

