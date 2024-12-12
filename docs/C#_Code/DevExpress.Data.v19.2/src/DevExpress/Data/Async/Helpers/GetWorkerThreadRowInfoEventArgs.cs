namespace DevExpress.Data.Async.Helpers
{
    using System;

    public class GetWorkerThreadRowInfoEventArgs : EventArgs
    {
        public readonly object TypeInfo;
        public readonly object WorkerThreadRow;
        public object RowInfo;

        public GetWorkerThreadRowInfoEventArgs(object typeInfo, object workerThreadRow);
    }
}

