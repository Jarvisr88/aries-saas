namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDisplayMetricAttributes : IMetricAttributes
    {
        bool TryGetDisplayIndex(string displayText, out int valueIndex);
        bool TryGetDisplayLookup(object owner, object uniqueValues, bool skipNulls, out object lookup);
        bool TryGetDisplayText(int valueIndex, out string displayText);

        bool FilterByDisplayText { get; }

        object DataItemsLookup { get; }
    }
}

