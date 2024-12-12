namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;

    public class CreateReportResult
    {
        public int ReportId { get; set; }

        public int RevisionId { get; set; }

        public int? OptimisticLock { get; set; }
    }
}

