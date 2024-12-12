namespace Devart.Security.SSL
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class r : IAsyncResult
    {
        private bool a;
        private object b;
        private object c;
        private ManualResetEvent d;
        private Exception e;
        [CompilerGenerated]
        private AsyncCallback f;

        internal r(AsyncCallback A_0, object A_1, object A_2);
        [CompilerGenerated]
        public void a(AsyncCallback A_0);
        public void a(Exception A_0);
        [CompilerGenerated]
        public void b(AsyncCallback A_0);
        public void b(Exception A_0);
        public Exception d();
        public void e();

        [__DynamicallyInvokable]
        public bool System.IAsyncResult.IsCompleted { get; }

        [__DynamicallyInvokable]
        public bool System.IAsyncResult.CompletedSynchronously { get; }

        [__DynamicallyInvokable]
        public object System.IAsyncResult.AsyncState { get; }

        [__DynamicallyInvokable]
        public WaitHandle System.IAsyncResult.AsyncWaitHandle { get; }
    }
}

