namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    public interface IBooleanChoiceMetricAttributes : IChoiceMetricAttributes<bool>, IMetricAttributes<bool>, IMetricAttributes
    {
        BooleanUIEditorType EditorType { get; }

        string TrueName { get; }

        string FalseName { get; }

        string DefaultName { get; }

        bool? DefaultValue { get; }
    }
}

