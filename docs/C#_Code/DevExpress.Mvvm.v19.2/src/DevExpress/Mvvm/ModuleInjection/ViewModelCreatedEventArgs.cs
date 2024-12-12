namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class ViewModelCreatedEventArgs : EventArgs
    {
        public ViewModelCreatedEventArgs(string regionName, object viewModel, string viewModelKey)
        {
            this.RegionName = regionName;
            this.ViewModel = viewModel;
            this.ViewModelKey = viewModelKey;
        }

        public string RegionName { get; private set; }

        public object ViewModel { get; private set; }

        public string ViewModelKey { get; private set; }
    }
}

