namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IUniqueValuesViewModel : IValueViewModel
    {
        bool HasValues { get; }

        object Values { get; }
    }
}

