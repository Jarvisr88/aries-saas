namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;

    public interface ICustomUIFilterValueViewModel
    {
        CriteriaOperator CreateFilterCriteria(IEndUserFilteringMetric metric);
        CriteriaOperator CreateFilterCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue value);
    }
}

