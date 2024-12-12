namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Transactions;

    internal class MySqlInternalConnection : DbConnectionInternal
    {
        private aw a;
        private string b;
        private bool c;

        public MySqlInternalConnection(bo connectionOptions, MySqlConnection owner);
        protected override void a();
        protected override void a(ac A_0);
        protected override void a(bool A_0);
        internal void a(Exception A_0);
        protected override void a(object A_0);
        protected override void b();
        private void b(bool A_0);
        public override DbTransaction BeginTransaction(IsolationLevel il);
        public override void BeginTransaction(Guid distributedIdentifier, IsolationLevel isolationLevel);
        private void c(bool A_0);
        public override void ChangeDatabase(string value);
        public bool CheckIsValid();
        public override void Close();
        public override void Commit();
        public void Connect(MySqlConnection owner, string userId, string password, string host, string database, int port, int connectionTimeout, MySqlProtocol protocol, bool compress, bool clientInteractive);
        public void Disconnect();
        private void e();
        public void Error();
        private void f();
        private void g();
        private HttpOptions h();
        private MySqlHttpOptions i();
        private ProxyOptions j();
        private SslOptions k();
        public void Kill(MySqlInternalConnection victim);
        public void Kill(int threadId);
        private SshOptions l();
        private MySqlConnection m();
        public bool Ping();
        public override void PrepareCommit();
        public override void Rollback();

        protected bo ConnectionOptions { get; }

        public aw Session { get; }

        public override string ServerVersion { get; }

        public override string ServerVersionNormalized { get; }

        public int ServerVersionNumber { get; }

        public string ClientVersion { get; }

        public string ClientVersionNormalized { get; }

        public string Database { get; }

        public string Charset { get; }

        public string HostInfo { get; }

        public string ProtocolInfo { get; }

        public override ConnectionState State { get; }

        protected override bool EnlistOnActive { get; }

        public override bool TwoPhaseCommitSupported { get; }
    }
}

