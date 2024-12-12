namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.DB;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public delegate IDataStore DataStoreCreationFromStringDelegate(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect);
}

