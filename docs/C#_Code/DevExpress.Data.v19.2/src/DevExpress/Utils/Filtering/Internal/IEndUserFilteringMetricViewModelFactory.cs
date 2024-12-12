namespace DevExpress.Utils.Filtering.Internal
{
    public interface IEndUserFilteringMetricViewModelFactory
    {
        IEndUserFilteringMetricViewModel Create(IEndUserFilteringMetric metric, IEndUserFilteringMetricViewModelValueBox valueBox);
    }
}

