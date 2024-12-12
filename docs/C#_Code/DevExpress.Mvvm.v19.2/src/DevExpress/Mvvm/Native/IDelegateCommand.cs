namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Windows.Input;

    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}

