namespace ActiproSoftware.WinUICore.Input
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ICommand
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}

