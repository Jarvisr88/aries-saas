namespace DevExpress.Utils.Filtering
{
    using DevExpress.Utils.Filtering.Internal;
    using System;

    public interface IFilteringUIClient
    {
        bool EnsureProviderInitialized();

        IEndUserFilteringViewModelProvider Provider { get; }
    }
}

