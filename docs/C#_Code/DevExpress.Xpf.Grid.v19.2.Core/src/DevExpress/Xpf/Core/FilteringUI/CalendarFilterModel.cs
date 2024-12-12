namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Grid.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class CalendarFilterModel : FilterModel
    {
        internal CalendarFilterModel(FilterModelClient client) : base(client)
        {
            this.AllowMultipleDateRanges = this.CanBuildMultipleDateRangesFilter();
        }

        [CompilerGenerated, DebuggerHidden]
        private Task <>n__0() => 
            base.UpdateCoreAsync();

        internal override CriteriaOperator BuildFilter() => 
            (this.SelectedDates != null) ? MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(base.PropertyName, this.SelectedDates) : null;

        internal override bool CanBuildFilterCore() => 
            this.CanBuildSingleDateRangeFilter() || this.CanBuildMultipleDateRangesFilter();

        private bool CanBuildMultipleDateRangesFilter() => 
            base.Column.GetFilterRestrictions().AllowedDateTimeFilters.HasFlag(AllowedDateTimeFilters.MultipleDateRanges);

        private bool CanBuildSingleDateRangeFilter() => 
            base.Column.GetFilterRestrictions().AllowedDateTimeFilters.HasFlag(AllowedDateTimeFilters.SingleDateRange);

        [AsyncStateMachine(typeof(<UpdateCoreAsync>d__1))]
        internal override Task UpdateCoreAsync()
        {
            <UpdateCoreAsync>d__1 d__;
            d__.<>4__this = this;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<UpdateCoreAsync>d__1>(ref d__);
            return d__.<>t__builder.Task;
        }

        public IList<DateTime> SelectedDates
        {
            get => 
                base.GetValue<IList<DateTime>>("SelectedDates");
            set => 
                base.SetValue<IList<DateTime>>(value, new Action(this.ApplyFilter), "SelectedDates");
        }

        public bool AllowMultipleDateRanges
        {
            get => 
                base.GetValue<bool>("AllowMultipleDateRanges");
            private set => 
                base.SetValue<bool>(value, "AllowMultipleDateRanges");
        }

        [CompilerGenerated]
        private struct <UpdateCoreAsync>d__1 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public CalendarFilterModel <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    List<DateTime> list1;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                        goto TR_0007;
                    }
                    else
                    {
                        awaiter = this.<>4__this.<>n__0().GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0007;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CalendarFilterModel.<UpdateCoreAsync>d__1>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0007:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    CriteriaOperator filter = this.<>4__this.Filter;
                    if (filter != null)
                    {
                        list1 = filter.ToDates(this.<>4__this.PropertyName).ToList<DateTime>();
                    }
                    else
                    {
                        CriteriaOperator local1 = filter;
                        list1 = null;
                    }
                    this.<>4__this.SelectedDates = list1;
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

