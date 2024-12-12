namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    public delegate IDataStore DataStoreCreationFromConnectionDelegate(IDbConnection connection, AutoCreateOption autoCreateOption);
}

