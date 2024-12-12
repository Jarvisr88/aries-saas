namespace Devart.DbMonitor
{
    using System;
    using System.Collections.Generic;

    internal class l : Devart.DbMonitor.e
    {
        private int a;
        private int b;
        private int c;
        private string d;
        private List<Devart.DbMonitor.b> e;
        private int f;

        public l()
        {
            base.a(m.d);
            this.e = new List<Devart.DbMonitor.b>();
        }

        internal List<Devart.DbMonitor.b> a() => 
            this.e;

        public override void a(Devart.DbMonitor.f A_0)
        {
            base.a(A_0);
            this.a(A_0.b());
            this.b(A_0.b());
            this.c(A_0.b());
            this.a(A_0.c());
            int num = A_0.b();
            for (int i = 0; i < num; i++)
            {
                Devart.DbMonitor.b item = new Devart.DbMonitor.b();
                item.c(A_0.c());
                item.b(A_0.c());
                item.a(A_0.c());
                item.d(A_0.c());
                this.a().Add(item);
            }
            this.d(A_0.b());
        }

        public override void a(g A_0)
        {
            base.a(A_0);
            A_0.a(this.c());
            A_0.a(this.b());
            A_0.a(this.f());
            A_0.a(this.d());
            A_0.a(this.a().Count);
            foreach (Devart.DbMonitor.b b in this.a())
            {
                A_0.a(b.d());
                A_0.a(b.b());
                A_0.a(b.a());
                A_0.a(b.c());
            }
            A_0.a(this.e());
        }

        internal void a(List<Devart.DbMonitor.b> A_0)
        {
            this.e = A_0;
        }

        public void a(int A_0)
        {
            this.a = A_0;
        }

        public void a(string A_0)
        {
            this.d = A_0;
        }

        public int b() => 
            this.b;

        public void b(int A_0)
        {
            this.b = A_0;
        }

        public int c() => 
            this.a;

        public void c(int A_0)
        {
            this.c = A_0;
        }

        public string d() => 
            this.d;

        public void d(int A_0)
        {
            this.f = A_0;
        }

        public int e() => 
            this.f;

        public int f() => 
            this.c;
    }
}

