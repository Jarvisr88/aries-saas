namespace DevExpress.Xpo.DB
{
    using DevExpress.Xpo.DB.Helpers;
    using DevExpress.Xpo.Helpers;
    using System;

    public class DataCacheRoot : DataCacheBase
    {
        private IDataStore _nested;

        public DataCacheRoot(IDataStore subjectForCache) : base(subjectForCache as ICommandChannel)
        {
            this._nested = subjectForCache;
            PerformanceCounters.DataCacheRootCount.Increment();
            PerformanceCounters.DataCacheRootCreated.Increment();
        }

        public override void Configure(DataCacheConfiguration configuration)
        {
            using (base.LockForChange())
            {
                base.cacheConfiguration = configuration;
                this.ResetCore();
            }
        }

        ~DataCacheRoot()
        {
            PerformanceCounters.DataCacheRootCount.Decrement();
            PerformanceCounters.DataCacheRootFinalized.Increment();
        }

        public override DBTable[] GetStorageTables(params string[] tables)
        {
            IDataStoreSchemaExplorer nested = this.Nested as IDataStoreSchemaExplorer;
            return nested?.GetStorageTables(tables);
        }

        public override string[] GetStorageTablesList(bool includeViews)
        {
            IDataStoreSchemaExplorer nested = this.Nested as IDataStoreSchemaExplorer;
            return nested?.GetStorageTablesList(includeViews);
        }

        protected override DataCacheModificationResult ModifyDataCore(DataCacheCookie cookie, ModificationStatement[] dmlStatements)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheRootTotalRequests, PerformanceCounters.DataCacheRootTotalQueue, PerformanceCounters.DataCacheRootModifyRequests, PerformanceCounters.DataCacheRootModifyQueue))
            {
                PerformanceCounters.DataCacheRootModifyStatements.Increment(dmlStatements.Length);
                DataCacheModificationResult result = new DataCacheModificationResult {
                    ModificationResult = this.Nested.ModifyData(dmlStatements)
                };
                string[] tablesNames = BaseStatement.GetTablesNames(dmlStatements);
                this.NotifyDirtyTablesCore(tablesNames);
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        private void NotifyDirtyTablesCore(string[] dirtyTablesNames)
        {
            if ((dirtyTablesNames != null) && (dirtyTablesNames.Length != 0))
            {
                using (base.LockForChange())
                {
                    if (base.Age == 0x7fffffffffffffffL)
                    {
                        this.ResetCore();
                    }
                    else
                    {
                        base.Age += 1L;
                        foreach (string str in dirtyTablesNames)
                        {
                            base.TablesAges[str] = base.Age;
                        }
                    }
                }
            }
        }

        protected override DataCacheResult NotifyDirtyTablesCore(DataCacheCookie cookie, params string[] dirtyTablesNames)
        {
            dirtyTablesNames ??= new string[0];
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheRootTotalRequests, PerformanceCounters.DataCacheRootTotalQueue, PerformanceCounters.DataCacheRootNotifyDirtyTablesRequests, PerformanceCounters.DataCacheRootNotifyDirtyTablesQueue))
            {
                PerformanceCounters.DataCacheRootNotifyDirtyTablesTables.Increment(dirtyTablesNames.Length);
                DataCacheResult result = new DataCacheResult();
                this.NotifyDirtyTablesCore(dirtyTablesNames);
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        protected override DataCacheResult ProcessCookieCore(DataCacheCookie cookie)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheRootTotalRequests, PerformanceCounters.DataCacheRootTotalQueue, PerformanceCounters.DataCacheRootProcessCookieRequests, PerformanceCounters.DataCacheRootProcessCookieQueue))
            {
                DataCacheResult result = new DataCacheResult();
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        protected override DataCacheSelectDataResult SelectDataCore(DataCacheCookie cookie, SelectStatement[] selects)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheRootTotalRequests, PerformanceCounters.DataCacheRootTotalQueue, PerformanceCounters.DataCacheRootSelectRequests, PerformanceCounters.DataCacheRootSelectQueue))
            {
                PerformanceCounters.DataCacheRootSelectQueries.Increment(selects.Length);
                DataCacheSelectDataResult result = new DataCacheSelectDataResult {
                    SelectingCookie = base.GetCurrentCookieSafe(),
                    SelectedData = this.Nested.SelectData(selects)
                };
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        protected override DataCacheUpdateSchemaResult UpdateSchemaCore(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheRootTotalRequests, PerformanceCounters.DataCacheRootTotalQueue, PerformanceCounters.DataCacheRootSchemaUpdateRequests, PerformanceCounters.DataCacheRootSchemaUpdateQueue))
            {
                DataCacheUpdateSchemaResult result = new DataCacheUpdateSchemaResult {
                    UpdateSchemaResult = this.Nested.UpdateSchema(doNotCreateIfFirstTableNotExist, tables)
                };
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        protected IDataStore Nested =>
            this._nested;

        public override DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            this.Nested.AutoCreateOption;
    }
}

