namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using System;
    using System.Runtime.CompilerServices;

    public class CreateDocumentProgressEventArgs : EventArgs
    {
        public CreateDocumentProgressEventArgs(int progressPosition, int pageCount)
        {
            this.PageCount = pageCount;
            this.ProgressPosition = progressPosition;
        }

        public int PageCount { get; private set; }

        public int ProgressPosition { get; private set; }
    }
}

