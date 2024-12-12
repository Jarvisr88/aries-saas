namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public abstract class ProviderFactory
    {
        public const string UseIntegratedSecurityParamID = "useIntegratedSecurity";
        public const string ServerParamID = "server";
        public const string PortParamID = "port";
        public const string DatabaseParamID = "database";
        public const string UserIDParamID = "userid";
        public const string PasswordParamID = "password";
        public const string ReadOnlyParamID = "read only";

        protected ProviderFactory()
        {
        }

        public abstract IDataStore CreateProvider(Dictionary<string, string> parameters, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect);
        public abstract IDataStore CreateProviderFromConnection(IDbConnection connection, AutoCreateOption autoCreateOption);
        public abstract IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect);
        public abstract string GetConnectionString(Dictionary<string, string> parameters);
        public abstract string[] GetDatabases(string server, string userId, string password);
        public virtual string[] GetDatabases(string server, int port, string userId, string password)
        {
            throw new NotImplementedException();
        }

        public abstract bool HasUserName { get; }

        public abstract bool HasPassword { get; }

        public abstract bool HasIntegratedSecurity { get; }

        public abstract bool IsServerbased { get; }

        public virtual bool HasPort =>
            false;

        public abstract bool IsFilebased { get; }

        public abstract bool HasMultipleDatabases { get; }

        public abstract string ProviderKey { get; }

        public virtual string DisplayName =>
            this.ProviderKey;

        public abstract string FileFilter { get; }

        public abstract bool MeanSchemaGeneration { get; }

        public virtual bool SupportStoredProcedures =>
            false;
    }
}

