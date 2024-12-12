namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public sealed class CheckedListFilterModel : FilterModel
    {
        private Locker locker;

        internal CheckedListFilterModel(FilterModelClient client) : base(client)
        {
            this.locker = new Locker();
            this.SelectedFilterValuesSource = new ObservableCollectionCore<object>();
            this.SelectedFilterValuesSource.CollectionChanged += (o, e) => this.locker.DoLockedActionIfNotLocked(() => this.SelectedFilterValues = this.SelectedFilterValuesSource.ToList<object>());
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter()
        {
            List<object> list2;
            List<object> selectedFilterValues = this.SelectedFilterValues;
            if (selectedFilterValues != null)
            {
                list2 = selectedFilterValues.Cast<FilterValueInfo>().Select<FilterValueInfo, object>((<>c.<>9__13_0 ??= x => x.Value)).ToList<object>();
            }
            else
            {
                List<object> local1 = selectedFilterValues;
                list2 = null;
            }
            List<object> local3 = list2;
            List<object> list3 = local3;
            if (local3 == null)
            {
                List<object> local4 = local3;
                list3 = new List<object>();
            }
            List<object> checkedValues = list3;
            return BuildMultiValuesFilter(this, new CheckedValuesInfo(checkedValues, delegate {
                Func<FilterValueInfo, object> selector = <>c.<>9__13_2;
                if (<>c.<>9__13_2 == null)
                {
                    Func<FilterValueInfo, object> local1 = <>c.<>9__13_2;
                    selector = <>c.<>9__13_2 = x => x.Value;
                }
                return this.FilterValues.Select<FilterValueInfo, object>(selector).Except<object>(checkedValues).ToList<object>();
            }, null));
        }

        internal static CriteriaOperator BuildMultiValuesFilter(FilterModelBase model, CheckedValuesInfo values)
        {
            Func<string, CriteriaOperator> getBlanksFilter = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<string, CriteriaOperator> local1 = <>c.<>9__14_0;
                getBlanksFilter = <>c.<>9__14_0 = x => new UnaryOperator(UnaryOperatorType.IsNull, new OperandProperty(x));
            }
            return HierarchyFilterBuilderHelper.GetAppropriateBuilder(model.PropertyName, getBlanksFilter, delegate (string name) {
                if (name != model.Column.Name)
                {
                    throw new InvalidOperationException();
                }
                return model.Column.GetUseRangeDateFilter();
            }).BuildValuesFilter(model.Column.GetFilterRestrictions(), values, true);
        }

        internal override bool CanBuildFilterCore() => 
            SimpleFilterTreeBase.CanBuildFilter(base.Column.GetFilterRestrictions());

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__12))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__12 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__12>(ref d__);
            return d__.<>t__builder.Task;
        }

        public ObservableCollection<object> SelectedFilterValuesSource { get; private set; }

        public List<object> SelectedFilterValues
        {
            get => 
                base.GetValue<List<object>>("SelectedFilterValues");
            set
            {
                bool flag = FilteringUIExtensions.ListReallyChanged(this.SelectedFilterValues, value);
                if (base.SetValue<List<object>>(value, "SelectedFilterValues") & flag)
                {
                    this.locker.DoLockedActionIfNotLocked(delegate {
                        if (this.SelectedFilterValuesSource.Count != 0)
                        {
                            this.SelectedFilterValuesSource.Clear();
                        }
                        foreach (object obj2 in this.SelectedFilterValues ?? new List<object>())
                        {
                            this.SelectedFilterValuesSource.Add(obj2);
                        }
                    });
                    base.ApplyFilter();
                }
            }
        }

        internal bool SelectAllWhenFilterIsNull
        {
            get => 
                base.GetValue<bool>("SelectAllWhenFilterIsNull");
            set
            {
                if (base.SetValue<bool>(value, "SelectAllWhenFilterIsNull") && (base.Filter == null))
                {
                    base.Update();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckedListFilterModel.<>c <>9 = new CheckedListFilterModel.<>c();
            public static Func<FilterValueInfo, object> <>9__13_0;
            public static Func<FilterValueInfo, object> <>9__13_2;
            public static Func<string, CriteriaOperator> <>9__14_0;

            internal object <BuildFilter>b__13_0(FilterValueInfo x) => 
                x.Value;

            internal object <BuildFilter>b__13_2(FilterValueInfo x) => 
                x.Value;

            internal CriteriaOperator <BuildMultiValuesFilter>b__14_0(string x) => 
                new UnaryOperator(UnaryOperatorType.IsNull, new OperandProperty(x));
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__12 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public CheckedListFilterModel <>4__this;
            private CheckedListFilterModel.<>c__DisplayClass12_0 <>8__1;
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
                        this.<>8__1 = new CheckedListFilterModel.<>c__DisplayClass12_0();
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CheckedListFilterModel.<UpdateCoreAsync>d__12>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>8__1.isSelectedFunc = FilterTreeHelper.CreateContainsValueFunc(this.<>4__this.PropertyName, this.<>4__this.Column.Type, this.<>4__this.Filter, this.<>4__this.client.SubstituteFilter);
                    this.<>4__this.SelectedFilterValues = this.<>4__this.FilterValuesCreated ? (this.<>4__this.HasFilter ? this.<>4__this.FilterValues.Where<FilterValueInfo>(new Func<FilterValueInfo, bool>(this.<>8__1.<UpdateCoreAsync>b__0)).ToList<object>() : (this.<>4__this.SelectAllWhenFilterIsNull ? this.<>4__this.FilterValues.ToList<object>() : new List<object>())) : new List<object>();
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

