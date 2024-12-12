namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Helpers;
    using DevExpress.Xpo.DB.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DataStoreBase : DataStoreSerializedBase, IDataStoreForTests, IDataStore
    {
        private DevExpress.Xpo.DB.AutoCreateOption _AutoCreateOption;
        private static IDictionary providersCreationByString = new Dictionary<string, object>();
        private static IDictionary providersCreationByConnection = new Dictionary<string, object>(StringExtensions.ComparerInvariantCultureIgnoreCase);
        private static Dictionary<string, ProviderFactory> providersFactory = new Dictionary<string, ProviderFactory>();
        public const string XpoProviderTypeParameterName = "XpoProvider";

        protected DataStoreBase(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            this._AutoCreateOption = autoCreateOption;
        }

        void IDataStoreForTests.ClearDatabase()
        {
            using (this.LockHelper.Lock())
            {
                this.ProcessClearDatabase();
            }
        }

        protected abstract void ProcessClearDatabase();
        protected sealed override SelectedData ProcessSelectData(params SelectStatement[] selects)
        {
            SelectStatementResult[] resultSet = new SelectStatementResult[selects.Length];
            for (int i = 0; i < selects.Length; i++)
            {
                resultSet[i] = this.ProcessSelectData(selects[i]);
            }
            return new SelectedData(resultSet);
        }

        protected abstract SelectStatementResult ProcessSelectData(SelectStatement select);
        [AsyncStateMachine(typeof(<ProcessSelectDataAsync>d__10))]
        protected sealed override System.Threading.Tasks.Task<SelectedData> ProcessSelectDataAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            <ProcessSelectDataAsync>d__10 d__;
            d__.<>4__this = this;
            d__.asyncOperationId = asyncOperationId;
            d__.cancellationToken = cancellationToken;
            d__.selects = selects;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ProcessSelectDataAsync>d__10>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected virtual System.Threading.Tasks.Task<SelectStatementResult> ProcessSelectDataAsync(SelectStatement select, AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public static IDataStore QueryDataStore(IDbConnection connection, DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            Type type = connection.GetType();
            DataStoreCreationFromConnectionDelegate delegate2 = (DataStoreCreationFromConnectionDelegate) providersCreationByConnection[type.FullName];
            delegate2 ??= ((DataStoreCreationFromConnectionDelegate) providersCreationByConnection[type.Name]);
            return delegate2?.Invoke(connection, autoCreateOption);
        }

        public static IDataStore QueryDataStore(string providerType, string connectionString, DevExpress.Xpo.DB.AutoCreateOption defaultAutoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            DataStoreCreationFromStringDelegate delegate2 = (DataStoreCreationFromStringDelegate) providersCreationByString[providerType];
            if (delegate2 != null)
            {
                return delegate2(connectionString, defaultAutoCreateOption, out objectsToDisposeOnDisconnect);
            }
            objectsToDisposeOnDisconnect = null;
            return null;
        }

        public static void RegisterDataStoreProvider(string connectionTypeShortName, DataStoreCreationFromConnectionDelegate createFromConnectionDelegate)
        {
            object syncRoot = providersCreationByConnection.SyncRoot;
            lock (syncRoot)
            {
                providersCreationByConnection[connectionTypeShortName] = createFromConnectionDelegate;
            }
        }

        public static void RegisterDataStoreProvider(string providerKey, DataStoreCreationFromStringDelegate createFromStringDelegate)
        {
            object syncRoot = providersCreationByString.SyncRoot;
            lock (syncRoot)
            {
                providersCreationByString[providerKey] = createFromStringDelegate;
            }
        }

        public static void RegisterFactory(ProviderFactory providerFactory)
        {
            Dictionary<string, ProviderFactory> providersFactory = DataStoreBase.providersFactory;
            lock (providersFactory)
            {
                DataStoreBase.providersFactory[providerFactory.ProviderKey] = providerFactory;
            }
        }

        public void UpdateSchema(params DBTable[] tables)
        {
            this.UpdateSchema(false, tables);
        }

        public override DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            this._AutoCreateOption;

        public static ProviderFactory[] Factories
        {
            get
            {
                ProviderFactory[] factoryArray = new ProviderFactory[providersFactory.Count];
                int index = 0;
                foreach (KeyValuePair<string, ProviderFactory> pair in providersFactory)
                {
                    factoryArray[index] = pair.Value;
                    index++;
                }
                return factoryArray;
            }
        }

        [CompilerGenerated]
        private struct <ProcessSelectDataAsync>d__10 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public SelectStatement[] selects;
            private SelectStatementResult[] <result>5__1;
            public DataStoreBase <>4__this;
            public AsyncOperationIdentifier asyncOperationId;
            public CancellationToken cancellationToken;
            private int <i>5__2;
            private SelectStatementResult[] <>7__wrap1;
            private int <>7__wrap2;
            private ConfiguredTaskAwaitable<SelectStatementResult>.ConfiguredTaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    SelectedData data;
                    ConfiguredTaskAwaitable<SelectStatementResult>.ConfiguredTaskAwaiter awaiter;
                    SelectStatementResult result2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<SelectStatementResult>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        this.<result>5__1 = new SelectStatementResult[this.selects.Length];
                        this.<i>5__2 = 0;
                        goto TR_0009;
                    }
                TR_0005:
                    result2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<SelectStatementResult>.ConfiguredTaskAwaiter();
                    this.<>7__wrap1[this.<>7__wrap2] = result2;
                    this.<>7__wrap1 = null;
                    int num2 = this.<i>5__2 + 1;
                    this.<i>5__2 = num2;
                TR_0009:
                    while (true)
                    {
                        if (this.<i>5__2 < this.selects.Length)
                        {
                            this.<>7__wrap1 = this.<result>5__1;
                            this.<>7__wrap2 = this.<i>5__2;
                            SelectStatementResult result1 = this.<>7__wrap1[this.<>7__wrap2];
                            awaiter = this.<>4__this.ProcessSelectDataAsync(this.selects[this.<i>5__2], this.asyncOperationId, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0005;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectStatementResult>.ConfiguredTaskAwaiter, DataStoreBase.<ProcessSelectDataAsync>d__10>(ref awaiter, ref this);
                            }
                            return;
                        }
                        else
                        {
                            data = new SelectedData(this.<result>5__1);
                        }
                        break;
                    }
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(data);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

