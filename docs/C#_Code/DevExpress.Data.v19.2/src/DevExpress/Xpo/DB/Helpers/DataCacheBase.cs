namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Xpo.DB;
    using DevExpress.Xpo.Helpers;
    using DevExpress.Xpo.Logger;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DataCacheBase : MarshalByRefObject, ICachedDataStore, ICacheToCacheCommunicationCore, IDataStore, IDataStoreSchemaExplorer, ICommandChannel, ICommandChannelAsync
    {
        protected DataCacheConfiguration cacheConfiguration = new DataCacheConfiguration();
        protected readonly ICommandChannel nestedCommandChannel;
        public const string LogCategory = "DataCache";
        protected long Age;
        protected Guid MyGuid = Guid.NewGuid();
        private readonly ReaderWriterLockSlim _RWL = new ReaderWriterLockSlim();
        protected readonly Dictionary<string, long> TablesAges = new Dictionary<string, long>();

        protected DataCacheBase(ICommandChannel nestedCommandChannel)
        {
            this.nestedCommandChannel = nestedCommandChannel;
        }

        public abstract void Configure(DataCacheConfiguration configuration);
        object ICommandChannel.Do(string command, object args) => 
            this.DoInternal(command, args);

        Task<object> ICommandChannelAsync.DoAsync(string command, object args, CancellationToken cancellationToken) => 
            this.DoInternalAsync(command, args, cancellationToken);

        protected virtual object DoInternal(string command, object args)
        {
            this.ThrowIfCommandNotSupported(command);
            return this.nestedCommandChannel.Do(command, args);
        }

        protected virtual Task<object> DoInternalAsync(string command, object args, CancellationToken cancellationToken)
        {
            this.ThrowIfCommandNotSupported(command);
            ICommandChannelAsync nestedCommandChannel = this.nestedCommandChannel as ICommandChannelAsync;
            if (nestedCommandChannel != null)
            {
                return nestedCommandChannel.DoAsync(command, args, cancellationToken);
            }
            object[] objArray1 = new object[] { this.nestedCommandChannel.GetType().FullName };
            throw new InvalidOperationException(DbRes.GetString("Async_CommandChannelDoesNotImplementICommandChannelAsync", objArray1));
        }

        protected DataCacheCookie GetCurrentCookie() => 
            new DataCacheCookie(this.MyGuid, this.Age);

        protected DataCacheCookie GetCurrentCookieSafe()
        {
            using (this.LockForRead())
            {
                return this.GetCurrentCookie();
            }
        }

        public abstract DBTable[] GetStorageTables(params string[] tables);
        public abstract string[] GetStorageTablesList(bool includeViews);
        public static bool IsBadForCache(DataCacheConfiguration config, JoinNode node) => 
            !(node.Table is DBProjection) ? IsBadForCache(config, node.Table.Name) : false;

        public static bool IsBadForCache(DataCacheConfiguration config, string tableName)
        {
            if ((config == null) || (config.TableDictionary == null))
            {
                return false;
            }
            DataCacheConfigurationCaching caching = config.Caching;
            return ((caching == DataCacheConfigurationCaching.InList) ? !config.TableDictionary.ContainsKey(tableName) : ((caching == DataCacheConfigurationCaching.NotInList) && config.TableDictionary.ContainsKey(tableName)));
        }

        protected IDisposable LockForChange()
        {
            if (this._RWL.IsReadLockHeld)
            {
                throw new InvalidOperationException("internal error: reader lock already held!!!");
            }
            return new DataCacheWriterLock(this._RWL);
        }

        protected IDisposable LockForRead() => 
            new DataCacheReaderLock(this._RWL);

        public virtual ModificationResult ModifyData(params ModificationStatement[] dmlStatements) => 
            this.ModifyData(DataCacheCookie.Empty, dmlStatements).ModificationResult;

        public DataCacheModificationResult ModifyData(DataCacheCookie cookie, ModificationStatement[] statements) => 
            LogManager.LogMany<DataCacheModificationResult>("DataCache", () => this.ModifyDataCore(cookie, statements), delegate (TimeSpan d) {
                if ((statements == null) && (statements.Length == 0))
                {
                    return null;
                }
                LogMessage[] messageArray = new LogMessage[statements.Length];
                for (int i = 0; i < statements.Length; i++)
                {
                    messageArray[i] = LogMessage.CreateMessage(this, cookie, statements[i].ToString(), d);
                }
                return messageArray;
            });

        protected abstract DataCacheModificationResult ModifyDataCore(DataCacheCookie cookie, ModificationStatement[] statements);
        public DataCacheResult NotifyDirtyTables(params string[] dirtyTablesNames) => 
            this.NotifyDirtyTables(DataCacheCookie.Empty, dirtyTablesNames);

        public DataCacheResult NotifyDirtyTables(DataCacheCookie cookie, params string[] dirtyTablesNames) => 
            LogManager.Log<DataCacheResult>("DataCache", () => this.NotifyDirtyTablesCore(cookie, dirtyTablesNames), d => LogMessage.CreateMessage(this, cookie, "NotifyDirtyTables: " + string.Join(";", dirtyTablesNames), d));

        protected abstract DataCacheResult NotifyDirtyTablesCore(DataCacheCookie cookie, params string[] dirtyTablesNames);
        protected void ProcessChildResultSinceCookie(DataCacheResult result, DataCacheCookie cookie)
        {
            using (this.LockForRead())
            {
                this.ProcessChildResultSinceCookieCore(result, cookie);
            }
        }

        protected void ProcessChildResultSinceCookieCore(DataCacheResult result, DataCacheCookie cookie)
        {
            result.Cookie = this.GetCurrentCookie();
            if (this.MyGuid != cookie.Guid)
            {
                result.CacheConfig = this.cacheConfiguration;
                result.UpdatedTableAges = null;
            }
            else if (this.Age == cookie.Age)
            {
                result.CacheConfig = null;
                result.UpdatedTableAges = new TableAge[0];
            }
            else
            {
                result.CacheConfig = null;
                List<TableAge> list = new List<TableAge>();
                foreach (KeyValuePair<string, long> pair in this.TablesAges)
                {
                    if (pair.Value > cookie.Age)
                    {
                        list.Add(new TableAge(pair.Key, pair.Value));
                    }
                }
                result.UpdatedTableAges = list.ToArray();
            }
        }

        public DataCacheResult ProcessCookie(DataCacheCookie cookie) => 
            LogManager.Log<DataCacheResult>("DataCache", () => this.ProcessCookieCore(cookie), d => LogMessage.CreateMessage(this, cookie, "ProcessCookie", d));

        protected abstract DataCacheResult ProcessCookieCore(DataCacheCookie cookie);
        public void Reset()
        {
            using (this.LockForChange())
            {
                this.ResetCore();
            }
        }

        protected virtual void ResetCore()
        {
            this.MyGuid = Guid.NewGuid();
            this.Age = 0L;
            this.TablesAges.Clear();
        }

        public virtual SelectedData SelectData(params SelectStatement[] selects) => 
            this.SelectData(DataCacheCookie.Empty, selects).SelectedData;

        public DataCacheSelectDataResult SelectData(DataCacheCookie cookie, SelectStatement[] selects) => 
            LogManager.LogMany<DataCacheSelectDataResult>("DataCache", () => this.SelectDataCore(cookie, selects), delegate (TimeSpan d) {
                if ((selects == null) && (selects.Length == 0))
                {
                    return null;
                }
                LogMessage[] messageArray = new LogMessage[selects.Length];
                for (int i = 0; i < selects.Length; i++)
                {
                    messageArray[i] = LogMessage.CreateMessage(this, cookie, selects[i].ToString(), d);
                }
                return messageArray;
            });

        protected abstract DataCacheSelectDataResult SelectDataCore(DataCacheCookie cookie, SelectStatement[] selects);
        private void ThrowIfCommandNotSupported(string command)
        {
            if (this.nestedCommandChannel == null)
            {
                throw new NotSupportedException($"Command '{command}' is not supported.");
            }
            if (!DataCacheConfiguration.SuppressExplicitTransactionExceptions && ((command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitBeginTransaction") || ((command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitCommitTransaction") || (command == "DevExpress.Xpo.Helpers.CommandChannelHelper.ExplicitRollbackTransaction"))))
            {
                throw new NotSupportedException("An attempt to execute an explicit transaction has been detected.\x00a0Caching may work incorrectly with\x00a0explicit transactions. Either\x00a0connect your data layer\x00a0to the database directly\x00a0or set the\x00a0DataCacheConfiguration.SuppressExplicitTransactionExceptions static property to true.");
            }
        }

        public virtual UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables) => 
            this.UpdateSchema(DataCacheCookie.Empty, tables, doNotCreateIfFirstTableNotExist).UpdateSchemaResult;

        public DataCacheUpdateSchemaResult UpdateSchema(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist) => 
            LogManager.Log<DataCacheUpdateSchemaResult>("DataCache", () => this.UpdateSchemaCore(cookie, tables, doNotCreateIfFirstTableNotExist), delegate (TimeSpan d) {
                Function<string, DBTable> getString = <>c.<>9__14_2;
                if (<>c.<>9__14_2 == null)
                {
                    Function<string, DBTable> local1 = <>c.<>9__14_2;
                    getString = <>c.<>9__14_2 = table => table.Name;
                }
                return LogMessage.CreateMessage(this, cookie, "UpdateSchema: " + LogMessage.CollectionToString<DBTable>(tables, getString), d);
            });

        protected abstract DataCacheUpdateSchemaResult UpdateSchemaCore(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist);
        [Conditional("DEBUG")]
        protected void ValidateLockedRead()
        {
            if (!this._RWL.IsReadLockHeld && !this._RWL.IsWriteLockHeld)
            {
                throw new InvalidOperationException("internal error: reader lock expected!!!");
            }
        }

        [Conditional("DEBUG")]
        protected void ValidateLockedWrite()
        {
            if (!this._RWL.IsWriteLockHeld)
            {
                throw new InvalidOperationException("internal error: writer lock expected!!!");
            }
        }

        public abstract DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataCacheBase.<>c <>9 = new DataCacheBase.<>c();
            public static Function<string, DBTable> <>9__14_2;

            internal string <UpdateSchema>b__14_2(DBTable table) => 
                table.Name;
        }
    }
}

