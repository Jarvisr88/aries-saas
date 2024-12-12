namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class RangeFilterModel : RangeFilterModelBase<decimal>
    {
        internal RangeFilterModel(FilterModelClient client) : base(client)
        {
        }

        protected override CriteriaOperator BuildFilterCore() => 
            this.IsBetweenAllowed() ? ((CriteriaOperator) new BetweenOperator(base.PropertyName, base.From, base.To)) : (this.IsGreaterLessAllowed() ? ((CriteriaOperator) (new BinaryOperator(base.PropertyName, base.From, BinaryOperatorType.GreaterOrEqual) & new BinaryOperator(base.PropertyName, base.To, BinaryOperatorType.LessOrEqual))) : null);

        internal override bool CanBuildFilterCore()
        {
            FilterRestrictions restrictions = base.Column.GetFilterRestrictions();
            return (this.IsBetweenAllowed() || this.IsGreaterLessAllowed());
        }

        private bool IsBetweenAllowed() => 
            base.Column.GetFilterRestrictions().AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.Between);

        private bool IsGreaterLessAllowed()
        {
            AllowedBinaryFilters allowedBinaryFilters = base.Column.GetFilterRestrictions().AllowedBinaryFilters;
            return (allowedBinaryFilters.HasFlag(AllowedBinaryFilters.LessOrEqual) && allowedBinaryFilters.HasFlag(AllowedBinaryFilters.GreaterOrEqual));
        }

        protected override Tuple<decimal, decimal> ParseFilter()
        {
            BetweenOperatorMapper<Tuple<decimal, decimal>> between = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                BetweenOperatorMapper<Tuple<decimal, decimal>> local1 = <>c.<>9__2_0;
                between = <>c.<>9__2_0 = (name, from, to) => new Tuple<decimal, decimal>(Convert.ToDecimal(from), Convert.ToDecimal(to));
            }
            FallbackMapper<Tuple<decimal, decimal>> fallback = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                FallbackMapper<Tuple<decimal, decimal>> local2 = <>c.<>9__2_1;
                fallback = <>c.<>9__2_1 = (FallbackMapper<Tuple<decimal, decimal>>) (_ => null);
            }
            Tuple<decimal, decimal> local3 = base.Filter.Map<Tuple<decimal, decimal>>(null, null, null, between, null, null, null, null, fallback, null);
            Tuple<decimal, decimal> local6 = local3;
            if (local3 == null)
            {
                Tuple<decimal, decimal> local4 = local3;
                local6 = (base.Filter as GroupOperator).With<GroupOperator, Tuple<decimal, decimal>>(<>c.<>9__2_2 ??= delegate (GroupOperator x) {
                    Tuple<object, object> tuple = CriteriaRangeHelper.TryGetRegularRange<decimal>(x, true).TryConvertToValueRange();
                    return ((tuple != null) ? new Tuple<decimal, decimal>(Convert.ToDecimal(tuple.Item1), Convert.ToDecimal(tuple.Item2)) : null);
                });
            }
            return local6;
        }

        protected override Tuple<decimal, decimal> ValidateRange(Tuple<object, object> range) => 
            new Tuple<decimal, decimal>(Convert.ToDecimal(range.Item1), Convert.ToDecimal(range.Item2));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeFilterModel.<>c <>9 = new RangeFilterModel.<>c();
            public static BetweenOperatorMapper<Tuple<decimal, decimal>> <>9__2_0;
            public static FallbackMapper<Tuple<decimal, decimal>> <>9__2_1;
            public static Func<GroupOperator, Tuple<decimal, decimal>> <>9__2_2;

            internal Tuple<decimal, decimal> <ParseFilter>b__2_0(string name, object from, object to) => 
                new Tuple<decimal, decimal>(Convert.ToDecimal(from), Convert.ToDecimal(to));

            internal Tuple<decimal, decimal> <ParseFilter>b__2_1(CriteriaOperator _) => 
                null;

            internal Tuple<decimal, decimal> <ParseFilter>b__2_2(GroupOperator x)
            {
                Tuple<object, object> tuple = CriteriaRangeHelper.TryGetRegularRange<decimal>(x, true).TryConvertToValueRange();
                return ((tuple != null) ? new Tuple<decimal, decimal>(Convert.ToDecimal(tuple.Item1), Convert.ToDecimal(tuple.Item2)) : null);
            }
        }
    }
}

