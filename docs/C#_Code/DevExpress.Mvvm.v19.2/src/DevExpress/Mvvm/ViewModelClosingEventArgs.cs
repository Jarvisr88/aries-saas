namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ViewModelClosingEventArgs : CancelEventArgs
    {
        public ViewModelClosingEventArgs(object viewModel)
        {
            this.ViewModel = viewModel;
        }

        public object ViewModel { get; private set; }
    }
}

