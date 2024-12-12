namespace Devart.Security
{
    using System;
    using System.Threading;

    internal class t : IAsyncResult
    {
        private bool a;
        private object b;
        private aj c;
        private string d;
        private Devart.Security.f e;
        private ad f;
        private ManualResetEvent g;
        private AsyncCallback h;
        private bool i;
        private Exception j;
        private o k;

        public t(aj A_0, string A_1, Devart.Security.f A_2, ad A_3, AsyncCallback A_4, object A_5);
        public void a(bool A_0);
        internal void a(Exception A_0, o A_1);
        public string c();
        public Devart.Security.f d();
        public aj g();
        public Exception h();
        public bool i();
        public o j();
        public ad k();

        [__DynamicallyInvokable]
        public bool System.IAsyncResult.CompletedSynchronously { get; }

        [__DynamicallyInvokable]
        public bool System.IAsyncResult.IsCompleted { get; }

        [__DynamicallyInvokable]
        public WaitHandle System.IAsyncResult.AsyncWaitHandle { get; }

        [__DynamicallyInvokable]
        public object System.IAsyncResult.AsyncState { get; }
    }
}

