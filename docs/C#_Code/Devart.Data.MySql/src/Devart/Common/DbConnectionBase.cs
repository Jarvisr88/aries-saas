namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Transactions;

    public abstract class DbConnectionBase : DbConnection
    {
        private readonly DbConnectionFactory a;
        private DbConnectionInternal b;
        private DbConnectionPool c;
        private StateChangeEventHandler d;
        private Devart.Common.ac e;
        private int f;
        private bool g;
        private bool h;
        private bool i;
        private ILocalFailoverManager j;
        private const int k = 0x7fffffff;
        private bool l;
        private static readonly object m = new object();
        private Devart.Common.DbConnectionBase.a n;
        private string o;
        private object p;

        protected event ConnectionLostEventHandler ConnectionLost
        {
            add
            {
                base.Events.AddHandler(m, A_0);
            }
            remove
            {
                base.Events.RemoveHandler(m, A_0);
            }
        }

        [Devart.Common.aa("DbConnection_StateChange"), Category("StateChange")]
        public override event StateChangeEventHandler StateChange
        {
            add
            {
                this.d += value;
            }
            remove
            {
                this.d -= value;
            }
        }

        public event TransactionStateChangedEventHandler TransactionStateChanged;

        public event TransactionStateChangingEventHandler TransactionStateChanging;

        protected DbConnectionBase(DbConnectionBase A_0)
        {
            this.g = true;
            this.l = true;
            this.o = string.Empty;
            Utils.CheckArgumentNull(A_0, "connection");
            this.a = A_0.ConnectionFactory;
            this.e = A_0.UserConnectionOptions;
            this.c = A_0.Pool;
            this.b = DbConnectionClosed.c;
        }

        internal DbConnectionBase(DbConnectionFactory A_0)
        {
            this.g = true;
            this.l = true;
            this.o = string.Empty;
            Utils.CheckArgumentNull(A_0, "connectionFactory");
            this.a = A_0;
            this.b = DbConnectionClosed.c;
        }

        private void a()
        {
            Utils.CheckArgumentNull(this.a, "connectionFactory");
            DbMonitorHelper.b(this.a.MonitorInstance, MonitorTracePoint.BeforeEvent, this);
            DbMonitorHelper.b(this.a.MonitorInstance, MonitorTracePoint.AfterEvent, this);
        }

        private DbMetaDataFactory a(DbConnectionInternal A_0) => 
            this.a.a(this.c, A_0);

        internal void a(DbLoader A_0)
        {
            this.a(A_0, 3);
        }

        protected void a(TransactionAction A_0)
        {
            if (this.q != null)
            {
                TransactionStateChangingEventArgs e = new TransactionStateChangingEventArgs(A_0);
                this.q(this, e);
            }
        }

        internal void a(DbCommand A_0)
        {
            this.a(A_0);
        }

        internal void a(DbDataReader A_0)
        {
            this.a(A_0, 2);
        }

        internal void a(int A_0)
        {
            this.InnerConnection.a(A_0);
        }

        protected internal void a(object A_0)
        {
            this.InnerConnection.f(A_0);
        }

        internal bool a(DbConnectionInternal A_0, bool A_1)
        {
            DbConnectionInternal internal2 = Interlocked.Exchange<DbConnectionInternal>(ref this.b, A_0);
            if (A_1)
            {
                ConnectionState state = internal2.State & ConnectionState.Open;
                this.a(state, A_0.State & ConnectionState.Open);
            }
            return true;
        }

        private void a(ConnectionState A_0, ConnectionState A_1)
        {
            if (A_0 != A_1)
            {
                if (A_1 == ConnectionState.Closed)
                {
                    Interlocked.Increment(ref this.f);
                }
                this.b(A_0, A_1);
            }
        }

        internal void a(int A_0, int A_1)
        {
            this.InnerConnection.a(A_0, A_1);
        }

        protected void a(object A_0, TransactionStateChangeEventArgs A_1)
        {
            this.a(A_1.Action);
        }

        protected internal void a(object A_0, int A_1)
        {
            this.InnerConnection.a(A_0, A_1);
        }

        internal bool a(DbConnectionInternal A_0, DbConnectionInternal A_1, bool A_2)
        {
            DbConnectionInternal objB = Interlocked.CompareExchange<DbConnectionInternal>(ref this.b, A_0, A_1);
            bool flag = ReferenceEquals(A_1, objB);
            if (A_2 & flag)
            {
                ConnectionState state = objB.State & ConnectionState.Open;
                this.a(state, A_0.State & ConnectionState.Open);
            }
            return flag;
        }

        internal RetryMode a(object A_0, ConnectionLostCause A_1, RetryMode A_2, Exception A_3, ref int A_4, bool A_5)
        {
            if (!this.LocalFailover || this.HasLocalFailoverRestriction())
            {
                return RetryMode.Raise;
            }
            ConnectionLostEventHandler handler = (ConnectionLostEventHandler) base.Events[m];
            if (handler == null)
            {
                A_2 = RetryMode.Raise;
            }
            else
            {
                while (true)
                {
                    if ((this.b == null) || ((A_4 > 0x7ffffffe) || !this.IsConnectionLostError(A_3)))
                    {
                        return RetryMode.Raise;
                    }
                    ConnectionLostContext none = ConnectionLostContext.None;
                    if (this.b.aa())
                    {
                        none = ConnectionLostContext.InFetch;
                        A_2 = RetryMode.Raise;
                    }
                    else if (this.b.ad())
                    {
                        none = ConnectionLostContext.HasPrepared;
                        A_2 = RetryMode.Raise;
                    }
                    else if (this.InTransaction())
                    {
                        none = ConnectionLostContext.InTransaction;
                        A_2 = RetryMode.Raise;
                    }
                    A_4++;
                    ConnectionLostEventArgs e = new ConnectionLostEventArgs(A_0, A_1, none, A_2, A_4);
                    handler(A_0, e);
                    A_2 = e.RetryMode;
                    try
                    {
                        if (A_2 == RetryMode.Reexecute)
                        {
                            this.Reconnect();
                        }
                        else if (A_5)
                        {
                            this.DoErrorEvent(A_3);
                        }
                        break;
                    }
                    catch (Exception)
                    {
                        A_2 = RetryMode.Raise;
                    }
                }
            }
            return A_2;
        }

        internal DbMetaDataFactory b(DbConnectionInternal A_0) => 
            this.a(A_0);

        internal void b(DbLoader A_0)
        {
            this.a(A_0);
        }

        protected void b(TransactionAction A_0)
        {
            if (this.r != null)
            {
                TransactionStateChangedEventArgs e = new TransactionStateChangedEventArgs(A_0);
                this.r(this, e);
            }
        }

        internal void b(DbCommand A_0)
        {
            this.a(A_0, 1);
        }

        internal void b(DbDataReader A_0)
        {
            this.a(A_0);
        }

        protected void b(ConnectionState A_0, ConnectionState A_1)
        {
            StateChangeEventHandler d = this.d;
            if (d != null)
            {
                d(this, new StateChangeEventArgs(A_0, A_1));
            }
        }

        protected void b(object A_0, TransactionStateChangeEventArgs A_1)
        {
            this.b(A_1.Action);
            if ((A_1.Action == TransactionAction.Commit) || (A_1.Action == TransactionAction.Rollback))
            {
                DbTransactionBase base1 = (DbTransactionBase) A_0;
                base1.StateChanging -= new TransactionStateChangingEventHandler(this.a);
                base1.StateChanged -= new TransactionStateChangedEventHandler(this.b);
            }
        }

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            this.a(TransactionAction.BeginTransaction);
            this.b(TransactionAction.BeginTransaction);
            DbTransactionBase base1 = (DbTransactionBase) this.InnerConnection.BeginTransaction(isolationLevel);
            base1.StateChanging += new TransactionStateChangingEventHandler(this.a);
            base1.StateChanged += new TransactionStateChangedEventHandler(this.b);
            return base1;
        }

        public IAsyncResult BeginOpen(AsyncCallback callback, object stateObject)
        {
            if (this.n != null)
            {
                throw new InvalidOperationException();
            }
            this.n = new Devart.Common.DbConnectionBase.a(this.Open);
            return this.n.BeginInvoke(callback, stateObject);
        }

        public override void ChangeDatabase(string value)
        {
            this.InnerConnection.ChangeDatabase(value);
        }

        public override void Close()
        {
            try
            {
                this.a(0);
            }
            finally
            {
                this.InnerConnection.Close();
                GC.KeepAlive(this);
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            DbCommand command1 = this.ConnectionFactory.ProviderFactory.CreateCommand();
            command1.Connection = this;
            return command1;
        }

        protected virtual DbCommand CreateInitializationCommand()
        {
            DbCommand command1 = base.CreateCommand();
            command1.CommandText = this.ConnectionOptions.bf();
            return command1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.e = null;
                this.c = null;
                this.Close();
            }
            base.Dispose(disposing);
            if ((this.Owner != null) && !base.DesignMode)
            {
                GlobalComponentsCache.RemoveFromGlobalList(this);
            }
        }

        protected virtual void DoErrorEvent(Exception ex)
        {
        }

        public void EndOpen(IAsyncResult result)
        {
            if (this.n == null)
            {
                throw new InvalidOperationException();
            }
            try
            {
                this.n.EndInvoke(result);
            }
            finally
            {
                this.n = null;
            }
        }

        public override void EnlistTransaction(Transaction transaction)
        {
            DbConnectionInternal innerConnection = this.InnerConnection;
            if ((innerConnection == null) || (innerConnection is DbConnectionClosed))
            {
                throw new InvalidOperationException(av.a("ConnMustOpen"));
            }
            innerConnection.EnlistToDistributedTransaction(transaction);
        }

        public override DataTable GetSchema() => 
            this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);

        public override DataTable GetSchema(string collectionName) => 
            this.GetSchema(collectionName, null);

        public override DataTable GetSchema(string collectionName, string[] restrictionValues) => 
            this.InnerConnection.a(this, collectionName, restrictionValues);

        protected virtual bool HasLocalFailoverRestriction() => 
            false;

        protected internal virtual bool InTransaction()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual bool IsConnectionLostError(Exception e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Open()
        {
            DbMonitorHelper.a(this.a.MonitorInstance, MonitorTracePoint.BeforeEvent, (IDbConnection) this, (this.UserConnectionOptions != null) ? this.UserConnectionOptions.a(true) : string.Empty);
            if (this.State != ConnectionState.Open)
            {
                ILocalFailoverManager manager = this.LocalFailoverManager.StartUse(true);
                try
                {
                    this.InnerConnection.Open(this);
                }
                catch (Exception exception)
                {
                    if (this.LocalFailoverManager.DoLocalFailoverEvent(this, ConnectionLostCause.Connect, RetryMode.Raise, exception) == RetryMode.Raise)
                    {
                        throw;
                    }
                }
                finally
                {
                    if (manager != null)
                    {
                        manager.Dispose();
                    }
                }
                if (this.State == ConnectionState.Open)
                {
                    if (!this.InnerConnection.RunOnceCommandExecuted && !string.IsNullOrEmpty(this.ConnectionOptions.bi()))
                    {
                        try
                        {
                            this.RunScript(this.ConnectionOptions.bi());
                            this.InnerConnection.RunOnceCommandExecuted = true;
                        }
                        catch (Exception exception2)
                        {
                            try
                            {
                                this.Close();
                            }
                            catch
                            {
                            }
                            throw exception2;
                        }
                    }
                    if (!string.IsNullOrEmpty(this.ConnectionOptions.bf()))
                    {
                        try
                        {
                            this.RunScript(this.ConnectionOptions.bf());
                        }
                        catch (Exception exception3)
                        {
                            try
                            {
                                this.Close();
                            }
                            catch
                            {
                            }
                            throw exception3;
                        }
                    }
                }
            }
            DbMonitorHelper.a(this.a.MonitorInstance, MonitorTracePoint.AfterEvent, (IDbConnection) this, (this.UserConnectionOptions != null) ? this.UserConnectionOptions.a(true) : string.Empty);
        }

        protected virtual void Reconnect()
        {
        }

        protected virtual void RunScript(string scriptText)
        {
            using (DbCommand command = this.CreateInitializationCommand())
            {
                command.CommandText = scriptText;
                command.ExecuteNonQuery();
            }
        }

        protected internal int CloseCount =>
            this.f;

        internal DbConnectionFactory ConnectionFactory =>
            this.a;

        internal Devart.Common.ac ConnectionOptions
        {
            get
            {
                DbConnectionPool pool = this.Pool;
                return ((pool != null) ? pool.ConnectionOptions : this.e);
            }
            set
            {
                DbConnectionPool pool = this.ConnectionFactory.a(null, ref value, this);
                DbConnectionInternal innerConnection = this.InnerConnection;
                bool allowSetConnectionString = innerConnection.AllowSetConnectionString;
                if (allowSetConnectionString && (allowSetConnectionString = this.a(DbConnectionClosed.b, innerConnection, false)))
                {
                    this.e = value;
                    this.c = pool;
                    this.b = DbConnectionClosed.c;
                }
                if (!allowSetConnectionString)
                {
                    throw new InvalidOperationException(av.a("OpenConnectionStringSet"));
                }
            }
        }

        [MergableProperty(false)]
        public override string ConnectionString
        {
            get
            {
                bool shouldHidePassword = this.InnerConnection.ShouldHidePassword;
                Devart.Common.ac userConnectionOptions = this.UserConnectionOptions;
                if (userConnectionOptions == null)
                {
                    return string.Empty;
                }
                if (base.DesignMode)
                {
                    shouldHidePassword = false;
                }
                string str = userConnectionOptions.a(shouldHidePassword).TrimEnd(new char[0]);
                return (((str.Length <= 0) || (str[str.Length - 1] == ';')) ? str : (str + ";"));
            }
            set
            {
                if (this.ConnectionString != value)
                {
                    this.Close();
                    Devart.Common.ac ac = null;
                    DbConnectionPool pool = this.ConnectionFactory.a(value, ref ac, this);
                    DbConnectionInternal innerConnection = this.InnerConnection;
                    bool allowSetConnectionString = innerConnection.AllowSetConnectionString;
                    if (allowSetConnectionString && (allowSetConnectionString = this.a(DbConnectionClosed.b, innerConnection, false)))
                    {
                        this.e = ac;
                        this.c = pool;
                        this.b = DbConnectionClosed.c;
                    }
                    if (!allowSetConnectionString)
                    {
                        throw new InvalidOperationException(av.a("OpenConnectionStringSet"));
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.a();
                        this.l = false;
                    }
                }
            }
        }

        public override int ConnectionTimeout =>
            this.ConnectionTimeoutInternal;

        protected virtual int ConnectionTimeoutInternal =>
            0;

        internal DbConnectionInternal InnerConnection =>
            this.b;

        internal DbConnectionPool Pool
        {
            get => 
                this.c;
            set => 
                this.c = value;
        }

        internal Devart.Common.ac UserConnectionOptions =>
            this.e;

        [TypeConverter(typeof(Devart.Common.b)), Browsable(false), Devart.Common.aa("DbConnection_State"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ConnectionState State =>
            this.ConnectionStateInternal;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        protected internal virtual ConnectionState ConnectionStateInternal
        {
            get => 
                this.InnerConnection.State;
            set
            {
                if (value != this.State)
                {
                    if (value == ConnectionState.Closed)
                    {
                        this.Close();
                    }
                    else if (value == ConnectionState.Open)
                    {
                        this.Open();
                    }
                    else
                    {
                        if (!base.DesignMode)
                        {
                            throw new NotSupportedException(string.Format(av.a("ConnectionStateNotSupported"), value));
                        }
                        if (this.State == ConnectionState.Open)
                        {
                            this.Close();
                        }
                        else
                        {
                            this.Open();
                        }
                    }
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string ServerVersion
        {
            get
            {
                if (this.State == ConnectionState.Open)
                {
                    return this.InnerConnection.ServerVersion;
                }
                if (!base.DesignMode)
                {
                    throw new InvalidOperationException("Invalid operation. The connection is closed.");
                }
                return string.Empty;
            }
        }

        [Devart.Common.aa("DbConnection_Database"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override string Database =>
            this.DatabaseInternal;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        protected virtual string DatabaseInternal
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        [Devart.Common.aa("DbConnection_DataSource"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override string DataSource =>
            this.DataSourceInternal;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        protected virtual string DataSourceInternal
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        [DesignOnly(true), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(true), Browsable(false)]
        public bool DesignTimeVisible
        {
            get => 
                this.g;
            set
            {
                this.g = value;
                TypeDescriptor.Refresh(this);
            }
        }

        internal ILocalFailoverManager LocalFailoverManager
        {
            get
            {
                this.j ??= new Devart.Common.o(this);
                return this.j;
            }
        }

        protected bool LocalFailover
        {
            get => 
                this.i;
            set => 
                this.i = value;
        }

        [DefaultValue(""), Browsable(false)]
        public string Name
        {
            get => 
                (this.Site != null) ? this.Site.Name : ((this.o == null) ? string.Empty : this.o);
            set
            {
                if (this.Site == null)
                {
                    this.o = (value == null) ? string.Empty : value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Owner
        {
            get => 
                this.p;
            set
            {
                if ((this.p != null) && !base.DesignMode)
                {
                    GlobalComponentsCache.RemoveFromGlobalList(this);
                }
                this.p = value;
                if ((this.p != null) && !base.DesignMode)
                {
                    GlobalComponentsCache.AddToGlobalList(this);
                }
            }
        }

        internal bool IsNHibernate
        {
            get => 
                this.h;
            set => 
                this.h = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InDistributedTransaction =>
            (this.InnerConnection != null) && (this.InnerConnection.Transaction != null);

        private delegate void a();
    }
}

