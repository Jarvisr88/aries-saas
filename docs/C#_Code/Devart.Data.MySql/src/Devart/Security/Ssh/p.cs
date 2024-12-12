namespace Devart.Security.Ssh
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Sockets;
    using System.Security.Cryptography;

    internal abstract class p
    {
        private static int a;
        internal Devart.Security.Ssh.d b;
        internal ae c;
        protected byte[] d;
        internal ICryptoTransform e;
        protected Devart.Security.Ssh.f f;
        protected object g;
        protected bool h;
        protected bool i;
        protected Devart.Security.Ssh.e j;
        protected HybridDictionary k;
        protected int l;

        static p();
        protected p(Devart.Security.Ssh.f A_0, ae A_1);
        internal abstract ag a();
        internal abstract Devart.Security.Ssh.e a(Devart.Security.Ssh.d A_0);
        public void a(bool A_0);
        internal void a(int A_0);
        public abstract void a(string A_0);
        internal int a(ad A_0, Devart.Security.Ssh.j A_1);
        private static void a(Devart.Security.Ssh.d A_0, Devart.Security.Ssh.f A_1);
        internal void a(Exception A_0, string A_1);
        public abstract void a(string A_0, int A_1);
        public static Devart.Security.Ssh.p a(Devart.Security.Ssh.f A_0, ae A_1, Socket A_2);
        private static Devart.Security.Ssh.p a(Devart.Security.Ssh.f A_0, ae A_1, av A_2, Devart.Security.Ssh.d A_3);
        public Devart.Security.Ssh.p a(Devart.Security.Ssh.f A_0, ae A_1, string A_2, int A_3);
        public abstract ad a(Devart.Security.Ssh.j A_0, string A_1, int A_2, string A_3, int A_4);
        public abstract void b();
        internal Devart.Security.Ssh.p.a b(int A_0);
        public abstract void b(string A_0);
        public abstract void b(string A_0, int A_1);
        internal static Devart.Security.Ssh.p b(Devart.Security.Ssh.f A_0, ae A_1, av A_2, Devart.Security.Ssh.d A_3);
        public abstract ai c();
        public virtual bool d();
        public bool i();
        public Devart.Security.Ssh.f j();
        protected void k();
        public ae l();
        protected int m();
        public bool n();
        public virtual int o();
        public Devart.Security.Ssh.e p();
        public bool q();

        internal class a
        {
            private int a;
            private ad b;

            public a(int A_0, ad A_1);
            public int a();
            public void a(ad A_0);
            public void a(int A_0);
            public ad b();
        }
    }
}

