namespace DevExpress.Xpo.DB
{
    public interface IDataStoreSchemaExplorerSp : IDataStoreSchemaExplorer
    {
        DBStoredProcedure[] GetStoredProcedures();
    }
}

