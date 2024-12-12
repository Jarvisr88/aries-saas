namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;

    public interface IRangeValueViewModel<T> : IRangeValueViewModel, IValueViewModel where T: struct
    {
        T? Average { get; }

        T? Minimum { get; }

        T? Maximum { get; }

        T? FromValue { get; set; }

        T? ToValue { get; set; }

        IReadOnlyCollection<Interval<T>> Intervals { get; set; }
    }
}

