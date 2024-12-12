namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public interface IStrategy : IBaseStrategy
    {
        void Clear();
        object GetParentViewModel();
        void Inject(object viewModel, Type viewType);
        void Remove(object viewModel);
        void Select(object viewModel, bool focus);

        object SelectedViewModel { get; }
    }
}

