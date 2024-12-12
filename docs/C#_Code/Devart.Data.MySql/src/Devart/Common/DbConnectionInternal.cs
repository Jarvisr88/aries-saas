namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Transactions;

    internal abstract class DbConnectionInternal
    {
        private readonly bool a;
        private readonly bool b;
        protected WeakReference c;
        private object d;
        private DbConnectionPool e;
        private Devart.Common.x f;
        private DateTime g;
        private bool h;
        private int i;
        protected Guid j;
        protected bool k;
        private WeakReference l;
        private Devart.Common.c m;
        private bool n;
        protected Devart.Common.ac o;
        internal object p;
        protected System.Transactions.Transaction q;

        protected DbConnectionInternal() : this(true, false)
        {
        }

        internal DbConnectionInternal(bool A_0, bool A_1)
        {
            this.b = A_0;
            this.a = A_1;
            this.g = DateTime.UtcNow;
        }

        protected virtual void a()
        {
            if ((this.o != null) && this.o.bg())
            {
                this.m.h.e();
            }
            this.o();
            this.l = null;
        }

        protected virtual void a(Devart.Common.ac A_0)
        {
        }

        protected virtual void a(bool A_0)
        {
            if (A_0)
            {
                this.Close();
            }
            this.e = null;
        }

        internal void a(int A_0)
        {
            this.a(A_0, 0);
        }

        protected abstract void a(object A_0);
        private static Guid a(System.Transactions.Transaction A_0)
        {
            byte[] src = A_0.TransactionInformation.DistributedIdentifier.ToByteArray();
            byte[] bytes = Encoding.Default.GetBytes(A_0.TransactionInformation.LocalIdentifier);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            SHA1 sha1 = SHA1.Create();
            byte[] buffer4 = new byte[0x10];
            Buffer.BlockCopy(sha1.ComputeHash(dst), 0, buffer4, 0, buffer4.Length);
            sha1.Dispose();
            return new Guid(buffer4);
        }

        internal void a(DbConnectionPool A_0, Devart.Common.c A_1)
        {
            this.e = A_0;
            this.m = A_1;
        }

        internal void a(int A_0, int A_1)
        {
            Devart.Common.x referenceCollection = this.ReferenceCollection;
            if (referenceCollection != null)
            {
                referenceCollection.a(A_0, A_1, this);
            }
        }

        internal void a(object A_0, Devart.Common.c A_1)
        {
            this.e = null;
            this.Owner = A_0;
            this.i = -1;
            this.m = A_1;
        }

        private void a(object A_0, EventArgs A_1)
        {
            lock (this)
            {
                if (!this.ConnectionIsClosedAndDeffered)
                {
                    this.o();
                }
                else
                {
                    this.CloseInternalConnection();
                    this.ConnectionIsClosedAndDeffered = false;
                    this.q = null;
                }
            }
        }

        internal void a(object A_0, int A_1)
        {
            Devart.Common.x f = this.f;
            if (f == null)
            {
                f = this.ae();
                this.f = f;
            }
            if (f != null)
            {
                f.b(A_0, A_1);
            }
        }

        private static void a(System.Transactions.Transaction A_0, Devart.Common.j A_1)
        {
            if (!A_0.EnlistPromotableSinglePhase(A_1))
            {
                throw new InvalidOperationException("Cannot enlist transaction. Possibly single phase transaction was already used.");
            }
        }

        protected internal virtual DataTable a(DbConnectionBase A_0, string A_1, string[] A_2) => 
            A_0.b(this).GetSchema(A_0, this, A_1, A_2);

        internal bool aa()
        {
            bool flag;
            if (this.f == null)
            {
                return false;
            }
            using (IEnumerator enumerator = this.f.a())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        if (!(enumerator.Current is IDataReader))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        internal bool ad()
        {
            bool flag;
            if (this.f == null)
            {
                return false;
            }
            PropertyInfo property = typeof(DbCommandBase).GetProperty("IsPrepared", BindingFlags.NonPublic | BindingFlags.Instance);
            using (IEnumerator enumerator = this.f.a())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DbCommandBase current = enumerator.Current as DbCommandBase;
                        if ((current == null) || !((bool) property.GetValue(current, null)))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        internal virtual Devart.Common.x ae() => 
            new Devart.Common.x();

        protected virtual void b()
        {
            if (this.EnlistOnActive)
            {
                this.EnlistToDistributedTransaction(System.Transactions.Transaction.Current);
            }
        }

        protected virtual Devart.Common.j b(System.Transactions.Transaction A_0) => 
            new Devart.Common.j(this, A_0.IsolationLevel);

        public abstract DbTransaction BeginTransaction(System.Data.IsolationLevel il);
        public virtual void BeginTransaction(Guid distributedIdentifier, System.Transactions.IsolationLevel isolationLevel)
        {
        }

        internal void c(object A_0)
        {
            if (this.Owner != null)
            {
                throw new InvalidOperationException(av.a("PooledObjectHasOwner"));
            }
            this.Owner = A_0;
            this.i--;
            if (this.Pool != null)
            {
                if (this.i != 0)
                {
                    throw new InvalidOperationException(av.a("PooledObjectInPoolMoreThanOnce"));
                }
            }
            else if (this.i != -1)
            {
                throw new InvalidOperationException(av.a("NonPooledObjectUsedMoreThanOnce"));
            }
        }

        public virtual void ChangeDatabase(string value)
        {
            throw new NotSupportedException();
        }

        public virtual void Close()
        {
            DbConnectionBase owner = (DbConnectionBase) this.Owner;
            if ((owner != null) && owner.a(DbConnectionClosed.e, this, false))
            {
                try
                {
                    this.CloseInternalConnection();
                }
                finally
                {
                    owner.a(DbConnectionClosed.d, true);
                }
            }
        }

        public virtual void CloseInternalConnection()
        {
            object owner = this.Owner;
            DbConnectionBase base2 = (owner != null) ? ((DbConnectionBase) owner) : null;
            if (base2 != null)
            {
                DbMonitorHelper.a(base2.ConnectionFactory.MonitorInstance, MonitorTracePoint.BeforeEvent, base2, (this.o != null) ? this.o.a(true) : string.Empty);
            }
            DbConnectionPool pool = this.Pool;
            if ((pool == null) || (this.State != ConnectionState.Open))
            {
                lock (this)
                {
                    this.a();
                    this.Owner = null;
                    this.Dispose();
                    if ((this.o != null) && this.o.bg())
                    {
                        this.m.e.d();
                        this.m.m.e();
                    }
                    this.a(this.Owner);
                }
            }
            else
            {
                object obj3 = base2;
                obj3 ??= pool;
                DbMonitorHelper.a(pool.PoolGroup.ConnectionFactory.MonitorInstance, MonitorTracePoint.BeforeEvent, (this.o != null) ? this.o.a(true) : string.Empty, obj3, pool);
                this.a(pool.ConnectionOptions);
                this.q();
                lock (this)
                {
                    this.d(this.Owner);
                    pool.PutObject(this);
                }
                if ((this.o != null) && this.o.bg())
                {
                    this.m.g.d();
                }
                DbMonitorHelper.a(pool.PoolGroup.ConnectionFactory.MonitorInstance, MonitorTracePoint.AfterEvent, (this.o != null) ? this.o.a(true) : string.Empty, obj3, pool);
            }
            if (base2 != null)
            {
                DbMonitorHelper.a(base2.ConnectionFactory.MonitorInstance, MonitorTracePoint.AfterEvent, base2, (this.o != null) ? this.o.a(true) : string.Empty);
            }
        }

        public virtual void Commit()
        {
        }

        internal void d(object A_0)
        {
            if (A_0 == null)
            {
                if (this.Owner != null)
                {
                    throw new InvalidOperationException(av.a("UnpooledObjectHasOwner"));
                }
            }
            else if (this.Owner != A_0)
            {
                throw new InvalidOperationException(av.a("UnpooledObjectHasWrongOwner"));
            }
            if (this.i > 0)
            {
                throw new InvalidOperationException(av.a("PushingObjectSecondTime"));
            }
            this.i++;
            this.Owner = null;
        }

        public void Dispose()
        {
            this.a(true);
        }

        public void EnlistToDistributedTransaction(System.Transactions.Transaction transaction)
        {
            if (transaction != null)
            {
                this.EnlistToDistributedTransactionInternal(transaction);
            }
        }

        public void EnlistToDistributedTransactionInternal(System.Transactions.Transaction transaction)
        {
            if (((DbConnectionBase) this.Owner).InTransaction())
            {
                throw new InvalidOperationException("Cannot enlist in the distributed transaction because local transaction already exists.");
            }
            if (this.Transaction != null)
            {
                if (this.Transaction != transaction)
                {
                    throw new InvalidOperationException("Connection is already attached to distributed transaction.");
                }
            }
            else
            {
                this.q = transaction;
                this.l = new WeakReference(transaction);
                Devart.Common.j j = this.b(transaction);
                if (!this.TwoPhaseCommitSupported)
                {
                    if (Devart.Common.v.d(transaction))
                    {
                        throw new InvalidOperationException("Cannot enlist local transaction, because current global transaction already contains distributed transactions.");
                    }
                    this.j = Guid.Empty;
                    a(transaction, j);
                }
                else if (!this.SimulateTwoPhaseCommit)
                {
                    this.j = a(transaction);
                    transaction.EnlistVolatile((IEnlistmentNotification) j, EnlistmentOptions.None);
                }
                else
                {
                    this.j = Guid.Empty;
                    transaction.EnlistVolatile((ISinglePhaseNotification) j, EnlistmentOptions.None);
                }
                Devart.Common.v.a(transaction, this);
                j.a(new EventHandler(this.a));
                j.a(this.j);
            }
        }

        public bool EquivalentTo(Devart.Common.ac options) => 
            (this.ConnectionOptions != null) ? this.ConnectionOptions.Equals(options) : false;

        internal void f(object A_0)
        {
            Devart.Common.x referenceCollection = this.ReferenceCollection;
            if (referenceCollection != null)
            {
                referenceCollection.a(A_0);
            }
        }

        protected virtual void o()
        {
            this.q = null;
        }

        public virtual void Open(DbConnectionBase outerConnection)
        {
            throw new InvalidOperationException(av.a("ConnectionAlreadyOpen"));
        }

        protected virtual void p()
        {
        }

        public virtual void PrepareCommit()
        {
        }

        internal void q()
        {
            this.a();
            this.p();
            this.a(this.Owner);
        }

        protected void r()
        {
            if (this.q != null)
            {
                bool flag = true;
                try
                {
                    if (this.q.TransactionInformation.Status == TransactionStatus.Aborted)
                    {
                        flag = false;
                    }
                }
                catch (ObjectDisposedException)
                {
                    flag = false;
                }
                if (!flag)
                {
                    this.o();
                }
            }
        }

        public virtual void Rollback()
        {
        }

        protected void s()
        {
        }

        internal void t()
        {
            this.v();
        }

        protected void u()
        {
            Devart.Common.x referenceCollection = this.ReferenceCollection;
            if (referenceCollection != null)
            {
                referenceCollection.b();
            }
        }

        protected internal void v()
        {
            this.h = true;
            DbConnectionPool pool = this.Pool;
            if (pool != null)
            {
                pool.MarkInvalidVersion();
            }
        }

        internal void y()
        {
            this.b();
            if ((this.o != null) && this.o.bg())
            {
                this.m.h.d();
            }
        }

        protected virtual bool EnlistOnActive =>
            true;

        internal bool AllowSetConnectionString =>
            this.a;

        internal DateTime CreateTime =>
            this.g;

        internal bool CanBePooled =>
            !this.h && !Devart.Common.Utils.GetWeakIsAlive(this.c);

        internal Devart.Common.x ReferenceCollection =>
            this.f;

        protected internal bool IsConnectionDoomed =>
            this.h;

        internal virtual bool IsEmancipated =>
            ((this.i < 1) && !Devart.Common.Utils.GetWeakIsAlive(this.c)) && !this.ConnectionIsClosedAndDeffered;

        protected internal object Owner
        {
            get => 
                Devart.Common.Utils.GetWeakTarget(this.c);
            set => 
                Devart.Common.Utils.SetWeakTarget(ref this.c, value);
        }

        protected internal object LastOwner
        {
            get
            {
                object owner = this.Owner;
                return ((owner == null) ? this.d : owner);
            }
            set => 
                this.d = value;
        }

        internal DbConnectionPool Pool =>
            this.e;

        public abstract string ServerVersion { get; }

        public virtual string ServerVersionNormalized
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool ShouldHidePassword =>
            this.b;

        public abstract ConnectionState State { get; }

        public System.Transactions.Transaction Transaction
        {
            get
            {
                lock (this)
                {
                    this.r();
                    return this.q;
                }
            }
        }

        public System.Transactions.Transaction LastTransaction
        {
            get
            {
                System.Transactions.Transaction transaction = (this.l == null) ? null : ((System.Transactions.Transaction) this.l.Target);
                if (transaction != null)
                {
                    try
                    {
                        if (transaction.TransactionInformation.Status == TransactionStatus.Aborted)
                        {
                            transaction = null;
                            this.l = null;
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        transaction = null;
                        this.l = null;
                    }
                }
                return transaction;
            }
        }

        public virtual bool SimulateTwoPhaseCommit =>
            false;

        public virtual bool TwoPhaseCommitSupported =>
            false;

        public bool ConnectionIsClosedAndDeffered
        {
            get => 
                this.k;
            set
            {
                if (this.k != value)
                {
                    this.k = value;
                    if ((this.Pool != null) && ((this.o != null) && this.o.bg()))
                    {
                        if (value)
                        {
                            this.m.j.d();
                        }
                        else
                        {
                            this.m.j.e();
                        }
                    }
                }
            }
        }

        protected Devart.Common.ac ConnectionOptions =>
            this.o;

        public bool RunOnceCommandExecuted
        {
            get => 
                this.n;
            set => 
                this.n = value;
        }
    }
}

