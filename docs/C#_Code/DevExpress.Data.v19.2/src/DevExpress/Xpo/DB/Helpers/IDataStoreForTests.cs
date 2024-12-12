namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;

    public interface IDataStoreForTests : IDataStore
    {
        void ClearDatabase();
    }
}

