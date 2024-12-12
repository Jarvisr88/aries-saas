namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Xpf.Core.FilteringUI;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class DateRangesFilterInfo
    {
        private static DateTime? AddDaysSafe(this DateTime date)
        {
            if (date <= DateTime.MaxValue.AddDays(-1.0))
            {
                return new DateTime?(date.AddDays(1.0));
            }
            return null;
        }

        private static DateTime? AddMonthSafe(this DateTime date)
        {
            if (date <= DateTime.MaxValue.AddMonths(-1))
            {
                return new DateTime?(date.AddMonths(1));
            }
            return null;
        }

        private static DateTime? AddYearSafe(this DateTime date)
        {
            if (date <= DateTime.MaxValue.AddYears(-1))
            {
                return new DateTime?(date.AddYears(1));
            }
            return null;
        }

        public static IEnumerable<FilterDateRange> Create(IEnumerable<NodeBase<NodeValueInfo>> nodes)
        {
            List<FilterDateRange> ranges = new List<FilterDateRange>();
            FilterDateRange currentRange = null;
            foreach (NodeBase<NodeValueInfo> base2 in nodes)
            {
                NodeValueInfo info = base2.Value;
                DateTimeUnit unit = info.Value as DateTimeUnit;
                if (unit != null)
                {
                    currentRange = Process(ranges, currentRange, unit.DateTimeUnitType, unit.Date);
                    continue;
                }
                if (base2.Value.Value is DateTime)
                {
                    currentRange = Process(ranges, currentRange, DateTimeUnitType.Day, (DateTime) base2.Value.Value);
                }
            }
            if (currentRange != null)
            {
                ranges.Add(currentRange);
            }
            return ranges;
        }

        private static FilterDateRange CreateDateRange(DateTimeUnitType dateTimeUnitType, DateTime date)
        {
            switch (dateTimeUnitType)
            {
                case DateTimeUnitType.Year:
                    return new FilterDateRange(new DateTime(date.Year, 1, 1), new DateTime(date.Year, 1, 1).AddYearSafe());

                case DateTimeUnitType.Month:
                    return new FilterDateRange(new DateTime(date.Year, date.Month, 1), new DateTime(date.Year, date.Month, 1).AddMonthSafe());

                case DateTimeUnitType.Day:
                    return new FilterDateRange(date.Date, date.Date.AddDaysSafe());
            }
            throw new NotSupportedException();
        }

        private static FilterDateRange EnlargeGroup(List<FilterDateRange> ranges, FilterDateRange currentRange, FilterDateRange newRange)
        {
            if (currentRange != null)
            {
                if (currentRange.Enlarge(newRange))
                {
                    return currentRange;
                }
                ranges.Add(currentRange);
            }
            return newRange;
        }

        private static FilterDateRange Process(List<FilterDateRange> ranges, FilterDateRange currentRange, DateTimeUnitType dateTimeUnitType, DateTime date) => 
            EnlargeGroup(ranges, currentRange, CreateDateRange(dateTimeUnitType, date));
    }
}

