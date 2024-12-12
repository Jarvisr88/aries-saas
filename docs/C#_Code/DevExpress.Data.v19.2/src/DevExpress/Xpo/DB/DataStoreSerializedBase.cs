namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Helpers;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DataStoreSerializedBase : MarshalByRefObject, IDataStore, IDataStoreAsync
    {
        private readonly AsyncLockHelper lockHelper = new AsyncLockHelper();

        protected DataStoreSerializedBase()
        {
        }

        public virtual ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {
            using (this.LockHelper.Lock())
            {
                return this.ProcessModifyData(dmlStatements);
            }
        }

        [AsyncStateMachine(typeof(<ModifyDataAsync>d__13))]
        public virtual System.Threading.Tasks.Task<ModificationResult> ModifyDataAsync(CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            <ModifyDataAsync>d__13 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.dmlStatements = dmlStatements;
            d__.<>t__builder = AsyncTaskMethodBuilder<ModificationResult>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ModifyDataAsync>d__13>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected abstract ModificationResult ProcessModifyData(params ModificationStatement[] dmlStatements);
        protected virtual System.Threading.Tasks.Task<ModificationResult> ProcessModifyDataAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, params ModificationStatement[] dmlStatements)
        {
            throw new NotImplementedException();
        }

        protected abstract SelectedData ProcessSelectData(params SelectStatement[] selects);
        protected virtual System.Threading.Tasks.Task<SelectedData> ProcessSelectDataAsync(AsyncOperationIdentifier asyncOperationId, CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            throw new NotImplementedException();
        }

        protected abstract UpdateSchemaResult ProcessUpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables);
        public virtual SelectedData SelectData(params SelectStatement[] selects)
        {
            using (this.LockHelper.Lock())
            {
                return this.ProcessSelectData(selects);
            }
        }

        [AsyncStateMachine(typeof(<SelectDataAsync>d__12))]
        public virtual System.Threading.Tasks.Task<SelectedData> SelectDataAsync(CancellationToken cancellationToken, params SelectStatement[] selects)
        {
            <SelectDataAsync>d__12 d__;
            d__.<>4__this = this;
            d__.cancellationToken = cancellationToken;
            d__.selects = selects;
            d__.<>t__builder = AsyncTaskMethodBuilder<SelectedData>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<SelectDataAsync>d__12>(ref d__);
            return d__.<>t__builder.Task;
        }

        public virtual UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            using (this.LockHelper.Lock())
            {
                return this.ProcessUpdateSchema(doNotCreateIfFirstTableNotExist, tables);
            }
        }

        [Obsolete("SyncRoot is obsolette, use LockHelper.Lock() or LockHelper.LockAsync() instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public abstract object SyncRoot { get; }

        protected virtual AsyncLockHelper LockHelper =>
            this.lockHelper;

        [Description(""), Browsable(false)]
        public abstract DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption { get; }

        [CompilerGenerated]
        private struct <ModifyDataAsync>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ModificationResult> <>t__builder;
            public DataStoreSerializedBase <>4__this;
            private AsyncOperationIdentifier <asyncOperationId>5__1;
            public CancellationToken cancellationToken;
            public ModificationStatement[] dmlStatements;
            private IDisposable <>7__wrap1;
            private TaskAwaiter<IDisposable> <>u__1;
            private ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IDisposable> awaiter;
                    IDisposable disposable2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<IDisposable>();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            this.<asyncOperationId>5__1 = AsyncOperationIdentifier.New();
                            awaiter = this.<>4__this.LockHelper.LockAsync(this.<asyncOperationId>5__1).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IDisposable>, DataStoreSerializedBase.<ModifyDataAsync>d__13>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_000E;
                TR_000D:
                    try
                    {
                        ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter awaiter2;
                        ModificationResult result3;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter2 = this.<>4__this.ProcessModifyDataAsync(this.<asyncOperationId>5__1, this.cancellationToken, this.dmlStatements).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter, DataStoreSerializedBase.<ModifyDataAsync>d__13>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_0008:
                        result3 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<ModificationResult>.ConfiguredTaskAwaiter();
                        ModificationResult result = result3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    return;
                TR_000E:
                    disposable2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IDisposable>();
                    IDisposable disposable = disposable2;
                    this.<>7__wrap1 = disposable;
                    goto TR_000D;
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
        private struct <SelectDataAsync>d__12 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<SelectedData> <>t__builder;
            public DataStoreSerializedBase <>4__this;
            private AsyncOperationIdentifier <asyncOperationId>5__1;
            public CancellationToken cancellationToken;
            public SelectStatement[] selects;
            private IDisposable <>7__wrap1;
            private TaskAwaiter<IDisposable> <>u__1;
            private ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IDisposable> awaiter;
                    IDisposable disposable2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<IDisposable>();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            goto TR_000D;
                        }
                        else
                        {
                            this.<asyncOperationId>5__1 = AsyncOperationIdentifier.New();
                            awaiter = this.<>4__this.LockHelper.LockAsync(this.<asyncOperationId>5__1).GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_000E;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IDisposable>, DataStoreSerializedBase.<SelectDataAsync>d__12>(ref awaiter, ref this);
                            }
                        }
                        return;
                    }
                    goto TR_000E;
                TR_000D:
                    try
                    {
                        ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter awaiter2;
                        SelectedData data3;
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                            this.<>1__state = num = -1;
                            goto TR_0008;
                        }
                        else
                        {
                            awaiter2 = this.<>4__this.ProcessSelectDataAsync(this.<asyncOperationId>5__1, this.cancellationToken, this.selects).ConfigureAwait(false).GetAwaiter();
                            if (awaiter2.IsCompleted)
                            {
                                goto TR_0008;
                            }
                            else
                            {
                                this.<>1__state = num = 1;
                                this.<>u__2 = awaiter2;
                                this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter, DataStoreSerializedBase.<SelectDataAsync>d__12>(ref awaiter2, ref this);
                            }
                        }
                        return;
                    TR_0008:
                        data3 = awaiter2.GetResult();
                        awaiter2 = new ConfiguredTaskAwaitable<SelectedData>.ConfiguredTaskAwaiter();
                        SelectedData result = data3;
                        this.<>1__state = -2;
                        this.<>t__builder.SetResult(result);
                        return;
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    return;
                TR_000E:
                    disposable2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IDisposable>();
                    IDisposable disposable = disposable2;
                    this.<>7__wrap1 = disposable;
                    goto TR_000D;
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

