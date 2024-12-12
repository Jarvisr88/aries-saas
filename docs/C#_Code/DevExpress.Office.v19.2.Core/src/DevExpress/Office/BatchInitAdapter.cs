namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;

    public class BatchInitAdapter : IBatchUpdateHandler
    {
        private readonly IBatchInitHandler batchInitHandler;

        public BatchInitAdapter(IBatchInitHandler batchInitHandler)
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

