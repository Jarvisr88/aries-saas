namespace DevExpress.Office.History
{
    using DevExpress.Utils;
    using System;

    public class HistoryTransaction : IDisposable
    {
        private readonly DocumentHistory history;
        private bool suppressRaiseOperationComplete;
        private bool disposed;

        public HistoryTransaction(DocumentHistory history)
        {
            Guard.ArgumentNotNull(history, "history");
            this.history = history;
            history.BeginTransaction();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                bool flag = this.history.TransactionLevel == 1;
                if (flag && this.SuppressRaiseOperationComplete)
                {
                    this.history.SuppressRaiseOperationComplete = true;
                }
                this.history.EndTransaction();
                if (flag && this.SuppressRaiseOperationComplete)
                {
                    this.history.SuppressRaiseOperationComplete = false;
                }
            }
            this.disposed = true;
        }

        public bool SuppressRaiseOperationComplete
        {
            get => 
                this.suppressRaiseOperationComplete;
            set => 
                this.suppressRaiseOperationComplete = value;
        }
    }
}

