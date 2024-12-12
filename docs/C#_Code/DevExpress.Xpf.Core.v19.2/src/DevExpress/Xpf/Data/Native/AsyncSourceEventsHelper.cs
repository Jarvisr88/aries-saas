namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AsyncSourceEventsHelper : VirtualSourceEventsHelper
    {
        public AsyncSourceEventsHelper(IAsyncSourceEventsHelperClient client) : base((VirtualSourceBase) client)
        {
        }

        protected override AsyncWorkerBase CreateWorker() => 
            new AsyncParallelWorker(new Action(base.client.RaiseIsDataLoadingChanged));

        protected override PropertyDescriptorCollection GetItemPropertiesCore() => 
            new PropertyDescriptorCollection(base.client.GetSourceProperties(), true);

        public override Task<object[]> GetSummariesAsync(VirtualSourceEventsHelper.GetTotalSummariesState state, CancellationToken cancellationToken)
        {
            GetSummariesAsyncEventArgs e = new GetSummariesAsyncEventArgs(cancellationToken, state.Summaries, state.Filter);
            this.client.GetTotalSummariesHandler()(this.client, e);
            return e.Result;
        }

        [AsyncStateMachine(typeof(<GetUniqueValuesAsync>d__7))]
        public override Task<Either<object[], ValueAndCount[]>> GetUniqueValuesAsync(VirtualSourceEventsHelper.GetUniqueValuesState state, CancellationToken cancellationToken)
        {
            <GetUniqueValuesAsync>d__7 d__;
            d__.<>4__this = this;
            d__.state = state;
            d__.cancellationToken = cancellationToken;
            d__.<>t__builder = AsyncTaskMethodBuilder<Either<object[], ValueAndCount[]>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetUniqueValuesAsync>d__7>(ref d__);
            return d__.<>t__builder.Task;
        }

        public override void RequestSourceIfNeeded()
        {
        }

        public override Task<UpdateRowResult> UpdateRowAsync(VirtualSourceEventsHelper.UpdateRowState state, CancellationToken cancellationToken)
        {
            UpdateRowAsyncEventArgs e = new UpdateRowAsyncEventArgs(cancellationToken, state.Row);
            this.client.GetUpdateRowHandler()(this.client, e);
            return e.Result;
        }

        private IAsyncSourceEventsHelperClient client =>
            (IAsyncSourceEventsHelperClient) base.client;

        [CompilerGenerated]
        private struct <GetUniqueValuesAsync>d__7 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<Either<object[], ValueAndCount[]>> <>t__builder;
            public CancellationToken cancellationToken;
            public VirtualSourceEventsHelper.GetUniqueValuesState state;
            public AsyncSourceEventsHelper <>4__this;
            private GetUniqueValuesAsyncEventArgs <getUniqueValuesArgs>5__1;
            private TaskAwaiter<ValueAndCount[]> <>u__1;
            private TaskAwaiter<object[]> <>u__2;

            private void MoveNext()
            {
                Either<object[], ValueAndCount[]> either;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<ValueAndCount[]> awaiter;
                    ValueAndCount[] countArray2;
                    TaskAwaiter<object[]> awaiter2;
                    object[] objArray2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<ValueAndCount[]>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new TaskAwaiter<object[]>();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            this.<getUniqueValuesArgs>5__1 = new GetUniqueValuesAsyncEventArgs(this.cancellationToken, this.state.PropertyName, this.state.Filter);
                            this.<>4__this.client.GetUniqueValuesHandler()(this.<>4__this.client, this.<getUniqueValuesArgs>5__1);
                            if (this.<getUniqueValuesArgs>5__1.ResultWithCounts == null)
                            {
                                if (this.<getUniqueValuesArgs>5__1.Result == null)
                                {
                                    either = new object[0];
                                    goto TR_0003;
                                }
                                else
                                {
                                    awaiter2 = this.<getUniqueValuesArgs>5__1.Result.GetAwaiter();
                                    if (awaiter2.IsCompleted)
                                    {
                                        goto TR_0008;
                                    }
                                    else
                                    {
                                        this.<>1__state = num = 1;
                                        this.<>u__2 = awaiter2;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<object[]>, AsyncSourceEventsHelper.<GetUniqueValuesAsync>d__7>(ref awaiter2, ref this);
                                    }
                                }
                            }
                            else
                            {
                                awaiter = this.<getUniqueValuesArgs>5__1.ResultWithCounts.GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0004;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueAndCount[]>, AsyncSourceEventsHelper.<GetUniqueValuesAsync>d__7>(ref awaiter, ref this);
                                }
                            }
                            return;
                        }
                        goto TR_0008;
                    }
                    return;
                TR_0004:
                    countArray2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<ValueAndCount[]>();
                    either = countArray2;
                    goto TR_0003;
                TR_0008:
                    objArray2 = awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter<object[]>();
                    either = objArray2;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0003:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(either);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

