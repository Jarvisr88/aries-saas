namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class PrintDocumentStartedEventArgs : EventArgs
    {
        public PrintDocumentStartedEventArgs(DevExpress.DocumentServices.ServiceModel.DataContracts.PrintId printId)
        {
            Guard.ArgumentNotNull(printId, "printId");
            this.PrintId = printId;
        }

        public DevExpress.DocumentServices.ServiceModel.DataContracts.PrintId PrintId { get; private set; }
    }
}

