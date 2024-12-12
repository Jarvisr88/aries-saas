namespace Devart.Common
{
    using System;
    using System.Data;
    using System.Text;

    public abstract class DbMonitorHelper : DbMonitor
    {
        private const string a = "Creating object";

        protected DbMonitorHelper()
        {
        }

        private string a(ac A_0)
        {
            StringBuilder builder = new StringBuilder();
            a(builder, A_0, "User Id");
            if (!a(builder, A_0, "Host") && !a(builder, A_0, "Server"))
            {
                a(builder, A_0, "Data Source");
            }
            a(builder, A_0, "Port");
            if (!a(builder, A_0, "Database"))
            {
                a(builder, A_0, "Initial Catalog");
            }
            return builder.ToString();
        }

        internal static void a(DbMonitor A_0, DbCommandBase A_1)
        {
            OnCreate(A_0, MonitorTracePoint.BeforeEvent, A_1, false);
            OnCreate(A_0, MonitorTracePoint.AfterEvent, A_1, false);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, object A_2)
        {
            OnPoolManagerCreate(A_0, A_1, A_2);
        }

        internal static void a(DbMonitor A_0, Exception A_1, object A_2)
        {
            OnError(A_0, A_1, A_2);
        }

        private static bool a(StringBuilder A_0, ac A_1, string A_2)
        {
            string str = A_1.d(A_2);
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            if (A_0.Length > 0)
            {
                A_0.Append(";");
            }
            A_0.Append(A_2);
            A_0.Append("=");
            A_0.Append(str);
            return true;
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, DbConnectionBase A_2, string A_3)
        {
            OnDeactivate(A_0, A_1, A_2, A_3);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, IDbCommand A_2, string A_3)
        {
            OnPrepare(A_0, A_1, A_2, A_3);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, IDbConnection A_2, string A_3)
        {
            OnActivate(A_0, A_1, A_2, A_3);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, object A_2, string A_3)
        {
            OnPoolGroupCreate(A_0, A_1, A_2, A_3);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, IDbCommand A_2, string A_3, int A_4)
        {
            OnExecute(A_0, A_1, A_2, A_3, A_4);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, IDbConnection A_2, string A_3, object A_4)
        {
            OnTakeFromPool(A_0, A_1, A_2, A_3, A_4);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, object A_2, string A_3, bool A_4)
        {
            OnConnect(A_0, A_1, A_2, A_3, A_4);
        }

        internal static void a(DbMonitor A_0, MonitorTracePoint A_1, string A_2, object A_3, DbConnectionPool A_4)
        {
            OnReturnToPool(A_0, A_1, A_3, A_4, A_2);
        }

        internal static void b(DbMonitor A_0, MonitorTracePoint A_1, object A_2)
        {
            OnCreate(A_0, A_1, A_2, A_2 is DbConnectionBase);
        }

        protected override string GetObjectName(object obj)
        {
            DbConnectionPool pool = obj as DbConnectionPool;
            return ((pool == null) ? (!(obj is DbConnectionFactory) ? null : "Pools") : ("Pool (" + this.a(pool.ConnectionOptions) + ")"));
        }

        protected override object GetParentObject(object sender)
        {
            object parentObject = base.GetParentObject(sender);
            if ((parentObject != null) || !(sender is DbConnectionPool))
            {
                return parentObject;
            }
            DbConnectionPoolGroup poolGroup = ((DbConnectionPool) sender).PoolGroup;
            Utils.CheckArgumentNull(poolGroup, "poolGroup");
            DbConnectionFactory connectionFactory = poolGroup.ConnectionFactory;
            Utils.CheckArgumentNull(connectionFactory, "connectionFactory");
            return connectionFactory;
        }

        protected override int GetPoolGroupConnectionCount(object dbConnectionPool)
        {
            DbConnectionPool pool = dbConnectionPool as DbConnectionPool;
            return ((pool == null) ? -1 : pool.TotalCount);
        }
    }
}

