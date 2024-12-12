namespace DevExpress.Printing.Core
{
    using System;
    using System.Threading;

    internal class DefaultThreadingServices : IThreadingServices
    {
        public int CurrentThreadID =>
            Thread.CurrentThread.ManagedThreadId;
    }
}

