namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class DocumentProgressChangedEventArgs : EventArgs
    {
        public DocumentProgressChangedEventArgs(bool isCompleted, long totalProgress, long progress)
        {
            this.IsCompleted = isCompleted;
            this.TotalProgress = totalProgress;
            this.Progress = progress;
        }

        public bool IsCompleted { get; private set; }

        public long TotalProgress { get; private set; }

        public long Progress { get; private set; }
    }
}

