namespace DevExpress.Utils.Filtering.Internal
{
    public interface IMetricAttributesQueryFactory
    {
        IMetricAttributesQuery CreateQuery(IEndUserFilteringMetric metric, IMetricAttributesQueryOwner owner);
    }
}

