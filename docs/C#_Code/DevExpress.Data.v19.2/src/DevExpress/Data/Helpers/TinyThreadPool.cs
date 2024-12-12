namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    internal static class TinyThreadPool
    {
        private static readonly ConcurrentQueue<Action> Q;
        private static readonly AutoResetEvent SomethingInQ;
        private static int spareThreads;
        private static int theThreadRun;

        static TinyThreadPool();
        private static void Core();
        public static void Enqueue(Action action);
        private static void PooledThread(object useless);
        private static void RunThreadPoolThread();
        private static void TheThread();
        private static void TryRunTheThread();
    }
}

