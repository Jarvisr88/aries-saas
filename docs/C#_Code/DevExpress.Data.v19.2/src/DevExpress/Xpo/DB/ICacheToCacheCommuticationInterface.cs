namespace DevExpress.Xpo.DB
{
    using System;

    [Obsolete("Please use ICachedDataStore interface instead", true)]
    public interface ICacheToCacheCommuticationInterface : ICachedDataStore, ICacheToCacheCommunicationCore, IDataStore
    {
    }
}

