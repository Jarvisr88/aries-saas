namespace DevExpress.Xpo.DB
{
    using System.Data;

    public interface ISqlDataStore : IDataStore
    {
        IDbCommand CreateCommand();

        IDbConnection Connection { get; }
    }
}

