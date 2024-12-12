namespace Devart.DbMonitor
{
    using System;
    using System.Collections.Generic;

    internal class a : Devart.DbMonitor.e
    {
        private int a;
        private int b;
        private int c;
        private string d;
        private Devart.DbMonitor.d e;
        private string f;
        private int g;
        private string h;
        private Devart.DbMonitor.d i;
        private string j;
        private string k;
        private List<Devart.DbMonitor.b> l;
        private List<string> m;
        private Devart.DbMonitor.p n;
        private Devart.DbMonitor.l o;
        private Devart.DbMonitor.i p;

        public a()
        {
            base.a(Devart.DbMonitor.m.c);
            this.l = new List<Devart.DbMonitor.b>();
            this.m = new List<string>();
        }

        internal Devart.DbMonitor.d a() => 
            this.i;

        internal void a(Devart.DbMonitor.d A_0)
        {
            this.i = A_0;
        }

        public override void a(Devart.DbMonitor.f A_0)
        {
            base.a(A_0);
            this.a(A_0.b());
            this.c(A_0.b());
            this.b(A_0.b());
            this.e(A_0.c());
            this.b((Devart.DbMonitor.d) A_0.b());
            this.b(A_0.c());
            this.d(A_0.b());
            this.a(A_0.c());
            this.a((Devart.DbMonitor.d) A_0.b());
            this.c(A_0.c());
            this.d(A_0.c());
            int num = A_0.b();
            for (int i = 0; i < num; i++)
            {
                Devart.DbMonitor.b item = new Devart.DbMonitor.b();
                item.c(A_0.c());
                item.b(A_0.c());
                item.a(A_0.c());
                item.d(A_0.c());
                this.b().Add(item);
            }
            num = A_0.b();
            for (int j = 0; j < num; j++)
            {
                this.l().Add(A_0.c());
            }
        }

        public override void a(Devart.DbMonitor.g A_0)
        {
            base.a(A_0);
            A_0.a(this.o());
            A_0.a(this.g());
            A_0.a(this.k());
            A_0.a(this.p());
            A_0.a((int) this.n());
            A_0.a(this.f());
            A_0.a(this.j());
            A_0.a(this.d());
            A_0.a((int) this.a());
            A_0.a(this.i());
            A_0.a(this.e());
            A_0.a(this.b().Count);
            foreach (Devart.DbMonitor.b b in this.b())
            {
                A_0.a(b.d());
                A_0.a(b.b());
                A_0.a(b.a());
                A_0.a(b.c());
            }
            A_0.a(this.l().Count);
            foreach (string str in this.l())
            {
                A_0.a(str);
            }
        }

        internal void a(Devart.DbMonitor.i A_0)
        {
            this.p = A_0;
        }

        internal void a(Devart.DbMonitor.l A_0)
        {
            this.o = A_0;
        }

        internal void a(Devart.DbMonitor.p A_0)
        {
            this.n = A_0;
        }

        internal void a(List<Devart.DbMonitor.b> A_0)
        {
            this.l = A_0;
        }

        public void a(List<string> A_0)
        {
            this.m = A_0;
        }

        public void a(int A_0)
        {
            this.a = A_0;
        }

        public void a(string A_0)
        {
            this.h = A_0;
        }

        internal List<Devart.DbMonitor.b> b() => 
            this.l;

        internal void b(Devart.DbMonitor.d A_0)
        {
            this.e = A_0;
        }

        public void b(int A_0)
        {
            this.c = A_0;
        }

        public void b(string A_0)
        {
            this.f = A_0;
        }

        internal Devart.DbMonitor.p c() => 
            this.n;

        public void c(int A_0)
        {
            this.b = A_0;
        }

        public void c(string A_0)
        {
            this.j = A_0;
        }

        public string d() => 
            this.h;

        public void d(int A_0)
        {
            this.g = A_0;
        }

        public void d(string A_0)
        {
            this.k = A_0;
        }

        public string e() => 
            this.k;

        public void e(string A_0)
        {
            this.d = A_0;
        }

        public string f() => 
            this.f;

        public int g() => 
            this.b;

        internal Devart.DbMonitor.i h() => 
            this.p;

        public string i() => 
            this.j;

        public int j() => 
            this.g;

        public int k() => 
            this.c;

        public List<string> l() => 
            this.m;

        internal Devart.DbMonitor.l m() => 
            this.o;

        internal Devart.DbMonitor.d n() => 
            this.e;

        public int o() => 
            this.a;

        public string p() => 
            this.d;
    }
}

