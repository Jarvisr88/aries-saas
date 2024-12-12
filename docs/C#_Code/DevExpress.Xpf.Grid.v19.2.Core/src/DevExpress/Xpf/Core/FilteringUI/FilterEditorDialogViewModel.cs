namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public sealed class FilterEditorDialogViewModel : ViewModelBase
    {
        internal FilterEditorDialogViewModel(FilteringUIContext context, string propertyName, DataTemplate filterEditorTemplate)
        {
            this.<Context>k__BackingField = context;
            this.<PropertyName>k__BackingField = propertyName;
            this.<FilterEditorTemplate>k__BackingField = filterEditorTemplate;
        }

        internal void Apply()
        {
            IApplyService service = this.GetService<IApplyService>();
            if (service != null)
            {
                service.Apply();
            }
        }

        public FilteringUIContext Context { get; }

        public string PropertyName { get; }

        public DataTemplate FilterEditorTemplate { get; }
    }
}

