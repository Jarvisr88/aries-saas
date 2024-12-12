namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public class ExportDocumentCompletedEventArgs : ServiceOperationCompletedEventArgs<ExportId>
    {
        public ExportDocumentCompletedEventArgs(byte[] data, ExportId exportId, Exception error, bool cancelled, object userState) : base(exportId, error, cancelled, userState)
        {
            this.Data = data;
        }

        public byte[] Data { get; private set; }
    }
}

