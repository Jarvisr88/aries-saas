namespace DevExpress.Xpo.DB
{
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DataStoreForkBase : MarshalByRefObject, IDataStore, IDataStoreAsync, ICommandChannel, ICommandChannelAsync
    {
        protected DataStoreForkBase()
        {
        }

        public abstract IDataStore AcquireChangeProvider();
        public virtual Task<IDataStore> AcquireChangeProviderAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public abstract IDataStore AcquireReadProvider();
        public virtual Task<IDataStore> AcquireReadProviderAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public virtual IDataStore AcquireUpdateSchemaProvider() => 
            this.AcquireChangeProvider();

        object ICommandChannel.Do(string command, object args)
        {
            object obj2;
            IDataStore provider = this.AcquireChangeProvider();
            try
            {
                ICommandChannel channel = provider as ICommandChannel;
                if (channel == null)
                {
                    if (provider == null)
                    {
                        throw new NotSupportedException($"Command '{command}' is not supported.");
                    }
                    throw new NotSupportedException($"Command '{command}' is not supported by {provider.GetType().FullName}.");
                }
                obj2 = channel.Do(command, args);
            }
            finally
            {
                this.ReleaseChangeProvider(provider);
            }
            return obj2;
        }

        [AsyncStateMachine(typeof(<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16))]
        Task<object> ICommandChannelAsync.DoAsync(string command, object args, CancellationToken cancellationToken)
        {
            <DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16 d__;
            d__.<>4__this = this;
            d__.command = command;
            d__.args = args;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16>(ref d__);
            return d__.<>t__builder.Task;
        }

        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {
            ModificationResult result;
            IDataStore provider = this.AcquireChangeProvider();
            try
            {
                result = provider.ModifyData(dmlStatements);
            }
            finally
            {
                this.ReleaseChangeProvider(provider);
            }
            return result;
        }

        [AsyncStateMachine(typeof(<ModifyDataAsync>d__15))]
        public Task<ModificationResult> ModifyDataAsync(CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            <ModifyDataAsync>d__15 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.dmlStatements = dmlStatements;
            d__.<>t__builder = AsyncTaskMethodBuilder<ModificationResult>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ModifyDataAsync>d__15>(ref d__);
            return d__.<>t__builder.Task;
        }

        public abstract void ReleaseChangeProvider(IDataStore provider);
        public abstract void ReleaseReadProvider(IDataStore provider);
        public virtual void ReleaseUpdateSchemaProvider(IDataStore provider)
        {
            this.ReleaseChangeProvider(provider);
        }

        public SelectedData SelectData(params SelectStatement[] selects)
        {
            SelectedData data;
            IDataStore provider = this.AcquireReadProvider();
            try
            {
                data = provider.SelectData(selects);
            }
            finally
            {
                this.ReleaseReadProvider(provider);
            }
            return data;
        }

        [AsyncStateMachine(typeof(<SelectDataAsync>d__14))]
        public Task<SelectedData> SelectDataAsync(CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            <SelectDataAsync>d__14 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.selects = selects;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SelectDataAsync>d__14>(ref d__);
            return d__.<>t__builder.Task;
        }

        public UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            UpdateSchemaResult result;
            IDataStore provider = this.AcquireUpdateSchemaProvider();
            try
            {
                result = provider.UpdateSchema(doNotCreateIfFirstTableNotExist, tables);
            }
            finally
            {
                this.ReleaseUpdateSchemaProvider(provider);
            }
            return result;
        }

        public abstract DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption { get; }

        [CompilerGenerated]
        private struct <DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<object> <>t__builder;
            public DataStoreForkBase <>4__this;
            public CancellationToken cancellationToken;
            public string command;
            public object args;
            private IDataStore <provider>5__1;
            private ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter awaiter;
                    IDataStore store2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_0012;
                        }
                        else
                        {
                            awaiter = this.<>4__this.AcquireChangeProviderAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0013;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter, DataStoreForkBase.<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_0013;
                TR_0012:
                    try
                    {
                        ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
                        object obj4;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_000B;
                        }
                        else
                        {
                            ICommandChannelAsync async = this.<provider>5__1 as ICommandChannelAsync;
                            if (async == null)
                            {
                                if (this.<provider>5__1 == null)
                                {
                                    throw new NotSupportedException($"Command '{this.command}' is not supported.");
                                }
                                object[] args = new object[] { async.GetType().FullName };
                                throw new InvalidOperationException(DbRes.GetString("Async_CommandChannelDoesNotImplementICommandChannelAsync", args));
                            }
                            awaiter2 = async.DoAsync(this.command, this.args, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_000B;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, DataStoreForkBase.<DevExpress-Xpo-Helpers-ICommandChannelAsync-DoAsync>d__16>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_000B:
                        obj4 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter();
                        object result = obj4;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>4__this.ReleaseChangeProvider(this.<provider>5__1);
                        }
                    }
                    return;
                TR_0013:
                    store2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                    IDataStore store = store2;
                    this.<provider>5__1 = store;
                    goto TR_0012;
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

        [CompilerGenerated]
        private struct <ModifyDataAsync>d__15 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ModificationResult> <>t__builder;
            public DataStoreForkBase <>4__this;
            public CancellationToken cancellationToken;
            public ModificationStatement[] dmlStatements;
            private IDataStore <provider>5__1;
            private ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter awaiter;
                    IDataStore store2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_0010;
                        }
                        else
                        {
                            awaiter = this.<>4__this.AcquireChangeProviderAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0011;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter, DataStoreForkBase.<ModifyDataAsync>d__15>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_0011;
                TR_0010:
                    try
                    {
                        ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter awaiter2;
                        ModificationResult result3;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0009;
                        }
                        else
                        {
                            IDataStoreAsync async = this.<provider>5__1 as IDataStoreAsync;
                            if (async == null)
                            {
                                object[] objArray1 = new object[] { (this.<provider>5__1 != null) ? this.<provider>5__1.GetType().FullName : "" };
                                object[] args = new object[] { (this.<provider>5__1 != null) ? this.<provider>5__1.GetType().FullName : "" };
                                throw new InvalidOperationException(DbRes.GetString("Async_ConnectionProviderDoesNotImplementIDataStoreAsync", args));
                            }
                            awaiter2 = async.ModifyDataAsync(this.cancellationToken, this.dmlStatements).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0009;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter, DataStoreForkBase.<ModifyDataAsync>d__15>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_0009:
                        result3 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                        ModificationResult result = result3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>4__this.ReleaseChangeProvider(this.<provider>5__1);
                        }
                    }
                    return;
                TR_0011:
                    store2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                    IDataStore store = store2;
                    this.<provider>5__1 = store;
                    goto TR_0010;
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

        [CompilerGenerated]
        private struct <SelectDataAsync>d__14 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public DataStoreForkBase <>4__this;
            public CancellationToken cancellationToken;
            public SelectStatement[] selects;
            private IDataStore <provider>5__1;
            private ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter <>u__1;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter awaiter;
                    IDataStore store2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_0010;
                        }
                        else
                        {
                            awaiter = this.<>4__this.AcquireReadProviderAsync(this.cancellationToken).ConfigureAwait(false).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0011;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter, DataStoreForkBase.<SelectDataAsync>d__14>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_0011;
                TR_0010:
                    try
                    {
                        ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter2;
                        SelectedData data3;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0009;
                        }
                        else
                        {
                            IDataStoreAsync async = this.<provider>5__1 as IDataStoreAsync;
                            if (async == null)
                            {
                                object[] objArray1 = new object[] { (this.<provider>5__1 != null) ? this.<provider>5__1.GetType().FullName : "" };
                                object[] args = new object[] { (this.<provider>5__1 != null) ? this.<provider>5__1.GetType().FullName : "" };
                                throw new InvalidOperationException(DbRes.GetString("Async_ConnectionProviderDoesNotImplementIDataStoreAsync", args));
                            }
                            awaiter2 = async.SelectDataAsync(this.cancellationToken, this.selects).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0009;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, DataStoreForkBase.<SelectDataAsync>d__14>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_0009:
                        data3 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                        SelectedData result = data3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if (num < 0)
                        {
                            this.<>4__this.ReleaseReadProvider(this.<provider>5__1);
                        }
                    }
                    return;
                TR_0011:
                    store2 = awaiter.GetResult();
                    awaiter = new ConfiguredTaskAwaitable<IDataStore>.ConfiguredTaskAwaiter();
                    IDataStore store = store2;
                    this.<provider>5__1 = store;
                    goto TR_0010;
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

