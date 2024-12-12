namespace Devart.Common
{
    using System;
    using System.Data;
    using System.Data.Common;

    internal class DbConnectionClosed : DbConnectionInternal
    {
        private readonly ConnectionState a;
        internal static DbConnectionClosed b = new DbConnectionClosed(ConnectionState.Closed, true, false);
        internal static DbConnectionClosed c = new DbConnectionClosed(ConnectionState.Closed, false, true);
        internal static DbConnectionClosed d = new DbConnectionClosed(ConnectionState.Closed, true, true);
        internal static DbConnectionClosed e = new DbConnectionClosed(ConnectionState.Open, true, false);
        internal static DbConnectionClosed f = new DbConnectionClosed(ConnectionState.Connecting, true, false);

        protected DbConnectionClosed(ConnectionState A_0, bool A_1, bool A_2) : base(A_1, A_2)
        {
            this.a = A_0;
        }

        protected override void a()
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        protected override void a(object A_0)
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        protected internal override DataTable a(DbConnectionBase A_0, string A_1, string[] A_2)
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        protected override void b()
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        public override DbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        public override void ChangeDatabase(string database)
        {
            throw new InvalidOperationException(av.a("ClosedConnectionError"));
        }

        public override void Close()
        {
        }

        public override void Open(DbConnectionBase outerConnection)
        {
            if (ReferenceEquals(this, f))
            {
                throw new InvalidOperationException(av.a("ConnectionAlreadyOpen"));
            }
            Utils.CheckArgumentNull(outerConnection, "outerConnection");
            if (outerConnection.a(f, this, false))
            {
                DbConnectionInternal internal2 = null;
                try
                {
                    internal2 = outerConnection.ConnectionFactory.b(outerConnection);
                }
                catch
                {
                    outerConnection.a(this, false);
                    throw;
                }
                if (internal2 == null)
                {
                    outerConnection.a(this, false);
                    throw new InvalidOperationException(av.a("GetConnectionReturnsNull"));
                }
                outerConnection.a(internal2, true);
            }
        }

        public override string ServerVersion
        {
            get
            {
                throw new InvalidOperationException(av.a("ClosedConnectionError"));
            }
        }

        public override string ServerVersionNormalized
        {
            get
            {
                throw new InvalidOperationException(av.a("ClosedConnectionError"));
            }
        }

        public override ConnectionState State =>
            this.a;
    }
}

