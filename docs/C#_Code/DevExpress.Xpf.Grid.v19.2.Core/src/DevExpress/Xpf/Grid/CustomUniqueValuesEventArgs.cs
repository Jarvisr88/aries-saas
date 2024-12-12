namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;

    public class CustomUniqueValuesEventArgs : RoutedEventArgs
    {
        public CustomUniqueValuesEventArgs(ColumnBase column, bool includeFilteredOut, bool roundDateTime, OperationCompleted asyncCompleted, CriteriaOperator filter = null)
        {
            this.<Column>k__BackingField = column;
            this.<IncludeFilteredOut>k__BackingField = includeFilteredOut;
            this.<RoundDateTime>k__BackingField = roundDateTime;
            this.<AsyncCompleted>k__BackingField = asyncCompleted;
            this.<Filter>k__BackingField = filter;
        }

        [AsyncStateMachine(typeof(<GetUniqueValuesAsync>d__34))]
        internal Task<Either<ValueAndCount[], object[]>> GetUniqueValuesAsync()
        {
            <GetUniqueValuesAsync>d__34 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder<Either<ValueAndCount[], object[]>>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetUniqueValuesAsync>d__34>(ref d__);
            return d__.<>t__builder.Task;
        }

        public DataControlBase Source =>
            this.Column.OwnerControl;

        public bool IncludeFilteredOut { get; }

        public bool RoundDateTime { get; }

        public ColumnBase Column { get; }

        public OperationCompleted AsyncCompleted { get; }

        public CriteriaOperator Filter { get; }

        public Task<ValueAndCount[]> UniqueValuesAndCountsAsync { get; set; }

        public Task<object[]> UniqueValuesAsync { get; set; }

        public ValueAndCount[] UniqueValuesAndCounts { get; set; }

        public object[] UniqueValues { get; set; }

        [CompilerGenerated]
        private struct <GetUniqueValuesAsync>d__34 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<Either<ValueAndCount[], object[]>> <>t__builder;
            public CustomUniqueValuesEventArgs <>4__this;
            private TaskAwaiter<object[]> <>u__1;
            private TaskAwaiter<ValueAndCount[]> <>u__2;

            private void MoveNext()
            {
                Either<ValueAndCount[], object[]> uniqueValuesAndCounts;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<object[]> awaiter;
                    object[] objArray2;
                    TaskAwaiter<ValueAndCount[]> awaiter2;
                    ValueAndCount[] countArray2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<object[]>();
                        this.<>1__state = num = -1;
                        goto TR_0006;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            awaiter2 = this.<>u__2;
                            this.<>u__2 = new TaskAwaiter<ValueAndCount[]>();
                            this.<>1__state = num = -1;
                        }
                        else
                        {
                            if (this.<>4__this.UniqueValues == null)
                            {
                                if (this.<>4__this.UniqueValuesAndCounts == null)
                                {
                                    if (this.<>4__this.UniqueValuesAsync == null)
                                    {
                                        if (this.<>4__this.UniqueValuesAndCountsAsync == null)
                                        {
                                            uniqueValuesAndCounts = null;
                                            goto TR_0002;
                                        }
                                        else
                                        {
                                            awaiter2 = this.<>4__this.UniqueValuesAndCountsAsync.GetAwaiter();
                                            if (awaiter2.IsCompleted)
                                            {
                                                goto TR_000A;
                                            }
                                            else
                                            {
                                                this.<>1__state = num = 1;
                                                this.<>u__2 = awaiter2;
                                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueAndCount[]>, CustomUniqueValuesEventArgs.<GetUniqueValuesAsync>d__34>(ref awaiter2, ref this);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        awaiter = this.<>4__this.UniqueValuesAsync.GetAwaiter();
                                        if (awaiter.IsCompleted)
                                        {
                                            goto TR_0006;
                                        }
                                        else
                                        {
                                            this.<>1__state = num = 0;
                                            this.<>u__1 = awaiter;
                                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<object[]>, CustomUniqueValuesEventArgs.<GetUniqueValuesAsync>d__34>(ref awaiter, ref this);
                                        }
                                    }
                                }
                                else
                                {
                                    uniqueValuesAndCounts = this.<>4__this.UniqueValuesAndCounts;
                                    goto TR_0002;
                                }
                            }
                            else
                            {
                                uniqueValuesAndCounts = this.<>4__this.UniqueValues;
                                goto TR_0002;
                            }
                            return;
                        }
                        goto TR_000A;
                    }
                    return;
                TR_0006:
                    objArray2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<object[]>();
                    uniqueValuesAndCounts = objArray2;
                    goto TR_0002;
                TR_000A:
                    countArray2 = awaiter2.GetResult();
                    awaiter2 = new TaskAwaiter<ValueAndCount[]>();
                    uniqueValuesAndCounts = countArray2;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(uniqueValuesAndCounts);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

