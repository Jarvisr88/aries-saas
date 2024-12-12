namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IViewInjectionService
    {
        object GetKey(object viewModel);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void Inject(object key, object viewModel, string viewName, Type viewType);
        bool Remove(object viewModel);

        [EditorBrowsable(EditorBrowsableState.Never)]
        string RegionName { get; }

        IEnumerable<object> ViewModels { get; }

        object SelectedViewModel { get; set; }
    }
}

