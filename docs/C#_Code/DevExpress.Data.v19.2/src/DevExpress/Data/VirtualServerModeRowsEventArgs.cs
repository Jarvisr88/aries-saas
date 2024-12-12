namespace DevExpress.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class VirtualServerModeRowsEventArgs : EventArgs
    {
        private readonly System.Threading.CancellationToken _CancellationToken;
        private readonly VirtualServerModeConfigurationInfo _ConfigurationInfo;
        private readonly int _CurrentRowCount;

        public VirtualServerModeRowsEventArgs(System.Threading.CancellationToken cancellationToken, VirtualServerModeConfigurationInfo configurationInfo, int currentRowCount, object userData);

        public System.Threading.CancellationToken CancellationToken { get; }

        public VirtualServerModeConfigurationInfo ConfigurationInfo { get; }

        public int CurrentRowCount { get; }

        public Task<VirtualServerModeRowsTaskResult> RowsTask { get; set; }

        public object UserData { get; set; }
    }
}

