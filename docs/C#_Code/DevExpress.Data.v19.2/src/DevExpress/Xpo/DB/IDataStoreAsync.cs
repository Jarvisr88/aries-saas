namespace DevExpress.Xpo.DB
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDataStoreAsync : IDataStore
    {
        Task<ModificationResult> ModifyDataAsync(CancellationToken cancellationToken, params ModificationStatement[] dmlStatements);
        Task<SelectedData> SelectDataAsync(CancellationToken cancellationToken, params SelectStatement[] selects);
    }
}

