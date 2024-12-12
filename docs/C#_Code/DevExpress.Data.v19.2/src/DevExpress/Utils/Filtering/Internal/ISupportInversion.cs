namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ISupportInversion
    {
        bool HasInversion { get; }

        object InvertedValues { get; }
    }
}

