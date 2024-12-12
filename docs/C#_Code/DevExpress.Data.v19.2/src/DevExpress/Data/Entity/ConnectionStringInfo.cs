namespace DevExpress.Data.Entity
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConnectionStringInfo : IConnectionStringInfo
    {
        public string Name { get; set; }

        public string RunTimeConnectionString { get; set; }

        public DataConnectionLocation Location { get; }

        public string ProviderName { get; set; }
    }
}

