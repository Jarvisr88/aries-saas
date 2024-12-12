namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterDatePeriod : CustomUIFilter
    {
        private IEnumerable<ICustomUIFilterValue> children;

        public CustomUIFilterDatePeriod(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new DatePeriodCriteriaParser(this, metric);

        [IteratorStateMachine(typeof(<DataPeriods>d__7))]
        internal static IEnumerable<ICustomUIFilter> DataPeriods(IEnumerable<ICustomUIFilter> children)
        {
            <DataPeriods>d__7 d__1 = new <DataPeriods>d__7(-2);
            d__1.<>3__children = children;
            return d__1;
        }

        [IteratorStateMachine(typeof(<ExcludeDataPeriods>d__8))]
        internal static IEnumerable<ICustomUIFilter> ExcludeDataPeriods(IEnumerable<ICustomUIFilter> children)
        {
            <ExcludeDataPeriods>d__8 d__1 = new <ExcludeDataPeriods>d__8(-2);
            d__1.<>3__children = children;
            return d__1;
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            if ((filterValue == null) || !(filterValue.Value is CustomUIFilterType))
            {
                return null;
            }
            CustomUIFilterType childFilterType = (CustomUIFilterType) filterValue.Value;
            CustomUIFilter @this = (base.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, CustomUIFilter>(filters => filters[childFilterType] as CustomUIFilter, null);
            ICustomUIFilterValue childValue = this.children.Get<IEnumerable<ICustomUIFilterValue>, ICustomUIFilterValue>(delegate (IEnumerable<ICustomUIFilterValue> child) {
                Func<ICustomUIFilterValue, bool> <>9__2;
                Func<ICustomUIFilterValue, bool> predicate = <>9__2;
                if (<>9__2 == null)
                {
                    Func<ICustomUIFilterValue, bool> local1 = <>9__2;
                    predicate = <>9__2 = value => value.FilterType == childFilterType;
                }
                return child.FirstOrDefault<ICustomUIFilterValue>(predicate);
            }, null);
            return @this.Get<ICustomUIFilterValueViewModel, CriteriaOperator>(vm => vm.CreateFilterCriteria(metric, childValue), null);
        }

        protected sealed override ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter)
        {
            ICustomUIFilterValuesFactory valuesFactory = base.GetService<ICustomUIFilterValuesFactory>();
            this.children = (from child in this.GetDataPeriods() select valuesFactory.Create(child.GetID(), new object[0])).ToArray<ICustomUIFilterValue>();
            ICustomUIFilterValue value1 = base.Value;
            ICustomUIFilterValue value2 = value1;
            if (value1 == null)
            {
                ICustomUIFilterValue local1 = value1;
                object[] values = new object[2];
                values[1] = this.children;
                value2 = valuesFactory.Create(base.id, values);
            }
            return value2;
        }

        private IEnumerable<ICustomUIFilter> GetDataPeriods()
        {
            Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> get = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> local1 = <>c.<>9__6_0;
                get = <>c.<>9__6_0 = filters => DataPeriods(filters);
            }
            return (base.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, IEnumerable<ICustomUIFilter>>(get, null);
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.AllDatesInThePeriod;

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            object[] objArray1 = new object[2];
            object[] objArray2 = new object[2];
            objArray2[0] = viewModel.Parameter?.Value;
            object[] values = objArray2;
            values[1] = this.children;
            viewModel.SetResult(values);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterDatePeriod.<>c <>9 = new CustomUIFilterDatePeriod.<>c();
            public static Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> <>9__6_0;

            internal IEnumerable<ICustomUIFilter> <GetDataPeriods>b__6_0(ICustomUIFilters filters) => 
                CustomUIFilterDatePeriod.DataPeriods(filters);
        }

        [CompilerGenerated]
        private sealed class <DataPeriods>d__7 : IEnumerable<ICustomUIFilter>, IEnumerable, IEnumerator<ICustomUIFilter>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ICustomUIFilter <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<ICustomUIFilter> children;
            public IEnumerable<ICustomUIFilter> <>3__children;
            private ICustomUIFilter <datePeriod>5__1;
            private IEnumerator<ICustomUIFilter> <>7__wrap1;

            [DebuggerHidden]
            public <DataPeriods>d__7(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<datePeriod>5__1 = null;
                        this.<>7__wrap1 = this.children.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            ICustomUIFilter current = this.<>7__wrap1.Current;
                            ICustomUIFilter filter1 = this.<datePeriod>5__1;
                            if (this.<datePeriod>5__1 == null)
                            {
                                ICustomUIFilter local1 = this.<datePeriod>5__1;
                                filter1 = current as CustomUIFilterDatePeriod;
                            }
                            this.<datePeriod>5__1 = filter1;
                            if ((this.<datePeriod>5__1 == null) || (current.ParentGroup != this.<datePeriod>5__1.Group))
                            {
                                continue;
                            }
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<ICustomUIFilter> IEnumerable<ICustomUIFilter>.GetEnumerator()
            {
                CustomUIFilterDatePeriod.<DataPeriods>d__7 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomUIFilterDatePeriod.<DataPeriods>d__7(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.children = this.<>3__children;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Filtering.Internal.ICustomUIFilter>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            ICustomUIFilter IEnumerator<ICustomUIFilter>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        [CompilerGenerated]
        private sealed class <ExcludeDataPeriods>d__8 : IEnumerable<ICustomUIFilter>, IEnumerable, IEnumerator<ICustomUIFilter>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ICustomUIFilter <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<ICustomUIFilter> children;
            public IEnumerable<ICustomUIFilter> <>3__children;
            private ICustomUIFilter <datePeriod>5__1;
            private IEnumerator<ICustomUIFilter> <>7__wrap1;

            [DebuggerHidden]
            public <ExcludeDataPeriods>d__8(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<datePeriod>5__1 = null;
                        this.<>7__wrap1 = this.children.GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            ICustomUIFilter current = this.<>7__wrap1.Current;
                            ICustomUIFilter filter1 = this.<datePeriod>5__1;
                            if (this.<datePeriod>5__1 == null)
                            {
                                ICustomUIFilter local1 = this.<datePeriod>5__1;
                                filter1 = current as CustomUIFilterDatePeriod;
                            }
                            this.<datePeriod>5__1 = filter1;
                            if ((this.<datePeriod>5__1 != null) && (current.ParentGroup == this.<datePeriod>5__1.Group))
                            {
                                continue;
                            }
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<ICustomUIFilter> IEnumerable<ICustomUIFilter>.GetEnumerator()
            {
                CustomUIFilterDatePeriod.<ExcludeDataPeriods>d__8 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new CustomUIFilterDatePeriod.<ExcludeDataPeriods>d__8(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                d__.children = this.<>3__children;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Utils.Filtering.Internal.ICustomUIFilter>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            ICustomUIFilter IEnumerator<ICustomUIFilter>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private sealed class DatePeriodCriteriaParser : CustomUIFilter.BaseCustomUIFilterCriteriaParser
        {
            private readonly CustomUIFilterDatePeriod datePeriod;
            private readonly IEndUserFilteringMetric metric;
            private readonly object[] values;

            internal DatePeriodCriteriaParser(CustomUIFilterDatePeriod datePeriod, IEndUserFilteringMetric metric) : base(CustomUIFilterType.AllDatesInThePeriod, metric.Path)
            {
                this.values = new object[2];
                this.datePeriod = datePeriod;
                this.metric = metric;
            }

            protected sealed override void ParseCore(CriteriaOperator criteria)
            {
                ICustomUIFilters parentViewModel = this.datePeriod.ParentViewModel as ICustomUIFilters;
                using (IEnumerator<ICustomUIFilter> enumerator = this.datePeriod.GetDataPeriods().GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ICustomUIFilter current = enumerator.Current;
                        IEndUserFilteringCriteriaAwareViewModel model = current as IEndUserFilteringCriteriaAwareViewModel;
                        if ((model != null) && current.Allow(parentViewModel.UserOptions))
                        {
                            current.SetParentViewModel(parentViewModel);
                            if (model.TryParse(this.metric, criteria))
                            {
                                this.values[0] = current.GetID();
                                return;
                            }
                        }
                    }
                }
                base.MarkInvalid();
            }

            protected sealed override object[] PrepareLocalValues()
            {
                this.values[0] = null;
                this.values[1] = this.datePeriod.children;
                return this.values;
            }
        }
    }
}

