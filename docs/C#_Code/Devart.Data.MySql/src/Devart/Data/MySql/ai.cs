namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Data.Common;

    internal class ai : DbConnectionFactory
    {
        public static ai a;
        private const int b = 30;

        static ai();
        private ai();
        protected override DbConnectionPoolOptions a(ac A_0);
        protected internal override bool a(DbConnectionInternal A_0);
        protected override ac a(string A_0, ac A_1);
        protected override DbConnectionInternal a(ac A_0, object A_1, DbConnectionBase A_2);
        internal override DbMonitor b();
        protected override DbMetaDataFactory b(DbConnectionInternal A_0);

        public override DbProviderFactory Devart.Common.DbConnectionFactory.ProviderFactory { get; }
    }
}

