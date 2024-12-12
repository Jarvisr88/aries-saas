namespace Devart.Data.MySql
{
    using System;
    using System.Data.Common;

    public class MySqlProviderFactory : DbProviderFactory, IServiceProvider
    {
        private static object a;
        public static MySqlProviderFactory Instance;

        static MySqlProviderFactory();
        public override DbCommand CreateCommand();
        public override DbCommandBuilder CreateCommandBuilder();
        public override DbConnection CreateConnection();
        public override DbConnectionStringBuilder CreateConnectionStringBuilder();
        public override DbDataAdapter CreateDataAdapter();
        public override DbDataSourceEnumerator CreateDataSourceEnumerator();
        public override DbParameter CreateParameter();
        object IServiceProvider.GetService(Type serviceType);

        public override bool CanCreateDataSourceEnumerator { get; }
    }
}

