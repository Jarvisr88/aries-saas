namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public class CreateDocumentStartedEventArgs : EventArgs
    {
        public CreateDocumentStartedEventArgs(DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId documentId)
        {
            if (documentId == null)
            {
                throw new ArgumentNullException("documentId");
            }
            this.DocumentId = documentId;
        }

        public DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId DocumentId { get; private set; }
    }
}

