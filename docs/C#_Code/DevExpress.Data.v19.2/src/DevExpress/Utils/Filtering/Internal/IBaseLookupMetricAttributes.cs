namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;

    public interface IBaseLookupMetricAttributes : ICollectionMetricAttributes, IMetricAttributes, IUniqueValuesMetricAttributes
    {
        LookupUIEditorType EditorType { get; }
    }
}

