namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class StaSafeHelper
    {
        public static bool? DontTouchStackTrace;

        public static void Invoke(Action worker);
        public static T Invoke<T>(Func<T> worker);

        private static bool IsStaCurrentThread { get; }

        private class STASafeDataStoreExecutionState<T>
        {
            public volatile bool Ready;
            public Exception Error;
            public T Result;
        }
    }
}

