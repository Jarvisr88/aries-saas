namespace DevExpress.Xpo.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class PerformanceCounters
    {
        private static CounterCreationDataCollection CountersData;
        private static Counter[] counters;
        private static object syncRoot = new object();
        private static bool inited = false;

        static PerformanceCounters()
        {
            CounterCreationData[] dataArray = new CounterCreationData[] { new CounterCreationData("Objects in identity map", "Objects in identity map", PerformanceCounterType.NumberOfItems32), new CounterCreationData("Objects added to identity map", "Objects added to identity map", PerformanceCounterType.RateOfCountsPerSecond32), new CounterCreationData("Objects removed from identity map", "Objects removed from identity map", PerformanceCounterType.RateOfCountsPerSecond32), new CounterCreationData("Session connected count", "Connected Session count", PerformanceCounterType.NumberOfItems32), new CounterCreationData("Sessions connected", "Sessions connected", PerformanceCounterType.RateOfCountsPerSecond32), new CounterCreationData("Sessions disconnected", "Sessions disconnected", PerformanceCounterType.RateOfCountsPerSecond32), new CounterCreationData("SqlDataStore instances", "SqlDataStore instances", PerformanceCounterType.NumberOfItems32), new CounterCreationData("SqlDataStore created", "SqlDataStore created", PerformanceCounterType.RateOfCountsPerSecond32), new CounterCreationData("SqlDataStore finalized", "SqlDataStore finalized", PerformanceCounterType.RateOfCountsPerSecond32) };
            dataArray[9] = new CounterCreationData("SqlDataStore requests handled", "SqlDataStore requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[10] = new CounterCreationData("SqlDataStore schema update requests handled", "SqlDataStore schema update requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[11] = new CounterCreationData("SqlDataStore select data requests handled", "SqlDataStore select data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[12] = new CounterCreationData("SqlDataStore modify data requests handled", "SqlDataStore modify data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[13] = new CounterCreationData("SqlDataStore select data queries handled", "SqlDataStore select data queries handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[14] = new CounterCreationData("SqlDataStore modify data statements handled", "SqlDataStore modify data statements handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[15] = new CounterCreationData("SqlDataStore queue length", "SqlDataStore queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x10] = new CounterCreationData("SqlDataStore select data queue length", "SqlDataStore select data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x11] = new CounterCreationData("SqlDataStore modify data queue length", "SqlDataStore modify data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x12] = new CounterCreationData("SqlDataStore schema update queue length", "SqlDataStore schema update queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x13] = new CounterCreationData("DataCacheNode cache hit", "DataCacheNode cache hit", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[20] = new CounterCreationData("DataCacheNode cache miss", "DataCacheNode cache miss", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x15] = new CounterCreationData("DataCacheNode cache passthrough", "DataCacheNode cache passthrough", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x16] = new CounterCreationData("DataCacheNode cached records count", "DataCacheNode cached records count", PerformanceCounterType.NumberOfItems32);
            dataArray[0x17] = new CounterCreationData("DataCacheNode cached records added", "DataCacheNode cached records added", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x18] = new CounterCreationData("DataCacheNode cached records removed", "DataCacheNode cached records removed", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x1f] = new CounterCreationData("MSSql2005CacheRoot dependency established", "MSSql2005CacheRoot dependency established", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x20] = new CounterCreationData("MSSql2005CacheRoot dependency triggered", "MSSql2005CacheRoot dependency triggered", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x1c] = new CounterCreationData("DataCacheRoot count", "DataCacheRoot count", PerformanceCounterType.NumberOfItems32);
            dataArray[0x1d] = new CounterCreationData("DataCacheRoot created", "DataCacheRoot created", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[30] = new CounterCreationData("DataCacheRoot finalized", "DataCacheRoot finalized", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x21] = new CounterCreationData("DataCacheRoot requests handled", "DataCacheRoot requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x22] = new CounterCreationData("DataCacheRoot schema update requests handled", "DataCacheRoot schema update requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x23] = new CounterCreationData("DataCacheRoot select data requests handled", "DataCacheRoot select data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x24] = new CounterCreationData("DataCacheRoot modify data requests handled", "DataCacheRoot modify data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x2d] = new CounterCreationData("DataCacheRoot process cookie requests handled", "DataCacheRoot process cookie requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x2e] = new CounterCreationData("DataCacheRoot notify dirty tables requests handled", "DataCacheRoot notify dirty tables requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x25] = new CounterCreationData("DataCacheRoot select data queries handled", "DataCacheRoot select data queries handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x26] = new CounterCreationData("DataCacheRoot modify data statements handled", "DataCacheRoot modify data statements handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x2f] = new CounterCreationData("DataCacheRoot notify dirty tables table names handled", "DataCacheRoot notify dirty tables table names handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x27] = new CounterCreationData("DataCacheRoot queue length", "DataCacheRoot queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[40] = new CounterCreationData("DataCacheRoot select data queue length", "DataCacheRoot select data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x29] = new CounterCreationData("DataCacheRoot modify data queue length", "DataCacheRoot modify data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x2a] = new CounterCreationData("DataCacheRoot schema update queue length", "DataCacheRoot schema update queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x2b] = new CounterCreationData("DataCacheRoot process cookie queue length", "DataCacheRoot process cookie queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x2c] = new CounterCreationData("DataCacheRoot notify dirty tables queue length", "DataCacheRoot notify dirty tables queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x19] = new CounterCreationData("DataCacheNode count", "DataCacheNode count", PerformanceCounterType.NumberOfItems32);
            dataArray[0x1a] = new CounterCreationData("DataCacheNode created", "DataCacheNode created", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x1b] = new CounterCreationData("DataCacheNode finalized", "DataCacheNode finalized", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x30] = new CounterCreationData("DataCacheNode requests handled", "DataCacheNode requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x31] = new CounterCreationData("DataCacheNode schema update requests handled", "DataCacheNode schema update requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[50] = new CounterCreationData("DataCacheNode select data requests handled", "DataCacheNode select data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x33] = new CounterCreationData("DataCacheNode modify data requests handled", "DataCacheNode modify data requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[60] = new CounterCreationData("DataCacheNode process cookie requests handled", "DataCacheNode process cookie requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x3d] = new CounterCreationData("DataCacheNode notify dirty tables requests handled", "DataCacheNode notify dirty tables requests handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x34] = new CounterCreationData("DataCacheNode select data queries handled", "DataCacheNode select data queries handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x35] = new CounterCreationData("DataCacheNode modify data statements handled", "DataCacheNode modify data statements handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x3e] = new CounterCreationData("DataCacheNode notify dirty tables table names handled", "DataCacheNode notify dirty tables table names handled", PerformanceCounterType.RateOfCountsPerSecond32);
            dataArray[0x36] = new CounterCreationData("DataCacheNode queue length", "DataCacheNode queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x37] = new CounterCreationData("DataCacheNode select data queue length", "DataCacheNode select data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x38] = new CounterCreationData("DataCacheNode modify data queue length", "DataCacheNode modify data queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x39] = new CounterCreationData("DataCacheNode schema update queue length", "DataCacheNode schema update queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x3a] = new CounterCreationData("DataCacheNode process cookie queue length", "DataCacheNode process cookie queue length", PerformanceCounterType.NumberOfItems32);
            dataArray[0x3b] = new CounterCreationData("DataCacheNode notify dirty tables queue length", "DataCacheNode notify dirty tables queue length", PerformanceCounterType.NumberOfItems32);
            CountersData = new CounterCreationDataCollection(dataArray);
            counters = new Counter[CountersData.Count];
            for (int i = 0; i < counters.Length; i++)
            {
                counters[i] = Counter.Empty;
            }
            InitIfNeeded();
        }

        private static void Clear()
        {
            for (int i = 0; i < counters.Length; i++)
            {
                counters[i].Dispose();
            }
        }

        private static Counter CreateCounter(CounterCreationData counter, string category, string instanceName)
        {
            if ((PlatformID.Win32NT == Environment.OSVersion.Platform) && (Environment.OSVersion.Version.Major >= 5))
            {
                try
                {
                    return new Counter(category, instanceName, counter.CounterName, counter.CounterType);
                }
                catch (InvalidOperationException)
                {
                }
            }
            return Counter.Empty;
        }

        private static void CreateCounters(string category, string instanceName)
        {
            Counter[] counterArray = new Counter[CountersData.Count];
            for (int i = 0; i < counterArray.Length; i++)
            {
                counterArray[i] = CreateCounter(CountersData[i], category, instanceName);
            }
            counters = counterArray;
        }

        private static void DomainUnloadEventHandler(object sender, EventArgs e)
        {
            Clear();
        }

        private static void EnusreCategory(string category)
        {
            if (PerformanceCounterCategory.Exists(category))
            {
                bool flag = true;
                foreach (CounterCreationData data in CountersData)
                {
                    if (!PerformanceCounterCategory.CounterExists(data.CounterName, category))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return;
                }
                PerformanceCounterCategory.Delete(category);
            }
            PerformanceCounterCategory.Create(category, "Xpo", PerformanceCounterCategoryType.MultiInstance, CountersData);
        }

        private static Counter GetCounter(CounterIndexes index) => 
            counters[(int) index];

        private static string GetInstanceName()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string friendlyName = null;
            if (entryAssembly != null)
            {
                AssemblyName name = entryAssembly.GetName();
                if (name != null)
                {
                    friendlyName = name.Name;
                }
            }
            if (string.IsNullOrEmpty(friendlyName))
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;
                if (currentDomain != null)
                {
                    friendlyName = currentDomain.FriendlyName;
                }
            }
            if (friendlyName == null)
            {
                return null;
            }
            char[] chArray = friendlyName.ToCharArray();
            int index = 0;
            while (true)
            {
                while (true)
                {
                    if (index >= chArray.Length)
                    {
                        return (new string(chArray) + "[" + Process.GetCurrentProcess().Id.ToString() + "]");
                    }
                    char ch = chArray[index];
                    if (ch <= '(')
                    {
                        if (ch != '#')
                        {
                            if (ch == '(')
                            {
                                chArray[index] = '[';
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (ch == ')')
                        {
                            chArray[index] = ']';
                            break;
                        }
                        if ((ch != '/') && (ch != '\\'))
                        {
                            break;
                        }
                    }
                    chArray[index] = '_';
                    break;
                }
                index++;
            }
        }

        public static void Init()
        {
            object syncRoot = PerformanceCounters.syncRoot;
            lock (syncRoot)
            {
                if (!inited)
                {
                    inited = true;
                }
                else
                {
                    return;
                }
            }
            EnusreCategory("DevExpress Xpo");
            CreateCounters("DevExpress Xpo", GetInstanceName());
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(PerformanceCounters.DomainUnloadEventHandler);
        }

        private static void InitIfNeeded()
        {
            if (new BooleanSwitch("XpoPerfomanceCounters", "").Enabled)
            {
                Init();
            }
        }

        public static Counter ObjectsInCache =>
            GetCounter(CounterIndexes.ObjectsInCache);

        public static Counter ObjectsInCacheAdded =>
            GetCounter(CounterIndexes.ObjectsInCacheAdded);

        public static Counter ObjectsInCacheRemoved =>
            GetCounter(CounterIndexes.ObjectsInCacheRemoved);

        public static Counter SessionCount =>
            GetCounter(CounterIndexes.SessionCount);

        public static Counter SessionConnected =>
            GetCounter(CounterIndexes.SessionConnected);

        public static Counter SessionDisconnected =>
            GetCounter(CounterIndexes.SessionDisconnected);

        public static Counter SqlDataStoreCount =>
            GetCounter(CounterIndexes.SqlDataStoreCount);

        public static Counter SqlDataStoreCreated =>
            GetCounter(CounterIndexes.SqlDataStoreCreated);

        public static Counter SqlDataStoreFinalized =>
            GetCounter(CounterIndexes.SqlDataStoreFinalized);

        public static Counter SqlDataStoreTotalRequests =>
            GetCounter(CounterIndexes.SqlDataStoreTotalRequests);

        public static Counter SqlDataStoreSchemaUpdateRequests =>
            GetCounter(CounterIndexes.SqlDataStoreSchemaUpdateRequests);

        public static Counter SqlDataStoreSelectRequests =>
            GetCounter(CounterIndexes.SqlDataStoreSelectRequests);

        public static Counter SqlDataStoreModifyRequests =>
            GetCounter(CounterIndexes.SqlDataStoreModifyRequests);

        public static Counter SqlDataStoreSelectQueries =>
            GetCounter(CounterIndexes.SqlDataStoreSelectQueries);

        public static Counter SqlDataStoreModifyStatements =>
            GetCounter(CounterIndexes.SqlDataStoreModifyStatements);

        public static Counter SqlDataStoreTotalQueue =>
            GetCounter(CounterIndexes.SqlDataStoreTotalQueue);

        public static Counter SqlDataStoreSelectQueue =>
            GetCounter(CounterIndexes.SqlDataStoreSelectQueue);

        public static Counter SqlDataStoreModifyQueue =>
            GetCounter(CounterIndexes.SqlDataStoreModifyQueue);

        public static Counter SqlDataStoreSchemaUpdateQueue =>
            GetCounter(CounterIndexes.SqlDataStoreSchemaUpdateQueue);

        public static Counter DataCacheNodeCacheHit =>
            GetCounter(CounterIndexes.DataCacheNodeCacheHit);

        public static Counter DataCacheNodeCacheMiss =>
            GetCounter(CounterIndexes.DataCacheNodeCacheMiss);

        public static Counter DataCacheNodeCachePassthrough =>
            GetCounter(CounterIndexes.DataCacheNodeCachePassthrough);

        public static Counter DataCacheNodeCachedCount =>
            GetCounter(CounterIndexes.DataCacheNodeCachedCount);

        public static Counter DataCacheNodeCachedAdded =>
            GetCounter(CounterIndexes.DataCacheNodeCachedAdded);

        public static Counter DataCacheNodeCachedRemoved =>
            GetCounter(CounterIndexes.DataCacheNodeCachedRemoved);

        public static Counter DataCacheNodeCount =>
            GetCounter(CounterIndexes.DataCacheNodeCount);

        public static Counter DataCacheNodeCreated =>
            GetCounter(CounterIndexes.DataCacheNodeCreated);

        public static Counter DataCacheNodeFinalized =>
            GetCounter(CounterIndexes.DataCacheNodeFinalized);

        public static Counter DataCacheRootCount =>
            GetCounter(CounterIndexes.DataCacheRootCount);

        public static Counter DataCacheRootCreated =>
            GetCounter(CounterIndexes.DataCacheRootCreated);

        public static Counter DataCacheRootFinalized =>
            GetCounter(CounterIndexes.DataCacheRootFinalized);

        public static Counter MSSql2005CacheRootDependencyEstablished =>
            GetCounter(CounterIndexes.MSSql2005CacheRootDependencyEstablished);

        public static Counter MSSql2005CacheRootDependencyTriggered =>
            GetCounter(CounterIndexes.MSSql2005CacheRootDependencyTriggered);

        public static Counter DataCacheRootTotalRequests =>
            GetCounter(CounterIndexes.DataCacheRootTotalRequests);

        public static Counter DataCacheRootSchemaUpdateRequests =>
            GetCounter(CounterIndexes.DataCacheRootSchemaUpdateRequests);

        public static Counter DataCacheRootSelectRequests =>
            GetCounter(CounterIndexes.DataCacheRootSelectRequests);

        public static Counter DataCacheRootModifyRequests =>
            GetCounter(CounterIndexes.DataCacheRootModifyRequests);

        public static Counter DataCacheRootSelectQueries =>
            GetCounter(CounterIndexes.DataCacheRootSelectQueries);

        public static Counter DataCacheRootModifyStatements =>
            GetCounter(CounterIndexes.DataCacheRootModifyStatements);

        public static Counter DataCacheRootTotalQueue =>
            GetCounter(CounterIndexes.DataCacheRootTotalQueue);

        public static Counter DataCacheRootSelectQueue =>
            GetCounter(CounterIndexes.DataCacheRootSelectQueue);

        public static Counter DataCacheRootModifyQueue =>
            GetCounter(CounterIndexes.DataCacheRootModifyQueue);

        public static Counter DataCacheRootSchemaUpdateQueue =>
            GetCounter(CounterIndexes.DataCacheRootSchemaUpdateQueue);

        public static Counter DataCacheRootProcessCookieQueue =>
            GetCounter(CounterIndexes.DataCacheRootProcessCookieQueue);

        public static Counter DataCacheRootNotifyDirtyTablesQueue =>
            GetCounter(CounterIndexes.DataCacheRootNotifyDirtyTablesQueue);

        public static Counter DataCacheRootProcessCookieRequests =>
            GetCounter(CounterIndexes.DataCacheRootProcessCookieRequests);

        public static Counter DataCacheRootNotifyDirtyTablesRequests =>
            GetCounter(CounterIndexes.DataCacheRootNotifyDirtyTablesRequests);

        public static Counter DataCacheRootNotifyDirtyTablesTables =>
            GetCounter(CounterIndexes.DataCacheRootNotifyDirtyTablesTables);

        public static Counter DataCacheNodeTotalRequests =>
            GetCounter(CounterIndexes.DataCacheNodeTotalRequests);

        public static Counter DataCacheNodeSchemaUpdateRequests =>
            GetCounter(CounterIndexes.DataCacheNodeSchemaUpdateRequests);

        public static Counter DataCacheNodeSelectRequests =>
            GetCounter(CounterIndexes.DataCacheNodeSelectRequests);

        public static Counter DataCacheNodeModifyRequests =>
            GetCounter(CounterIndexes.DataCacheNodeModifyRequests);

        public static Counter DataCacheNodeSelectQueries =>
            GetCounter(CounterIndexes.DataCacheNodeSelectQueries);

        public static Counter DataCacheNodeModifyStatements =>
            GetCounter(CounterIndexes.DataCacheNodeModifyStatements);

        public static Counter DataCacheNodeTotalQueue =>
            GetCounter(CounterIndexes.DataCacheNodeTotalQueue);

        public static Counter DataCacheNodeSelectQueue =>
            GetCounter(CounterIndexes.DataCacheNodeSelectQueue);

        public static Counter DataCacheNodeModifyQueue =>
            GetCounter(CounterIndexes.DataCacheNodeModifyQueue);

        public static Counter DataCacheNodeSchemaUpdateQueue =>
            GetCounter(CounterIndexes.DataCacheNodeSchemaUpdateQueue);

        public static Counter DataCacheNodeProcessCookieQueue =>
            GetCounter(CounterIndexes.DataCacheNodeProcessCookieQueue);

        public static Counter DataCacheNodeNotifyDirtyTablesQueue =>
            GetCounter(CounterIndexes.DataCacheNodeNotifyDirtyTablesQueue);

        public static Counter DataCacheNodeProcessCookieRequests =>
            GetCounter(CounterIndexes.DataCacheNodeProcessCookieRequests);

        public static Counter DataCacheNodeNotifyDirtyTablesRequests =>
            GetCounter(CounterIndexes.DataCacheNodeNotifyDirtyTablesRequests);

        public static Counter DataCacheNodeNotifyDirtyTablesTables =>
            GetCounter(CounterIndexes.DataCacheNodeNotifyDirtyTablesTables);

        public class Counter
        {
            public static readonly PerformanceCounters.Counter Empty = new PerformanceCounters.Counter();
            private PerformanceCounter counter;

            public Counter()
            {
            }

            public Counter(string categoryName, string instanceName, string counterName, PerformanceCounterType counterType)
            {
                this.counter = new PerformanceCounter();
                this.counter.CategoryName = categoryName;
                this.counter.CounterName = counterName;
                this.counter.InstanceName = instanceName;
                this.counter.ReadOnly = false;
                this.counter.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
                this.counter.RawValue = 0L;
            }

            public void Decrement()
            {
                this.Increment(-1);
            }

            public void Decrement(int count)
            {
                this.Increment(-count);
            }

            public void Dispose()
            {
                if (this.counter != null)
                {
                    this.counter.RemoveInstance();
                }
            }

            public void Increment()
            {
                if (this.counter != null)
                {
                    this.counter.Increment();
                }
            }

            public void Increment(int count)
            {
                if (this.counter != null)
                {
                    this.counter.IncrementBy((long) count);
                }
            }
        }

        public enum CounterIndexes
        {
            ObjectsInCache,
            ObjectsInCacheAdded,
            ObjectsInCacheRemoved,
            SessionCount,
            SessionConnected,
            SessionDisconnected,
            SqlDataStoreCount,
            SqlDataStoreCreated,
            SqlDataStoreFinalized,
            SqlDataStoreTotalRequests,
            SqlDataStoreSchemaUpdateRequests,
            SqlDataStoreSelectRequests,
            SqlDataStoreModifyRequests,
            SqlDataStoreSelectQueries,
            SqlDataStoreModifyStatements,
            SqlDataStoreTotalQueue,
            SqlDataStoreSelectQueue,
            SqlDataStoreModifyQueue,
            SqlDataStoreSchemaUpdateQueue,
            DataCacheNodeCacheHit,
            DataCacheNodeCacheMiss,
            DataCacheNodeCachePassthrough,
            DataCacheNodeCachedCount,
            DataCacheNodeCachedAdded,
            DataCacheNodeCachedRemoved,
            DataCacheNodeCount,
            DataCacheNodeCreated,
            DataCacheNodeFinalized,
            DataCacheRootCount,
            DataCacheRootCreated,
            DataCacheRootFinalized,
            MSSql2005CacheRootDependencyEstablished,
            MSSql2005CacheRootDependencyTriggered,
            DataCacheRootTotalRequests,
            DataCacheRootSchemaUpdateRequests,
            DataCacheRootSelectRequests,
            DataCacheRootModifyRequests,
            DataCacheRootSelectQueries,
            DataCacheRootModifyStatements,
            DataCacheRootTotalQueue,
            DataCacheRootSelectQueue,
            DataCacheRootModifyQueue,
            DataCacheRootSchemaUpdateQueue,
            DataCacheRootProcessCookieQueue,
            DataCacheRootNotifyDirtyTablesQueue,
            DataCacheRootProcessCookieRequests,
            DataCacheRootNotifyDirtyTablesRequests,
            DataCacheRootNotifyDirtyTablesTables,
            DataCacheNodeTotalRequests,
            DataCacheNodeSchemaUpdateRequests,
            DataCacheNodeSelectRequests,
            DataCacheNodeModifyRequests,
            DataCacheNodeSelectQueries,
            DataCacheNodeModifyStatements,
            DataCacheNodeTotalQueue,
            DataCacheNodeSelectQueue,
            DataCacheNodeModifyQueue,
            DataCacheNodeSchemaUpdateQueue,
            DataCacheNodeProcessCookieQueue,
            DataCacheNodeNotifyDirtyTablesQueue,
            DataCacheNodeProcessCookieRequests,
            DataCacheNodeNotifyDirtyTablesRequests,
            DataCacheNodeNotifyDirtyTablesTables
        }

        public class QueueLengthCounter : IDisposable
        {
            private readonly PerformanceCounters.Counter CounterTotal;
            private readonly PerformanceCounters.Counter CounterExact;
            private readonly PerformanceCounters.Counter CounterTotalQueue;
            private readonly PerformanceCounters.Counter CounterExactQueue;

            public QueueLengthCounter(PerformanceCounters.Counter cntTotal, PerformanceCounters.Counter cntTotalQueue, PerformanceCounters.Counter cntExact, PerformanceCounters.Counter cntExactQueue)
            {
                this.CounterTotal = cntTotal;
                this.CounterTotal.Increment();
                this.CounterExact = cntExact;
                this.CounterExact.Increment();
                this.CounterTotalQueue = cntTotalQueue;
                this.CounterTotalQueue.Increment();
                this.CounterExactQueue = cntExactQueue;
                this.CounterExactQueue.Increment();
            }

            public void Dispose()
            {
                this.CounterTotalQueue.Decrement();
                this.CounterExactQueue.Decrement();
            }
        }
    }
}

