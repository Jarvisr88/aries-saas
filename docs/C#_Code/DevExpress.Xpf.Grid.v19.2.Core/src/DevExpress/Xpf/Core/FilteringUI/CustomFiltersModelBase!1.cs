namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public abstract class CustomFiltersModelBase<TFilter> : FilterModel where TFilter: class
    {
        private readonly LazyReadonlyObservableCollection<ValueInfo<TFilter>> filters;

        internal CustomFiltersModelBase(FilterModelClient client) : base(client)
        {
            this.filters = new LazyReadonlyObservableCollection<ValueInfo<TFilter>>(new Action(this.ForceUpdate));
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter() => 
            CriteriaOperator.Or((from x in (this.SelectedFilters ?? new List<object>()).Cast<ValueInfo<TFilter>>() select this.CreateFilter(x.Value)).ToArray<FunctionOperator>());

        internal sealed override bool CanBuildFilterCore() => 
            true;

        protected abstract FunctionOperator CreateFilter(TFilter filter);
        internal void EnsureFilters()
        {
            this.filters.EnsureCollection();
        }

        protected abstract Func<TFilter, bool> GetIsSelectedFilterChecker(CriteriaOperator filter);
        protected static ICollection<T> ParseCustomFunctions<T>(CriteriaOperator filter, Func<FunctionOperator, T> mapper) where T: class => 
            FormatConditionFiltersHelper.ParseGroupOrOperator<T>(filter, delegate (CriteriaOperator x) {
                Func<CriteriaOperator, T> otherwise = <>c__0<TFilter, T>.<>9__0_1;
                if (<>c__0<TFilter, T>.<>9__0_1 == null)
                {
                    Func<CriteriaOperator, T> local1 = <>c__0<TFilter, T>.<>9__0_1;
                    otherwise = <>c__0<TFilter, T>.<>9__0_1 = _ => default(T);
                }
                return x.Transform<FunctionOperator, T>((Func<FunctionOperator, T>) mapper, otherwise);
            });

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__11))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__11<TFilter> d__;
            d__.<>4__this = (CustomFiltersModelBase<TFilter>) this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__11<TFilter>>(ref d__);
            return d__.<>t__builder.Task;
        }

        private void UpdateFilters()
        {
            ReadOnlyCollection<TFilter> first = this.CoreFilters.ToReadOnlyCollection<TFilter>();
            if (this.filters.CollectionCreated)
            {
                Either<ReadOnlyCollection<TFilter>, ReadOnlyCollection<Counted<TFilter>>> either;
                int[] second = base.ActualShowCounts ? base.client.GetCounts((IEnumerable<CriteriaOperator>) (from x in first select this.CreateFilter(x))) : null;
                if (second == null)
                {
                    either = first;
                }
                else
                {
                    Func<TFilter, int, Counted<TFilter>> resultSelector = <>c<TFilter>.<>9__13_1;
                    if (<>c<TFilter>.<>9__13_1 == null)
                    {
                        Func<TFilter, int, Counted<TFilter>> local1 = <>c<TFilter>.<>9__13_1;
                        resultSelector = <>c<TFilter>.<>9__13_1 = (x, y) => new Counted<TFilter>(x, y);
                    }
                    either = first.Zip<TFilter, int, Counted<TFilter>>(second, resultSelector).ToReadOnlyCollection<Counted<TFilter>>();
                }
                Func<TFilter, int?, object, ValueInfo<TFilter>> createValueWithCount = <>c<TFilter>.<>9__13_2;
                if (<>c<TFilter>.<>9__13_2 == null)
                {
                    Func<TFilter, int?, object, ValueInfo<TFilter>> local2 = <>c<TFilter>.<>9__13_2;
                    createValueWithCount = <>c<TFilter>.<>9__13_2 = (val, count, row) => new ValueInfo<TFilter>(val, count, null);
                }
                object obj1 = null;
                if (<>c<TFilter>.<>9__13_4 == null)
                {
                    obj1 = <>c<TFilter>.<>9__13_4 = (actualValues, _) => actualValues;
                }
                UpdateCountedValues<TFilter, ValueInfo<TFilter>>(this.filters.Collection, either, createValueWithCount, <>c<TFilter>.<>9__13_3 ??= delegate (ValueInfo<TFilter> _, object __) {
                }, (LookUpEditSettingsBase) <>c<TFilter>.<>9__13_4, this.IncludeNullValues, false, (Func<IEnumerable<FilterModel.CountedLookUp<TFilter>>, bool, IEnumerable<FilterModel.CountedLookUp<TFilter>>>) obj1);
            }
        }

        protected abstract IEnumerable<TFilter> CoreFilters { get; }

        public ReadOnlyObservableCollection<ValueInfo<TFilter>> Filters =>
            this.filters.ReadonlyCollection;

        public List<object> SelectedFilters
        {
            get => 
                base.GetProperty<List<object>>(Expression.Lambda<Func<List<object>>>(Expression.Property(Expression.Constant(this, typeof(CustomFiltersModelBase<TFilter>)), (MethodInfo) methodof(CustomFiltersModelBase<TFilter>.get_SelectedFilters, CustomFiltersModelBase<TFilter>)), new ParameterExpression[0]));
            set
            {
                bool flag = FilteringUIExtensions.ListReallyChanged(this.SelectedFilters, value);
                if (base.SetProperty<List<object>>(Expression.Lambda<Func<List<object>>>(Expression.Property(Expression.Constant(this, typeof(CustomFiltersModelBase<TFilter>)), (MethodInfo) methodof(CustomFiltersModelBase<TFilter>.get_SelectedFilters, CustomFiltersModelBase<TFilter>)), new ParameterExpression[0]), value) & flag)
                {
                    base.ApplyFilter();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomFiltersModelBase<TFilter>.<>c <>9;
            public static Func<TFilter, int, Counted<TFilter>> <>9__13_1;
            public static Func<TFilter, int?, object, ValueInfo<TFilter>> <>9__13_2;
            public static Action<ValueInfo<TFilter>, object> <>9__13_3;
            public static Func<IEnumerable<FilterModel.CountedLookUp<TFilter>>, bool, IEnumerable<FilterModel.CountedLookUp<TFilter>>> <>9__13_4;

            static <>c()
            {
                CustomFiltersModelBase<TFilter>.<>c.<>9 = new CustomFiltersModelBase<TFilter>.<>c();
            }

            internal Counted<TFilter> <UpdateFilters>b__13_1(TFilter x, int y) => 
                new Counted<TFilter>(x, y);

            internal ValueInfo<TFilter> <UpdateFilters>b__13_2(TFilter val, int? count, object row) => 
                new ValueInfo<TFilter>(val, count, null);

            internal void <UpdateFilters>b__13_3(ValueInfo<TFilter> _, object __)
            {
            }

            internal IEnumerable<FilterModel.CountedLookUp<TFilter>> <UpdateFilters>b__13_4(IEnumerable<FilterModel.CountedLookUp<TFilter>> actualValues, bool _) => 
                actualValues;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<T> where T: class
        {
            public static readonly CustomFiltersModelBase<TFilter>.<>c__0<T> <>9;
            public static Func<CriteriaOperator, T> <>9__0_1;

            static <>c__0()
            {
                CustomFiltersModelBase<TFilter>.<>c__0<T>.<>9 = new CustomFiltersModelBase<TFilter>.<>c__0<T>();
            }

            internal T <ParseCustomFunctions>b__0_1(CriteriaOperator _) => 
                default(T);
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__11 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public CustomFiltersModelBase<TFilter> <>4__this;
            private CustomFiltersModelBase<TFilter>.<>c__DisplayClass11_0 <>8__1;
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
                        this.<>8__1 = new CustomFiltersModelBase<TFilter>.<>c__DisplayClass11_0();
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CustomFiltersModelBase<TFilter>.<UpdateCoreAsync>d__11>(ref awaiter, ref (CustomFiltersModelBase<TFilter>.<UpdateCoreAsync>d__11) ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>4__this.UpdateFilters();
                    this.<>8__1.isSelectedFilter = this.<>4__this.GetIsSelectedFilterChecker(this.<>4__this.Filter);
                    this.<>4__this.SelectedFilters = (!this.<>4__this.filters.CollectionCreated || (this.<>8__1.isSelectedFilter == null)) ? new List<object>() : this.<>4__this.Filters.Where<ValueInfo<TFilter>>(new Func<ValueInfo<TFilter>, bool>(this.<>8__1.<UpdateCoreAsync>b__0)).ToList<object>();
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

