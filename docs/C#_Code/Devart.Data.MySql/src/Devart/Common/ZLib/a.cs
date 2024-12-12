namespace Devart.Common.ZLib
{
    using System;
    using System.IO;

    internal class a : Stream
    {
        protected byte[] a;
        protected Devart.Common.ZLib.j b;
        protected Stream c;

        internal a(Stream A_0);
        internal a(Stream A_0, Devart.Common.ZLib.j A_1);
        internal a(Stream A_0, Devart.Common.ZLib.j A_1, int A_2);
        public override void a();
        public override void a(byte A_0);
        public override long a(long A_0, SeekOrigin A_1);
        public override int a(byte[] A_0, int A_1, int A_2);
        public override void b(long A_0);
        public override void b(byte[] A_0, int A_1, int A_2);
        public override int c();
        public override void f();
        internal virtual void i();
        protected void j();

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

