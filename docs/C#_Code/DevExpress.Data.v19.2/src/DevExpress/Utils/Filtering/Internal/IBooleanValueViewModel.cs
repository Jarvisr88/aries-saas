namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IBooleanValueViewModel : ISimpleValueViewModel<bool>, IValueViewModel
    {
        string DefaultName { get; }

        string TrueName { get; }

        string FalseName { get; }

        bool? DefaultValue { get; }
    }
}

