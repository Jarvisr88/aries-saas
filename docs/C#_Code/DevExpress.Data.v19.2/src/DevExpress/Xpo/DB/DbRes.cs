namespace DevExpress.Xpo.DB
{
    using System;
    using System.Globalization;
    using System.Resources;

    internal sealed class DbRes
    {
        private ResourceManager manager;
        private static DbRes res;
        public const string ConnectionProvider_TypeMappingMissing = "ConnectionProvider_TypeMappingMissing";
        public const string ConnectionProvider_UnableToCreateDBObject = "ConnectionProvider_UnableToCreateDBObject";
        public const string ConnectionProvider_SqlExecutionError = "ConnectionProvider_SqlExecutionError";
        public const string ConnectionProvider_SchemaCorrectionNeeded = "ConnectionProvider_SchemaCorrectionNeeded";
        public const string ConnectionProvider_AtLeastOneColumnExpected = "ConnectionProvider_AtLeastOneColumnExpected";
        public const string ConnectionProvider_Locking = "ConnectionProvider_Locking";
        public const string ConnectionProvider_UnableToOpenDatabase = "ConnectionProvider_UnableToOpenDatabase";
        public const string Async_CommandChannelDoesNotImplementICommandChannelAsync = "Async_CommandChannelDoesNotImplementICommandChannelAsync";
        public const string Async_ConnectionProviderDoesNotImplementIDataStoreAsync = "Async_ConnectionProviderDoesNotImplementIDataStoreAsync";
        public const string ConnectionProviderSql_DbCommandAsyncOperationsNotSupported = "ConnectionProviderSql_DbCommandAsyncOperationsNotSupported";
        public const string ConnectionProviderSql_DbDataReaderAsyncOperationsNotSupported = "ConnectionProviderSql_DbDataReaderAsyncOperationsNotSupported";
        public const string CommandChannelHelper_CannotDeserializeResponse = "CommandChannelHelper_CannotDeserializeResponse";

        private DbRes()
        {
            this.manager = new ResourceManager("DevExpress.Data.Db.Messages", base.GetType().Assembly);
        }

        private static DbRes GetLoader()
        {
            if (res == null)
            {
                Type type = typeof(DbRes);
                lock (type)
                {
                    res ??= new DbRes();
                }
            }
            return res;
        }

        public static string GetString(string name) => 
            GetString(null, name);

        public static string GetString(CultureInfo culture, string name)
        {
            DbRes loader = GetLoader();
            return ((loader != null) ? loader.manager.GetString(name, culture) : null);
        }

        public static string GetString(string name, params object[] args) => 
            GetString(null, name, args);

        public static string GetString(CultureInfo culture, string name, params object[] args)
        {
            DbRes loader = GetLoader();
            if (loader == null)
            {
                return null;
            }
            string format = loader.manager.GetString(name, culture);
            return (((args == null) || (args.Length == 0)) ? format : string.Format(format, args));
        }
    }
}

