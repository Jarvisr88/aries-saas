namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class DateRangesFilterBuilder
    {
        public static unsafe CriteriaOperator Build(string propertyName, IEnumerable<FilterDateRange> dateRanges)
        {
            List<FilterDateRange> ranges = new List<FilterDateRange>();
            List<DateTime> dates = new List<DateTime>();
            foreach (FilterDateRange dateRange in dateRanges)
            {
                int? nullable4;
                TimeSpan? nullable1;
                int? nullable5;
                DateTime? end = dateRange.End;
                DateTime begin = dateRange.Begin;
                if (end != null)
                {
                    nullable1 = new TimeSpan?(end.GetValueOrDefault() - begin);
                }
                else
                {
                    nullable1 = null;
                }
                TimeSpan?* nullablePtr1 = &nullable1;
                if (nullablePtr1 != null)
                {
                    nullable5 = new int?(nullablePtr1.GetValueOrDefault().Days);
                }
                else
                {
                    TimeSpan?* local1 = nullablePtr1;
                    nullable4 = null;
                    nullable5 = nullable4;
                }
                int? nullable = nullable5;
                nullable4 = nullable;
                int num = 3;
                if ((nullable4.GetValueOrDefault() < num) ? (nullable4 != null) : false)
                {
                    dates.AddRange(from x in Enumerable.Range(0, nullable.Value) select dateRange.Begin.AddDays((double) x));
                }
                else
                {
                    ranges.Add(dateRange);
                }
            }
            return (CreateRangesFilter(propertyName, ranges) | CreateDatesFilter(propertyName, dates));
        }

        private static CriteriaOperator CreateDatesFilter(string propertyName, List<DateTime> dates) => 
            (dates.Count > 0) ? MultiselectRoundedDateTimeFilterHelper.DatesToCriteria(propertyName, dates) : null;

        private static CriteriaOperator CreateRangesFilter(string propertyName, List<FilterDateRange> ranges)
        {
            CriteriaOperator @operator = null;
            foreach (FilterDateRange range in ranges)
            {
                @operator |= RangeToCriteria(range, propertyName);
            }
            return @operator;
        }

        private static CriteriaOperator RangeToCriteria(FilterDateRange range, string propertyName)
        {
            BinaryOperator @operator = new BinaryOperator(propertyName, range.Begin, BinaryOperatorType.GreaterOrEqual);
            return ((range.End != null) ? (@operator & new BinaryOperator(propertyName, range.End.Value, BinaryOperatorType.Less)) : @operator);
        }
    }
}

