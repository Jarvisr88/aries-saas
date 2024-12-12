namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class WindowInjectionResult
    {
        public WindowInjectionResult(string regionName, object viewModel, object viewModelKey, MessageBoxResult? result)
        {
            this.RegionName = regionName;
            this.ViewModel = viewModel;
            this.ViewModelKey = viewModelKey;
            this.Result = result;
        }

        public string RegionName { get; private set; }

        public object ViewModel { get; private set; }

        public object ViewModelKey { get; private set; }

        public MessageBoxResult? Result { get; private set; }
    }
}

