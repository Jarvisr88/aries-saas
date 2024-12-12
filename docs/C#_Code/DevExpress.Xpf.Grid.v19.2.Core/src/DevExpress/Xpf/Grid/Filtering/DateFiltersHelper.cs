namespace DevExpress.Xpf.Grid.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class DateFiltersHelper
    {
        private static readonly Interval[] Intervals;
        private static readonly Dictionary<FilterDateType, Interval> FilterToIntervalMap;
        private static readonly Dictionary<FunctionOperatorType, FilterDateType> IntervalToFilterMap;
        private static readonly FilterDateType[] EmptyFilters = new FilterDateType[0];
        private static readonly Tuple<DateTime?, DateTime?>[] EmptyDates = new Tuple<DateTime?, DateTime?>[0];

        static DateFiltersHelper()
        {
            FunctionOperatorType? start = null;
            FunctionOperatorType[] parts = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalPriorThisYear };
            Interval[] intervalArray1 = new Interval[13];
            intervalArray1[0] = new Interval(start, 0x36, parts);
            FunctionOperatorType[] typeArray2 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalEarlierThisYear };
            intervalArray1[1] = new Interval(0x36, 0x37, typeArray2);
            FunctionOperatorType[] typeArray3 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalEarlierThisMonth };
            intervalArray1[2] = new Interval(0x37, 0x38, typeArray3);
            FunctionOperatorType[] typeArray4 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalLastWeek };
            intervalArray1[3] = new Interval(0x38, 0x39, typeArray4);
            FunctionOperatorType[] typeArray5 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalEarlierThisWeek };
            intervalArray1[4] = new Interval(0x39, 0x3a, typeArray5);
            FunctionOperatorType[] typeArray6 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalYesterday };
            intervalArray1[5] = new Interval(0x3a, 0x3b, typeArray6);
            FunctionOperatorType[] typeArray7 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalToday };
            intervalArray1[6] = new Interval(0x3b, 0x3d, typeArray7);
            FunctionOperatorType[] typeArray8 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalTomorrow };
            intervalArray1[7] = new Interval(0x3d, 0x3e, typeArray8);
            FunctionOperatorType[] typeArray9 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalLaterThisWeek };
            intervalArray1[8] = new Interval(0x3e, 0x3f, typeArray9);
            FunctionOperatorType[] typeArray10 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalNextWeek };
            intervalArray1[9] = new Interval(0x3f, 0x40, typeArray10);
            FunctionOperatorType[] typeArray11 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalLaterThisMonth };
            intervalArray1[10] = new Interval(0x40, 0x41, typeArray11);
            FunctionOperatorType[] typeArray12 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalLaterThisYear };
            intervalArray1[11] = new Interval(0x41, 0x42, typeArray12);
            start = null;
            FunctionOperatorType[] typeArray13 = new FunctionOperatorType[] { FunctionOperatorType.IsOutlookIntervalBeyondThisYear };
            intervalArray1[12] = new Interval(0x42, start, typeArray13);
            Intervals = intervalArray1;
            Dictionary<FilterDateType, Interval> dictionary1 = new Dictionary<FilterDateType, Interval>();
            dictionary1.Add(FilterDateType.PriorThisYear, Intervals[0]);
            dictionary1.Add(FilterDateType.EarlierThisYear, Intervals[1]);
            dictionary1.Add(FilterDateType.EarlierThisMonth, Intervals[2]);
            dictionary1.Add(FilterDateType.LastWeek, Intervals[3]);
            dictionary1.Add(FilterDateType.EarlierThisWeek, Intervals[4]);
            dictionary1.Add(FilterDateType.Yesterday, Intervals[5]);
            dictionary1.Add(FilterDateType.Today, Intervals[6]);
            dictionary1.Add(FilterDateType.Tomorrow, Intervals[7]);
            dictionary1.Add(FilterDateType.LaterThisWeek, Intervals[8]);
            dictionary1.Add(FilterDateType.NextWeek, Intervals[9]);
            dictionary1.Add(FilterDateType.LaterThisMonth, Intervals[10]);
            dictionary1.Add(FilterDateType.LaterThisYear, Intervals[11]);
            dictionary1.Add(FilterDateType.BeyondThisYear, Intervals[12]);
            FilterToIntervalMap = dictionary1;
            IntervalToFilterMap = FilterToIntervalMap.ToDictionary<KeyValuePair<FilterDateType, Interval>, FunctionOperatorType, FilterDateType>(kv => kv.Value.Parts[0], kv => kv.Key);
        }

        private static DateTime AddMonthsAndGetBeginningOfMonth(this DateTime now, int months) => 
            now.AddMonths(months).BeginningOfMonth();

        private static CriteriaOperator AggregateWithOr(this IEnumerable<CriteriaOperator> source)
        {
            Func<CriteriaOperator, CriteriaOperator, CriteriaOperator> func = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Func<CriteriaOperator, CriteriaOperator, CriteriaOperator> local1 = <>c.<>9__20_0;
                func = <>c.<>9__20_0 = (acc, cur) => acc | cur;
            }
            return source.Aggregate<CriteriaOperator>(func);
        }

        private static bool AreFirstTwoFilters(FunctionOperatorType[] filters) => 
            (filters[0] == FunctionOperatorType.IsOutlookIntervalPriorThisYear) && (filters[1] == FunctionOperatorType.IsOutlookIntervalEarlierThisYear);

        private static bool AreLastTwoFilters(FunctionOperatorType[] filters) => 
            (filters[0] == FunctionOperatorType.IsOutlookIntervalLaterThisYear) && (filters[1] == FunctionOperatorType.IsOutlookIntervalBeyondThisYear);

        internal static DateTime BeginningOfMonth(this DateTime now) => 
            new DateTime(now.Year, now.Month, 1);

        private static bool CanCreateOrFilter(FunctionOperatorType[] filters) => 
            !AreFirstTwoFilters(filters) && !AreLastTwoFilters(filters);

        public static CriteriaOperator ExpandDates(this CriteriaOperator criteria) => 
            ActualDatesProcessor.Do(criteria);

        private static DateTime? ExtractDate(CriteriaOperator criteria, BinaryOperatorType type, OperandProperty property)
        {
            BinaryOperator @operator = criteria as BinaryOperator;
            if ((@operator != null) && ((@operator.OperatorType == type) && Equals(@operator.LeftOperand, property)))
            {
                OperandValue rightOperand = @operator.RightOperand as OperandValue;
                if ((rightOperand != null) && (rightOperand.Value is DateTime))
                {
                    return new DateTime?((DateTime) rightOperand.Value);
                }
            }
            return null;
        }

        private static FilterDateType[] ExtractFromBinary(CriteriaOperator criteria, OperandProperty property)
        {
            BinaryOperator binary = criteria as BinaryOperator;
            if (binary == null)
            {
                return EmptyFilters;
            }
            int? nullable = ExtractGreaterOrEqual(binary, property);
            if (nullable != null)
            {
                return GetIntervals(nullable.Value, Intervals.Length - 1);
            }
            int? nullable2 = ExtractLess(binary, property);
            return ((nullable2 == null) ? EmptyFilters : GetIntervals(0, nullable2.Value));
        }

        private static FilterDateType[] ExtractFromBinaryAlt(CriteriaOperator criteria, OperandProperty property)
        {
            DateTime? nullable = null;
            nullable = ExtractDate(criteria, BinaryOperatorType.Less, property);
            if ((nullable != null) && IsEarlier(nullable.Value))
            {
                return new FilterDateType[] { FilterDateType.Earlier };
            }
            nullable = ExtractDate(criteria, BinaryOperatorType.GreaterOrEqual, property);
            if ((nullable == null) || !IsBeyond(nullable.Value))
            {
                return EmptyFilters;
            }
            return new FilterDateType[] { FilterDateType.Beyond };
        }

        private static FilterDateType[] ExtractFromFunc(CriteriaOperator criteria, OperandProperty property)
        {
            FunctionOperator @operator = criteria as FunctionOperator;
            if ((@operator == null) || ((@operator.Operands.Count != 1) || !Equals(@operator.Operands[0], property)))
            {
                return EmptyFilters;
            }
            Func<Interval, FunctionOperatorType?> type = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<Interval, FunctionOperatorType?> local1 = <>c.<>9__24_0;
                type = <>c.<>9__24_0 = x => new FunctionOperatorType?(x.Parts[0]);
            }
            int? nullable = ExtractIndex(@operator, type);
            return ((nullable != null) ? GetIntervals(nullable.Value, nullable.Value) : EmptyFilters);
        }

        private static FilterDateType[] ExtractFromGroup(CriteriaOperator criteria, OperandProperty property)
        {
            GroupOperator @operator = criteria as GroupOperator;
            if ((@operator == null) || (@operator.Operands.Count != 2))
            {
                return EmptyFilters;
            }
            int? nullable = ExtractGreaterOrEqual(@operator.Operands[0] as BinaryOperator, property);
            int? nullable2 = ExtractLess(@operator.Operands[1] as BinaryOperator, property);
            return (((nullable == null) || (nullable2 == null)) ? EmptyFilters : GetIntervals(nullable.Value, nullable2.Value));
        }

        private static FilterDateType[] ExtractFromGroupAlt(CriteriaOperator criteria, OperandProperty property)
        {
            Tuple<DateTime?, DateTime?>[] tupleArray = ToDateSpansCore(criteria, property);
            if (tupleArray.Length == 0)
            {
                return EmptyFilters;
            }
            Tuple<DateTime?, DateTime?> pair = tupleArray[0];
            if (IsMonthAgo6(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo6 };
            }
            if (IsMonthAgo5(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo5 };
            }
            if (IsMonthAgo4(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo4 };
            }
            if (IsMonthAgo3(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo3 };
            }
            if (IsMonthAgo2(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo2 };
            }
            if (IsMonthAgo1(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAgo1 };
            }
            if (IsThisMonth(pair))
            {
                return new FilterDateType[] { FilterDateType.ThisMonth };
            }
            if (IsThisWeek(pair))
            {
                return new FilterDateType[] { FilterDateType.ThisWeek };
            }
            if (IsMonthAfter(pair))
            {
                return new FilterDateType[] { FilterDateType.MonthAfter1 };
            }
            if (!IsTwoMonthAfter(pair))
            {
                return EmptyFilters;
            }
            return new FilterDateType[] { FilterDateType.MonthAfter2 };
        }

        private static int? ExtractGreaterOrEqual(BinaryOperator binary, OperandProperty property)
        {
            if (!IsFilterCriteria(binary, property))
            {
                return null;
            }
            if (binary.OperatorType != BinaryOperatorType.GreaterOrEqual)
            {
                return null;
            }
            Func<Interval, FunctionOperatorType?> type = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Func<Interval, FunctionOperatorType?> local1 = <>c.<>9__27_0;
                type = <>c.<>9__27_0 = x => x.Start;
            }
            return ExtractIndex(binary.RightOperand, type);
        }

        private static int? ExtractIndex(CriteriaOperator criteria, Func<Interval, FunctionOperatorType?> type)
        {
            FunctionOperator func = criteria as FunctionOperator;
            if (func != null)
            {
                int num = Array.FindIndex<Interval>(Intervals, delegate (Interval x) {
                    FunctionOperatorType? nullable = type(x);
                    return (func.OperatorType == ((FunctionOperatorType) nullable.GetValueOrDefault())) ? (nullable != null) : false;
                });
                if (num != -1)
                {
                    return new int?(num);
                }
            }
            return null;
        }

        private static int? ExtractLess(BinaryOperator binary, OperandProperty property)
        {
            if (!IsFilterCriteria(binary, property))
            {
                return null;
            }
            if (binary.OperatorType != BinaryOperatorType.Less)
            {
                return null;
            }
            Func<Interval, FunctionOperatorType?> type = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<Interval, FunctionOperatorType?> local1 = <>c.<>9__28_0;
                type = <>c.<>9__28_0 = x => x.End;
            }
            return ExtractIndex(binary.RightOperand, type);
        }

        internal static DateTime FiveMonthsAgo(this DateTime now) => 
            now.MonthsAgo(5);

        internal static DateTime FourMonthsAgo(this DateTime now) => 
            now.MonthsAgo(4);

        private static IEnumerable<DateTime> GetDaysInSpan(Tuple<DateTime?, DateTime?> pair) => 
            GetDaysInSpan(pair.Item1.Value, pair.Item2.Value);

        internal static IEnumerable<DateTime> GetDaysInSpan(DateTime start, DateTime end)
        {
            TimeSpan span = end.Subtract(start);
            return (from d in Enumerable.Range(0, span.Days) select start.AddDays((double) d));
        }

        private static string GetFilterNameFromDate(DateTime date) => 
            date.ToString("yyyy, MMMM");

        private static FilterDateType[] GetIntervals(int from, int to)
        {
            Func<int, Interval> selector = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<int, Interval> local1 = <>c.<>9__31_0;
                selector = <>c.<>9__31_0 = i => Intervals[i];
            }
            Func<Interval, FilterDateType> func2 = <>c.<>9__31_1;
            if (<>c.<>9__31_1 == null)
            {
                Func<Interval, FilterDateType> local2 = <>c.<>9__31_1;
                func2 = <>c.<>9__31_1 = x => IntervalToFilterMap[x.Parts[0]];
            }
            return Enumerable.Range(from, (to + 1) - from).Select<int, Interval>(selector).Select<Interval, FilterDateType>(func2).ToArray<FilterDateType>();
        }

        private static bool IsBeyond(DateTime date) => 
            date == Today.ThreeMonthsAfter();

        internal static bool IsDateColumn(Type fieldType) => 
            (fieldType == typeof(DateTime)) || (fieldType == typeof(DateTime?));

        private static bool IsDateSpan(Tuple<DateTime?, DateTime?> pair, DateTime start, DateTime end)
        {
            DateTime? nullable = pair.Item1;
            DateTime? nullable2 = pair.Item2;
            if ((nullable == null) || (nullable2 == null))
            {
                return false;
            }
            DateTime? nullable3 = nullable;
            DateTime time = start;
            if (!((nullable3 != null) ? ((nullable3 != null) ? (nullable3.GetValueOrDefault() == time) : true) : false))
            {
                return false;
            }
            nullable3 = nullable2;
            time = end;
            return ((nullable3 != null) ? ((nullable3 != null) ? (nullable3.GetValueOrDefault() == time) : true) : false);
        }

        private static bool IsEarlier(DateTime date) => 
            date == Today.SixMonthsAgo();

        private static bool IsFilterCriteria(BinaryOperator binary, OperandProperty property) => 
            (binary != null) && Equals(binary.LeftOperand, property);

        public static bool IsFilterValid(this FilterDateType filter)
        {
            if ((filter == FilterDateType.None) || !FilterToIntervalMap.ContainsKey(filter))
            {
                return false;
            }
            Interval interval = FilterToIntervalMap[filter];
            return ((interval.Start == null) || ((interval.End == null) || (EvalHelpers.EvaluateLocalDateTime(interval.End.Value) > EvalHelpers.EvaluateLocalDateTime(interval.Start.Value))));
        }

        private static bool IsMonthAfter(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.MonthAfter(), Today.TwoMonthsAfter());

        private static bool IsMonthAgo1(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.MonthAgo(), Today.BeginningOfMonth());

        private static bool IsMonthAgo2(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.TwoMonthsAgo(), Today.MonthAgo());

        private static bool IsMonthAgo3(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.ThreeMonthsAgo(), Today.TwoMonthsAgo());

        private static bool IsMonthAgo4(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.FourMonthsAgo(), Today.ThreeMonthsAgo());

        private static bool IsMonthAgo5(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.FiveMonthsAgo(), Today.FourMonthsAgo());

        private static bool IsMonthAgo6(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.SixMonthsAgo(), Today.FiveMonthsAgo());

        private static bool IsThisMonth(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.BeginningOfMonth(), Today.MonthAfter());

        private static bool IsThisWeek(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.WeekStart(), Today.WeekAfter().WeekStart());

        private static bool IsTwoMonthAfter(Tuple<DateTime?, DateTime?> pair) => 
            IsDateSpan(pair, Today.TwoMonthsAfter(), Today.ThreeMonthsAfter());

        private static Interval[] Merge(IEnumerable<Interval> intervals)
        {
            if (!intervals.Any<Interval>())
            {
                return new Interval[] { Interval.Empty };
            }
            Stack<Interval> source = new Stack<Interval>();
            IEnumerable<Interval> enumerable = intervals.Skip<Interval>(1);
            source.Push(intervals.First<Interval>());
            foreach (Interval interval2 in enumerable)
            {
                Interval interval3 = source.Peek();
                if (!interval3.IsAdjacent(interval2))
                {
                    source.Push(interval2);
                    continue;
                }
                source.Pop();
                source.Push(interval3.Merge(interval2));
            }
            return source.Reverse<Interval>().ToArray<Interval>();
        }

        internal static DateTime MonthAfter(this DateTime now) => 
            now.MonthsAfter(1);

        internal static DateTime MonthAgo(this DateTime now) => 
            now.MonthsAgo(1);

        private static DateTime MonthsAfter(this DateTime now, int months) => 
            now.AddMonthsAndGetBeginningOfMonth(months);

        private static DateTime MonthsAgo(this DateTime now, int months) => 
            now.AddMonthsAndGetBeginningOfMonth(-months);

        internal static DateTime SixMonthsAgo(this DateTime now) => 
            now.MonthsAgo(6);

        internal static DateTime ThreeMonthsAfter(this DateTime now) => 
            now.MonthsAfter(3);

        internal static DateTime ThreeMonthsAgo(this DateTime now) => 
            now.MonthsAgo(3);

        internal static CriteriaOperator ToCriteria(this FilterDateType filter, string propertyName)
        {
            if (filter == FilterDateType.Empty)
            {
                return new UnaryOperator(UnaryOperatorType.IsNull, new OperandProperty(propertyName));
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(propertyName) };
            return new FunctionOperator(FilterToIntervalMap[filter].Parts[0], operands);
        }

        private static CriteriaOperator ToCriteria(IEnumerable<FilterDateType> filters, OperandProperty property)
        {
            Func<FilterDateType, bool> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<FilterDateType, bool> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = x => (x >= FilterDateType.PriorThisYear) && (x <= FilterDateType.BeyondThisYear);
            }
            Func<FilterDateType, FilterDateType> keySelector = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<FilterDateType, FilterDateType> local2 = <>c.<>9__10_1;
                keySelector = <>c.<>9__10_1 = x => x;
            }
            Func<FilterDateType, Interval> selector = <>c.<>9__10_2;
            if (<>c.<>9__10_2 == null)
            {
                Func<FilterDateType, Interval> local3 = <>c.<>9__10_2;
                selector = <>c.<>9__10_2 = x => FilterToIntervalMap[x];
            }
            return ToCriteria(Merge(filters.Where<FilterDateType>(predicate).OrderBy<FilterDateType, FilterDateType>(keySelector).Select<FilterDateType, Interval>(selector)), property);
        }

        public static CriteriaOperator ToCriteria(this IEnumerable<FilterDateType> filters, string fieldName)
        {
            Guard.ArgumentNotNull(fieldName, "fieldName");
            OperandProperty property = new OperandProperty(fieldName);
            return ((ToCriteria(filters, property) | ToCriteriaAlt(filters, property)) | ToNullCriteria(filters, property));
        }

        private static CriteriaOperator ToCriteria(Interval[] intervals, OperandProperty property) => 
            intervals.Select<Interval, CriteriaOperator>(delegate (Interval x) {
                FunctionOperatorType[] parts = x.Parts;
                if (parts.Length == 1)
                {
                    CriteriaOperator[] operatorArray1 = new CriteriaOperator[] { property };
                    return new FunctionOperator(parts[0], operatorArray1);
                }
                if ((parts.Length != 2) || !CanCreateOrFilter(parts))
                {
                    return ToLeftClosedInterval(x, property);
                }
                CriteriaOperator[] operands = new CriteriaOperator[] { property };
                CriteriaOperator[] operatorArray3 = new CriteriaOperator[] { property };
                return (new FunctionOperator(parts[0], operands) | new FunctionOperator(parts[1], operatorArray3));
            }).AggregateWithOr();

        private static CriteriaOperator ToCriteriaAlt(FilterDateType filter, OperandProperty property) => 
            (filter != FilterDateType.Earlier) ? ((filter != FilterDateType.MonthAgo6) ? ((filter != FilterDateType.MonthAgo5) ? ((filter != FilterDateType.MonthAgo4) ? ((filter != FilterDateType.MonthAgo3) ? ((filter != FilterDateType.MonthAgo2) ? ((filter != FilterDateType.MonthAgo1) ? ((filter != FilterDateType.ThisMonth) ? ((filter != FilterDateType.ThisWeek) ? ((filter != FilterDateType.MonthAfter1) ? ((filter != FilterDateType.MonthAfter2) ? ((filter != FilterDateType.Beyond) ? null : ((CriteriaOperator) (property >= Today.ThreeMonthsAfter()))) : ((CriteriaOperator) ((property >= Today.TwoMonthsAfter()) & (property < Today.ThreeMonthsAfter())))) : ((CriteriaOperator) ((property >= Today.MonthAfter()) & (property < Today.TwoMonthsAfter())))) : ((CriteriaOperator) ((property >= Today.WeekStart()) & (property < Today.WeekAfter().WeekStart())))) : ((CriteriaOperator) ((property >= Today.BeginningOfMonth()) & (property < Today.MonthAfter())))) : ((CriteriaOperator) ((property >= Today.MonthAgo()) & (property < Today.BeginningOfMonth())))) : ((CriteriaOperator) ((property >= Today.TwoMonthsAgo()) & (property < Today.MonthAgo())))) : ((CriteriaOperator) ((property >= Today.ThreeMonthsAgo()) & (property < Today.TwoMonthsAgo())))) : ((CriteriaOperator) ((property >= Today.FourMonthsAgo()) & (property < Today.ThreeMonthsAgo())))) : ((CriteriaOperator) ((property >= Today.FiveMonthsAgo()) & (property < Today.FourMonthsAgo())))) : ((CriteriaOperator) ((property >= Today.SixMonthsAgo()) & (property < Today.FiveMonthsAgo())))) : ((CriteriaOperator) (property < Today.SixMonthsAgo()));

        private static CriteriaOperator ToCriteriaAlt(IEnumerable<FilterDateType> filters, OperandProperty property)
        {
            Func<FilterDateType, bool> predicate = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<FilterDateType, bool> local1 = <>c.<>9__17_0;
                predicate = <>c.<>9__17_0 = x => (x >= FilterDateType.Earlier) && (x <= FilterDateType.Beyond);
            }
            Func<FilterDateType, FilterDateType> keySelector = <>c.<>9__17_1;
            if (<>c.<>9__17_1 == null)
            {
                Func<FilterDateType, FilterDateType> local2 = <>c.<>9__17_1;
                keySelector = <>c.<>9__17_1 = x => x;
            }
            IOrderedEnumerable<FilterDateType> enumerable = filters.Where<FilterDateType>(predicate).OrderBy<FilterDateType, FilterDateType>(keySelector);
            return ((filters.Count<FilterDateType>() != 0) ? (from x in filters select ToCriteriaAlt(x, property)).AggregateWithOr() : null);
        }

        public static DateTime[] ToDates(this CriteriaOperator criteria, string fieldName)
        {
            Guard.ArgumentNotNull(fieldName, "fieldName");
            Func<Tuple<DateTime?, DateTime?>, IEnumerable<DateTime>> selector = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Func<Tuple<DateTime?, DateTime?>, IEnumerable<DateTime>> local1 = <>c.<>9__51_0;
                selector = <>c.<>9__51_0 = span => GetDaysInSpan(span);
            }
            return criteria.ToDateSpans(fieldName).SelectMany<Tuple<DateTime?, DateTime?>, DateTime>(selector).ToArray<DateTime>();
        }

        private static Tuple<DateTime?, DateTime?>[] ToDateSpans(this CriteriaOperator criteria, string fieldName)
        {
            OperandProperty property = new OperandProperty(fieldName);
            GroupOperator @operator = criteria as GroupOperator;
            if ((@operator == null) || (@operator.OperatorType != GroupOperatorType.Or))
            {
                return ToDateSpansCore(criteria, property);
            }
            Func<Tuple<DateTime?, DateTime?>, bool> predicate = <>c.<>9__54_1;
            if (<>c.<>9__54_1 == null)
            {
                Func<Tuple<DateTime?, DateTime?>, bool> local1 = <>c.<>9__54_1;
                predicate = <>c.<>9__54_1 = x => (x.Item1 != null) && (x.Item2 != null);
            }
            return (from op in @operator.Operands select ToDateSpansCore(op, property)).Where<Tuple<DateTime?, DateTime?>>(predicate).ToArray<Tuple<DateTime?, DateTime?>>();
        }

        private static Tuple<DateTime?, DateTime?>[] ToDateSpansCore(CriteriaOperator criteria, OperandProperty property)
        {
            GroupOperator @operator = criteria as GroupOperator;
            if ((@operator == null) || ((@operator.OperatorType != GroupOperatorType.And) || (@operator.Operands.Count != 2)))
            {
                return EmptyDates;
            }
            DateTime? nullable = ExtractDate(@operator.Operands[0], BinaryOperatorType.GreaterOrEqual, property);
            DateTime? nullable2 = ExtractDate(@operator.Operands[1], BinaryOperatorType.Less, property);
            if (!((nullable != null) & (nullable2 != null)))
            {
                return EmptyDates;
            }
            return new Tuple<DateTime?, DateTime?>[] { Tuple.Create<DateTime?, DateTime?>(nullable, nullable2) };
        }

        private static FilterDateType[] ToFilters(CriteriaOperator criteria, OperandProperty property)
        {
            GroupOperator @operator = criteria as GroupOperator;
            return (((@operator == null) || (@operator.OperatorType != GroupOperatorType.Or)) ? ToFiltersCore(criteria, property) : (from op in @operator.Operands select ToFilters(op, property)).ToArray<FilterDateType>());
        }

        public static FilterDateType[] ToFilters(this CriteriaOperator criteria, string fieldName, bool useFiltersAlt = true)
        {
            Guard.ArgumentNotNull(fieldName, "fieldName");
            OperandProperty property = new OperandProperty(fieldName);
            FilterDateType[] first = ToFilters(criteria, property);
            FilterDateType[] second = criteria.ToNullFilter(property);
            return first.Concat<FilterDateType>((useFiltersAlt ? ((IEnumerable<FilterDateType>) criteria.ToFiltersAlt(property)) : ((IEnumerable<FilterDateType>) new FilterDateType[0]))).Concat<FilterDateType>(second).Distinct<FilterDateType>().OrderBy<FilterDateType, FilterDateType>((<>c.<>9__21_0 ??= x => x)).ToArray<FilterDateType>();
        }

        private static FilterDateType[] ToFiltersAlt(this CriteriaOperator criteria, OperandProperty property)
        {
            GroupOperator @operator = criteria as GroupOperator;
            return (((@operator == null) || (@operator.OperatorType != GroupOperatorType.Or)) ? criteria.ToFiltersAltCore(property) : (from x in @operator.Operands select x.ToFiltersAltCore(property)).ToArray<FilterDateType>());
        }

        private static FilterDateType[] ToFiltersAltCore(this CriteriaOperator criteria, OperandProperty property) => 
            !(criteria is BinaryOperator) ? (!(criteria is GroupOperator) ? EmptyFilters : ExtractFromGroupAlt(criteria, property)) : ExtractFromBinaryAlt(criteria, property);

        private static FilterDateType[] ToFiltersCore(CriteriaOperator criteria, OperandProperty property) => 
            !(criteria is FunctionOperator) ? (!(criteria is BinaryOperator) ? (!(criteria is GroupOperator) ? EmptyFilters : ExtractFromGroup(criteria, property)) : ExtractFromBinary(criteria, property)) : ExtractFromFunc(criteria, property);

        private static CriteriaOperator ToLeftClosedInterval(Interval x, OperandProperty property)
        {
            FunctionOperatorType? start = x.Start;
            FunctionOperatorType? end = x.End;
            CriteriaOperator @operator = null;
            CriteriaOperator operator2 = null;
            if (start != null)
            {
                @operator = (CriteriaOperator) (property >= new FunctionOperator(start.Value, new CriteriaOperator[0]));
            }
            if (end != null)
            {
                operator2 = (CriteriaOperator) (property < new FunctionOperator(end.Value, new CriteriaOperator[0]));
            }
            return (@operator & operator2);
        }

        private static CriteriaOperator ToNullCriteria(IEnumerable<FilterDateType> filters, OperandProperty property) => 
            filters.Contains<FilterDateType>(FilterDateType.Empty) ? new NullOperator(property) : null;

        private static FilterDateType[] ToNullFilter(this CriteriaOperator criteria, OperandProperty property)
        {
            GroupOperator @operator = criteria as GroupOperator;
            return (((@operator == null) || (@operator.OperatorType != GroupOperatorType.Or)) ? criteria.ToNullFilterCore(property) : (from x in @operator.Operands select x.ToNullFilterCore(property)).ToArray<FilterDateType>());
        }

        private static FilterDateType[] ToNullFilterCore(this CriteriaOperator criteria, OperandProperty property)
        {
            if (criteria is NullOperator)
            {
                return new FilterDateType[] { FilterDateType.Empty };
            }
            return EmptyFilters;
        }

        internal static DateTime TwoMonthsAfter(this DateTime now) => 
            now.MonthsAfter(2);

        internal static DateTime TwoMonthsAgo(this DateTime now) => 
            now.MonthsAgo(2);

        internal static DateTime WeekAfter(this DateTime now) => 
            now.AddDays(7.0);

        internal static DateTime WeekStart(this DateTime now) => 
            now.WeekStart(CultureInfo.CurrentCulture);

        internal static DateTime WeekStart(this DateTime now, CultureInfo culture)
        {
            int num = (int) (((now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek) + 7) % (DayOfWeek.Saturday | DayOfWeek.Monday));
            return now.AddDays((double) -num).Date;
        }

        private static DateTime Today =>
            DateTime.Today;

        internal static string SixMonthsAgoFilterName =>
            GetFilterNameFromDate(Today.SixMonthsAgo());

        internal static string FiveMonthsAgoFilterName =>
            GetFilterNameFromDate(Today.FiveMonthsAgo());

        internal static string FourMonthsAgoFilterName =>
            GetFilterNameFromDate(Today.FourMonthsAgo());

        internal static string ThreeMonthsAgoFilterName =>
            GetFilterNameFromDate(Today.ThreeMonthsAgo());

        internal static string TwoMonthsAgoFilterName =>
            GetFilterNameFromDate(Today.TwoMonthsAgo());

        internal static string MonthAgoFilterName =>
            GetFilterNameFromDate(Today.MonthAgo());

        internal static string MonthAfterFilterName =>
            GetFilterNameFromDate(Today.MonthAfter());

        internal static string TwoMonthsAfterFilterName =>
            GetFilterNameFromDate(Today.TwoMonthsAfter());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateFiltersHelper.<>c <>9 = new DateFiltersHelper.<>c();
            public static Func<FilterDateType, bool> <>9__10_0;
            public static Func<FilterDateType, FilterDateType> <>9__10_1;
            public static Func<FilterDateType, DateFiltersHelper.Interval> <>9__10_2;
            public static Func<FilterDateType, bool> <>9__17_0;
            public static Func<FilterDateType, FilterDateType> <>9__17_1;
            public static Func<CriteriaOperator, CriteriaOperator, CriteriaOperator> <>9__20_0;
            public static Func<FilterDateType, FilterDateType> <>9__21_0;
            public static Func<DateFiltersHelper.Interval, FunctionOperatorType?> <>9__24_0;
            public static Func<DateFiltersHelper.Interval, FunctionOperatorType?> <>9__27_0;
            public static Func<DateFiltersHelper.Interval, FunctionOperatorType?> <>9__28_0;
            public static Func<int, DateFiltersHelper.Interval> <>9__31_0;
            public static Func<DateFiltersHelper.Interval, FilterDateType> <>9__31_1;
            public static Func<Tuple<DateTime?, DateTime?>, IEnumerable<DateTime>> <>9__51_0;
            public static Func<Tuple<DateTime?, DateTime?>, bool> <>9__54_1;

            internal FunctionOperatorType <.cctor>b__6_0(KeyValuePair<FilterDateType, DateFiltersHelper.Interval> kv) => 
                kv.Value.Parts[0];

            internal FilterDateType <.cctor>b__6_1(KeyValuePair<FilterDateType, DateFiltersHelper.Interval> kv) => 
                kv.Key;

            internal CriteriaOperator <AggregateWithOr>b__20_0(CriteriaOperator acc, CriteriaOperator cur) => 
                acc | cur;

            internal FunctionOperatorType? <ExtractFromFunc>b__24_0(DateFiltersHelper.Interval x) => 
                new FunctionOperatorType?(x.Parts[0]);

            internal FunctionOperatorType? <ExtractGreaterOrEqual>b__27_0(DateFiltersHelper.Interval x) => 
                x.Start;

            internal FunctionOperatorType? <ExtractLess>b__28_0(DateFiltersHelper.Interval x) => 
                x.End;

            internal DateFiltersHelper.Interval <GetIntervals>b__31_0(int i) => 
                DateFiltersHelper.Intervals[i];

            internal FilterDateType <GetIntervals>b__31_1(DateFiltersHelper.Interval x) => 
                DateFiltersHelper.IntervalToFilterMap[x.Parts[0]];

            internal bool <ToCriteria>b__10_0(FilterDateType x) => 
                (x >= FilterDateType.PriorThisYear) && (x <= FilterDateType.BeyondThisYear);

            internal FilterDateType <ToCriteria>b__10_1(FilterDateType x) => 
                x;

            internal DateFiltersHelper.Interval <ToCriteria>b__10_2(FilterDateType x) => 
                DateFiltersHelper.FilterToIntervalMap[x];

            internal bool <ToCriteriaAlt>b__17_0(FilterDateType x) => 
                (x >= FilterDateType.Earlier) && (x <= FilterDateType.Beyond);

            internal FilterDateType <ToCriteriaAlt>b__17_1(FilterDateType x) => 
                x;

            internal IEnumerable<DateTime> <ToDates>b__51_0(Tuple<DateTime?, DateTime?> span) => 
                DateFiltersHelper.GetDaysInSpan(span);

            internal bool <ToDateSpans>b__54_1(Tuple<DateTime?, DateTime?> x) => 
                (x.Item1 != null) && (x.Item2 != null);

            internal FilterDateType <ToFilters>b__21_0(FilterDateType x) => 
                x;
        }

        private class ActualDatesProcessor : ClientCriteriaLazyPatcherBase.AggregatesAsIsBase
        {
            private static readonly DateFiltersHelper.ActualDatesProcessor Instance = new DateFiltersHelper.ActualDatesProcessor();

            private ActualDatesProcessor()
            {
            }

            public static CriteriaOperator Do(CriteriaOperator op) => 
                Instance.Process(op);

            public override CriteriaOperator Visit(FunctionOperator theOperator)
            {
                switch (theOperator.OperatorType)
                {
                    case FunctionOperatorType.LocalDateTimeThisYear:
                    case FunctionOperatorType.LocalDateTimeThisMonth:
                    case FunctionOperatorType.LocalDateTimeLastWeek:
                    case FunctionOperatorType.LocalDateTimeThisWeek:
                    case FunctionOperatorType.LocalDateTimeYesterday:
                    case FunctionOperatorType.LocalDateTimeToday:
                    case FunctionOperatorType.LocalDateTimeNow:
                    case FunctionOperatorType.LocalDateTimeTomorrow:
                    case FunctionOperatorType.LocalDateTimeDayAfterTomorrow:
                    case FunctionOperatorType.LocalDateTimeNextWeek:
                    case FunctionOperatorType.LocalDateTimeTwoWeeksAway:
                    case FunctionOperatorType.LocalDateTimeNextMonth:
                    case FunctionOperatorType.LocalDateTimeNextYear:
                        return new OperandValue(EvalHelpers.EvaluateLocalDateTime(theOperator.OperatorType));

                    case FunctionOperatorType.IsOutlookIntervalBeyondThisYear:
                    case FunctionOperatorType.IsOutlookIntervalLaterThisYear:
                    case FunctionOperatorType.IsOutlookIntervalLaterThisMonth:
                    case FunctionOperatorType.IsOutlookIntervalNextWeek:
                    case FunctionOperatorType.IsOutlookIntervalLaterThisWeek:
                    case FunctionOperatorType.IsOutlookIntervalTomorrow:
                    case FunctionOperatorType.IsOutlookIntervalToday:
                    case FunctionOperatorType.IsOutlookIntervalYesterday:
                    case FunctionOperatorType.IsOutlookIntervalEarlierThisWeek:
                    case FunctionOperatorType.IsOutlookIntervalLastWeek:
                    case FunctionOperatorType.IsOutlookIntervalEarlierThisMonth:
                    case FunctionOperatorType.IsOutlookIntervalEarlierThisYear:
                    case FunctionOperatorType.IsOutlookIntervalPriorThisYear:
                        return base.Process(EvalHelpers.ExpandIsOutlookInterval(theOperator));
                }
                return base.Visit(theOperator);
            }
        }

        private class Interval : IEquatable<DateFiltersHelper.Interval>
        {
            internal readonly FunctionOperatorType? Start;
            internal readonly FunctionOperatorType? End;
            internal readonly FunctionOperatorType[] Parts;

            internal Interval(FunctionOperatorType? start, FunctionOperatorType? end, params FunctionOperatorType[] parts)
            {
                this.Start = start;
                this.End = end;
                FunctionOperatorType[] typeArray1 = parts;
                if (parts == null)
                {
                    FunctionOperatorType[] local1 = parts;
                    typeArray1 = new FunctionOperatorType[0];
                }
                this.Parts = typeArray1;
            }

            public bool Equals(DateFiltersHelper.Interval other)
            {
                FunctionOperatorType? start = this.Start;
                FunctionOperatorType? end = other.Start;
                if (!((start.GetValueOrDefault() == end.GetValueOrDefault()) ? ((start != null) == (end != null)) : false))
                {
                    return false;
                }
                end = this.End;
                start = other.End;
                return ((end.GetValueOrDefault() == start.GetValueOrDefault()) ? ((end != null) == (start != null)) : false);
            }

            public override bool Equals(object obj) => 
                (obj != null) && (!(base.GetType() != obj.GetType()) && base.Equals((DateFiltersHelper.Interval) obj));

            public override int GetHashCode() => 
                this.ToString().GetHashCode();

            internal bool IsAdjacent(DateFiltersHelper.Interval other)
            {
                FunctionOperatorType? end = this.End;
                FunctionOperatorType? start = other.Start;
                return ((end.GetValueOrDefault() == start.GetValueOrDefault()) ? ((end != null) == (start != null)) : false);
            }

            internal DateFiltersHelper.Interval Merge(DateFiltersHelper.Interval other) => 
                new DateFiltersHelper.Interval(this.Start, other.End, this.Parts.Concat<FunctionOperatorType>(other.Parts).ToArray<FunctionOperatorType>());

            public override string ToString() => 
                $"[{this.Start} {this.End}]";

            internal static DateFiltersHelper.Interval Empty
            {
                get
                {
                    FunctionOperatorType? start = null;
                    start = null;
                    return new DateFiltersHelper.Interval(start, start, new FunctionOperatorType[0]);
                }
            }
        }
    }
}

