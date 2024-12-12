namespace Devart.DbMonitor
{
    using System;
    using System.Collections.Generic;

    internal class i
    {
        private string a;
        private bool b;
        private bool c;
        private bool d;
        private Devart.DbMonitor.i e;
        private List<Devart.DbMonitor.i> f = new List<Devart.DbMonitor.i>();

        internal Devart.DbMonitor.i a() => 
            this.e;

        internal void a(Devart.DbMonitor.i A_0)
        {
            if (!ReferenceEquals(this.e, A_0))
            {
                if (this.e == null)
                {
                    throw new ArgumentNullException("Parent");
                }
                this.e = A_0;
                this.e.e().Add(this);
            }
        }

        public void a(bool A_0)
        {
            this.b = A_0;
        }

        internal void a(List<Devart.DbMonitor.i> A_0)
        {
            this.f = A_0;
        }

        public void a(string A_0)
        {
            this.a = A_0;
        }

        public bool b() => 
            this.d;

        public void b(bool A_0)
        {
            this.d = A_0;
        }

        public bool c() => 
            this.b;

        public void c(bool A_0)
        {
            this.c = A_0;
        }

        public string d() => 
            this.a;

        internal List<Devart.DbMonitor.i> e() => 
            this.f;

        public Devart.DbMonitor.i f()
        {
            Devart.DbMonitor.i i = this.a();
            while ((i != null) && i.i())
            {
                i = i.a();
            }
            return i;
        }

        public bool g() => 
            this.c;

        public Devart.DbMonitor.i h()
        {
            Devart.DbMonitor.i i = this.a();
            while ((i != null) && i.c())
            {
                i = i.a();
            }
            return i;
        }

        public bool i() => 
            this.c() | this.g();
    }
}

