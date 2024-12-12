namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.IO;

    internal class ExportReportTask : ReportServiceTaskBase<byte[]>
    {
        private ExportOptionsBase exportOptions;

        public ExportReportTask(IReportServiceClient client) : base(client)
        {
        }

        private ExportDocumentOperation CreateExportOperation(DocumentId documentId, DocumentExportArgs exportArgs, TimeSpan statusUpdateInterval) => 
            new ExportDocumentOperation(base.Client, documentId, exportArgs, statusUpdateInterval);

        public void ExecuteAsync(InstanceIdentity reportIdentity, ExportOptionsBase exportOptions, ReportParameter[] parameters, object asyncState)
        {
            Guard.ArgumentNotNull(reportIdentity, "reportIdentity");
            Guard.ArgumentNotNull(exportOptions, "exportOptions");
            this.exportOptions = exportOptions;
            base.CreateDocumentAsync(reportIdentity, parameters, asyncState);
        }

        private void ExportDocument_Completed(object sender, ExportDocumentCompletedEventArgs e)
        {
            ((ExportDocumentOperation) sender).Completed -= new EventHandler<ExportDocumentCompletedEventArgs>(this.ExportDocument_Completed);
            if (!base.HasErrorOrCancelled(e))
            {
                this.SetResult(e);
                base.RaiseCompleted(null, false);
            }
        }

        protected override void ProcessGeneratedReport(DocumentId documentId)
        {
            DocumentExportArgs args1 = new DocumentExportArgs();
            args1.CustomArgs = null;
            args1.Format = this.exportOptions.GetFormat();
            args1.SerializedExportOptions = Serialize(this.exportOptions);
            DocumentExportArgs exportArgs = args1;
            ExportDocumentOperation operation = this.CreateExportOperation(documentId, exportArgs, ReportServiceTaskBase<byte[]>.DefaultUpdateStatusInterval);
            operation.Completed += new EventHandler<ExportDocumentCompletedEventArgs>(this.ExportDocument_Completed);
            operation.Start();
        }

        private static byte[] Serialize(ExportOptionsBase exportOptions)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new XmlXtraSerializer().SerializeObject(exportOptions, (Stream) stream, typeof(ExportOptions).Name);
                return stream.ToArray();
            }
        }

        private void SetResult(ExportDocumentCompletedEventArgs e)
        {
            base.Result = e.Data;
        }
    }
}

