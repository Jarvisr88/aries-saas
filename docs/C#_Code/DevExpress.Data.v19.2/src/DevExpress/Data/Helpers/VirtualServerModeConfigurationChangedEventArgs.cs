namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;

    public class VirtualServerModeConfigurationChangedEventArgs : EventArgs
    {
        private readonly VirtualServerModeConfigurationInfo _ConfigurationInfo;

        public VirtualServerModeConfigurationChangedEventArgs(VirtualServerModeConfigurationInfo configurationInfo);

        public VirtualServerModeConfigurationInfo ConfigurationInfo { get; }
    }
}

