namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;

    public interface IFilterCriteriaQueryFactory
    {
        QueryFilterCriteriaEventArgs Create(IEndUserFilteringMetricViewModel metricViewModel);
    }
}

