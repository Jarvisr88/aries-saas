namespace DevExpress.Data.Async.Helpers
{
    using System;

    public class GetTypeInfoEventArgs : EventArgs
    {
        public readonly object ListServerSource;
        public readonly object Tag;
        public object TypeInfo;

        public GetTypeInfoEventArgs(object listServerSource, object tag);
    }
}

