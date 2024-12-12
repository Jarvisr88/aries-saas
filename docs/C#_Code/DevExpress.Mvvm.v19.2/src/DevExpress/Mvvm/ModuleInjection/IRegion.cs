namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Collections.Generic;

    public interface IRegion
    {
        string GetKey(object viewModel);
        DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode GetLogicalSerializationMode(string key);
        object GetViewModel(string key);
        DevExpress.Mvvm.ModuleInjection.VisualSerializationMode GetVisualSerializationMode(string key);
        void ResetVisualState();
        void SetLogicalSerializationMode(string key, DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode? mode);
        void SetVisualSerializationMode(string key, DevExpress.Mvvm.ModuleInjection.VisualSerializationMode? mode);

        string RegionName { get; }

        IEnumerable<object> ViewModels { get; }

        object SelectedViewModel { get; }

        string SelectedKey { get; }

        DevExpress.Mvvm.ModuleInjection.LogicalSerializationMode LogicalSerializationMode { get; set; }

        DevExpress.Mvvm.ModuleInjection.VisualSerializationMode VisualSerializationMode { get; set; }
    }
}

