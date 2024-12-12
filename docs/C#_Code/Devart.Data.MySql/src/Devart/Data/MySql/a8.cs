namespace Devart.Data.MySql
{
    using System;
    using System.Runtime.InteropServices;

    internal abstract class a8 : IDisposable
    {
        protected ad[] a;
        protected int[] b;
        protected int[] c;
        public readonly int d;
        protected aw e;
        protected bool f;
        protected byte[] g;

        protected a8(ad[] A_0, aw A_1);
        protected a8(int A_0, aw A_1);
        public abstract int a();
        public void a(ad[] A_0);
        public virtual void a(bool A_0);
        public abstract void a(int A_0);
        public virtual byte[] a(int A_0, out int A_1, out int A_2);
        public virtual long a(int A_0, long A_1, byte[] A_2, int A_3, int A_4);
        public virtual long a(int A_0, long A_1, char[] A_2, int A_3, int A_4);
        public abstract int b();
        public virtual bool b(int A_0);
        public abstract bool c();
        public abstract void Dispose();
        public abstract bool e();
        public virtual void i();
        public virtual bool j();
        public ad[] n();
    }
}

