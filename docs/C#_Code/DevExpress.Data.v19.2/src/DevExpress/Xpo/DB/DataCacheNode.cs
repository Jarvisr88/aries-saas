namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB.Helpers;
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DataCacheNode : DataCacheBase
    {
        private ICacheToCacheCommunicationCore _nested;
        private int objectsInCacheForPerfCounters;
        protected readonly Dictionary<string, Dictionary<string, CacheRecord>> RecordsByTables;
        protected CacheRecord First;
        protected CacheRecord Last;
        protected DateTime LastUpdateTime;
        public TimeSpan MaxCacheLatency;
        public static long GlobalTotalMemoryPurgeThreshold = 0x7fffffffffffffffL;
        public long TotalMemoryPurgeThreshold;
        public long TotalMemoryNotPurgeThreshold;
        public int MinCachedRequestsAfterPurge;
        private volatile bool isAutoCreateOptionCached;
        private DevExpress.Xpo.DB.AutoCreateOption _AutoCreateOption;
        private object _AutoCreateOptionLock;
        private bool purgingEnabled;

        public DataCacheNode(ICacheToCacheCommunicationCore parentCache) : base(parentCache as ICommandChannel)
        {
            this.RecordsByTables = new Dictionary<string, Dictionary<string, CacheRecord>>();
            this.LastUpdateTime = DateTime.MinValue;
            this.MaxCacheLatency = new TimeSpan(0, 0, 30);
            this.TotalMemoryPurgeThreshold = 0x7fffffffffffffffL;
            this.TotalMemoryNotPurgeThreshold = 0x4000000L;
            this.MinCachedRequestsAfterPurge = 0x10;
            this._AutoCreateOption = DevExpress.Xpo.DB.AutoCreateOption.None;
            this._AutoCreateOptionLock = new object();
            this.purgingEnabled = true;
            this._nested = parentCache;
            PerformanceCounters.DataCacheNodeCount.Increment();
            PerformanceCounters.DataCacheNodeCreated.Increment();
        }

        public void CatchUp()
        {
            base.NotifyDirtyTables(new string[0]);
        }

        public override void Configure(DataCacheConfiguration configuration)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        protected virtual void DoPurge()
        {
            new Thread(new ThreadStart(this.Purge)).Start();
        }

        ~DataCacheNode()
        {
            PerformanceCounters.DataCacheNodeCachedCount.Decrement(this.objectsInCacheForPerfCounters);
            PerformanceCounters.DataCacheNodeCachedRemoved.Increment(this.objectsInCacheForPerfCounters);
            this.objectsInCacheForPerfCounters = 0;
            PerformanceCounters.DataCacheNodeCount.Decrement();
            PerformanceCounters.DataCacheNodeFinalized.Increment();
        }

        protected CacheRecord GetCachedRecord(CacheRecord sample) => 
            this.GetCachedRecord(sample.TableName, sample.HashString);

        protected CacheRecord GetCachedRecord(string rootTableName, string statementUniqueString)
        {
            Dictionary<string, CacheRecord> dictionary;
            CacheRecord record;
            if (!this.RecordsByTables.TryGetValue(rootTableName, out dictionary))
            {
                return null;
            }
            dictionary.TryGetValue(statementUniqueString, out record);
            return record;
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

        protected virtual bool IsGoodForCache(SelectStatement stmt)
        {
            bool flag;
            if (IsBadForCache(base.cacheConfiguration, stmt))
            {
                return false;
            }
            List<JoinNode> listToNodeCollection = new List<JoinNode>();
            IndeterminateStatmentFinder indeterminateStatementFinder = new IndeterminateStatmentFinder(listToNodeCollection);
            using (List<CriteriaOperator>.Enumerator enumerator = stmt.Operands.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    CriteriaOperator current = enumerator.Current;
                    if (indeterminateStatementFinder.Process(current))
                    {
                        return false;
                    }
                }
            }
            if (indeterminateStatementFinder.Process(stmt.Condition))
            {
                return false;
            }
            using (List<CriteriaOperator>.Enumerator enumerator2 = stmt.GroupProperties.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator2.MoveNext())
                    {
                        break;
                    }
                    CriteriaOperator current = enumerator2.Current;
                    if (indeterminateStatementFinder.Process(current))
                    {
                        return false;
                    }
                }
            }
            if (indeterminateStatementFinder.Process(stmt.GroupCondition))
            {
                return false;
            }
            using (List<SortingColumn>.Enumerator enumerator3 = stmt.SortProperties.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator3.MoveNext())
                    {
                        break;
                    }
                    SortingColumn current = enumerator3.Current;
                    if (indeterminateStatementFinder.Process(current.Property))
                    {
                        return false;
                    }
                }
            }
            using (List<JoinNode>.Enumerator enumerator4 = stmt.SubNodes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator4.MoveNext())
                    {
                        JoinNode current = enumerator4.Current;
                        if (this.IsGoodForCacheJoinNode(indeterminateStatementFinder, current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        for (int i = 0; i < listToNodeCollection.Count; i++)
                        {
                            if (!this.IsGoodForCacheJoinNode(indeterminateStatementFinder, listToNodeCollection[i]))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        protected virtual bool IsGoodForCache(SelectStatement stmt, SelectStatementResult stmtResult) => 
            !IsBadForCache(base.cacheConfiguration, stmt);

        private bool IsGoodForCacheJoinNode(IndeterminateStatmentFinder indeterminateStatementFinder, JoinNode node)
        {
            bool flag;
            if (IsBadForCache(base.cacheConfiguration, node))
            {
                return false;
            }
            if (indeterminateStatementFinder.Process(node.Condition))
            {
                return false;
            }
            using (List<JoinNode>.Enumerator enumerator = node.SubNodes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        JoinNode current = enumerator.Current;
                        if (this.IsGoodForCacheJoinNode(indeterminateStatementFinder, current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsNotInvalidated(CacheRecord newRecord, DataCacheCookie actualCookie)
        {
            if (actualCookie.Guid != base.MyGuid)
            {
                return false;
            }
            if (actualCookie.Age != base.Age)
            {
                foreach (string str in newRecord.TablesInStatement)
                {
                    long num2;
                    if (base.TablesAges.TryGetValue(str, out num2) && (num2 > actualCookie.Age))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected bool IsWorkingSetOverlap(long totalMemory) => 
            totalMemory > Environment.WorkingSet;

        protected override DataCacheModificationResult ModifyDataCore(DataCacheCookie cookie, ModificationStatement[] dmlStatements)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeModifyRequests, PerformanceCounters.DataCacheNodeModifyQueue))
            {
                PerformanceCounters.DataCacheNodeModifyStatements.Increment(dmlStatements.Length);
                DataCacheModificationResult result = this.Nested.ModifyData(base.GetCurrentCookieSafe(), dmlStatements);
                using (base.LockForChange())
                {
                    this.ProcessParentResultCore(result);
                    base.ProcessChildResultSinceCookieCore(result, cookie);
                }
                return result;
            }
        }

        protected override DataCacheResult NotifyDirtyTablesCore(DataCacheCookie cookie, params string[] dirtyTablesNames)
        {
            dirtyTablesNames ??= new string[0];
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeNotifyDirtyTablesRequests, PerformanceCounters.DataCacheNodeNotifyDirtyTablesQueue))
            {
                PerformanceCounters.DataCacheNodeNotifyDirtyTablesTables.Increment(dirtyTablesNames.Length);
                DataCacheResult result = this.Nested.NotifyDirtyTables(base.GetCurrentCookieSafe(), dirtyTablesNames);
                using (base.LockForChange())
                {
                    this.ProcessParentResultCore(result);
                    base.ProcessChildResultSinceCookieCore(result, cookie);
                }
                return result;
            }
        }

        protected override DataCacheResult ProcessCookieCore(DataCacheCookie cookie)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeProcessCookieRequests, PerformanceCounters.DataCacheNodeProcessCookieQueue))
            {
                this.ProcessCurrentCookieIfNeeded();
                DataCacheResult result = new DataCacheResult();
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        protected void ProcessCurrentCookieIfNeeded()
        {
            using (base.LockForRead())
            {
                if (this.IsCacheFresh)
                {
                    return;
                }
            }
            DataCacheResult result = this.Nested.ProcessCookie(base.GetCurrentCookieSafe());
            this.ProcessParentResult(result);
        }

        protected void ProcessParentResult(DataCacheResult result)
        {
            using (base.LockForChange())
            {
                this.ProcessParentResultCore(result);
            }
        }

        private void ProcessParentResultCore(DataCacheResult result)
        {
            if (result.CacheConfig != null)
            {
                base.cacheConfiguration = result.CacheConfig;
            }
            if (base.MyGuid != result.Cookie.Guid)
            {
                this.ResetCore();
                base.MyGuid = result.Cookie.Guid;
                base.Age = result.Cookie.Age;
            }
            else if (result.Cookie.Age > base.Age)
            {
                base.Age = result.Cookie.Age;
                foreach (TableAge age in result.UpdatedTableAges)
                {
                    long num2;
                    if (!base.TablesAges.TryGetValue(age.Name, out num2) || (num2 < age.Age))
                    {
                        base.TablesAges[age.Name] = age.Age;
                        this.UnregisterRecordsForTable(age.Name);
                    }
                }
            }
            this.LastUpdateTime = DateTime.UtcNow;
        }

        protected void PromoteRecordToMRU(CacheRecord record)
        {
            if (record.Prev != null)
            {
                record.Prev.Next = record.Next;
                if (record.Next != null)
                {
                    record.Next.Prev = record.Prev;
                }
                else
                {
                    this.Last = record.Prev;
                }
                record.Next = this.First;
                record.Prev = null;
                this.First.Prev = record;
                this.First = record;
            }
        }

        protected virtual void Purge()
        {
            using (base.LockForChange())
            {
                if ((this.Last != null) && this.purgingEnabled)
                {
                    GCFlagger flagger1 = new GCFlagger(this);
                    CacheRecord first = this.First;
                    int num = 1;
                    while (true)
                    {
                        if (first.Next == null)
                        {
                            if (num > this.MinCachedRequestsAfterPurge)
                            {
                                int num2 = Math.Max((num / 3) * 2, this.MinCachedRequestsAfterPurge);
                                while (num > num2)
                                {
                                    this.RemoveFromCache(this.Last);
                                    num--;
                                }
                            }
                            break;
                        }
                        num++;
                        first = first.Next;
                    }
                }
            }
        }

        protected virtual void PurgeIfNeeded()
        {
            if (this.purgingEnabled)
            {
                long totalMemory = GC.GetTotalMemory(false);
                if ((totalMemory > this.TotalMemoryNotPurgeThreshold) && ((totalMemory >= this.TotalMemoryPurgeThreshold) || ((totalMemory >= GlobalTotalMemoryPurgeThreshold) || this.IsWorkingSetOverlap(totalMemory))))
                {
                    this.DoPurge();
                }
            }
        }

        protected void RegisterNewRecord(CacheRecord newRecord)
        {
            foreach (string str in newRecord.TablesInStatement)
            {
                Dictionary<string, CacheRecord> dictionary;
                if (!this.RecordsByTables.TryGetValue(str, out dictionary))
                {
                    dictionary = new Dictionary<string, CacheRecord>();
                    this.RecordsByTables.Add(str, dictionary);
                }
                dictionary.Add(newRecord.HashString, newRecord);
            }
            if (this.First != null)
            {
                this.First.Prev = newRecord;
            }
            newRecord.Next = this.First;
            this.First = newRecord;
            this.Last ??= newRecord;
            this.objectsInCacheForPerfCounters++;
            PerformanceCounters.DataCacheNodeCachedCount.Increment();
            PerformanceCounters.DataCacheNodeCachedAdded.Increment();
            this.PurgeIfNeeded();
        }

        protected void RemoveFromCache(CacheRecord record)
        {
            foreach (string str in record.TablesInStatement)
            {
                this.RecordsByTables[str].Remove(record.HashString);
            }
            if (record.Prev == null)
            {
                this.First = record.Next;
            }
            else
            {
                record.Prev.Next = record.Next;
            }
            if (record.Next == null)
            {
                this.Last = record.Prev;
            }
            else
            {
                record.Next.Prev = record.Prev;
            }
            this.objectsInCacheForPerfCounters--;
            PerformanceCounters.DataCacheNodeCachedCount.Decrement();
            PerformanceCounters.DataCacheNodeCachedRemoved.Increment();
        }

        protected override void ResetCore()
        {
            base.ResetCore();
            PerformanceCounters.DataCacheNodeCachedCount.Decrement(this.objectsInCacheForPerfCounters);
            PerformanceCounters.DataCacheNodeCachedRemoved.Increment(this.objectsInCacheForPerfCounters);
            this.objectsInCacheForPerfCounters = 0;
            this.RecordsByTables.Clear();
            this.First = null;
            this.Last = null;
        }

        protected override DataCacheSelectDataResult SelectDataCore(DataCacheCookie cookie, SelectStatement[] selects)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeSelectRequests, PerformanceCounters.DataCacheNodeSelectQueue))
            {
                DataCacheSelectDataResult result;
                SelectStatementResult[] resultArray;
                int num2;
                int num3;
                SelectStatement[] statementArray;
                IDisposable disposable4;
                PerformanceCounters.DataCacheNodeSelectQueries.Increment(selects.Length);
                CacheRecord[] recordArray = new CacheRecord[selects.Length];
                int index = 0;
                while (true)
                {
                    if (index >= selects.Length)
                    {
                        if ((recordArray.Length != 0) && (recordArray[0] != null))
                        {
                            bool flag = false;
                            using (base.LockForRead())
                            {
                                flag = this.GetCachedRecord(recordArray[0]) != null;
                            }
                            if (flag)
                            {
                                this.ProcessCurrentCookieIfNeeded();
                            }
                        }
                        result = new DataCacheSelectDataResult();
                        resultArray = new SelectStatementResult[selects.Length];
                        num2 = 0;
                        break;
                    }
                    SelectStatement stmt = selects[index];
                    if (this.IsGoodForCache(stmt))
                    {
                        recordArray[index] = new CacheRecord(stmt);
                    }
                    index++;
                }
                goto TR_0039;
            TR_0029:
                statementArray = new SelectStatement[num3];
                int num5 = 0;
                while (true)
                {
                    if (num5 >= num3)
                    {
                        DataCacheSelectDataResult result2 = this.Nested.SelectData(base.GetCurrentCookieSafe(), statementArray);
                        this.ProcessParentResult(result2);
                        if (num2 == 0)
                        {
                            result.SelectingCookie = result2.SelectingCookie;
                        }
                        for (int i = 0; i < num3; i++)
                        {
                            bool flag2;
                            SelectStatementResult stmtResult = result2.SelectedData.ResultSet[i];
                            resultArray[num2] = stmtResult;
                            CacheRecord sample = recordArray[num2];
                            if (sample == null)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                using (base.LockForChange())
                                {
                                    if ((this.GetCachedRecord(sample) != null) || (!this.IsGoodForCache(selects[num2], stmtResult) || !this.IsNotInvalidated(sample, result2.SelectingCookie)))
                                    {
                                        flag2 = true;
                                    }
                                    else
                                    {
                                        sample.QueryResult = stmtResult;
                                        this.RegisterNewRecord(sample);
                                        flag2 = false;
                                    }
                                }
                            }
                            if (flag2)
                            {
                                PerformanceCounters.DataCacheNodeCachePassthrough.Increment();
                            }
                            else
                            {
                                PerformanceCounters.DataCacheNodeCacheMiss.Increment();
                            }
                            num2++;
                        }
                        break;
                    }
                    statementArray[num5] = selects[num2 + num5];
                    num5++;
                }
                goto TR_0039;
            TR_0030:
                try
                {
                    while (true)
                    {
                        int num4 = num2 + num3;
                        if ((num4 < selects.Length) && ((recordArray[num4] == null) || (this.GetCachedRecord(recordArray[num4]) == null)))
                        {
                            num3++;
                            if (num3 < 0xc0)
                            {
                                break;
                            }
                            num3 = 0x80;
                            goto TR_0029;
                        }
                        else
                        {
                            goto TR_0029;
                        }
                        break;
                    }
                    goto TR_0030;
                }
                finally
                {
                    if (disposable4 != null)
                    {
                        disposable4.Dispose();
                    }
                }
                goto TR_0029;
            TR_0039:
                while (true)
                {
                    if (num2 < selects.Length)
                    {
                        if (recordArray[num2] != null)
                        {
                            using (base.LockForChange())
                            {
                                CacheRecord cachedRecord = this.GetCachedRecord(recordArray[num2]);
                                if (cachedRecord == null)
                                {
                                    break;
                                }
                                if (num2 == 0)
                                {
                                    result.SelectingCookie = base.GetCurrentCookie();
                                }
                                resultArray[num2] = cachedRecord.QueryResult;
                                this.PromoteRecordToMRU(cachedRecord);
                                num2++;
                                PerformanceCounters.DataCacheNodeCacheHit.Increment();
                            }
                            continue;
                        }
                    }
                    else
                    {
                        result.SelectedData = new SelectedData(resultArray);
                        base.ProcessChildResultSinceCookie(result, cookie);
                        return result;
                    }
                    break;
                }
                num3 = 1;
                disposable4 = base.LockForRead();
                goto TR_0030;
            }
        }

        protected void UnregisterRecordsForTable(string table)
        {
            Dictionary<string, CacheRecord> dictionary;
            if (this.RecordsByTables.TryGetValue(table, out dictionary))
            {
                foreach (CacheRecord record in new List<CacheRecord>(dictionary.Values))
                {
                    this.RemoveFromCache(record);
                }
            }
        }

        protected override DataCacheUpdateSchemaResult UpdateSchemaCore(DataCacheCookie cookie, DBTable[] tables, bool doNotCreateIfFirstTableNotExist)
        {
            using (new PerformanceCounters.QueueLengthCounter(PerformanceCounters.DataCacheNodeTotalRequests, PerformanceCounters.DataCacheNodeTotalQueue, PerformanceCounters.DataCacheNodeSchemaUpdateRequests, PerformanceCounters.DataCacheNodeSchemaUpdateQueue))
            {
                DataCacheUpdateSchemaResult result = this.Nested.UpdateSchema(base.GetCurrentCookieSafe(), tables, doNotCreateIfFirstTableNotExist);
                this.ProcessParentResult(result);
                base.ProcessChildResultSinceCookie(result, cookie);
                return result;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use GlobalTotalMemoryPurgeThreshold field instead")]
        public static long GlobalTotalMemoryPurgeTreshhold
        {
            get => 
                GlobalTotalMemoryPurgeThreshold;
            set => 
                GlobalTotalMemoryPurgeThreshold = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use TotalMemoryPurgeThreshold field instead")]
        public long TotalMemoryPurgeTreshhold
        {
            get => 
                this.TotalMemoryPurgeThreshold;
            set => 
                this.TotalMemoryPurgeThreshold = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use TotalMemoryNotPurgeThreshold field instead")]
        public long TotalMemoryNotPurgeTreshhold
        {
            get => 
                this.TotalMemoryNotPurgeThreshold;
            set => 
                this.TotalMemoryNotPurgeThreshold = value;
        }

        protected ICacheToCacheCommunicationCore Nested =>
            this._nested;

        public override DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption
        {
            get
            {
                if (!this.isAutoCreateOptionCached)
                {
                    object obj2 = this._AutoCreateOptionLock;
                    lock (obj2)
                    {
                        if (!this.isAutoCreateOptionCached)
                        {
                            this.isAutoCreateOptionCached = true;
                            try
                            {
                                this._AutoCreateOption = ((IDataStore) this.Nested).AutoCreateOption;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                return this._AutoCreateOption;
            }
        }

        protected bool IsCacheFresh
        {
            get
            {
                if (this.MaxCacheLatency == TimeSpan.Zero)
                {
                    return false;
                }
                DateTime utcNow = DateTime.UtcNow;
                DateTime lastUpdateTime = this.LastUpdateTime;
                return (((utcNow - lastUpdateTime) < this.MaxCacheLatency) ? ((lastUpdateTime - utcNow) < this.MaxCacheLatency) : false);
            }
        }

        private class GCFlagger
        {
            public readonly DataCacheNode Node;

            public GCFlagger(DataCacheNode owner)
            {
                this.Node = owner;
                this.Node.purgingEnabled = false;
            }

            ~GCFlagger()
            {
                try
                {
                    this.Node.purgingEnabled = true;
                }
                catch
                {
                }
            }
        }
    }
}

