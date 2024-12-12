namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.Windows;

    public interface IStrategyOwner
    {
        bool CanRemoveViewModel(object viewModel);
        void ConfigureChild(DependencyObject child);
        string GetKey(object viewModel);
        void RemoveViewModel(object viewModel);
        void SelectViewModel(object viewModel);

        string RegionName { get; }

        DependencyObject Target { get; }
    }
}

