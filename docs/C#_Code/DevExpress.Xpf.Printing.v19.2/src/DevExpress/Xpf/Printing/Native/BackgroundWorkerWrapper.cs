namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class BackgroundWorkerWrapper : IBackgroundWorkerWrapper
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public event EventHandler<DoWorkEventArgs> DoWork;

        public event EventHandler<RunWorkerCompletedEventArgs> RunWorkerCompleted;

        public BackgroundWorkerWrapper()
        {
            this.worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
        }

        public void RunWorkerAsync()
        {
            this.worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.DoWork != null)
            {
                this.DoWork(this, e);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.RunWorkerCompleted != null)
            {
                this.RunWorkerCompleted(this, e);
            }
        }

        public bool IsBusy =>
            this.worker.IsBusy;
    }
}

