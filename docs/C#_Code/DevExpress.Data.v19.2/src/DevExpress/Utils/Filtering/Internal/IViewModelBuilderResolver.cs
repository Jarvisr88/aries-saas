namespace DevExpress.Utils.Filtering.Internal
{
    public interface IViewModelBuilderResolver
    {
        IViewModelBuilder CreateValueViewModelBuilder(IEndUserFilteringMetric metric);
        IViewModelBuilder CreateViewModelBuilder();
    }
}

