namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IUICommand
    {
        event EventHandler Executed;

        void RaiseExecuted();
    }
}

