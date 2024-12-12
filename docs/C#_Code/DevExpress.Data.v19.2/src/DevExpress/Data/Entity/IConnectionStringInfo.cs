namespace DevExpress.Data.Entity
{
    using System;

    public interface IConnectionStringInfo
    {
        string Name { get; }

        string RunTimeConnectionString { get; }

        DataConnectionLocation Location { get; }

        string ProviderName { get; }
    }
}

