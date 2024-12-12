namespace DevExpress.Utils
{
    using System;

    public class BatchUpdateHelper
    {
        private IBatchUpdateHandler batchUpdateHandler;
        private int suspendUpdateCount;
        private bool overlappedTransaction;

        public BatchUpdateHelper(IBatchUpdateHandler batchUpdateHandler)
        {
            if (batchUpdateHandler == null)
            {
                throw new ArgumentException("batchUpdateHandler", "batchUpdateHandler");
            }
            this.batchUpdateHandler = batchUpdateHandler;
        }

        public void BeginUpdate()
        {
            if (!this.overlappedTransaction)
            {
                if (!this.IsUpdateLocked)
                {
                    this.overlappedTransaction = true;
                    try
                    {
                        this.batchUpdateHandler.OnFirstBeginUpdate();
                    }
                    finally
                    {
                        this.overlappedTransaction = false;
                    }
                }
                this.batchUpdateHandler.OnBeginUpdate();
                this.suspendUpdateCount++;
            }
        }

        public void CancelUpdate()
        {
            if (!this.overlappedTransaction && this.IsUpdateLocked)
            {
                this.batchUpdateHandler.OnCancelUpdate();
                this.suspendUpdateCount--;
                if (!this.IsUpdateLocked)
                {
                    this.overlappedTransaction = true;
                    try
                    {
                        this.batchUpdateHandler.OnLastCancelUpdate();
                    }
                    finally
                    {
                        this.overlappedTransaction = false;
                    }
                }
            }
        }

        public void EndUpdate()
        {
            if (!this.overlappedTransaction && this.IsUpdateLocked)
            {
                this.batchUpdateHandler.OnEndUpdate();
                this.suspendUpdateCount--;
                if (!this.IsUpdateLocked)
                {
                    this.overlappedTransaction = true;
                    try
                    {
                        this.batchUpdateHandler.OnLastEndUpdate();
                    }
                    finally
                    {
                        this.overlappedTransaction = false;
                    }
                }
            }
        }

        public bool IsUpdateLocked =>
            this.suspendUpdateCount > 0;

        public int SuspendUpdateCount =>
            this.suspendUpdateCount;

        public IBatchUpdateHandler BatchUpdateHandler
        {
            get => 
                this.batchUpdateHandler;
            set => 
                this.batchUpdateHandler = value;
        }

        public bool OverlappedTransaction =>
            this.overlappedTransaction;
    }
}

