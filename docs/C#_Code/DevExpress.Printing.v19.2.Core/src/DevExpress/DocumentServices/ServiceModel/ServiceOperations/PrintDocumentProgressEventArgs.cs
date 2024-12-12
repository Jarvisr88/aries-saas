namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using System;
    using System.Runtime.CompilerServices;

    public class PrintDocumentProgressEventArgs : EventArgs
    {
        public PrintDocumentProgressEventArgs(int progressPosition)
        {
            this.ProgressPosition = progressPosition;
        }

        public int ProgressPosition { get; private set; }
    }
}

