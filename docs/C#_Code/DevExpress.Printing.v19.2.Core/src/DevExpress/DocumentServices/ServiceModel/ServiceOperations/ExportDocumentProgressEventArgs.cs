namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExportDocumentProgressEventArgs : EventArgs
    {
        public ExportDocumentProgressEventArgs(int progressPosition)
        {
            this.ProgressPosition = progressPosition;
        }

        public int ProgressPosition { get; private set; }
    }
}

