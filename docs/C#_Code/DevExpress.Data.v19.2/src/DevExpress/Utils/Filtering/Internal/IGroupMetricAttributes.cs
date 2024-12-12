namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;

    public interface IGroupMetricAttributes : ICollectionMetricAttributes, IMetricAttributes, IUniqueValuesMetricAttributes
    {
        GroupUIEditorType EditorType { get; }

        string[] Grouping { get; }

        IDictionary<int, object[]> GroupValues { get; }

        IDictionary<int, string[]> GroupTexts { get; }
    }
}

