namespace DevExpress.Data.Helpers
{
    using System;

    public class ServerModeOptimalFetchResult
    {
        public readonly int Skip;
        public readonly int Take;
        public readonly bool IsFromEnd;

        public ServerModeOptimalFetchResult(bool isFromEnd, int skip, int take);
    }
}

