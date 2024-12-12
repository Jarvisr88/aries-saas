namespace DevExpress.DocumentServices.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ReportServiceClient : ServiceClientBase, IReportServiceClient, IServiceClientBase
    {
        public event EventHandler<AsyncCompletedEventArgs> ClearDocumentCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<BuildStatus>> GetBuildStatusCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<DocumentData>> GetDocumentDataCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetExportedDocumentCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<ExportStatus>> GetExportStatusCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>> GetLookUpValuesCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetPagesCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<byte[]>> GetPrintDocumentCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<PrintStatus>> GetPrintStatusCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<ReportParameterContainer>> GetReportParametersCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<DocumentId>> StartBuildCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<ExportId>> StartExportCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<PrintId>> StartPrintCompleted;

        public event EventHandler<AsyncCompletedEventArgs> StopBuildCompleted;

        public event EventHandler<AsyncCompletedEventArgs> StopPrintCompleted;

        public ReportServiceClient(IAsyncReportService channel) : base((IChannel) channel)
        {
        }

        public void ClearDocumentAsync(DocumentId documentId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndClearDocument), () => this.ClearDocumentCompleted);
            };
            try
            {
                this.Channel.BeginClearDocument(documentId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseVoidOperationCompleted(() => this.ClearDocumentCompleted, exception, false, asyncState);
            }
        }

        public void GetBuildStatusAsync(DocumentId documentId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<BuildStatus>(ar, new Func<IAsyncResult, BuildStatus>(channel.EndGetBuildStatus), () => this.GetBuildStatusCompleted);
            };
            try
            {
                this.Channel.BeginGetBuildStatus(documentId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<BuildStatus>(() => this.GetBuildStatusCompleted, null, exception, false, asyncState);
            }
        }

        public void GetDocumentDataAsync(DocumentId documentId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<DocumentData>(ar, new Func<IAsyncResult, DocumentData>(channel.EndGetDocumentData), () => this.GetDocumentDataCompleted);
            };
            try
            {
                this.Channel.BeginGetDocumentData(documentId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<DocumentData>(() => this.GetDocumentDataCompleted, null, exception, false, asyncState);
            }
        }

        public void GetExportedDocumentAsync(ExportId exportId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<byte[]>(ar, new Func<IAsyncResult, byte[]>(channel.EndGetExportedDocument), () => this.GetExportedDocumentCompleted);
            };
            try
            {
                this.Channel.BeginGetExportedDocument(exportId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<byte[]>(() => this.GetExportedDocumentCompleted, null, exception, false, asyncState);
            }
        }

        public void GetExportStatusAsync(ExportId exportId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<ExportStatus>(ar, new Func<IAsyncResult, ExportStatus>(channel.EndGetExportStatus), () => this.GetExportStatusCompleted);
            };
            try
            {
                this.Channel.BeginGetExportStatus(exportId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<ExportStatus>(() => this.GetExportStatusCompleted, null, exception, false, asyncState);
            }
        }

        [Obsolete("Use the GetLookUpValuesAsync method instead.")]
        public void GetLookUpValues(InstanceIdentity identity, ReportParameter[] parameterValues, string[] requiredParameterPaths, object asyncState)
        {
        }

        public void GetLookUpValuesAsync(InstanceIdentity identity, ReportParameter[] parameterValues, string[] requiredParameterPaths, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<ParameterLookUpValues[]>(ar, new Func<IAsyncResult, ParameterLookUpValues[]>(channel.EndGetLookUpValues), () => this.GetLookUpValuesCompleted);
            };
            try
            {
                this.Channel.BeginGetLookUpValues(identity, parameterValues, requiredParameterPaths, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<ParameterLookUpValues[]>(() => this.GetLookUpValuesCompleted, null, exception, false, asyncState);
            }
        }

        public void GetPagesAsync(DocumentId documentId, int[] pageIndexes, PageCompatibility compatibility, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<byte[]>(ar, new Func<IAsyncResult, byte[]>(channel.EndGetPages), () => this.GetPagesCompleted);
            };
            try
            {
                this.Channel.BeginGetPages(documentId, pageIndexes, compatibility, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<byte[]>(() => this.GetPagesCompleted, null, exception, false, asyncState);
            }
        }

        public void GetPrintDocumentAsync(PrintId printId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<byte[]>(ar, new Func<IAsyncResult, byte[]>(channel.EndGetPrintDocument), () => this.GetPrintDocumentCompleted);
            };
            try
            {
                this.Channel.BeginGetPrintDocument(printId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<byte[]>(() => this.GetPrintDocumentCompleted, null, exception, false, asyncState);
            }
        }

        public void GetPrintStatusAsync(PrintId printId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<PrintStatus>(ar, new Func<IAsyncResult, PrintStatus>(channel.EndGetPrintStatus), () => this.GetPrintStatusCompleted);
            };
            try
            {
                this.Channel.BeginGetPrintStatus(printId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<PrintStatus>(() => this.GetPrintStatusCompleted, null, exception, false, asyncState);
            }
        }

        public void GetReportParametersAsync(InstanceIdentity identity, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<ReportParameterContainer>(ar, new Func<IAsyncResult, ReportParameterContainer>(channel.EndGetReportParameters), () => this.GetReportParametersCompleted);
            };
            try
            {
                this.Channel.BeginGetReportParameters(identity, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<ReportParameterContainer>(() => this.GetReportParametersCompleted, null, exception, false, asyncState);
            }
        }

        public void StartBuildAsync(InstanceIdentity identity, ReportBuildArgs buildArgs, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<DocumentId>(ar, new Func<IAsyncResult, DocumentId>(channel.EndStartBuild), () => this.StartBuildCompleted);
            };
            try
            {
                this.Channel.BeginStartBuild(identity, buildArgs, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<DocumentId>(() => this.StartBuildCompleted, null, exception, false, asyncState);
            }
        }

        public void StartExportAsync(DocumentId documentId, DocumentExportArgs exportArgs, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<ExportId>(ar, new Func<IAsyncResult, ExportId>(channel.EndStartExport), () => this.StartExportCompleted);
            };
            try
            {
                this.Channel.BeginStartExport(documentId, exportArgs, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<ExportId>(() => this.StartExportCompleted, null, exception, false, asyncState);
            }
        }

        public void StartPrintAsync(DocumentId documentId, PageCompatibility compatibility, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndScalarOperation<PrintId>(ar, new Func<IAsyncResult, PrintId>(channel.EndStartPrint), () => this.StartPrintCompleted);
            };
            try
            {
                this.Channel.BeginStartPrint(documentId, compatibility, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseScalarOperationCompleted<PrintId>(() => this.StartPrintCompleted, null, exception, false, asyncState);
            }
        }

        public void StopBuildAsync(DocumentId documentId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndStopBuild), () => this.StopBuildCompleted);
            };
            try
            {
                this.Channel.BeginStopBuild(documentId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseVoidOperationCompleted(() => this.StopBuildCompleted, exception, false, asyncState);
            }
        }

        public void StopPrintAsync(PrintId printId, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IAsyncReportService channel = this.Channel;
                base.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndStopPrint), () => this.StopPrintCompleted);
            };
            try
            {
                this.Channel.BeginStopPrint(printId, callback, asyncState);
            }
            catch (Exception exception)
            {
                base.RaiseVoidOperationCompleted(() => this.StopPrintCompleted, exception, false, asyncState);
            }
        }

        protected IAsyncReportService Channel =>
            (IAsyncReportService) base.Channel;
    }
}

