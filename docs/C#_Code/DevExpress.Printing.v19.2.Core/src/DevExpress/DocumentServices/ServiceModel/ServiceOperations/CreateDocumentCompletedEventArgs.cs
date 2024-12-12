namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;

    public class CreateDocumentCompletedEventArgs : ServiceOperationCompletedEventArgs<DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId>
    {
        public CreateDocumentCompletedEventArgs(DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId documentId, Exception error, bool cancelled, object userState) : base(documentId, error, cancelled, userState)
        {
        }

        public DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId DocumentId =>
            base.OperationId;
    }
}

