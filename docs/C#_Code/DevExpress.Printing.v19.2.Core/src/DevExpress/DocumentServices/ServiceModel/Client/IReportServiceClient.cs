namespace DevExpress.DocumentServices.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public interface IReportServiceClient : IServiceClientBase
    {
        event EventHandler<AsyncCompletedEventArgs> ClearDocumentCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<BuildStatus>> GetBuildStatusCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<DocumentData>> GetDocumentDataCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetExportedDocumentCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<ExportStatus>> GetExportStatusCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>> GetLookUpValuesCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetPagesCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetPrintDocumentCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<PrintStatus>> GetPrintStatusCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<ReportParameterContainer>> GetReportParametersCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<DocumentId>> StartBuildCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<ExportId>> StartExportCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<PrintId>> StartPrintCompleted;

        event EventHandler<AsyncCompletedEventArgs> StopBuildCompleted;

        event EventHandler<AsyncCompletedEventArgs> StopPrintCompleted;

        void ClearDocumentAsync(DocumentId documentId, object asyncState);
        void GetBuildStatusAsync(DocumentId documentId, object asyncState);
        void GetDocumentDataAsync(DocumentId documentId, object asyncState);
        void GetExportedDocumentAsync(ExportId exportId, object asyncState);
        void GetExportStatusAsync(ExportId exportId, object asyncState);
        void GetLookUpValuesAsync(InstanceIdentity identity, ReportParameter[] parameterValues, string[] requiredParameterPaths, object asyncState);
        void GetPagesAsync(DocumentId documentId, int[] pageIndexes, PageCompatibility compatibility, object asyncState);
        void GetPrintDocumentAsync(PrintId printId, object asyncState);
        void GetPrintStatusAsync(PrintId printId, object asyncState);
        void GetReportParametersAsync(InstanceIdentity identity, object asyncState);
        void StartBuildAsync(InstanceIdentity identity, ReportBuildArgs buildArgs, object asyncState);
        void StartExportAsync(DocumentId documentId, DocumentExportArgs exportArgs, object asyncState);
        void StartPrintAsync(DocumentId documentId, PageCompatibility compatibility, object asyncState);
        void StopBuildAsync(DocumentId documentId, object asyncState);
        void StopPrintAsync(PrintId printId, object asyncState);
    }
}

