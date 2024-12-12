namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ZipCopyStreamOperationProgress : IZipOperationProgress
    {
        private double weight;
        private long totalSize;
        private long totalBytesCopied;
        private double currentProgress;
        private bool isStopped;

        public event EventHandler NotifyProgress;

        public ZipCopyStreamOperationProgress(long totalSize) : this(totalSize, 1.0)
        {
        }

        public ZipCopyStreamOperationProgress(long totalSize, double weight)
        {
            this.weight = weight;
            this.totalSize = totalSize;
        }

        public bool CopyHandler(int bytesCopied)
        {
            if (this.totalSize > 0L)
            {
                this.totalBytesCopied += bytesCopied;
                this.currentProgress = (1.0 * this.totalBytesCopied) / ((double) this.totalSize);
                if (this.NotifyProgress != null)
                {
                    this.NotifyProgress(this, EventArgs.Empty);
                }
            }
            return !this.isStopped;
        }

        public void Stop()
        {
            this.isStopped = true;
        }

        public double CurrentProgress =>
            this.currentProgress;

        public double Weight =>
            this.weight;

        public long TotalSize =>
            this.totalSize;

        public bool IsStopped =>
            this.isStopped;
    }
}

