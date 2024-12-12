namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Threading;

    internal class DbConnectionPoolGroup
    {
        private readonly DbConnectionPoolOptions a;
        private readonly DbConnectionFactory b;
        private readonly ArrayList c;
        private readonly ArrayList d;
        private readonly Timer e;
        private int f;
        private static readonly Devart.Common.d g = new Devart.Common.d();
        private const int h = 0xea60;
        private const int i = 0x7530;
        private const int j = 0x2710;
        private const int k = 0x9c40;

        public DbConnectionPoolGroup(DbConnectionFactory connectionFactory, DbConnectionPoolOptions poolOptions)
        {
            this.a = poolOptions;
            this.b = connectionFactory;
            this.c = new ArrayList();
            this.d = new ArrayList();
            this.e = this.a();
        }

        private Timer a() => 
            new Timer(new TimerCallback(this.a), null, 0xea60, 0x7530);

        internal DbConnectionPool a(ac A_0)
        {
            lock (this.c)
            {
                DbConnectionPool target;
                using (IEnumerator enumerator = this.c.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        WeakReference current = (WeakReference) enumerator.Current;
                        if (current.IsAlive)
                        {
                            try
                            {
                                target = (DbConnectionPool) current.Target;
                                if ((target != null) && target.ConnectionOptions.Equals(A_0))
                                {
                                    return target;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                target = new DbConnectionPool(this, A_0);
                string str = A_0.a(true);
                DbMonitorHelper.a(this.ConnectionFactory.MonitorInstance, MonitorTracePoint.BeforeEvent, target, str);
                DbMonitorHelper.a(this.ConnectionFactory.MonitorInstance, MonitorTracePoint.AfterEvent, target, str);
                this.c.Add(new WeakReference(target));
                if (this.PoolOptions.UsePerformanceMonitor)
                {
                    this.b.PerformanceMonitor.n.d();
                }
                return target;
            }
        }

        private void a(DbConnectionPool A_0)
        {
            ArrayList list;
            int num;
            int num2;
            int num3;
            int num4;
            lock ((list = A_0.Objects))
            {
                int num7 = 0;
                while (num7 < list.Count)
                {
                    DbConnectionInternal internal2 = (DbConnectionInternal) list[num7];
                    if (internal2.IsEmancipated && Monitor.TryEnter(internal2))
                    {
                        try
                        {
                            if (internal2.IsEmancipated)
                            {
                                internal2.d(null);
                                if (this.PoolOptions.UsePerformanceMonitor)
                                {
                                    this.ConnectionFactory.PerformanceMonitor.k.d();
                                }
                                if (!internal2.CanBePooled)
                                {
                                    A_0.RemoveObject(internal2);
                                    continue;
                                }
                                if (!A_0.PutObject(internal2))
                                {
                                    continue;
                                }
                            }
                        }
                        finally
                        {
                            Monitor.Exit(internal2);
                        }
                    }
                    num7++;
                }
            }
            A_0.EnqueueStatistics(out num, out num2, out num3, out num4);
            int count = A_0.Count;
            int num6 = 0;
            int num8 = 0;
            while (true)
            {
                if (num8 < count)
                {
                    int num9;
                    object connection = A_0.PeekObject(num, num3, out num9);
                    if (connection != null)
                    {
                        if (((num9 - num2) <= 0) && (A_0.TotalCount > A_0.MinPoolSize))
                        {
                            A_0.RemoveObject(connection);
                            num6++;
                        }
                        else if (this.a(connection, A_0.NeedValidateOnPruneConnPool()))
                        {
                            A_0.PutObject(connection, num9);
                        }
                        else
                        {
                            A_0.RemoveObject(connection);
                            num6++;
                        }
                        num8++;
                        continue;
                    }
                }
                if (num6 < num4)
                {
                    num4 -= num6;
                    for (int i = 0; i < num4; i++)
                    {
                        A_0.DoomObject();
                    }
                }
                A_0.PoolValidated();
                return;
            }
        }

        private void a(object A_0)
        {
            if (this.f == 0)
            {
                Interlocked.Increment(ref this.f);
                try
                {
                    DbConnectionPool[] poolArray;
                    lock (this.d)
                    {
                        poolArray = new DbConnectionPool[this.d.Count];
                        this.d.CopyTo(poolArray, 0);
                    }
                    DbConnectionPool[] poolArray2 = poolArray;
                    int index = 0;
                    while (true)
                    {
                        if (index >= poolArray2.Length)
                        {
                            lock (this.c)
                            {
                                int num2 = 0;
                                while (num2 < this.c.Count)
                                {
                                    if (((WeakReference) this.c[num2]).IsAlive)
                                    {
                                        num2++;
                                        continue;
                                    }
                                    if (this.PoolOptions.UsePerformanceMonitor)
                                    {
                                        this.ConnectionFactory.PerformanceMonitor.o.e();
                                    }
                                    this.c.RemoveAt(num2);
                                }
                            }
                            break;
                        }
                        DbConnectionPool pool = poolArray2[index];
                        this.a(pool);
                        if (pool.TotalCount == 0)
                        {
                            lock (this.d)
                            {
                                if (this.PoolOptions.UsePerformanceMonitor)
                                {
                                    this.ConnectionFactory.PerformanceMonitor.n.e();
                                    this.ConnectionFactory.PerformanceMonitor.o.d();
                                }
                                if (pool.Shutdown())
                                {
                                    this.d.Remove(pool);
                                }
                            }
                        }
                        index++;
                    }
                }
                finally
                {
                    Interlocked.Decrement(ref this.f);
                }
            }
        }

        internal object a(DbConnectionPool A_0, DbConnectionBase A_1) => 
            this.b.a(A_0, A_0.ConnectionOptions, A_1);

        internal bool a(object A_0, bool A_1) => 
            !A_1 || this.b.a((DbConnectionInternal) A_0);

        internal DbConnectionPool b(ac A_0)
        {
            DbConnectionPool[] poolArray;
            lock (this.d)
            {
                poolArray = new DbConnectionPool[this.d.Count];
                this.d.CopyTo(poolArray, 0);
            }
            foreach (DbConnectionPool pool in poolArray)
            {
                if (pool.ConnectionOptions.Equals(A_0))
                {
                    return pool;
                }
            }
            return null;
        }

        internal void b(DbConnectionPool A_0)
        {
            lock (this.d)
            {
                this.d.Add(A_0);
            }
        }

        internal void b(object A_0)
        {
            try
            {
                ((DbConnectionInternal) A_0).Dispose();
            }
            catch
            {
            }
        }

        public void Clear(bool forced)
        {
            lock (this.d)
            {
                using (IEnumerator enumerator = this.d.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        int cleanupWait = !Utils.MonoDetected ? g.a(0x2710, 0x9c40) : 0x61a8;
                        ((DbConnectionPool) enumerator.Current).Clear(cleanupWait, forced);
                    }
                }
            }
        }

        public DbConnectionPoolOptions PoolOptions =>
            this.a;

        public DbConnectionFactory ConnectionFactory =>
            this.b;
    }
}

