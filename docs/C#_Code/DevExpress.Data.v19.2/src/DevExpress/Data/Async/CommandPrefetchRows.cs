namespace DevExpress.Data.Async
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.Threading;

    public class CommandPrefetchRows : Command
    {
        public ListSourceGroupInfo[] GroupsToPrefetch;
        public bool Successful;
        internal System.Threading.CancellationTokenSource CancellationTokenSource;

        public CommandPrefetchRows(ListSourceGroupInfo[] groupsToPrefetch, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
        public override void Cancel();
    }
}

