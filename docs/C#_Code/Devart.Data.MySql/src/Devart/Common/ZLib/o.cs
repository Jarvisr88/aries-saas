namespace Devart.Common.ZLib
{
    using System;
    using System.IO;

    internal class o : Stream
    {
        protected Devart.Common.ZLib.b a;
        protected byte[] b;
        protected int c;
        private byte[] d;
        protected Stream e;
        protected byte[] f;
        private uint[] g;

        internal o(Stream A_0);
        internal o(Stream A_0, Devart.Common.ZLib.b A_1);
        internal o(Stream A_0, Devart.Common.ZLib.b A_1, int A_2);
        protected void a();
        protected void a(byte A_0);
        protected void a(string A_0);
        public override long a(long A_0, SeekOrigin A_1);
        private uint a(uint A_0, byte A_1);
        protected void a(byte[] A_0, int A_1, int A_2);
        public override void b();
        public override void b(byte A_0);
        public override void b(long A_0);
        public override int b(byte[] A_0, int A_1, int A_2);
        internal long c(long A_0);
        public override void c(byte[] A_0, int A_1, int A_2);
        public override int d();
        public override void f();
        internal virtual int j();
        protected byte k();

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanRead { get; }

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanSeek { get; }

        [__DynamicallyInvokable]
        public override bool System.IO.Stream.CanWrite { get; }

        [__DynamicallyInvokable]
        public override long System.IO.Stream.Length { get; }

        [__DynamicallyInvokable]
        public override long System.IO.Stream.Position { get; set; }
    }
}

