namespace DevExpress.Utils.Filtering.Internal
{
    public interface ICustomUIFiltersOptionsFactory
    {
        ICustomUIFiltersOptions Create(IEndUserFilteringMetric metric);
    }
}

