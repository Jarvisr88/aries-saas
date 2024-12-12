namespace Devart.Security.SSL
{
    using System;
    using System.IO;

    internal class u : Stream
    {
        private q a;
        private bool b;
        private bool c;
        private bool d;
        private Devart.Security.SSL.e e;

        public u(Devart.Security.SSL.e A_0);
        public u(Devart.Security.SSL.e A_0, bool A_1);
        public u(Devart.Security.SSL.e A_0, FileAccess A_1);
        public u(Devart.Security.SSL.e A_0, FileAccess A_1, bool A_2);
        internal q a();
        public void a(l A_0);
        internal void a(q A_0);
        private void a(Exception A_0);
        private void a(IAsyncResult A_0);
        public override long a(long A_0, SeekOrigin A_1);
        public override int a(byte[] A_0, int A_1, int A_2);
        public override void b();
        public override int b(IAsyncResult A_0);
        public override void b(long A_0);
        public override void b(byte[] A_0, int A_1, int A_2);
        public override void c(IAsyncResult A_0);
        public override void e();
        protected Devart.Security.SSL.e i();

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanRead { get; }

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanWrite { get; }

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanSeek { get; }

        [__DynamicallyInvokable]
        public override long System.IO.Stream.Length { get; }

        [__DynamicallyInvokable]
        public override long System.IO.Stream.Position { get; set; }
    }
}

