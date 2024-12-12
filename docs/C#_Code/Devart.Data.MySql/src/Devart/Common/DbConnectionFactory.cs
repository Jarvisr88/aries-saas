namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Security;
    using System.Transactions;

    internal abstract class DbConnectionFactory
    {
        private readonly ArrayList a = new ArrayList();

        protected DbConnectionFactory()
        {
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.BeforeEvent, this);
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.AfterEvent, this);
            try
            {
                this.a();
            }
            catch (SecurityException)
            {
            }
        }

        private void a()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(this.a);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(this.a);
        }

        protected abstract DbConnectionPoolOptions a(ac A_0);
        private DbConnectionInternal a(DbConnectionBase A_0)
        {
            ac connectionOptions = A_0.ConnectionOptions;
            return (((connectionOptions == null) || (!connectionOptions.d() || (Transaction.Current == null))) ? null : v.a(Transaction.Current, connectionOptions));
        }

        protected internal virtual bool a(DbConnectionInternal A_0) => 
            A_0.State == ConnectionState.Open;

        private DbConnectionPool a(string A_0)
        {
            DbConnectionPoolGroup[] groupArray;
            ac ac = this.a(A_0, null);
            if (ac == null)
            {
                throw new InvalidOperationException(av.a("ConnectionOptionsMissing"));
            }
            lock (this.a)
            {
                groupArray = new DbConnectionPoolGroup[this.a.Count];
                this.a.CopyTo(groupArray, 0);
            }
            DbConnectionPoolOptions options = this.a(ac);
            if (options != null)
            {
                options.UsePerformanceMonitor = ac.bg();
            }
            foreach (DbConnectionPoolGroup group in groupArray)
            {
                if (group.PoolOptions.Equals(options))
                {
                    return group.b(ac);
                }
            }
            return null;
        }

        protected virtual void a(ac A_0, DbConnectionBase A_1)
        {
        }

        internal DbConnectionInternal a(DbConnectionBase A_0, ac A_1)
        {
            if (A_1 == null)
            {
                throw new InvalidOperationException(av.a("ConnectionStringNotInitialized"));
            }
            if (A_1.bg() && !this.PerformanceMonitor.h())
            {
                this.PerformanceMonitor.g();
            }
            object obj2 = A_0;
            string str = A_1.a(true);
            DbConnectionInternal internal2 = null;
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.BeforeEvent, obj2, str, false);
            try
            {
                internal2 = this.a(A_1, null, A_0);
                if (internal2 != null)
                {
                    if (A_1.bg())
                    {
                        this.PerformanceMonitor.d.d();
                        this.PerformanceMonitor.m.d();
                    }
                    lock (internal2)
                    {
                        internal2.a(A_0, this.PerformanceMonitor);
                        internal2.p = obj2;
                    }
                }
            }
            catch (Exception exception)
            {
                DbMonitorHelper.a(this.MonitorInstance, exception, obj2);
                throw;
            }
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.AfterEvent, obj2, str, false);
            return internal2;
        }

        internal DbMetaDataFactory a(DbConnectionPool A_0, DbConnectionInternal A_1)
        {
            DbMetaDataFactory metaDataFactory = null;
            if (A_0 != null)
            {
                metaDataFactory = A_0.MetaDataFactory;
            }
            if (metaDataFactory == null)
            {
                metaDataFactory = this.b(A_1);
                if (A_0 != null)
                {
                    A_0.MetaDataFactory = metaDataFactory;
                }
            }
            return metaDataFactory;
        }

        private void a(object A_0, EventArgs A_1)
        {
            this.ClearAllPools(true);
        }

        protected abstract ac a(string A_0, ac A_1);
        protected abstract DbConnectionInternal a(ac A_0, object A_1, DbConnectionBase A_2);
        internal DbConnectionInternal a(DbConnectionPool A_0, ac A_1, DbConnectionBase A_2)
        {
            object obj2 = A_2;
            string str = A_1.a(true);
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.BeforeEvent, obj2, str, true);
            DbConnectionInternal internal2 = null;
            try
            {
                internal2 = this.a(A_1, null, A_2);
                if (internal2 != null)
                {
                    if (A_1.bg())
                    {
                        this.PerformanceMonitor.d.d();
                        this.PerformanceMonitor.l.d();
                    }
                    lock (internal2)
                    {
                        internal2.a(A_0, this.PerformanceMonitor);
                        internal2.d(null);
                    }
                    internal2.p = obj2;
                }
            }
            catch (Exception exception)
            {
                DbMonitorHelper.a(this.MonitorInstance, exception, obj2);
                throw;
            }
            DbMonitorHelper.a(this.MonitorInstance, MonitorTracePoint.AfterEvent, obj2, str, true);
            return internal2;
        }

        internal DbConnectionPool a(string A_0, ref ac A_1, DbConnectionBase A_2)
        {
            ac ac;
            if ((A_0 == null) || (A_0 == ""))
            {
                if (A_1 == null)
                {
                    return null;
                }
                ac = A_1;
            }
            else
            {
                ac = this.a(A_0, A_1);
                if (ac == null)
                {
                    throw new InvalidOperationException(av.a("ConnectionOptionsMissing"));
                }
                this.a(ac, A_2);
                A_1 = ac;
            }
            DbConnectionPoolOptions options = this.a(ac);
            if (options == null)
            {
                return null;
            }
            options.UsePerformanceMonitor = ac.bg();
            DbConnectionPoolGroup group = null;
            lock (this.a)
            {
                foreach (DbConnectionPoolGroup group2 in this.a)
                {
                    if (group2.PoolOptions.Equals(options))
                    {
                        group = group2;
                        break;
                    }
                }
                if (group == null)
                {
                    if (options.UsePerformanceMonitor && !this.PerformanceMonitor.h())
                    {
                        this.PerformanceMonitor.g();
                    }
                    group = new DbConnectionPoolGroup(this, options);
                    this.a.Add(group);
                }
            }
            return group.a(ac);
        }

        internal DbConnectionInternal b(DbConnectionBase A_0)
        {
            DbConnectionInternal connection = this.a(A_0);
            if (connection != null)
            {
                lock (connection)
                {
                    connection.Owner = A_0;
                }
                return connection;
            }
            DbConnectionPool pool = A_0.Pool;
            if (pool == null)
            {
                connection = this.a(A_0, A_0.UserConnectionOptions);
            }
            else
            {
                connection = (DbConnectionInternal) pool.GetObject(A_0);
                if (connection == null)
                {
                    throw new InvalidOperationException(av.a("PooledOpenTimeout"));
                }
                lock (connection)
                {
                    connection.c(A_0);
                }
            }
            try
            {
                connection.y();
            }
            catch
            {
                if (pool != null)
                {
                    lock (connection)
                    {
                        pool.RemoveObject(connection);
                    }
                }
                connection.Dispose();
                throw;
            }
            return connection;
        }

        protected virtual DbMetaDataFactory b(DbConnectionInternal A_0)
        {
            throw new NotSupportedException();
        }

        public void ClearAllPools()
        {
            this.ClearAllPools(false);
        }

        public void ClearAllPools(bool forced)
        {
            DbConnectionPoolGroup[] groupArray;
            lock (this.a)
            {
                groupArray = new DbConnectionPoolGroup[this.a.Count];
                this.a.CopyTo(groupArray, 0);
            }
            DbConnectionPoolGroup[] groupArray2 = groupArray;
            for (int i = 0; i < groupArray2.Length; i++)
            {
                groupArray2[i].Clear(forced);
            }
        }

        public void ClearPool(DbConnectionBase connection)
        {
            Utils.CheckArgumentNull(connection, "connection");
            DbConnectionPool pool = connection.Pool;
            if (pool != null)
            {
                pool.Clear();
            }
        }

        public void ClearPool(string connectionString)
        {
            Utils.CheckArgumentNull(connectionString, "connectionString");
            DbConnectionPool pool = this.a(connectionString);
            if (pool != null)
            {
                pool.Clear();
            }
        }

        public abstract DbProviderFactory ProviderFactory { get; }

        internal abstract DbMonitor MonitorInstance { get; }

        internal virtual Devart.Common.c PerformanceMonitor =>
            Devart.Common.i.a;
    }
}

