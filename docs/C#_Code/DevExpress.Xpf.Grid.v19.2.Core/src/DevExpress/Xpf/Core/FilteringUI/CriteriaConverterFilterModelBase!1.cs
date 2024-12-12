namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class CriteriaConverterFilterModelBase<T> : FilterModel
    {
        private readonly CriteriaConverter<T> converter;

        internal CriteriaConverterFilterModelBase(FilterModelClient client, CriteriaConverter<T> converter) : base(client)
        {
            this.converter = converter;
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal sealed override void BeginUpdateCountedValues(ObservableCollectionCore<FilterValueInfo> filterValues)
        {
            filterValues.BeginUpdate();
        }

        internal sealed override CriteriaOperator BuildFilter() => 
            this.BuildFilter(this.CreateConverterValue());

        internal CriteriaOperator BuildFilter(T converterValue) => 
            this.converter.ToCriteria(converterValue, base.PropertyName);

        internal sealed override bool CanBuildFilterCore() => 
            this.converter.CanBuildFilter(base.Column.GetFilterRestrictions());

        protected internal abstract T CreateConverterValue();
        internal sealed override void EndUpdateCountedValues(ObservableCollectionCore<FilterValueInfo> filterValues)
        {
            filterValues.EndUpdate();
        }

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__4))]
        internal sealed override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__4<T> d__;
            d__.<>4__this = (CriteriaConverterFilterModelBase<T>) this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__4<T>>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected abstract void UpdateFromConverterValue(T value);

        protected override bool IncludeNullValues =>
            false;

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__4 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public CriteriaConverterFilterModelBase<T> <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CriteriaConverterFilterModelBase<T>.<UpdateCoreAsync>d__4>(ref awaiter, ref (CriteriaConverterFilterModelBase<T>.<UpdateCoreAsync>d__4) ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>4__this.UpdateFromConverterValue(this.<>4__this.converter.ToValue(this.<>4__this.Filter));
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
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

