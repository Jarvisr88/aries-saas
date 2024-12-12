namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public class VirtualServerModeTotalSummaryReadyEventArgs : EventArgs
    {
        private IDictionary<ServerModeSummaryDescriptor, object> _Results;

        public VirtualServerModeTotalSummaryReadyEventArgs(IDictionary<ServerModeSummaryDescriptor, object> results);

        public IDictionary<ServerModeSummaryDescriptor, object> TotalSummary { get; }

        public static object NotReadyObject { get; }
    }
}

