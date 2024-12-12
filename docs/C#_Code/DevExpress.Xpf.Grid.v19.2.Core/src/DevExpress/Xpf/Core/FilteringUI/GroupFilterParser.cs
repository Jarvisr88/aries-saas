namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class GroupFilterParser
    {
        private static GroupFilterInfo AsChecked(this IEnumerable<object> values, string propertyName) => 
            new GroupFilterInfo(propertyName, values.ToList<object>(), GroupFilterInfoValuesType.Checked, null);

        private static GroupFilterInfo AsUnchecked(this IEnumerable<object> values, string propertyName) => 
            new GroupFilterInfo(propertyName, values.ToList<object>(), GroupFilterInfoValuesType.Unchecked, null);

        public static bool? GetCheckState(this Either<GroupFilterInfo, bool> state)
        {
            Func<GroupFilterInfo, bool?> left = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<GroupFilterInfo, bool?> local1 = <>c.<>9__6_0;
                left = <>c.<>9__6_0 = (Func<GroupFilterInfo, bool?>) (_ => null);
            }
            return state.Match<bool?>(left, <>c.<>9__6_1 ??= isChecked => new bool?(isChecked));
        }

        public static GroupFilterInfo GetGroupFilterInfo(this Either<GroupFilterInfo, bool> state)
        {
            Func<GroupFilterInfo, GroupFilterInfo> left = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<GroupFilterInfo, GroupFilterInfo> local1 = <>c.<>9__7_0;
                left = <>c.<>9__7_0 = x => x;
            }
            return state.Match<GroupFilterInfo>(left, <>c.<>9__7_1 ??= ((Func<bool, GroupFilterInfo>) (_ => null)));
        }

        private static bool HasNullItems(this GroupFilterInfo[] infos)
        {
            Func<GroupFilterInfo, bool> predicate = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<GroupFilterInfo, bool> local1 = <>c.<>9__2_0;
                predicate = <>c.<>9__2_0 = x => ReferenceEquals(x, null);
            }
            return infos.Any<GroupFilterInfo>(predicate);
        }

        private static GroupFilterInfo MakeNullChecked(string propertyName) => 
            null.Yield<object>().AsChecked(propertyName);

        public static GroupFilterInfo Parse(CriteriaOperator filter, string field, string[] groupFields)
        {
            try
            {
                return ParseUnsafe(filter, field, groupFields);
            }
            catch
            {
                return null;
            }
        }

        private static GroupFilterInfo ParseUnsafe(CriteriaOperator filter, string field, string[] groupFields)
        {
            filter = BetweenDatesHelper.SubstituteDateInRange(filter);
            BinaryOperatorMapper<GroupFilterInfo> binary = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                BinaryOperatorMapper<GroupFilterInfo> local1 = <>c.<>9__1_0;
                binary = <>c.<>9__1_0 = (propertyName, value, type) => (type != BinaryOperatorType.Equal) ? ((type != BinaryOperatorType.NotEqual) ? null : value.Yield<object>().AsUnchecked(propertyName)) : value.Yield<object>().AsChecked(propertyName);
            }
            InOperatorMapper<GroupFilterInfo> mapper2 = <>c.<>9__1_2 ??= (propertyName, values) => values.AsChecked(propertyName);
            if (<>c.<>9__1_3 == null)
            {
                InOperatorMapper<GroupFilterInfo> local4 = <>c.<>9__1_2 ??= (propertyName, values) => values.AsChecked(propertyName);
                mapper2 = (InOperatorMapper<GroupFilterInfo>) (<>c.<>9__1_3 = delegate (string propertyName, object[] values, FunctionOperatorType type) {
                    if (type == FunctionOperatorType.IsNullOrEmpty)
                    {
                        return MakeNullChecked(propertyName);
                    }
                    ValueData[] dataArray = values.Select<object, ValueData>(new Func<object, ValueData>(ValueData.FromValue)).ToArray<ValueData>();
                    Attributed<ValueData[]> attributed = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<DateTime>(propertyName, dataArray, type);
                    if (attributed == null)
                    {
                        Attributed<ValueDataRange> range = BetweenDatesHelper.TryGetRangeFromSubstituted<DateTime>(propertyName, dataArray, type);
                        return (range == null) ? null : (from x in Enumerable.Range(0, (((DateTime) range.Value.To.ToValue()) - ((DateTime) range.Value.From.ToValue())).Days + 1) select ((DateTime) range.Value.From.ToValue()).AddDays((double) x)).AsChecked(propertyName);
                    }
                    Func<ValueData, object> selector = <>c.<>9__1_4;
                    if (<>c.<>9__1_4 == null)
                    {
                        Func<ValueData, object> local1 = <>c.<>9__1_4;
                        selector = <>c.<>9__1_4 = x => x.ToValue();
                    }
                    return attributed.Value.Select<ValueData, object>(selector).AsChecked(propertyName);
                });
            }
            GroupOperatorMapper<GroupFilterInfo> or = <>c.<>9__1_10;
            if (<>c.<>9__1_10 == null)
            {
                GroupOperatorMapper<GroupFilterInfo> local5 = <>c.<>9__1_10;
                or = <>c.<>9__1_10 = infos => !infos.HasNullItems() ? GroupFilterInfo.Merge(infos) : null;
            }
            GroupFilterInfo info = filter.Map<GroupFilterInfo>(binary, <>c.<>9__1_1 ??= (propertyName, type) => ((type != UnaryOperatorType.IsNull) ? null : MakeNullChecked(propertyName)), null, (BetweenOperatorMapper<GroupFilterInfo>) <>c.<>9__1_3, (FunctionOperatorMapper<GroupFilterInfo>) mapper2, delegate (GroupFilterInfo[] infos) {
                Func<GroupFilterInfo, int> <>9__7;
                if (infos.HasNullItems())
                {
                    return null;
                }
                Func<GroupFilterInfo, int> keySelector = <>9__7;
                if (<>9__7 == null)
                {
                    Func<GroupFilterInfo, int> local1 = <>9__7;
                    keySelector = <>9__7 = x => groupFields.IndexOf<string>(y => y == x.PropertyName);
                }
                List<GroupFilterInfo> source = infos.OrderByDescending<GroupFilterInfo, int>(keySelector).ToList<GroupFilterInfo>();
                return source.Skip<GroupFilterInfo>(1).Aggregate<GroupFilterInfo, GroupFilterInfo>(source[0], <>c.<>9__1_9 ??= (acc, info) => info.MakeIndeterminate(acc));
            }, or, <>c.<>9__1_11 ??= info => info.Invert(), <>c.<>9__1_12 ??= ((FallbackMapper<GroupFilterInfo>) (_ => null)), <>c.<>9__1_13 ??= ((NullMapper<GroupFilterInfo>) (() => null)));
            if (((info != null) ? info.PropertyName : null) != field)
            {
                info = null;
            }
            return (info ?? new GroupFilterInfo(field, new List<object>(), GroupFilterInfoValuesType.Checked, null));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupFilterParser.<>c <>9 = new GroupFilterParser.<>c();
            public static BinaryOperatorMapper<GroupFilterInfo> <>9__1_0;
            public static UnaryOperatorMapper<GroupFilterInfo> <>9__1_1;
            public static InOperatorMapper<GroupFilterInfo> <>9__1_2;
            public static Func<ValueData, object> <>9__1_4;
            public static FunctionOperatorMapper<GroupFilterInfo> <>9__1_3;
            public static Func<GroupFilterInfo, GroupFilterInfo, GroupFilterInfo> <>9__1_9;
            public static GroupOperatorMapper<GroupFilterInfo> <>9__1_10;
            public static NotOperatorMapper<GroupFilterInfo> <>9__1_11;
            public static FallbackMapper<GroupFilterInfo> <>9__1_12;
            public static NullMapper<GroupFilterInfo> <>9__1_13;
            public static Func<GroupFilterInfo, bool> <>9__2_0;
            public static Func<GroupFilterInfo, bool?> <>9__6_0;
            public static Func<bool, bool?> <>9__6_1;
            public static Func<GroupFilterInfo, GroupFilterInfo> <>9__7_0;
            public static Func<bool, GroupFilterInfo> <>9__7_1;

            internal bool? <GetCheckState>b__6_0(GroupFilterInfo _) => 
                null;

            internal bool? <GetCheckState>b__6_1(bool isChecked) => 
                new bool?(isChecked);

            internal GroupFilterInfo <GetGroupFilterInfo>b__7_0(GroupFilterInfo x) => 
                x;

            internal GroupFilterInfo <GetGroupFilterInfo>b__7_1(bool _) => 
                null;

            internal bool <HasNullItems>b__2_0(GroupFilterInfo x) => 
                ReferenceEquals(x, null);

            internal GroupFilterInfo <ParseUnsafe>b__1_0(string propertyName, object value, BinaryOperatorType type) => 
                (type != BinaryOperatorType.Equal) ? ((type != BinaryOperatorType.NotEqual) ? null : value.Yield<object>().AsUnchecked(propertyName)) : value.Yield<object>().AsChecked(propertyName);

            internal GroupFilterInfo <ParseUnsafe>b__1_1(string propertyName, UnaryOperatorType type) => 
                (type != UnaryOperatorType.IsNull) ? null : GroupFilterParser.MakeNullChecked(propertyName);

            internal GroupFilterInfo <ParseUnsafe>b__1_10(GroupFilterInfo[] infos) => 
                !infos.HasNullItems() ? GroupFilterInfo.Merge(infos) : null;

            internal GroupFilterInfo <ParseUnsafe>b__1_11(GroupFilterInfo info) => 
                info.Invert();

            internal GroupFilterInfo <ParseUnsafe>b__1_12(CriteriaOperator _) => 
                null;

            internal GroupFilterInfo <ParseUnsafe>b__1_13() => 
                null;

            internal GroupFilterInfo <ParseUnsafe>b__1_2(string propertyName, object[] values) => 
                values.AsChecked(propertyName);

            internal GroupFilterInfo <ParseUnsafe>b__1_3(string propertyName, object[] values, FunctionOperatorType type)
            {
                if (type == FunctionOperatorType.IsNullOrEmpty)
                {
                    return GroupFilterParser.MakeNullChecked(propertyName);
                }
                ValueData[] dataArray = values.Select<object, ValueData>(new Func<object, ValueData>(ValueData.FromValue)).ToArray<ValueData>();
                Attributed<ValueData[]> attributed = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<DateTime>(propertyName, dataArray, type);
                if (attributed == null)
                {
                    Attributed<ValueDataRange> range = BetweenDatesHelper.TryGetRangeFromSubstituted<DateTime>(propertyName, dataArray, type);
                    return ((range == null) ? null : (from x in Enumerable.Range(0, (((DateTime) range.Value.To.ToValue()) - ((DateTime) range.Value.From.ToValue())).Days + 1) select ((DateTime) range.Value.From.ToValue()).AddDays((double) x)).AsChecked(propertyName));
                }
                Func<ValueData, object> selector = <>9__1_4;
                if (<>9__1_4 == null)
                {
                    Func<ValueData, object> local1 = <>9__1_4;
                    selector = <>9__1_4 = x => x.ToValue();
                }
                return attributed.Value.Select<ValueData, object>(selector).AsChecked(propertyName);
            }

            internal object <ParseUnsafe>b__1_4(ValueData x) => 
                x.ToValue();

            internal GroupFilterInfo <ParseUnsafe>b__1_9(GroupFilterInfo acc, GroupFilterInfo info) => 
                info.MakeIndeterminate(acc);
        }
    }
}

