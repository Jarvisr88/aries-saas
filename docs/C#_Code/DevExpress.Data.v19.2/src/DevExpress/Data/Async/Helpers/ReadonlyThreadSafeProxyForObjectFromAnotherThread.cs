namespace DevExpress.Data.Async.Helpers
{
    using System;

    public class ReadonlyThreadSafeProxyForObjectFromAnotherThread
    {
        public readonly object[] Content;
        public readonly object OriginalRow;

        public ReadonlyThreadSafeProxyForObjectFromAnotherThread(object original, object[] content);
        public static object ExtractOriginalRow(object uiThreadRow);
    }
}

