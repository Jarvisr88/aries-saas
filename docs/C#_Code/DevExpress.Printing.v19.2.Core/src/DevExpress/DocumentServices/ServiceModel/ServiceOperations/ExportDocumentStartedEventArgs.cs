namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public class ExportDocumentStartedEventArgs : EventArgs
    {
        public ExportDocumentStartedEventArgs(DevExpress.DocumentServices.ServiceModel.DataContracts.ExportId exportId)
        {
            this.ExportId = exportId;
        }

        public DevExpress.DocumentServices.ServiceModel.DataContracts.ExportId ExportId { get; private set; }
    }
}

