namespace DevExpress.Utils.Filtering.Internal
{
    public interface IFilterUIEditorPropertiesFactory
    {
        IFilterUIEditorProperties Create(IEndUserFilteringMetric metric);
    }
}

