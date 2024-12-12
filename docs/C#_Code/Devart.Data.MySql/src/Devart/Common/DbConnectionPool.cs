namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal sealed class DbConnectionPool
    {
        private readonly ac a;
        private readonly DbConnectionPoolGroup b;
        private readonly int c;
        private readonly int d;
        private readonly int[] e;
        private readonly int[] f;
        private readonly object[] g;
        private readonly object h;
        private readonly object i;
        private readonly bool j;
        private readonly TimeSpan k;
        private readonly TimeSpan l;
        private readonly AutoResetEvent m;
        private readonly ArrayList n;
        private DbMetaDataFactory o;
        private Timer p;
        private bool q;
        private int r;
        private int s;
        private int t;
        private int u;
        private int v;
        private int w;
        private int x;
        private bool y;
        private const int z = 8;
        private const int aa = 0;

        internal DbConnectionPool(DbConnectionPoolGroup A_0, ac A_1)
        {
            this.b = A_0;
            this.a = A_1;
            this.c = A_0.PoolOptions.MaxPoolSize;
            this.d = A_0.PoolOptions.MinPoolSize;
            this.l = new TimeSpan(0, 0, A_0.PoolOptions.CreationTimeout);
            this.j = A_0.PoolOptions.UseLoadBalancing;
            this.k = A_0.PoolOptions.LoadBalanceTimeout;
            this.g = new object[this.c];
            this.e = new int[this.c];
            this.f = new int[8];
            this.h = new object();
            this.i = new object();
            this.m = new AutoResetEvent(false);
            this.n = new ArrayList(this.c);
        }

        private object a(DbConnectionBase A_0)
        {
            object obj2 = this.b.a(this, A_0);
            lock (this.n)
            {
                this.n.Add(obj2);
            }
            return obj2;
        }

        private Timer a(int A_0) => 
            new Timer(new TimerCallback(this.a), null, A_0, A_0);

        private void a(object A_0)
        {
            int t = this.t;
            int num2 = 0;
            while (true)
            {
                if (num2 < t)
                {
                    int num3;
                    object connection = this.PeekObject(this.r, -1, out num3);
                    if (connection != null)
                    {
                        this.RemoveObject(connection);
                        num2++;
                        continue;
                    }
                }
                Timer p = this.p;
                this.p = null;
                if (p != null)
                {
                    p.Dispose();
                }
                return;
            }
        }

        private void b(object A_0)
        {
            lock (this.n)
            {
                this.n.Remove(A_0);
            }
            this.b.b(A_0);
            if (this.a.bg())
            {
                this.b.ConnectionFactory.PerformanceMonitor.l.e();
            }
        }

        private bool c(object A_0) => 
            ((DbConnectionInternal) A_0).State == ConnectionState.Open;

        public void Clear()
        {
            this.Clear(0, false);
        }

        public void Clear(int cleanupWait, bool forced)
        {
            this.r = this.Version;
            if (forced)
            {
                this.a(null);
            }
            else
            {
                this.p = this.a(cleanupWait);
            }
        }

        public void DoomObject()
        {
            object connection = null;
            lock (this.h)
            {
                if (this.t > 0)
                {
                    connection = Interlocked.Exchange(ref this.g[this.v], null);
                    int num = this.v + 1;
                    this.v = num;
                    if (num == this.c)
                    {
                        this.v = 0;
                    }
                    Interlocked.Decrement(ref this.t);
                    if (this.a.bg())
                    {
                        this.PoolGroup.ConnectionFactory.PerformanceMonitor.i.e();
                    }
                }
            }
            if (connection != null)
            {
                this.RemoveObject(connection);
            }
        }

        public void EnqueueStatistics(out int firstVersion, out int lastVersion, out int position, out int doomed)
        {
            int index = this.f.Length - 1;
            firstVersion = this.f[0];
            lastVersion = this.f[index];
            Array.Copy(this.f, 0, this.f, 1, index);
            this.f[0] = this.Version;
            lock (this.i)
            {
                doomed = ((this.u + 8) - 2) / 8;
                this.u = (this.Count - this.d) - doomed;
                position = this.w;
            }
        }

        public object GetObject(DbConnectionBase owningConnection)
        {
            object obj2;
            lock (this.h)
            {
                if (!this.Active)
                {
                    this.Startup();
                }
                while (true)
                {
                    object i;
                    int num2;
                    if (this.s < this.d)
                    {
                        object obj4 = this.a(owningConnection);
                        i = this.i;
                        lock (i)
                        {
                            Interlocked.Exchange(ref this.g[this.w], obj4);
                            Interlocked.Exchange(ref this.e[this.w], this.x);
                            num2 = this.w + 1;
                            this.w = num2;
                            if (num2 == this.c)
                            {
                                this.w = 0;
                            }
                            int num = Interlocked.Increment(ref this.t);
                            if (this.a.bg())
                            {
                                this.PoolGroup.ConnectionFactory.PerformanceMonitor.i.d();
                            }
                            if (num < (this.u + this.d))
                            {
                                this.u = num - this.d;
                            }
                        }
                        Interlocked.Increment(ref this.s);
                        continue;
                    }
                    if (this.t <= 0)
                    {
                        if (this.s != this.c)
                        {
                            obj2 = this.a(owningConnection);
                            Interlocked.Increment(ref this.s);
                            break;
                        }
                        i = !Utils.WaitOne(this.m, this.l, false) ? null : this.GetObject(owningConnection);
                    }
                    else
                    {
                        if (this.a.bg())
                        {
                            this.PoolGroup.ConnectionFactory.PerformanceMonitor.f.d();
                        }
                        DbMonitorHelper.a(owningConnection.ConnectionFactory.MonitorInstance, MonitorTracePoint.BeforeEvent, (IDbConnection) owningConnection, (this.a != null) ? this.a.a(true) : string.Empty, this);
                        int num1 = this.e[this.v];
                        obj2 = Interlocked.Exchange(ref this.g[this.v], null);
                        num2 = this.v + 1;
                        this.v = num2;
                        if (num2 == this.c)
                        {
                            this.v = 0;
                        }
                        Interlocked.Decrement(ref this.t);
                        if (this.a.bg())
                        {
                            this.PoolGroup.ConnectionFactory.PerformanceMonitor.i.e();
                        }
                        if (this.NeedValidateOnGet())
                        {
                            if (this.b.a(obj2, true))
                            {
                                this.PoolValidated();
                            }
                            else
                            {
                                this.b(obj2);
                                obj2 = this.a(owningConnection);
                            }
                        }
                        if (!this.c(obj2))
                        {
                            this.b(obj2);
                            obj2 = this.a(owningConnection);
                        }
                        DbMonitorHelper.a(owningConnection.ConnectionFactory.MonitorInstance, MonitorTracePoint.AfterEvent, (IDbConnection) owningConnection, (this.a != null) ? this.a.a(true) : string.Empty, this);
                        i = obj2;
                    }
                    return i;
                }
            }
            return obj2;
        }

        public void MarkInvalidVersion()
        {
            lock (this.i)
            {
                this.y = true;
            }
        }

        public bool NeedValidateOnGet() => 
            this.y || this.a.c();

        public bool NeedValidateOnPruneConnPool() => 
            !this.a.c();

        public object PeekObject(int checkVersion, int position, out int version)
        {
            object obj3;
            lock (this.h)
            {
                if (((this.t <= 0) || ((this.v == position) && (this.t != this.c))) || ((this.e[this.v] - checkVersion) > 0))
                {
                    version = 0;
                    obj3 = null;
                }
                else
                {
                    version = this.e[this.v];
                    int num = this.v + 1;
                    this.v = num;
                    if (num == this.c)
                    {
                        this.v = 0;
                    }
                    Interlocked.Decrement(ref this.t);
                    if (this.a.bg())
                    {
                        this.PoolGroup.ConnectionFactory.PerformanceMonitor.i.e();
                    }
                    obj3 = Interlocked.Exchange(ref this.g[this.v], null);
                }
            }
            return obj3;
        }

        public void PoolValidated()
        {
            lock (this.i)
            {
                this.y = false;
            }
        }

        public bool PutObject(object value)
        {
            DbConnectionInternal internal2 = (DbConnectionInternal) value;
            if ((!internal2.CanBePooled || (this.j && (internal2.CreateTime.Add(this.k) < DateTime.UtcNow))) || (internal2.State != ConnectionState.Open))
            {
                this.RemoveObject(value);
                return false;
            }
            this.PutObject(value, Interlocked.Increment(ref this.x));
            return true;
        }

        public void PutObject(object value, int version)
        {
            DbConnectionInternal internal2 = value as DbConnectionInternal;
            if ((internal2 != null) && (internal2.State != ConnectionState.Open))
            {
                throw new InvalidOperationException("Trying to put closed connection to pool");
            }
            lock (this.i)
            {
                Interlocked.Exchange(ref this.g[this.w], value);
                Interlocked.Exchange(ref this.e[this.w], version);
                int num2 = this.w + 1;
                this.w = num2;
                if (num2 == this.c)
                {
                    this.w = 0;
                }
                int num = Interlocked.Increment(ref this.t);
                if (this.a.bg())
                {
                    this.PoolGroup.ConnectionFactory.PerformanceMonitor.i.d();
                }
                if (num < (this.u + this.d))
                {
                    this.u = num - this.d;
                }
                this.m.Set();
            }
        }

        public void RemoveObject(object connection)
        {
            lock (this.n)
            {
                this.n.Remove(connection);
            }
            Interlocked.Decrement(ref this.s);
            this.m.Set();
            this.b.b(connection);
            if (this.a.bg())
            {
                this.b.ConnectionFactory.PerformanceMonitor.l.e();
            }
        }

        public bool Shutdown()
        {
            bool flag;
            lock (this.h)
            {
                if (this.s != 0)
                {
                    flag = false;
                }
                else
                {
                    this.q = false;
                    flag = true;
                }
            }
            return flag;
        }

        public void Startup()
        {
            this.b.b(this);
            this.q = true;
        }

        public bool Active =>
            this.q;

        internal DbMetaDataFactory MetaDataFactory
        {
            get => 
                this.o;
            set => 
                this.o = value;
        }

        internal int TotalCount =>
            this.s;

        internal ac ConnectionOptions =>
            this.a;

        internal ArrayList Objects =>
            this.n;

        internal int MinPoolSize =>
            this.d;

        public int Version =>
            this.x;

        public int Count =>
            this.t;

        public DbConnectionPoolGroup PoolGroup =>
            this.b;
    }
}

