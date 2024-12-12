namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IRangeValueViewModel : IValueViewModel
    {
        bool AllowNull { get; }

        string FromName { get; }

        string ToName { get; }

        string NullName { get; }

        bool? ParsedExact { get; }

        bool? ParsedExactFrom { get; }

        bool? ParsedExactTo { get; }
    }
}

