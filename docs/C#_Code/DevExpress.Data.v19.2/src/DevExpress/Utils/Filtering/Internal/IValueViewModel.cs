namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IValueViewModel
    {
        event EventHandler Changed;

        bool CanReset();
        bool CanResetAll();
        void Initialize(IEndUserFilteringMetricViewModel metricViewModel);
        void Initialize(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        void Release();
        void Reset();
        void ResetAll();

        bool IsModified { get; }

        bool IsInitializedWithValues { get; }
    }
}

