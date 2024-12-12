namespace Devart.Common
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    internal class a8 : Devart.Common.g
    {
        private string a;
        private int b;
        private int c;
        private int d;
        private TcpClient e;
        private Stream f;
        [CompilerGenerated]
        private bool g;
        private int h;

        public a8(string A_0, int A_1);
        public a8(string A_0, int A_1, int A_2, int A_3);
        protected override void a();
        protected override void a(bool A_0);
        protected override bool a(Exception A_0);
        private void a(byte[] A_0);
        public override void a(int A_0);
        private Stream a(string A_0, int A_1);
        protected override void a(byte[] A_0, int A_1, int A_2);
        [CompilerGenerated]
        public void b(bool A_0);
        protected override int b(byte[] A_0, int A_1, int A_2);
        public override int c();
        private Stream d();
        public IPEndPoint e();
        [CompilerGenerated]
        public bool f();
        public override bool g();
    }
}

