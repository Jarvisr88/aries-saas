namespace Devart.DbMonitor
{
    using System;

    internal class e : o
    {
        private j a;
        private string b;
        private DateTime c;
        private int d;

        public e() : base(m.a)
        {
        }

        public override void a(Devart.DbMonitor.f A_0)
        {
            base.a(A_0);
            this.a((j) A_0.b());
            this.f(A_0.c());
        }

        public override void a(g A_0)
        {
            base.a(A_0);
            A_0.a((int) this.r());
            A_0.a(this.t());
        }

        internal void a(j A_0)
        {
            this.a = A_0;
        }

        public void a(DateTime A_0)
        {
            this.c = A_0;
        }

        public void e(int A_0)
        {
            this.d = A_0;
        }

        public void f(string A_0)
        {
            this.b = A_0;
        }

        public DateTime q() => 
            this.c;

        internal j r() => 
            this.a;

        public int s() => 
            this.d;

        public string t() => 
            this.b;
    }
}

