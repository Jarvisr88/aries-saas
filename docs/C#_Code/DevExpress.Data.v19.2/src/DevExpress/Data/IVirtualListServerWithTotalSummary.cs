namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public interface IVirtualListServerWithTotalSummary : IVirtualListServer, IList, ICollection, IEnumerable
    {
        event EventHandler<VirtualServerModeTotalSummaryReadyEventArgs> TotalSummaryReady;

        bool CanCalculateTotalSummary(ServerModeSummaryDescriptor totalSummary);

        bool TotalSummarySupported { get; }
    }
}

