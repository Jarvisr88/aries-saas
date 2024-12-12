namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface ICustomUIFilterValue : IEquatable<ICustomUIFilterValue>
    {
        CustomUIFilterType FilterType { get; }

        string FilterName { get; }

        string FilterDescription { get; }

        bool IsDefault { get; }

        object Value { get; }

        bool HasChildren { get; }

        IEnumerable<ICustomUIFilterValue> Children { get; }
    }
}

