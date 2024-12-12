namespace Devart.Security.SSL
{
    using Devart.Security;
    using System;
    using System.Collections;
    using System.Security.Cryptography;

    internal sealed class n : HashAlgorithm
    {
        private HashAlgorithm a;
        private HashAlgorithm b;
        private byte[] c;
        private w d;
        private HashAlgorithm e;
        private ad f;
        private Queue g;

        public n();
        private int a();
        public void a(ad A_0);
        public void a(byte[] A_0);
        public byte[] a(Devart.Security.e A_0);
        public void a(w A_0);
        protected override void a(bool A_0);
        public bool a(Devart.Security.e A_0, byte[] A_1);
        public bool a(Devart.Security.e A_0, byte[] A_1, byte[] A_2);
        protected override void a(byte[] A_0, int A_1, int A_2);
        private void b();
        public byte[] b(Devart.Security.e A_0, byte[] A_1);
        public byte[] c();
        public ad d();
        public w e();
        protected override byte[] f();
        protected override void Finalize();
        public override void g();
    }
}

