namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class ConstantFilterModel : FilterModel
    {
        private readonly CriteriaOperator filter;

        private ConstantFilterModel(FilterModelClient client, CriteriaOperator filter) : base(client)
        {
            this.filter = filter;
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter() => 
            this.filter;

        internal override bool CanBuildFilterCore() => 
            base.Column.GetFilterRestrictions().Allow(this.filter);

        internal static FilterModel CreateFilter(FilterModelClient client, CriteriaOperator filter) => 
            new ConstantFilterModel(client, filter);

        internal static FilterModel CreateFunction(FilterModelClient client, FunctionOperatorType type)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(client.PropertyName) };
            return new ConstantFilterModel(client, new FunctionOperator(type, operands));
        }

        internal static FilterModel CreateIsNotNull(FilterModelClient client) => 
            new ConstantFilterModel(client, new UnaryOperator(UnaryOperatorType.Not, new UnaryOperator(UnaryOperatorType.IsNull, client.PropertyName)));

        internal static FilterModel CreateIsNull(FilterModelClient client) => 
            new ConstantFilterModel(client, new UnaryOperator(UnaryOperatorType.IsNull, client.PropertyName));

        internal static FilterModel IsNotNullOrEmpty(FilterModelClient client)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(client.PropertyName) };
            return new ConstantFilterModel(client, new UnaryOperator(UnaryOperatorType.Not, new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands)));
        }

        internal static FilterModel IsNullOrEmpty(FilterModelClient client)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(client.PropertyName) };
            return new ConstantFilterModel(client, new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands));
        }

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__8))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__8 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__8>(ref d__);
            return d__.<>t__builder.Task;
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public ConstantFilterModel <>4__this;
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
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, ConstantFilterModel.<UpdateCoreAsync>d__8>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
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

