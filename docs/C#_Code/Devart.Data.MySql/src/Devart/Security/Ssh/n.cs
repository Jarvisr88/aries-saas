namespace Devart.Security.Ssh
{
    using Devart.Cryptography;
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Threading;

    internal class n : ag
    {
        private l a;
        private byte[] b;
        private byte[] c;
        private int d;
        private ICryptoTransform e;
        private Devart.Cryptography.e f;
        private ManualResetEvent g;

        public n(l A_0);
        public void a();
        public void a(l A_0);
        public void a(bool A_0);
        private ac a(z A_0, out int A_1);
        public void a(Exception A_0, string A_1);
        public void a(ICryptoTransform A_0, Devart.Cryptography.e A_1);
        public l b();
        public void b(z A_0, out int A_1);
    }
}

