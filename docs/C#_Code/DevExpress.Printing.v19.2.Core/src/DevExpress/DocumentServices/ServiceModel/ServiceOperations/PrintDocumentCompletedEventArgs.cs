namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.Runtime.CompilerServices;

    public class PrintDocumentCompletedEventArgs : ServiceOperationCompletedEventArgs<PrintId>
    {
        public PrintDocumentCompletedEventArgs(string[] pages, bool stopped, PrintId printId, Exception error, bool cancelled, object userState) : base(printId, error, cancelled, userState)
        {
            this.Pages = pages;
            this.Stopped = stopped;
        }

        public string[] Pages { get; private set; }

        public bool Stopped { get; private set; }
    }
}

