namespace DevExpress.Mvvm
{
    using System;
    using System.Windows.Input;

    public interface ICommand<T> : ICommand
    {
        bool CanExecute(T param);
        void Execute(T param);
    }
}

