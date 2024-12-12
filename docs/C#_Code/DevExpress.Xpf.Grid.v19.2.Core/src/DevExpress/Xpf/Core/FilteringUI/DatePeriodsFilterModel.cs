namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class DatePeriodsFilterModel : FilterModel
    {
        internal DatePeriodsFilterModel(FilterModelClient client) : base(client)
        {
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter()
        {
            List<object> selectedPredefinedFilters = this.SelectedPredefinedFilters;
            if (selectedPredefinedFilters != null)
            {
                return selectedPredefinedFilters.Cast<DatePeriodFilterValue>().Select<DatePeriodFilterValue, FilterDateType>((<>c.<>9__11_0 ??= x => x.Value)).ToCriteria(base.PropertyName);
            }
            List<object> local1 = selectedPredefinedFilters;
            return null;
        }

        internal override bool CanBuildFilterCore() => 
            this.PredefinedFilters.Any<DatePeriodFilterValue>();

        internal static FilterDateType[] GetFilters(AllowedDateTimeFilters allowedFilters)
        {
            List<FilterDateType> list = new List<FilterDateType>();
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsBeyondThisYear))
            {
                list.Add(FilterDateType.BeyondThisYear);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsLaterThisYear))
            {
                list.Add(FilterDateType.LaterThisYear);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsLaterThisMonth))
            {
                list.Add(FilterDateType.LaterThisMonth);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsNextWeek))
            {
                list.Add(FilterDateType.NextWeek);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsLaterThisWeek))
            {
                list.Add(FilterDateType.LaterThisWeek);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsTomorrow))
            {
                list.Add(FilterDateType.Tomorrow);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsToday))
            {
                list.Add(FilterDateType.Today);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsYesterday))
            {
                list.Add(FilterDateType.Yesterday);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsEarlierThisWeek))
            {
                list.Add(FilterDateType.EarlierThisWeek);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsLastWeek))
            {
                list.Add(FilterDateType.LastWeek);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsEarlierThisMonth))
            {
                list.Add(FilterDateType.EarlierThisMonth);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsEarlierThisYear))
            {
                list.Add(FilterDateType.EarlierThisYear);
            }
            if (allowedFilters.HasFlag(AllowedDateTimeFilters.IsPriorThisYear))
            {
                list.Add(FilterDateType.PriorThisYear);
            }
            return list.ToArray();
        }

        private static EditorStringId GetLocalizationString(FilterDateType filterDateType)
        {
            switch (filterDateType)
            {
                case FilterDateType.PriorThisYear:
                    return EditorStringId.FilterClauseIsOutlookIntervalPriorThisYear;

                case FilterDateType.EarlierThisYear:
                    return EditorStringId.FilterClauseIsOutlookIntervalEarlierThisYear;

                case FilterDateType.EarlierThisMonth:
                    return EditorStringId.FilterClauseIsOutlookIntervalEarlierThisMonth;

                case FilterDateType.LastWeek:
                    return EditorStringId.FilterClauseIsOutlookIntervalLastWeek;

                case FilterDateType.EarlierThisWeek:
                    return EditorStringId.FilterClauseIsOutlookIntervalEarlierThisWeek;

                case FilterDateType.Yesterday:
                    return EditorStringId.FilterClauseIsOutlookIntervalYesterday;

                case FilterDateType.Today:
                    return EditorStringId.FilterClauseIsOutlookIntervalToday;

                case FilterDateType.Tomorrow:
                    return EditorStringId.FilterClauseIsOutlookIntervalTomorrow;

                case FilterDateType.LaterThisWeek:
                    return EditorStringId.FilterClauseIsOutlookIntervalLaterThisWeek;

                case FilterDateType.NextWeek:
                    return EditorStringId.FilterClauseIsOutlookIntervalNextWeek;

                case FilterDateType.LaterThisMonth:
                    return EditorStringId.FilterClauseIsOutlookIntervalLaterThisMonth;

                case FilterDateType.LaterThisYear:
                    return EditorStringId.FilterClauseIsOutlookIntervalLaterThisYear;

                case FilterDateType.BeyondThisYear:
                    return EditorStringId.FilterClauseIsOutlookIntervalBeyondThisYear;
            }
            throw new NotSupportedException();
        }

        private List<DatePeriodFilterValue> GetPredefinedFilters()
        {
            Func<FilterDateType, DatePeriodFilterValue> selector = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<FilterDateType, DatePeriodFilterValue> local1 = <>c.<>9__9_0;
                selector = <>c.<>9__9_0 = x => new DatePeriodFilterValue(x, EditorLocalizer.GetString(GetLocalizationString(x)));
            }
            return GetFilters(base.Column.GetFilterRestrictions().AllowedDateTimeFilters).Select<FilterDateType, DatePeriodFilterValue>(selector).ToList<DatePeriodFilterValue>();
        }

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__10))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__10 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__10>(ref d__);
            return d__.<>t__builder.Task;
        }

        public List<object> SelectedPredefinedFilters
        {
            get => 
                base.GetValue<List<object>>("SelectedPredefinedFilters");
            set => 
                base.SetValue<List<object>>(value, new Action(this.ApplyFilter), "SelectedPredefinedFilters");
        }

        public ReadOnlyCollection<DatePeriodFilterValue> PredefinedFilters
        {
            get => 
                base.GetValue<ReadOnlyCollection<DatePeriodFilterValue>>("PredefinedFilters");
            private set => 
                base.SetValue<ReadOnlyCollection<DatePeriodFilterValue>>(value, "PredefinedFilters");
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DatePeriodsFilterModel.<>c <>9 = new DatePeriodsFilterModel.<>c();
            public static Func<FilterDateType, DatePeriodFilterValue> <>9__9_0;
            public static Func<DatePeriodFilterValue, FilterDateType> <>9__11_0;

            internal FilterDateType <BuildFilter>b__11_0(DatePeriodFilterValue x) => 
                x.Value;

            internal DatePeriodFilterValue <GetPredefinedFilters>b__9_0(FilterDateType x) => 
                new DatePeriodFilterValue(x, EditorLocalizer.GetString(DatePeriodsFilterModel.GetLocalizationString(x)));
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__10 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public DatePeriodsFilterModel <>4__this;
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
                        goto TR_0006;
                    }
                    else
                    {
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0006;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, DatePeriodsFilterModel.<UpdateCoreAsync>d__10>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0006:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<>4__this.PredefinedFilters = this.<>4__this.GetPredefinedFilters().AsReadOnly();
                    if (this.<>4__this.Filter == null)
                    {
                        this.<>4__this.SelectedPredefinedFilters = null;
                    }
                    else
                    {
                        DatePeriodsFilterModel.<>c__DisplayClass10_0 class_;
                        List<FilterDateType> actualFilters = this.<>4__this.Filter.ToFilters(this.<>4__this.PropertyName, true).ToList<FilterDateType>();
                        this.<>4__this.SelectedPredefinedFilters = this.<>4__this.PredefinedFilters.Where<DatePeriodFilterValue>(new Func<DatePeriodFilterValue, bool>(class_.<UpdateCoreAsync>b__0)).ToList<object>();
                    }
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

