namespace DevExpress.Utils.MVVM
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IViewModelProvider
    {
        event EventHandler ViewModelChanged;

        object ViewModel { get; }

        bool IsViewModelCreated { get; }
    }
}

