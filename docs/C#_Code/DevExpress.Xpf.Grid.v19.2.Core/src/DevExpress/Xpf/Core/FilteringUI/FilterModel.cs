namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class FilterModel : FilterModelBase
    {
        private LazyReadonlyObservableCollection<FilterValueInfo> filterValues;
        internal Lazy<BaseEditSettings> SupportedEditSettingsLazy;

        internal FilterModel(FilterModelClient client) : base(client)
        {
            this.filterValues = new LazyReadonlyObservableCollection<FilterValueInfo>(new Action(this.ForceUpdate));
            this.FilterValuesSortModes = new FilterValuesSortMode[0];
        }

        internal virtual void BeginUpdateCountedValues(ObservableCollectionCore<FilterValueInfo> filterValues)
        {
        }

        internal virtual void EndUpdateCountedValues(ObservableCollectionCore<FilterValueInfo> filterValues)
        {
        }

        protected void EnsureFilterValuesCollection()
        {
            this.filterValues.EnsureCollection();
        }

        internal virtual FilteringColumn GetActualFilteringColumn() => 
            base.Column;

        private static FilterValuesSortMode[] GetFilterValuesSortModes(bool showCounts, bool showAllLookUpFilterValues, FilteringColumn column)
        {
            List<FilterValuesSortMode> list = new List<FilterValuesSortMode> {
                FilterValuesSortMode.NoSort
            };
            if (showCounts)
            {
                if (UseLookUpValues(((column != null) ? ((LookUpEditSettingsBase) column.GetEditSettings()) : null) as LookUpEditSettingsBase, showAllLookUpFilterValues))
                {
                    list.Add(FilterValuesSortMode.AvailableFirst);
                }
                list.Add(FilterValuesSortMode.PopularFirst);
            }
            return list.ToArray();
        }

        private static IEnumerable GetLookUpSource(LookUpEditSettingsBase editSettings)
        {
            Func<LookUpEditSettingsBase, bool> evaluator = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<LookUpEditSettingsBase, bool> local1 = <>c.<>9__31_0;
                evaluator = <>c.<>9__31_0 = settings => settings.ItemsSource is IEnumerable;
            }
            Func<LookUpEditSettingsBase, IEnumerable> func2 = <>c.<>9__31_1;
            if (<>c.<>9__31_1 == null)
            {
                Func<LookUpEditSettingsBase, IEnumerable> local2 = <>c.<>9__31_1;
                func2 = <>c.<>9__31_1 = settings => (IEnumerable) settings.ItemsSource;
            }
            return editSettings.If<LookUpEditSettingsBase>(evaluator).With<LookUpEditSettingsBase, IEnumerable>(func2);
        }

        protected virtual IEnumerable<CountedLookUp<TValue>> GetSortedFilterValues<TValue>(IEnumerable<CountedLookUp<TValue>> actualValues, bool showCounts)
        {
            if (!showCounts)
            {
                return actualValues;
            }
            FilterValuesSortMode filterValuesSortMode = base.FilterValuesSortMode;
            if (filterValuesSortMode == FilterValuesSortMode.AvailableFirst)
            {
                Func<CountedLookUp<TValue>, bool> predicate = <>c__22<TValue>.<>9__22_0;
                if (<>c__22<TValue>.<>9__22_0 == null)
                {
                    Func<CountedLookUp<TValue>, bool> local1 = <>c__22<TValue>.<>9__22_0;
                    predicate = <>c__22<TValue>.<>9__22_0 = x => x.Count != 0;
                }
                return actualValues.Where<CountedLookUp<TValue>>(predicate).Concat<CountedLookUp<TValue>>(actualValues.Where<CountedLookUp<TValue>>((<>c__22<TValue>.<>9__22_1 ??= x => (x.Count == 0))));
            }
            if (filterValuesSortMode != FilterValuesSortMode.PopularFirst)
            {
                return actualValues;
            }
            Func<CountedLookUp<TValue>, int> keySelector = <>c__22<TValue>.<>9__22_2;
            if (<>c__22<TValue>.<>9__22_2 == null)
            {
                Func<CountedLookUp<TValue>, int> local3 = <>c__22<TValue>.<>9__22_2;
                keySelector = <>c__22<TValue>.<>9__22_2 = x => x.Count;
            }
            return actualValues.OrderByDescending<CountedLookUp<TValue>, int>(keySelector);
        }

        internal virtual Task<UniqueValues> GetUniqueValues(CountsIncludeMode countsIncludeMode) => 
            base.client.GetUniqueValues(countsIncludeMode);

        internal virtual void ResetFilterValues()
        {
            CancellationTokenSource getFilterValuesCancellationTokenSource = this.GetFilterValuesCancellationTokenSource;
            if (getFilterValuesCancellationTokenSource == null)
            {
                CancellationTokenSource local1 = getFilterValuesCancellationTokenSource;
            }
            else
            {
                getFilterValuesCancellationTokenSource.Cancel();
            }
            this.filterValues = new LazyReadonlyObservableCollection<FilterValueInfo>(new Action(this.ForceUpdate));
            base.RaisePropertyChanged("FilterValues");
        }

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__21))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__21 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__21>(ref d__);
            return d__.<>t__builder.Task;
        }

        protected static void UpdateCountedValues<TValue, TValueWithCount>(ObservableCollectionCore<TValueWithCount> values, Either<ReadOnlyCollection<TValue>, ReadOnlyCollection<Counted<TValue>>> valuesWithCounts, Func<TValue, int?, object, TValueWithCount> createValueWithCount, Action<TValueWithCount, object> setRow, LookUpEditSettingsBase lookUpEditSettings, bool showAllLookUpFilterValues, bool includeNullValues, Func<IEnumerable<CountedLookUp<TValue>>, bool, IEnumerable<CountedLookUp<TValue>>> getSortedValues) where TValue: class where TValueWithCount: ValueInfo<TValue>
        {
            IEnumerable<CountedLookUp<TValue>> enumerable;
            bool flag = valuesWithCounts.IsRight();
            if (!UseLookUpValues(lookUpEditSettings, showAllLookUpFilterValues))
            {
                Func<ReadOnlyCollection<TValue>, IEnumerable<Counted<TValue>>> left = <>c__29<TValue, TValueWithCount>.<>9__29_7;
                if (<>c__29<TValue, TValueWithCount>.<>9__29_7 == null)
                {
                    Func<ReadOnlyCollection<TValue>, IEnumerable<Counted<TValue>>> local4 = <>c__29<TValue, TValueWithCount>.<>9__29_7;
                    left = <>c__29<TValue, TValueWithCount>.<>9__29_7 = delegate (ReadOnlyCollection<TValue> y) {
                        Func<TValue, Counted<TValue>> selector = <>c__29<TValue, TValueWithCount>.<>9__29_8;
                        if (<>c__29<TValue, TValueWithCount>.<>9__29_8 == null)
                        {
                            Func<TValue, Counted<TValue>> local1 = <>c__29<TValue, TValueWithCount>.<>9__29_8;
                            selector = <>c__29<TValue, TValueWithCount>.<>9__29_8 = x => new Counted<TValue>(x, 0);
                        }
                        return y.Select<TValue, Counted<TValue>>(selector);
                    };
                }
                enumerable = valuesWithCounts.Match<IEnumerable<Counted<TValue>>>(left, <>c__29<TValue, TValueWithCount>.<>9__29_9 ??= y => y).Select<Counted<TValue>, CountedLookUp<TValue>>(delegate (Counted<TValue> x) {
                    object itemFromValue;
                    if (lookUpEditSettings != null)
                    {
                        itemFromValue = lookUpEditSettings.GetItemFromValue(x.Value);
                    }
                    else
                    {
                        LookUpEditSettingsBase local1 = lookUpEditSettings;
                        itemFromValue = null;
                    }
                    return new CountedLookUp<TValue>(x.Value, itemFromValue, x.Count);
                });
            }
            else
            {
                Func<ReadOnlyCollection<TValue>, Func<TValue, int>> left = <>c__29<TValue, TValueWithCount>.<>9__29_0;
                if (<>c__29<TValue, TValueWithCount>.<>9__29_0 == null)
                {
                    Func<ReadOnlyCollection<TValue>, Func<TValue, int>> local1 = <>c__29<TValue, TValueWithCount>.<>9__29_0;
                    left = <>c__29<TValue, TValueWithCount>.<>9__29_0 = x => <>c__29<TValue, TValueWithCount>.<>9__29_1 ??= _ => 0;
                }
                Func<TValue, int> getCount = valuesWithCounts.Match<Func<TValue, int>>(left, <>c__29<TValue, TValueWithCount>.<>9__29_2 ??= delegate (ReadOnlyCollection<Counted<TValue>> x) {
                    Func<Counted<TValue>, TValue> keySelector = <>c__29<TValue, TValueWithCount>.<>9__29_3;
                    if (<>c__29<TValue, TValueWithCount>.<>9__29_3 == null)
                    {
                        Func<Counted<TValue>, TValue> local1 = <>c__29<TValue, TValueWithCount>.<>9__29_3;
                        keySelector = <>c__29<TValue, TValueWithCount>.<>9__29_3 = y => y.Value;
                    }
                    return new Func<TValue, int>(x.ToDictionaryWithNullableKey<Counted<TValue>, TValue, int>(keySelector, (<>c__29<TValue, TValueWithCount>.<>9__29_4 ??= y => y.Count)).GetValueOrDefault);
                });
                enumerable = from item in GetLookUpSource(lookUpEditSettings).Cast<object>()
                    select new LookUp<TValue>((TValue) lookUpEditSettings.GetValueFromItem(item), item) into x
                    select new CountedLookUp<TValue>(x.Value, x.Row, getCount(x.Value));
            }
            if (!includeNullValues)
            {
                Func<CountedLookUp<TValue>, bool> predicate = <>c__29<TValue, TValueWithCount>.<>9__29_11;
                if (<>c__29<TValue, TValueWithCount>.<>9__29_11 == null)
                {
                    Func<CountedLookUp<TValue>, bool> local6 = <>c__29<TValue, TValueWithCount>.<>9__29_11;
                    predicate = <>c__29<TValue, TValueWithCount>.<>9__29_11 = x => (x.Value != null) && (((x.Value as string) != string.Empty) && (x.Value != DBNull.Value));
                }
                enumerable = enumerable.Where<CountedLookUp<TValue>>(predicate);
            }
            int num = 0;
            foreach (CountedLookUp<TValue> up in getSortedValues(enumerable, flag))
            {
                int? nullable1;
                if (flag)
                {
                    nullable1 = new int?(up.Count);
                }
                else
                {
                    nullable1 = null;
                }
                int? nullable = nullable1;
                if (num >= values.Count)
                {
                    TValueWithCount item = createValueWithCount(up.Value, nullable, up.Row);
                    values.Add(item);
                }
                else
                {
                    TValueWithCount local = values[num];
                    local.Value = up.Value;
                    local.Count = nullable;
                    setRow(local, up.Row);
                }
                num++;
            }
            int num2 = values.Count - num;
            for (int i = 0; i < num2; i++)
            {
                values.RemoveAt(values.Count - 1);
            }
        }

        internal override void UpdateEditSettings()
        {
            this.SupportedEditSettingsLazy = null;
        }

        private static bool UseLookUpValues(LookUpEditSettingsBase editSettings, bool showAllLookUpFilterValues) => 
            showAllLookUpFilterValues && (GetLookUpSource(editSettings) != null);

        public ReadOnlyObservableCollection<FilterValueInfo> FilterValues =>
            this.filterValues.ReadonlyCollection;

        protected bool FilterValuesCreated =>
            this.filterValues.CollectionCreated;

        public FilterValuesSortMode[] FilterValuesSortModes
        {
            get => 
                base.GetValue<FilterValuesSortMode[]>("FilterValuesSortModes");
            private set => 
                base.SetValue<FilterValuesSortMode[]>(value, "FilterValuesSortModes");
        }

        protected virtual bool IncludeNullValues =>
            true;

        public bool IsLoading
        {
            get => 
                base.GetValue<bool>("IsLoading");
            private set => 
                base.SetValue<bool>(value, "IsLoading");
        }

        private CancellationTokenSource GetFilterValuesCancellationTokenSource { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterModel.<>c <>9 = new FilterModel.<>c();
            public static Func<LookUpEditSettingsBase, bool> <>9__31_0;
            public static Func<LookUpEditSettingsBase, IEnumerable> <>9__31_1;

            internal bool <GetLookUpSource>b__31_0(LookUpEditSettingsBase settings) => 
                settings.ItemsSource is IEnumerable;

            internal IEnumerable <GetLookUpSource>b__31_1(LookUpEditSettingsBase settings) => 
                (IEnumerable) settings.ItemsSource;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__22<TValue>
        {
            public static readonly FilterModel.<>c__22<TValue> <>9;
            public static Func<FilterModel.CountedLookUp<TValue>, bool> <>9__22_0;
            public static Func<FilterModel.CountedLookUp<TValue>, bool> <>9__22_1;
            public static Func<FilterModel.CountedLookUp<TValue>, int> <>9__22_2;

            static <>c__22()
            {
                FilterModel.<>c__22<TValue>.<>9 = new FilterModel.<>c__22<TValue>();
            }

            internal bool <GetSortedFilterValues>b__22_0(FilterModel.CountedLookUp<TValue> x) => 
                x.Count != 0;

            internal bool <GetSortedFilterValues>b__22_1(FilterModel.CountedLookUp<TValue> x) => 
                x.Count == 0;

            internal int <GetSortedFilterValues>b__22_2(FilterModel.CountedLookUp<TValue> x) => 
                x.Count;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__29<TValue, TValueWithCount> where TValue: class where TValueWithCount: ValueInfo<TValue>
        {
            public static readonly FilterModel.<>c__29<TValue, TValueWithCount> <>9;
            public static Func<TValue, int> <>9__29_1;
            public static Func<ReadOnlyCollection<TValue>, Func<TValue, int>> <>9__29_0;
            public static Func<Counted<TValue>, TValue> <>9__29_3;
            public static Func<Counted<TValue>, int> <>9__29_4;
            public static Func<ReadOnlyCollection<Counted<TValue>>, Func<TValue, int>> <>9__29_2;
            public static Func<TValue, Counted<TValue>> <>9__29_8;
            public static Func<ReadOnlyCollection<TValue>, IEnumerable<Counted<TValue>>> <>9__29_7;
            public static Func<ReadOnlyCollection<Counted<TValue>>, IEnumerable<Counted<TValue>>> <>9__29_9;
            public static Func<FilterModel.CountedLookUp<TValue>, bool> <>9__29_11;

            static <>c__29()
            {
                FilterModel.<>c__29<TValue, TValueWithCount>.<>9 = new FilterModel.<>c__29<TValue, TValueWithCount>();
            }

            internal Func<TValue, int> <UpdateCountedValues>b__29_0(ReadOnlyCollection<TValue> x) => 
                FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_1 ??= _ => 0;

            internal int <UpdateCountedValues>b__29_1(TValue _) => 
                0;

            internal bool <UpdateCountedValues>b__29_11(FilterModel.CountedLookUp<TValue> x) => 
                (x.Value != null) && (((x.Value as string) != string.Empty) && (x.Value != DBNull.Value));

            internal Func<TValue, int> <UpdateCountedValues>b__29_2(ReadOnlyCollection<Counted<TValue>> x)
            {
                Func<Counted<TValue>, TValue> keySelector = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_3;
                if (FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_3 == null)
                {
                    Func<Counted<TValue>, TValue> local1 = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_3;
                    keySelector = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_3 = y => y.Value;
                }
                return new Func<TValue, int>(x.ToDictionaryWithNullableKey<Counted<TValue>, TValue, int>(keySelector, (FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_4 ??= y => y.Count)).GetValueOrDefault);
            }

            internal TValue <UpdateCountedValues>b__29_3(Counted<TValue> y) => 
                y.Value;

            internal int <UpdateCountedValues>b__29_4(Counted<TValue> y) => 
                y.Count;

            internal IEnumerable<Counted<TValue>> <UpdateCountedValues>b__29_7(ReadOnlyCollection<TValue> y)
            {
                Func<TValue, Counted<TValue>> selector = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_8;
                if (FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_8 == null)
                {
                    Func<TValue, Counted<TValue>> local1 = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_8;
                    selector = FilterModel.<>c__29<TValue, TValueWithCount>.<>9__29_8 = x => new Counted<TValue>(x, 0);
                }
                return y.Select<TValue, Counted<TValue>>(selector);
            }

            internal Counted<TValue> <UpdateCountedValues>b__29_8(TValue x) => 
                new Counted<TValue>(x, 0);

            internal IEnumerable<Counted<TValue>> <UpdateCountedValues>b__29_9(ReadOnlyCollection<Counted<TValue>> y) => 
                y;
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__21 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public FilterModel <>4__this;
            private Either<ReadOnlyCollection<object>, ReadOnlyCollection<Counted<object>>> <uniqueValues>5__1;
            private FilterModel.<>c__DisplayClass21_0 <>8__2;
            private CancellationTokenSource <>7__wrap1;
            private TaskAwaiter<UniqueValues> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (num != 0)
                    {
                        this.<>8__2 = new FilterModel.<>c__DisplayClass21_0();
                        this.<>8__2.<>4__this = this.<>4__this;
                        if (this.<>4__this.FilterValuesCreated)
                        {
                            this.<>4__this.IsLoading = true;
                            this.<uniqueValues>5__1 = null;
                            this.<>7__wrap1 = this.<>4__this.GetFilterValuesCancellationTokenSource = new CancellationTokenSource();
                        }
                        else
                        {
                            goto TR_0002;
                        }
                    }
                    try
                    {
                        int num1 = num;
                        try
                        {
                            TaskAwaiter<UniqueValues> awaiter;
                            UniqueValues values2;
                            if (num == 0)
                            {
                                awaiter = this.<>u__1;
                                this.<>u__1 = new TaskAwaiter<UniqueValues>();
                                this.<>1__state = num = -1;
                                goto TR_0011;
                            }
                            else
                            {
                                awaiter = this.<>4__this.GetUniqueValues(this.<>4__this.ActualCountsInculedMode).AddCancellation<UniqueValues>(this.<>4__this.GetFilterValuesCancellationTokenSource.Token).GetAwaiter();
                                if (awaiter.IsCompleted)
                                {
                                    goto TR_0011;
                                }
                                else
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<UniqueValues>, FilterModel.<UpdateCoreAsync>d__21>(ref awaiter, ref this);
                                }
                            }
                            return;
                        TR_0011:
                            values2 = awaiter.GetResult();
                            awaiter = new TaskAwaiter<UniqueValues>();
                            UniqueValues values = values2;
                            this.<uniqueValues>5__1 = values.Value;
                        }
                        catch
                        {
                        }
                    }
                    finally
                    {
                        if ((num < 0) && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    this.<>7__wrap1 = null;
                    this.<>4__this.GetFilterValuesCancellationTokenSource = null;
                    this.<>4__this.IsLoading = false;
                    if ((this.<uniqueValues>5__1 != null) && this.<>4__this.FilterValuesCreated)
                    {
                        BaseEditSettings local3;
                        this.<>4__this.FilterValuesSortModes = FilterModel.GetFilterValuesSortModes(this.<>4__this.ActualShowCounts && this.<uniqueValues>5__1.IsRight(), this.<>4__this.ShowAllLookUpFilterValues, this.<>4__this.Column);
                        this.<>4__this.BeginUpdateCountedValues(this.<>4__this.filterValues.Collection);
                        this.<>8__2.filteringColumn = this.<>4__this.GetActualFilteringColumn();
                        if (this.<>4__this.SupportedEditSettingsLazy == null)
                        {
                            this.<>4__this.SupportedEditSettingsLazy = new Lazy<BaseEditSettings>(new Func<BaseEditSettings>(this.<>8__2.<UpdateCoreAsync>b__0));
                        }
                        if (this.<>4__this.SupportedEditSettingsLazy != null)
                        {
                            local3 = this.<>4__this.SupportedEditSettingsLazy.Value;
                        }
                        else
                        {
                            Lazy<BaseEditSettings> supportedEditSettingsLazy = this.<>4__this.SupportedEditSettingsLazy;
                            local3 = null;
                        }
                        FilterModel.UpdateCountedValues<object, FilterValueInfo>(this.<>4__this.filterValues.Collection, this.<uniqueValues>5__1, new Func<object, int?, object, FilterValueInfo>(this.<>8__2.<UpdateCoreAsync>b__1), new Action<FilterValueInfo, object>(this.<>4__this.<UpdateCoreAsync>b__21_2), local3 as LookUpEditSettingsBase, this.<>4__this.ShowAllLookUpFilterValues, this.<>4__this.IncludeNullValues, new Func<IEnumerable<FilterModel.CountedLookUp<object>>, bool, IEnumerable<FilterModel.CountedLookUp<object>>>(this.<>4__this.GetSortedFilterValues<object>));
                        this.<>4__this.EndUpdateCountedValues(this.<>4__this.filterValues.Collection);
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            TR_0002:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct CountedLookUp<TValue>
        {
            public readonly TValue Value;
            public readonly object Row;
            public readonly int Count;
            public CountedLookUp(TValue value, object row, int count)
            {
                this.Value = value;
                this.Row = row;
                this.Count = count;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LookUp<TValue>
        {
            public readonly TValue Value;
            public readonly object Row;
            public LookUp(TValue value, object row)
            {
                this.Value = value;
                this.Row = row;
            }
        }
    }
}

