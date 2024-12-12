namespace Devart.Security.Ssh
{
    using System;
    using System.Net.Sockets;

    internal class m : Devart.Security.Ssh.d
    {
        private Socket a;
        private byte[] b;
        private bool c;
        private const int d = 0x10000;
        private z e;

        internal m(Socket A_0, ag A_1);
        internal override void a();
        internal override void a(byte A_0);
        private void a(IAsyncResult A_0);
        internal override void a(byte[] A_0, int A_1, int A_2);
        internal override void b();
        internal override bool c();
        internal void d();
    }
}

