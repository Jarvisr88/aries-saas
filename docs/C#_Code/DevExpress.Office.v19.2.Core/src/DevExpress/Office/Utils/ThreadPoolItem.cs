namespace DevExpress.Office.Utils
{
    using System;
    using System.Threading;

    public class ThreadPoolItem : IDisposable
    {
        private const int CanExecuteEvent = 0;
        private const int AbortExecutionEvent = 1;
        private EventWaitHandle[] handles = new EventWaitHandle[] { new AutoResetEvent(false), new ManualResetEvent(false) };
        private AutoResetEvent complete = new AutoResetEvent(false);
        private WaitCallback currentJob;

        public ThreadPoolItem()
        {
            this.CreateThread();
        }

        private void CreateThread()
        {
            new Thread(new ThreadStart(this.Worker)) { 
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            }.Start();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.handles != null))
            {
                this.AbortExecution.Set();
                this.CanExecute.Dispose();
                this.Complete.Dispose();
                this.handles = null;
            }
        }

        protected internal virtual void Worker()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        int num = WaitHandle.WaitAny(this.handles);
                        if (num == 0)
                        {
                            try
                            {
                                this.CurrentJob(null);
                            }
                            catch
                            {
                            }
                            this.Complete.Set();
                        }
                        else
                        {
                            break;
                        }
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        public WaitCallback CurrentJob
        {
            get => 
                this.currentJob;
            set => 
                this.currentJob = value;
        }

        public EventWaitHandle CanExecute =>
            this.handles[0];

        public EventWaitHandle AbortExecution =>
            this.handles[1];

        public EventWaitHandle Complete =>
            this.complete;
    }
}

