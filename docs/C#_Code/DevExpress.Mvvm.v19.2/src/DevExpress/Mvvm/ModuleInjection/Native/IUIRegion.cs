namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using System;
    using System.Collections.Generic;

    public interface IUIRegion
    {
        void Clear();
        object GetView(object viewModel);
        void Inject(object viewModel, Type viewType);
        void Remove(object viewModel);
        void SelectViewModel(object vm, bool focus);

        string RegionName { get; }

        IEnumerable<object> ViewModels { get; }

        object SelectedViewModel { get; set; }
    }
}

