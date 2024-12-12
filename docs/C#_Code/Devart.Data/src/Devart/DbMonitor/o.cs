namespace Devart.DbMonitor
{
    using System;

    internal abstract class o
    {
        private m a;
        private int b;

        public o(m A_0)
        {
            this.a = A_0;
        }

        public virtual void a(Devart.DbMonitor.f A_0)
        {
            this.b = A_0.b();
        }

        public virtual void a(g A_0)
        {
            A_0.a(this.b);
        }

        internal void a(m A_0)
        {
            this.a = A_0;
        }

        public void f(int A_0)
        {
            this.b = A_0;
        }

        public int u() => 
            this.b;

        internal m v() => 
            this.a;
    }
}

