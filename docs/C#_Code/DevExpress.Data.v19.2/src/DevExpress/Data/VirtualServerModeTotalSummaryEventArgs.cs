namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class VirtualServerModeTotalSummaryEventArgs : EventArgs
    {
        private System.Threading.CancellationToken _CancellationToken;
        private VirtualServerModeConfigurationInfo _ConfigurationInfo;
        private Action<IDictionary<ServerModeSummaryDescriptor, object>> _NotifyIntermediateSummaryReady;

        public VirtualServerModeTotalSummaryEventArgs(System.Threading.CancellationToken cancellationToken, VirtualServerModeConfigurationInfo configurationInfo, Action<IDictionary<ServerModeSummaryDescriptor, object>> notifyIntermediateSummaryReady);
        public void NotifyIntermediateSummaryReady(IDictionary<ServerModeSummaryDescriptor, object> intermediateSummary);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void NotifySummaryReady(IDictionary<ServerModeSummaryDescriptor, object> intermediateSummary);

        public static object NotReadyObject { get; }

        public System.Threading.CancellationToken CancellationToken { get; }

        public VirtualServerModeConfigurationInfo ConfigurationInfo { get; }

        public Task<IDictionary<ServerModeSummaryDescriptor, object>> TotalSummaryTask { get; set; }
    }
}

