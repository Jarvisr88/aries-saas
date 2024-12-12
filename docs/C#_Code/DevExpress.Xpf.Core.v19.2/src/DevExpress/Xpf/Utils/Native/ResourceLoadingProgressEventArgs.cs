namespace DevExpress.Xpf.Utils.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class ResourceLoadingProgressEventArgs : EventArgs
    {
        public ResourceLoadingProgressEventArgs(long recieved, long total)
        {
            this.TotalBytes = total;
            this.BytesRecieved = recieved;
        }

        public long TotalBytes { get; private set; }

        public long BytesRecieved { get; private set; }
    }
}

