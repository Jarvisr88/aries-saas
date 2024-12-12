namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class DateRangeFilterModel : RangeFilterModelBase<DateTime>
    {
        internal DateRangeFilterModel(FilterModelClient client) : base(client)
        {
        }

        protected override CriteriaOperator BuildFilterCore() => 
            this.CanBuildFilterCore() ? (new BinaryOperator(base.PropertyName, base.From, BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(base.PropertyName, base.To.AddDays(1.0), BinaryOperatorType.Less)) : null;

        internal override bool CanBuildFilterCore() => 
            base.Column.GetFilterRestrictions().AllowedDateTimeFilters.HasFlag(AllowedDateTimeFilters.SingleDateRange);

        protected override Tuple<DateTime, DateTime> ParseFilter()
        {
            Func<GroupOperator, Tuple<DateTime, DateTime>> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<GroupOperator, Tuple<DateTime, DateTime>> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = delegate (GroupOperator groupOperator) {
                    Tuple<object, object> tuple = CriteriaRangeHelper.TryGetRoundedDateTimeRange(groupOperator, true).TryConvertToValueRange();
                    return (tuple != null) ? new Tuple<DateTime, DateTime>((DateTime) tuple.Item1, (DateTime) tuple.Item2) : null;
                };
            }
            return (base.Filter as GroupOperator).With<GroupOperator, Tuple<DateTime, DateTime>>(evaluator);
        }

        protected override Tuple<DateTime, DateTime> ValidateRange(Tuple<object, object> range) => 
            new Tuple<DateTime, DateTime>((DateTime) range.Item1, (DateTime) range.Item2);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateRangeFilterModel.<>c <>9 = new DateRangeFilterModel.<>c();
            public static Func<GroupOperator, Tuple<DateTime, DateTime>> <>9__2_0;

            internal Tuple<DateTime, DateTime> <ParseFilter>b__2_0(GroupOperator groupOperator)
            {
                Tuple<object, object> tuple = CriteriaRangeHelper.TryGetRoundedDateTimeRange(groupOperator, true).TryConvertToValueRange();
                return ((tuple != null) ? new Tuple<DateTime, DateTime>((DateTime) tuple.Item1, (DateTime) tuple.Item2) : null);
            }
        }
    }
}

