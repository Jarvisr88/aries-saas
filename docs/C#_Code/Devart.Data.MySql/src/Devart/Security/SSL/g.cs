namespace Devart.Security.SSL
{
    using Devart.Security;
    using System;
    using System.Security.Cryptography;

    internal abstract class g : IDisposable
    {
        protected Devart.Security.SSL.l a;
        protected bool b;
        protected RNGCryptoServiceProvider c;
        protected Devart.Security.SSL.h d;
        protected ap e;
        protected byte[] f;
        protected byte[] g;
        protected byte[] h;
        protected byte[] i;
        protected byte[] j;
        protected byte[] k;
        protected byte[] l;
        protected ac m;
        protected ac n;
        protected Devart.Security.SSL.p o;
        protected Devart.Security.e p;
        protected HashAlgorithm q;
        protected SHA1CryptoServiceProvider r;
        protected HashAlgorithm s;
        protected SHA1CryptoServiceProvider t;
        protected SHA256Managed u;
        protected SHA256Managed v;
        protected Devart.Security.SSL.n w;
        protected bool x;
        protected RSACryptoServiceProvider y;
        protected bool z;
        protected bool aa;

        public g(Devart.Security.SSL.g A_0);
        public g(Devart.Security.SSL.h A_0, Devart.Security.SSL.l A_1);
        public abstract ad a();
        public byte[] a(af A_0);
        protected abstract au a(ai A_0);
        protected abstract au a(ar A_0);
        internal void a(Devart.Security.SSL.h A_0);
        public void a(bool A_0);
        protected abstract void a(byte[] A_0);
        protected void a(aj A_0, bool A_1);
        protected void a(ai A_0, ao A_1);
        protected void a(byte[] A_0, ao A_1);
        protected au a(ai A_0, bool A_1);
        protected ai a(byte[] A_0, int A_1);
        protected abstract byte[] a(byte[] A_0, byte[] A_1, byte[] A_2);
        protected abstract byte[] b();
        public au b(ar A_0);
        public abstract au b(byte[] A_0);
        public abstract Devart.Security.SSL.b c();
        protected au c(ar A_0);
        protected abstract byte[] d();
        protected abstract byte[] e();
        protected Devart.Security.e[] e(byte[] A_0);
        public bool f();
        protected void f(byte[] A_0);
        protected override void g();
        public ac h();
        internal Devart.Security.SSL.h i();
        protected void j();
        public Devart.Security.SSL.e k();
        protected byte[] l();
        public RNGCryptoServiceProvider m();
        public void n();
        public bool o();
        public Devart.Security.e p();
    }
}

