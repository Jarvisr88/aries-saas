namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ISimpleValueViewModel<T> : IValueViewModel where T: struct
    {
        bool AllowNull { get; }

        T? Value { get; set; }
    }
}

