namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFilterValuesFactory
    {
        ICustomUIFilterValue Create(CustomUIFilterType filterType, params object[] values);
    }
}

