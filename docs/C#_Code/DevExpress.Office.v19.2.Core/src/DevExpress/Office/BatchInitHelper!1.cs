namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;

    public class BatchInitHelper<T> : BatchUpdateHelper<T>
    {
        public BatchInitHelper(IBatchInitHandler handler) : base(new InnerBatchUpdateHandler<T>(handler))
        {
        }

        public IBatchInitHandler BatchInitHandler =>
            ((InnerBatchUpdateHandler<T>) base.BatchUpdateHandler).BatchInitHandler;

        private class InnerBatchUpdateHandler : IBatchUpdateHandler
        {
            private IBatchInitHandler batchInitHandler;

            public InnerBatchUpdateHandler(IBatchInitHandler batchInitHandler)
            {
                Guard.ArgumentNotNull(batchInitHandler, "batchInitHandler");
                this.batchInitHandler = batchInitHandler;
            }

            public void OnBeginUpdate()
            {
                this.batchInitHandler.OnBeginInit();
            }

            public void OnCancelUpdate()
            {
                this.batchInitHandler.OnCancelInit();
            }

            public void OnEndUpdate()
            {
                this.batchInitHandler.OnEndInit();
            }

            public void OnFirstBeginUpdate()
            {
                this.batchInitHandler.OnFirstBeginInit();
            }

            public void OnLastCancelUpdate()
            {
                this.batchInitHandler.OnLastCancelInit();
            }

            public void OnLastEndUpdate()
            {
                this.batchInitHandler.OnLastEndInit();
            }

            public IBatchInitHandler BatchInitHandler =>
                this.batchInitHandler;
        }
    }
}

