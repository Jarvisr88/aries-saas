namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;

    public abstract class PreviewSettingsViewModelBase : ViewModelBase
    {
        protected PreviewSettingsViewModelBase()
        {
        }

        [DXHelpExclude(true)]
        public abstract DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType SettingsType { get; }
    }
}

