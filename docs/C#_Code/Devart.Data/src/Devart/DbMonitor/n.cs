namespace Devart.DbMonitor
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    internal class n : IDisposable
    {
        private const string a = "{0} monitoring is started";
        private const string b = "{0} monitoring is finished";
        private const string c = "dbMonitor is not active";
        private k d;
        private string e;
        private string f;
        private int g;
        private int h = 0x1388;
        private int i = 0x3e8;
        private int j;

        public int a() => 
            this.i;

        public void a(Devart.DbMonitor.c A_0)
        {
            this.c(A_0);
            this.b(A_0);
        }

        public void a(int A_0)
        {
            this.i = A_0;
        }

        public void a(string A_0)
        {
            this.f = A_0;
        }

        public string b() => 
            this.f;

        public void b(Devart.DbMonitor.c A_0)
        {
            if ((this.d == null) || !this.d.i())
            {
                this.g();
            }
            l l = new l();
            l.f(Process.GetCurrentProcess().Id);
            l.a(A_0.i());
            l.f(A_0.g());
            l.b(Environment.TickCount);
            if (A_0.p() == 0)
            {
                l.a(this.j);
            }
            else
            {
                l.a(A_0.p());
            }
            l.c(Convert.ToInt32(A_0.o()));
            l.a(A_0.b());
            l.a(A_0.c());
            l.d(A_0.h());
            this.d.a(l);
        }

        public void b(int A_0)
        {
            this.h = A_0;
        }

        public void b(string A_0)
        {
            this.e = A_0;
        }

        public int c() => 
            this.g;

        public void c(Devart.DbMonitor.c A_0)
        {
            if ((this.d == null) || !this.d.i())
            {
                this.g();
            }
            A_0.a(Interlocked.Increment(ref this.j));
            Devart.DbMonitor.a a = new Devart.DbMonitor.a();
            a.f(Process.GetCurrentProcess().Id);
            a.a(A_0.i());
            a.f(A_0.g());
            a.c(Environment.TickCount);
            a.a(A_0.p());
            a.b(A_0.l());
            a.e(A_0.q());
            a.b(A_0.n());
            a.b(A_0.f());
            a.d(A_0.k());
            a.a(A_0.d());
            a.a(A_0.a());
            a.c(A_0.j());
            a.d(A_0.e());
            a.a(A_0.c());
            a.a(A_0.m());
            this.d.a(a);
        }

        public void c(int A_0)
        {
            this.g = A_0;
        }

        public void d()
        {
            if ((this.d != null) && this.d.b())
            {
                this.f();
            }
            this.d = null;
        }

        public bool e()
        {
            if ((this.d != null) && this.d.i())
            {
                return true;
            }
            this.d ??= new k();
            this.d.b(this.b());
            this.d.d(this.c());
            this.d.c(this.i());
            this.d.b(this.a());
            return this.d.g();
        }

        public void f()
        {
            if ((this.d != null) && this.d.i())
            {
                Devart.DbMonitor.e e = new Devart.DbMonitor.e();
                e.f(Process.GetCurrentProcess().Id);
                e.a(Devart.DbMonitor.j.b);
                e.f($"{this.h()} monitoring is finished");
                this.d.a(e);
                this.d.c();
            }
        }

        public void g()
        {
            if (!this.e())
            {
                throw new InvalidOperationException("dbMonitor is not active");
            }
            Devart.DbMonitor.h h = new Devart.DbMonitor.h();
            h.f(Process.GetCurrentProcess().Id);
            h.a(Devart.DbMonitor.j.a);
            h.f($"{this.h()} monitoring is started");
            h.a(Application.ExecutablePath);
            if (string.IsNullOrEmpty(h.a()))
            {
                h.a(typeof(Devart.DbMonitor.h).Assembly.Location);
            }
            this.d.a(h);
        }

        public string h() => 
            this.e;

        public int i() => 
            this.h;
    }
}

