namespace DevExpress.Data.Entity
{
    using System;

    public interface IConnectionStringsProvider
    {
        IConnectionStringInfo[] GetConfigFileConnections();
        IConnectionStringInfo[] GetConnections();
        string GetConnectionString(string connectionStringName);
        IConnectionStringInfo GetConnectionStringInfo(string connectionStringName);
    }
}

