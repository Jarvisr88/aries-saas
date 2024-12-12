namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;

    public interface IFilterCriteriaParseFactory
    {
        DevExpress.Utils.Filtering.ParseFilterCriteriaEventArgs Create(IEndUserFilteringMetricViewModel metricViewModel);
    }
}

