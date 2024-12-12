namespace DevExpress.ReportServer.ServiceModel.Native.RemoteOperations
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;

    public class RemoteOperationFactory
    {
        private readonly IReportServiceClient client;
        private readonly TimeSpan updateInterval;
        private readonly DocumentId documentId;

        public RemoteOperationFactory(IReportServiceClient client, DocumentId documentId, TimeSpan updateInterval)
        {
            Guard.ArgumentNotNull(client, "client");
            Guard.ArgumentNotNull(documentId, "documentId");
            Guard.ArgumentNotNull(updateInterval, "updateInterval");
            this.client = client;
            this.updateInterval = updateInterval;
            this.documentId = documentId;
        }

        public ExportDocumentOperation CreateExportDocumentOperation(ExportFormat format, ExportOptions options) => 
            this.CreateExportDocumentOperation(format, options, null);

        public ExportDocumentOperation CreateExportDocumentOperation(ExportFormat format, ExportOptions options, object customArgs) => 
            new ExportDocumentOperation(this.client, this.documentId, format, options, this.updateInterval, customArgs);

        public RequestPagesOperation CreateRequestPagesOperation(int[] pageIndexes, IBrickPagePairFactory factory) => 
            new RequestPagesOperation(this.client, this.documentId, this.updateInterval, pageIndexes, factory);
    }
}

