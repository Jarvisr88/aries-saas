namespace Devart.Common
{
    using System;
    using System.IO;

    internal class ap : Stream
    {
        private s a;

        public ap(s A_0);
        public override long a(long A_0, SeekOrigin A_1);
        public override int a(byte[] A_0, int A_1, int A_2);
        public override void b(long A_0);
        public override void b(byte[] A_0, int A_1, int A_2);
        public override void f();

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

