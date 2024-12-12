namespace Devart.Common
{
    using Devart.DbMonitor;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class m : IDisposable
    {
        private Devart.DbMonitor.n a;
        private Thread b;
        private AutoResetEvent c = new AutoResetEvent(true);
        private LinkedList<Devart.Common.m.a> d = new LinkedList<Devart.Common.m.a>();
        private bool e;
        private object f = new object();
        private Stack<int> g = new Stack<int>(10);
        private string h = "localhost";
        private int i = 0x3e8;
        private int j = 0x3e8;
        private int k;
        private string l;

        private void a()
        {
            while (true)
            {
                int count;
                while (true)
                {
                    if (this.e)
                    {
                        return;
                    }
                    this.c.WaitOne();
                    count = this.d.Count;
                    break;
                }
                while (this.d.Count > 0)
                {
                    Devart.Common.m.a a = this.d.First.Value;
                    try
                    {
                        if (this.a == null)
                        {
                            this.a = new Devart.DbMonitor.n();
                            this.a.b(this.c());
                            this.a.a(this.e());
                            this.a.c(this.g());
                            this.a.g();
                        }
                        Devart.DbMonitor.c c = a.a;
                        if (a.b == MonitorTracePoint.BeforeEvent)
                        {
                            this.a.c(c);
                            this.a(c.p());
                        }
                        else
                        {
                            if (a.b != MonitorTracePoint.AfterEvent)
                            {
                                throw new NotSupportedException(a.b.ToString());
                            }
                            c.a(this.f());
                            this.a.b(c);
                        }
                        lock (this.f)
                        {
                            this.d.RemoveFirst();
                        }
                        continue;
                    }
                    catch
                    {
                        this.a = null;
                        lock (this.f)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                this.d.RemoveFirst();
                            }
                        }
                    }
                    break;
                }
            }
        }

        internal void a(int A_0)
        {
            this.g.Push(A_0);
        }

        public void a(string A_0)
        {
            if (this.h != A_0)
            {
                this.h = A_0;
                this.b();
            }
        }

        public void a(Devart.DbMonitor.c A_0, MonitorTracePoint A_1, bool A_2)
        {
            Devart.Common.m.a a = new Devart.Common.m.a(A_0, A_1, A_2);
            lock (this.f)
            {
                if ((this.j > 0) && (this.d.Count > this.j))
                {
                    this.k++;
                }
                else
                {
                    if (this.k > 0)
                    {
                        Devart.DbMonitor.c c1 = new Devart.DbMonitor.c();
                        c1.a(Devart.DbMonitor.j.q);
                        c1.d($"{this.k} events rejected.");
                        Devart.Common.m.a a2 = new Devart.Common.m.a(c1, MonitorTracePoint.BeforeEvent, false);
                        this.d.AddLast(a2);
                        a2 = new Devart.Common.m.a(c1, MonitorTracePoint.AfterEvent, false);
                        this.d.AddLast(a2);
                        this.k = 0;
                    }
                    this.d.AddLast(a);
                }
            }
            if (this.b == null)
            {
                this.b = new Thread(new ThreadStart(this.a));
                this.b.Name = "Devart_DbMonitor";
                this.b.IsBackground = true;
                this.b.Start();
                this.e = false;
            }
            this.c.Set();
        }

        public void b()
        {
            this.e = true;
            this.c.Set();
            if (this.b != null)
            {
                this.b.Join();
                this.b = null;
                this.d.Clear();
            }
            if (this.a != null)
            {
                try
                {
                    this.a.f();
                    this.a.d();
                }
                catch
                {
                }
                this.a = null;
            }
        }

        public void b(int A_0)
        {
            this.j = A_0;
        }

        public void b(string A_0)
        {
            this.l = A_0;
        }

        public string c() => 
            this.l;

        public void c(int A_0)
        {
            if (this.i != A_0)
            {
                this.i = A_0;
                this.b();
            }
        }

        public int d() => 
            this.j;

        public string e() => 
            this.h;

        internal int f() => 
            (this.g.Count != 0) ? this.g.Pop() : 0;

        public int g() => 
            this.i;

        private class a
        {
            public readonly Devart.DbMonitor.c a;
            public readonly MonitorTracePoint b;
            public readonly bool c;

            public a(Devart.DbMonitor.c A_0, MonitorTracePoint A_1, bool A_2)
            {
                this.a = A_0;
                this.b = A_1;
                this.c = A_2;
            }

            public override string a() => 
                $"{this.a.i()} {this.a.g()} {this.a.q()} IsParent:{this.c}";
        }
    }
}

