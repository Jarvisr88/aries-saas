namespace Devart.Common
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.IO;

    internal class DbMetaDataFactory
    {
        public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
        {
        }

        public virtual DataTable GetSchema(DbConnection connection, DbConnectionInternal internalConnection, string collectionName, string[] restrictions) => 
            null;
    }
}

