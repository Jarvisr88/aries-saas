namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;

    public interface IWindowStrategy : IBaseStrategy
    {
        void Activate();
        void Close();
        void Show(object viewModel, Type viewType);
        void ShowDialog(object viewModel, Type viewType);

        MessageBoxResult? Result { get; }
    }
}

