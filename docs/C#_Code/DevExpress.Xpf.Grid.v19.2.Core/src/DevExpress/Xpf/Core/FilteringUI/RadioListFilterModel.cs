namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public sealed class RadioListFilterModel : FilterModel
    {
        internal RadioListFilterModel(FilterModelClient client) : base(client)
        {
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter()
        {
            if (this.SelectedFilterValue == null)
            {
                return null;
            }
            List<object> checkedValues = new List<object>();
            checkedValues.Add(this.SelectedFilterValue.Value);
            Func<List<object>> getUncheckedValues = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<List<object>> local1 = <>c.<>9__5_0;
                getUncheckedValues = <>c.<>9__5_0 = delegate {
                    throw new InvalidOperationException();
                };
            }
            return CheckedListFilterModel.BuildMultiValuesFilter(this, new CheckedValuesInfo(checkedValues, getUncheckedValues, null));
        }

        internal override bool CanBuildFilterCore() => 
            base.Column.GetFilterRestrictions().Allow(BinaryOperatorType.Equal);

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__4))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__4 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__4>(ref d__);
            return d__.<>t__builder.Task;
        }

        public FilterValueInfo SelectedFilterValue
        {
            get => 
                base.GetValue<FilterValueInfo>("SelectedFilterValue");
            set => 
                base.SetValue<FilterValueInfo>(value, new Action(this.ApplyFilter), "SelectedFilterValue");
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RadioListFilterModel.<>c <>9 = new RadioListFilterModel.<>c();
            public static BinaryOperatorMapper<Option<object>> <>9__4_0;
            public static UnaryOperatorMapper<Option<object>> <>9__4_1;
            public static InOperatorMapper<Option<object>> <>9__4_2;
            public static FunctionOperatorMapper<Option<object>> <>9__4_3;
            public static FallbackMapper<Option<object>> <>9__4_4;
            public static Func<FilterValueInfo> <>9__4_7;
            public static Func<List<object>> <>9__5_0;

            internal List<object> <BuildFilter>b__5_0()
            {
                throw new InvalidOperationException();
            }

            internal Option<object> <UpdateCoreAsync>b__4_0(string name, object val, BinaryOperatorType type) => 
                (type != BinaryOperatorType.Equal) ? Option<object>.Empty : val.AsOption<object>();

            internal Option<object> <UpdateCoreAsync>b__4_1(string name, UnaryOperatorType type) => 
                (type == UnaryOperatorType.IsNull) ? null.AsOption<object>() : Option<object>.Empty;

            internal Option<object> <UpdateCoreAsync>b__4_2(string name, object[] values) => 
                (values.Length == 1) ? values.Single<object>().AsOption<object>() : Option<object>.Empty;

            internal Option<object> <UpdateCoreAsync>b__4_3(string name, object[] values, FunctionOperatorType type)
            {
                Attributed<ValueData[]> attributed = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<DateTime>(name, values.Select<object, ValueData>(new Func<object, ValueData>(ValueData.FromValue)).ToArray<ValueData>(), type);
                return (((attributed == null) || (attributed.Value.Length != 1)) ? (((type == FunctionOperatorType.IsNullOrEmpty) || (type == FunctionOperatorType.IsNull)) ? null.AsOption<object>() : Option<object>.Empty) : attributed.Value.Single<ValueData>().ToValue().AsOption<object>());
            }

            internal Option<object> <UpdateCoreAsync>b__4_4(CriteriaOperator _) => 
                Option<object>.Empty;

            internal FilterValueInfo <UpdateCoreAsync>b__4_7() => 
                null;
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__4 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public RadioListFilterModel <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    FilterValueInfo local7;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0013;
                    }
                    else
                    {
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0013;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RadioListFilterModel.<UpdateCoreAsync>d__4>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0013:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    BinaryOperatorMapper<Option<object>> binary = RadioListFilterModel.<>c.<>9__4_0;
                    if (RadioListFilterModel.<>c.<>9__4_0 == null)
                    {
                        BinaryOperatorMapper<Option<object>> local1 = RadioListFilterModel.<>c.<>9__4_0;
                        binary = RadioListFilterModel.<>c.<>9__4_0 = new BinaryOperatorMapper<Option<object>>(this.<UpdateCoreAsync>b__4_0);
                    }
                    InOperatorMapper<Option<object>> mapper2 = RadioListFilterModel.<>c.<>9__4_2 ??= new InOperatorMapper<Option<object>>(this.<UpdateCoreAsync>b__4_2);
                    if (RadioListFilterModel.<>c.<>9__4_3 == null)
                    {
                        InOperatorMapper<Option<object>> local4 = RadioListFilterModel.<>c.<>9__4_2 ??= new InOperatorMapper<Option<object>>(this.<UpdateCoreAsync>b__4_2);
                        mapper2 = (InOperatorMapper<Option<object>>) (RadioListFilterModel.<>c.<>9__4_3 = new FunctionOperatorMapper<Option<object>>(this.<UpdateCoreAsync>b__4_3));
                    }
                    FallbackMapper<Option<object>> fallback = RadioListFilterModel.<>c.<>9__4_4;
                    if (RadioListFilterModel.<>c.<>9__4_4 == null)
                    {
                        FallbackMapper<Option<object>> local5 = RadioListFilterModel.<>c.<>9__4_4;
                        fallback = RadioListFilterModel.<>c.<>9__4_4 = new FallbackMapper<Option<object>>(this.<UpdateCoreAsync>b__4_4);
                    }
                    Option<object> option = BetweenDatesHelper.SubstituteDateInRange(this.<>4__this.Filter).Map<Option<object>>(binary, RadioListFilterModel.<>c.<>9__4_1 ??= new UnaryOperatorMapper<Option<object>>(this.<UpdateCoreAsync>b__4_1), null, (BetweenOperatorMapper<Option<object>>) RadioListFilterModel.<>c.<>9__4_3, (FunctionOperatorMapper<Option<object>>) mapper2, null, null, null, fallback, null);
                    if (!this.<>4__this.FilterValuesCreated)
                    {
                        local7 = null;
                    }
                    else
                    {
                        Func<FilterValueInfo> getNoValue = RadioListFilterModel.<>c.<>9__4_7;
                        if (RadioListFilterModel.<>c.<>9__4_7 == null)
                        {
                            Func<FilterValueInfo> local6 = RadioListFilterModel.<>c.<>9__4_7;
                            getNoValue = RadioListFilterModel.<>c.<>9__4_7 = new Func<FilterValueInfo>(this.<UpdateCoreAsync>b__4_7);
                        }
                        local7 = option.Match<FilterValueInfo>(new Func<object, FilterValueInfo>(this.<>4__this.<UpdateCoreAsync>b__4_5), getNoValue);
                    }
                    this.<>4__this.SelectedFilterValue = local7;
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

