namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class CriteriaConverterFactory
    {
        public static CriteriaConverter<Tuple<ValueData, ValueData>> CreateBetween() => 
            new CriteriaConverter<Tuple<ValueData, ValueData>>(<>c.<>9__8_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData, ValueData, Option<Tuple<ValueData, ValueData>>> between = <>c.<>9__8_1;
                if (<>c.<>9__8_1 == null)
                {
                    Func<string, ValueData, ValueData, Option<Tuple<ValueData, ValueData>>> local1 = <>c.<>9__8_1;
                    between = <>c.<>9__8_1 = (_, beginVal, endVal) => new Tuple<ValueData, ValueData>(beginVal, endVal).ToOption<Tuple<ValueData, ValueData>>();
                }
                NullMapper<Tuple<ValueData, ValueData>> @null = <>c.<>9__8_2;
                if (<>c.<>9__8_2 == null)
                {
                    NullMapper<Tuple<ValueData, ValueData>> local2 = <>c.<>9__8_2;
                    @null = <>c.<>9__8_2 = () => new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue);
                }
                return filter.MapExtended<Tuple<ValueData, ValueData>>(null, null, null, between, null, null, null, null, null, @null);
            }, <>c.<>9__8_3 ??= (valuesData, propertyName) => new BetweenOperator(propertyName.ToProperty(), valuesData.Item1.ToCriteria(), valuesData.Item2.ToCriteria()), <>c.<>9__8_4 ??= restrictions => restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.Between));

        public static CriteriaConverter<Tuple<ValueData, ValueData>> CreateBetweenDates() => 
            new CriteriaConverter<Tuple<ValueData, ValueData>>(<>c.<>9__9_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> function = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> local1 = <>c.<>9__9_1;
                    function = <>c.<>9__9_1 = delegate (string propertyName, ValueData[] valuesData, FunctionOperatorType operatorType) {
                        Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> evaluator = <>c.<>9__9_2;
                        if (<>c.<>9__9_2 == null)
                        {
                            Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> local1 = <>c.<>9__9_2;
                            evaluator = <>c.<>9__9_2 = range => new Tuple<ValueData, ValueData>(range.Value.From, range.Value.To);
                        }
                        return TryGetRangeFromSubstitutedValuesData(propertyName, valuesData, operatorType).Return<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>>(evaluator, (<>c.<>9__9_3 ??= ((Func<Tuple<ValueData, ValueData>>) (() => null)))).ToOption<Tuple<ValueData, ValueData>>();
                    };
                }
                FallbackMapper<Tuple<ValueData, ValueData>> fallback = <>c.<>9__9_4;
                if (<>c.<>9__9_4 == null)
                {
                    FallbackMapper<Tuple<ValueData, ValueData>> local2 = <>c.<>9__9_4;
                    fallback = <>c.<>9__9_4 = (FallbackMapper<Tuple<ValueData, ValueData>>) (_ => null);
                }
                return filter.MapExtended<Tuple<ValueData, ValueData>>(null, null, null, null, function, null, null, null, fallback, (<>c.<>9__9_5 ??= () => new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue)));
            }, <>c.<>9__9_6 ??= delegate (Tuple<ValueData, ValueData> valuesData, string propertyName) {
                bool flag1;
                ValueData local1 = valuesData.Item1;
                if (local1 != null)
                {
                    flag1 = !local1.IsNull;
                }
                else
                {
                    ValueData local2 = local1;
                    flag1 = false;
                }
                if (flag1)
                {
                    bool flag2;
                    ValueData local3 = valuesData.Item2;
                    if (local3 != null)
                    {
                        flag2 = !local3.IsNull;
                    }
                    else
                    {
                        ValueData local4 = local3;
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        return BetweenDatesHelper.CreateBetweenDatesFunction(propertyName.ToProperty(), valuesData.Item1, valuesData.Item2);
                    }
                }
                return null;
            }, <>c.<>9__9_7 ??= restrictions => restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.BetweenDates));

        public static CriteriaConverter<ValueData> CreateBinary(BinaryOperatorType type) => 
            new CriteriaConverter<ValueData>(delegate (CriteriaOperator filter) {
                Func<string, ValueData, BinaryOperatorType, Option<ValueData>> <>9__1;
                Func<string, ValueData, BinaryOperatorType, Option<ValueData>> binary = <>9__1;
                if (<>9__1 == null)
                {
                    Func<string, ValueData, BinaryOperatorType, Option<ValueData>> local1 = <>9__1;
                    binary = <>9__1 = (_, val, operatorType) => ((operatorType == type) ? val : InvalidFilterMap<ValueData>()).ToOption<ValueData>();
                }
                NullMapper<ValueData> @null = <>c.<>9__1_2;
                if (<>c.<>9__1_2 == null)
                {
                    NullMapper<ValueData> local2 = <>c.<>9__1_2;
                    @null = <>c.<>9__1_2 = () => ValueData.NullValue;
                }
                return filter.MapExtended<ValueData>(binary, null, null, null, null, null, null, null, null, @null);
            }, (valueData, propertyName) => new BinaryOperator(propertyName.ToProperty(), valueData.ToCriteria(), type), restrictions => restrictions.Allow(type));

        public static CriteriaConverter<ValueData> CreateBinaryFunction(FunctionOperatorType type) => 
            new CriteriaConverter<ValueData>(delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> <>9__1;
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>9__1;
                if (<>9__1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>9__1;
                    function = <>9__1 = (_, valuesData, operatorType) => ((operatorType == type) ? valuesData.Single<ValueData>() : InvalidFilterMap<ValueData>()).ToOption<ValueData>();
                }
                NullMapper<ValueData> @null = <>c.<>9__3_2;
                if (<>c.<>9__3_2 == null)
                {
                    NullMapper<ValueData> local2 = <>c.<>9__3_2;
                    @null = <>c.<>9__3_2 = (NullMapper<ValueData>) (() => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, null, @null);
            }, (valueData, propertyName) => new FunctionOperator(type, new CriteriaOperator[] { propertyName.ToProperty(), valueData.ToCriteria() }), restrictions => restrictions.Allow(type));

        public static CriteriaConverter<ValueData> CreateCustomBinaryFunction(string customFunctionName)
        {
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__4_4;
            if (<>c.<>9__4_4 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__4_4;
                canBuildFilter = <>c.<>9__4_4 = _ => true;
            }
            return new CriteriaConverter<ValueData>(delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> <>9__1;
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>9__1;
                if (<>9__1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>9__1;
                    function = <>9__1 = (_, valuesData, operatorType) => (IsCustomFunction(operatorType, valuesData, customFunctionName) ? valuesData[1] : InvalidFilterMap<ValueData>()).ToOption<ValueData>();
                }
                NullMapper<ValueData> @null = <>c.<>9__4_2;
                if (<>c.<>9__4_2 == null)
                {
                    NullMapper<ValueData> local2 = <>c.<>9__4_2;
                    @null = <>c.<>9__4_2 = (NullMapper<ValueData>) (() => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, null, @null);
            }, (valueData, propertyName) => new FunctionOperator(customFunctionName, new CriteriaOperator[] { propertyName.ToProperty(), valueData.ToCriteria() }), canBuildFilter);
        }

        public static CriteriaConverter<Tuple<ValueData, ValueData>> CreateCustomTernaryFunction(string customFunctionName)
        {
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__5_4;
            if (<>c.<>9__5_4 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__5_4;
                canBuildFilter = <>c.<>9__5_4 = _ => true;
            }
            return new CriteriaConverter<Tuple<ValueData, ValueData>>(delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> <>9__1;
                Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> function = <>9__1;
                if (<>9__1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> local1 = <>9__1;
                    function = <>9__1 = (_, valuesData, operatorType) => !IsCustomFunction(operatorType, valuesData, customFunctionName) ? InvalidFilterMap<Tuple<ValueData, ValueData>>().ToOption<Tuple<ValueData, ValueData>>() : new Tuple<ValueData, ValueData>(valuesData[1], valuesData[2]).ToOption<Tuple<ValueData, ValueData>>();
                }
                NullMapper<Tuple<ValueData, ValueData>> @null = <>c.<>9__5_2;
                if (<>c.<>9__5_2 == null)
                {
                    NullMapper<Tuple<ValueData, ValueData>> local2 = <>c.<>9__5_2;
                    @null = <>c.<>9__5_2 = (NullMapper<Tuple<ValueData, ValueData>>) (() => null);
                }
                return filter.MapExtended<Tuple<ValueData, ValueData>>(null, null, null, null, function, null, null, null, null, @null);
            }, (valuesData, propertyName) => new FunctionOperator(customFunctionName, new CriteriaOperator[] { propertyName.ToProperty(), valuesData.Item1.ToCriteria(), valuesData.Item2.ToCriteria() }), canBuildFilter);
        }

        public static CriteriaConverter<ValueData[]> CreateCustomVariadicFunction(string customFunctionName)
        {
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__6_5;
            if (<>c.<>9__6_5 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__6_5;
                canBuildFilter = <>c.<>9__6_5 = _ => true;
            }
            return new CriteriaConverter<ValueData[]>(delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> <>9__1;
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> function = <>9__1;
                if (<>9__1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> local1 = <>9__1;
                    function = <>9__1 = (_, valuesData, operatorType) => !IsCustomFunction(operatorType, valuesData, customFunctionName) ? InvalidFilterMap<ValueData[]>().ToOption<ValueData[]>() : valuesData.Skip<ValueData>(1).ToArray<ValueData>().ToOption<ValueData[]>();
                }
                NullMapper<ValueData[]> @null = <>c.<>9__6_2;
                if (<>c.<>9__6_2 == null)
                {
                    NullMapper<ValueData[]> local2 = <>c.<>9__6_2;
                    @null = <>c.<>9__6_2 = () => new ValueData[] { ValueData.NullValue };
                }
                return filter.MapExtended<ValueData[]>(null, null, null, null, function, null, null, null, null, @null);
            }, delegate (ValueData[] valuesData, string propertyName) {
                Func<ValueData, CriteriaOperator> selector = <>c.<>9__6_4;
                if (<>c.<>9__6_4 == null)
                {
                    Func<ValueData, CriteriaOperator> local1 = <>c.<>9__6_4;
                    selector = <>c.<>9__6_4 = x => x.ToCriteria();
                }
                return new FunctionOperator(customFunctionName, propertyName.ToProperty().Yield<CriteriaOperator>().Concat<CriteriaOperator>(valuesData.Select<ValueData, CriteriaOperator>(selector)));
            }, canBuildFilter);
        }

        public static CriteriaConverter<ValueData[]> CreateIsAnyOf() => 
            new CriteriaConverter<ValueData[]>(<>c.<>9__2_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData[], Option<ValueData[]>> @in = <>c.<>9__2_1;
                if (<>c.<>9__2_1 == null)
                {
                    Func<string, ValueData[], Option<ValueData[]>> local1 = <>c.<>9__2_1;
                    @in = <>c.<>9__2_1 = (_, values) => values.ToOption<ValueData[]>();
                }
                NullMapper<ValueData[]> @null = <>c.<>9__2_2;
                if (<>c.<>9__2_2 == null)
                {
                    NullMapper<ValueData[]> local2 = <>c.<>9__2_2;
                    @null = <>c.<>9__2_2 = () => new ValueData[] { ValueData.NullValue };
                }
                return filter.MapExtended<ValueData[]>(null, null, @in, null, null, null, null, null, null, @null);
            }, <>c.<>9__2_3 ??= delegate (ValueData[] valuesData, string propertyName) {
                Func<ValueData, CriteriaOperator> selector = <>c.<>9__2_4;
                if (<>c.<>9__2_4 == null)
                {
                    Func<ValueData, CriteriaOperator> local1 = <>c.<>9__2_4;
                    selector = <>c.<>9__2_4 = x => x.ToCriteria();
                }
                return new InOperator(propertyName.ToProperty(), valuesData.Select<ValueData, CriteriaOperator>(selector));
            }, <>c.<>9__2_5 ??= restrictions => restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.AnyOf));

        public static CriteriaConverter<ValueData> CreateIsNotOnDate()
        {
            CriteriaConverter<ValueData> dateEquals = CreateIsOnDate();
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__15_1;
            if (<>c.<>9__15_1 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__15_1;
                canBuildFilter = <>c.<>9__15_1 = restrictions => restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsNotOnDate);
            }
            return new CriteriaConverter<ValueData>(Not<ValueData>(dateEquals.ToValue), (valueData, propertyName) => !((valueData != null) ? valueData.IsNull : true) ? Not<ValueData>(dateEquals.ToCriteria)(valueData, propertyName) : null, canBuildFilter);
        }

        public static CriteriaConverter<ValueData> CreateIsOnDate() => 
            new CriteriaConverter<ValueData>(<>c.<>9__14_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>c.<>9__14_1;
                if (<>c.<>9__14_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>c.<>9__14_1;
                    function = <>c.<>9__14_1 = (propertyName, values, operatorType) => BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? values.Skip<ValueData>(1).Single<ValueData>().ToOption<ValueData>() : Option<ValueData>.Empty;
                }
                FallbackMapper<ValueData> fallback = <>c.<>9__14_2;
                if (<>c.<>9__14_2 == null)
                {
                    FallbackMapper<ValueData> local2 = <>c.<>9__14_2;
                    fallback = <>c.<>9__14_2 = (FallbackMapper<ValueData>) (_ => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, fallback, (<>c.<>9__14_3 ??= ((NullMapper<ValueData>) (() => null))));
            }, <>c.<>9__14_4 ??= (valueData, propertyName) => TryCreateIsOnDatesCriteria(propertyName, valueData.Yield<ValueData>()), <>c.<>9__14_5 ??= restrictions => restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsOnDate));

        public static CriteriaConverter<ValueData[]> CreateIsOnDates() => 
            new CriteriaConverter<ValueData[]>(<>c.<>9__13_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> function = <>c.<>9__13_1;
                if (<>c.<>9__13_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> local1 = <>c.<>9__13_1;
                    function = <>c.<>9__13_1 = (propertyName, values, operatorType) => !BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? Option<ValueData[]>.Empty : values.Skip<ValueData>(1).ToArray<ValueData>().ToOption<ValueData[]>();
                }
                NullMapper<ValueData[]> @null = <>c.<>9__13_2;
                if (<>c.<>9__13_2 == null)
                {
                    NullMapper<ValueData[]> local2 = <>c.<>9__13_2;
                    @null = <>c.<>9__13_2 = () => new ValueData[] { ValueData.NullValue };
                }
                return filter.MapExtended<ValueData[]>(null, null, null, null, function, null, null, null, null, @null);
            }, <>c.<>9__13_3 ??= (valuesData, propertyName) => TryCreateIsOnDatesCriteria(propertyName, valuesData), <>c.<>9__13_4 ??= restrictions => restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsOnDates));

        public static CriteriaConverter<ValueData> CreateLike() => 
            new CriteriaConverter<ValueData>(<>c.<>9__22_0 ??= delegate (CriteriaOperator filter) {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>c.<>9__22_1;
                if (<>c.<>9__22_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>c.<>9__22_1;
                    function = <>c.<>9__22_1 = delegate (string propertyName, ValueData[] values, FunctionOperatorType operatorType) {
                        Func<ValueData[], ValueData> evaluator = <>c.<>9__22_2;
                        if (<>c.<>9__22_2 == null)
                        {
                            Func<ValueData[], ValueData> local1 = <>c.<>9__22_2;
                            evaluator = <>c.<>9__22_2 = x => x[1];
                        }
                        return values.Return<ValueData[], ValueData>(evaluator, new Func<ValueData>(CriteriaConverterFactory.InvalidFilterMap<ValueData>)).ToOption<ValueData>();
                    };
                }
                NullMapper<ValueData> @null = <>c.<>9__22_3;
                if (<>c.<>9__22_3 == null)
                {
                    NullMapper<ValueData> local2 = <>c.<>9__22_3;
                    @null = <>c.<>9__22_3 = (NullMapper<ValueData>) (() => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, null, @null);
            }, <>c.<>9__22_4 ??= (valueData, propertyName) => ((valueData == null) ? null : LikeCustomFunction.Create(propertyName.ToProperty(), valueData.ToCriteria())), <>c.<>9__22_5 ??= restrictions => restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.Like));

        public static CriteriaConverter<ValueData[]> CreateNoneOf()
        {
            CriteriaConverter<ValueData[]> converter = CreateIsAnyOf();
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__16_0;
                canBuildFilter = <>c.<>9__16_0 = restrictions => restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.NoneOf);
            }
            return new CriteriaConverter<ValueData[]>(Not<ValueData[]>(converter.ToValue), Not<ValueData[]>(converter.ToCriteria), canBuildFilter);
        }

        public static CriteriaConverter<Tuple<ValueData, ValueData>> CreateNotBetween()
        {
            CriteriaConverter<Tuple<ValueData, ValueData>> converter = CreateBetween();
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__17_0;
                canBuildFilter = <>c.<>9__17_0 = restrictions => restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.NotBetween);
            }
            return new CriteriaConverter<Tuple<ValueData, ValueData>>(Not<Tuple<ValueData, ValueData>>(converter.ToValue), Not<Tuple<ValueData, ValueData>>(converter.ToCriteria), canBuildFilter);
        }

        public static CriteriaConverter<ValueData> CreateNotContains()
        {
            CriteriaConverter<ValueData> converter = CreateBinaryFunction(FunctionOperatorType.Contains);
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__18_0;
                canBuildFilter = <>c.<>9__18_0 = restrictions => restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.DoesNotContain);
            }
            return new CriteriaConverter<ValueData>(Not<ValueData>(converter.ToValue), Not<ValueData>(converter.ToCriteria), canBuildFilter);
        }

        public static CriteriaConverter<ValueData> CreateNotLike()
        {
            CriteriaConverter<ValueData> like = CreateLike();
            Func<FilterRestrictions, bool> canBuildFilter = <>c.<>9__23_1;
            if (<>c.<>9__23_1 == null)
            {
                Func<FilterRestrictions, bool> local1 = <>c.<>9__23_1;
                canBuildFilter = <>c.<>9__23_1 = restrictions => restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.NotLike);
            }
            return new CriteriaConverter<ValueData>(Not<ValueData>(like.ToValue), (valueData, propertyName) => (valueData != null) ? Not<ValueData>(like.ToCriteria)(valueData, propertyName) : null, canBuildFilter);
        }

        private static T InvalidFilterMap<T>()
        {
            throw new InvalidOperationException();
        }

        private static bool IsCustomFunction(FunctionOperatorType operatorType, ValueData[] valuesData, string expectedCustomFunctionName) => 
            (operatorType == FunctionOperatorType.Custom) && ((valuesData.Length != 0) && ((valuesData[0].ToValue() as string) == expectedCustomFunctionName));

        private static Func<CriteriaOperator, T> Not<T>(Func<CriteriaOperator, T> criteriaToValue) => 
            delegate (CriteriaOperator filter) {
                Func<UnaryOperator, T> <>9__2;
                if (filter.ReferenceEqualsNull())
                {
                    return criteriaToValue(null);
                }
                Predicate<UnaryOperator> condition = <>c__19<T>.<>9__19_1;
                if (<>c__19<T>.<>9__19_1 == null)
                {
                    Predicate<UnaryOperator> local1 = <>c__19<T>.<>9__19_1;
                    condition = <>c__19<T>.<>9__19_1 = unaryOperator => unaryOperator.OperatorType == UnaryOperatorType.Not;
                }
                Func<UnaryOperator, T> func = <>9__2;
                if (<>9__2 == null)
                {
                    Func<UnaryOperator, T> local2 = <>9__2;
                    func = <>9__2 = unaryOperator => criteriaToValue(unaryOperator.Operand);
                }
                return filter.Transform<UnaryOperator, T>(condition, func, <>c__19<T>.<>9__19_3 ??= _ => InvalidFilterMap<T>());
            };

        private static Func<T, string, CriteriaOperator> Not<T>(Func<T, string, CriteriaOperator> valueToCriteria) => 
            (obj, propertyName) => new UnaryOperator(UnaryOperatorType.Not, valueToCriteria(obj, propertyName));

        private static OperandProperty ToProperty(this string property) => 
            new OperandProperty(property);

        private static CriteriaOperator TryCreateIsOnDatesCriteria(string propertyName, IEnumerable<ValueData> valuesData)
        {
            Func<ValueData, bool> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<ValueData, bool> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = x => (x != null) ? !x.IsNull : false;
            }
            List<ValueData> source = valuesData.Where<ValueData>(predicate).ToList<ValueData>();
            return (!source.Any<ValueData>() ? null : BetweenDatesHelper.CreateIsOnDatesFunction(propertyName.ToProperty(), source));
        }

        internal static Attributed<ValueData[]> TryGetPropertyValuesFromSubstitutedValuesData(string propertyName, ValueData[] valuesData, FunctionOperatorType operatorType) => 
            BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(propertyName, valuesData, operatorType);

        internal static Attributed<ValueDataRange> TryGetRangeFromSubstitutedValuesData(string propertyName, ValueData[] valuesData, FunctionOperatorType operatorType) => 
            BetweenDatesHelper.TryGetRangeFromSubstituted<object>(propertyName, valuesData, operatorType);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaConverterFactory.<>c <>9 = new CriteriaConverterFactory.<>c();
            public static NullMapper<ValueData> <>9__1_2;
            public static Func<string, ValueData[], Option<ValueData[]>> <>9__2_1;
            public static NullMapper<ValueData[]> <>9__2_2;
            public static Func<CriteriaOperator, ValueData[]> <>9__2_0;
            public static Func<ValueData, CriteriaOperator> <>9__2_4;
            public static Func<ValueData[], string, CriteriaOperator> <>9__2_3;
            public static Func<FilterRestrictions, bool> <>9__2_5;
            public static NullMapper<ValueData> <>9__3_2;
            public static NullMapper<ValueData> <>9__4_2;
            public static Func<FilterRestrictions, bool> <>9__4_4;
            public static NullMapper<Tuple<ValueData, ValueData>> <>9__5_2;
            public static Func<FilterRestrictions, bool> <>9__5_4;
            public static NullMapper<ValueData[]> <>9__6_2;
            public static Func<ValueData, CriteriaOperator> <>9__6_4;
            public static Func<FilterRestrictions, bool> <>9__6_5;
            public static Func<string, ValueData, ValueData, Option<Tuple<ValueData, ValueData>>> <>9__8_1;
            public static NullMapper<Tuple<ValueData, ValueData>> <>9__8_2;
            public static Func<CriteriaOperator, Tuple<ValueData, ValueData>> <>9__8_0;
            public static Func<Tuple<ValueData, ValueData>, string, CriteriaOperator> <>9__8_3;
            public static Func<FilterRestrictions, bool> <>9__8_4;
            public static Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> <>9__9_2;
            public static Func<Tuple<ValueData, ValueData>> <>9__9_3;
            public static Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> <>9__9_1;
            public static FallbackMapper<Tuple<ValueData, ValueData>> <>9__9_4;
            public static NullMapper<Tuple<ValueData, ValueData>> <>9__9_5;
            public static Func<CriteriaOperator, Tuple<ValueData, ValueData>> <>9__9_0;
            public static Func<Tuple<ValueData, ValueData>, string, CriteriaOperator> <>9__9_6;
            public static Func<FilterRestrictions, bool> <>9__9_7;
            public static Func<ValueData, bool> <>9__12_0;
            public static Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> <>9__13_1;
            public static NullMapper<ValueData[]> <>9__13_2;
            public static Func<CriteriaOperator, ValueData[]> <>9__13_0;
            public static Func<ValueData[], string, CriteriaOperator> <>9__13_3;
            public static Func<FilterRestrictions, bool> <>9__13_4;
            public static Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> <>9__14_1;
            public static FallbackMapper<ValueData> <>9__14_2;
            public static NullMapper<ValueData> <>9__14_3;
            public static Func<CriteriaOperator, ValueData> <>9__14_0;
            public static Func<ValueData, string, CriteriaOperator> <>9__14_4;
            public static Func<FilterRestrictions, bool> <>9__14_5;
            public static Func<FilterRestrictions, bool> <>9__15_1;
            public static Func<FilterRestrictions, bool> <>9__16_0;
            public static Func<FilterRestrictions, bool> <>9__17_0;
            public static Func<FilterRestrictions, bool> <>9__18_0;
            public static Func<ValueData[], ValueData> <>9__22_2;
            public static Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> <>9__22_1;
            public static NullMapper<ValueData> <>9__22_3;
            public static Func<CriteriaOperator, ValueData> <>9__22_0;
            public static Func<ValueData, string, CriteriaOperator> <>9__22_4;
            public static Func<FilterRestrictions, bool> <>9__22_5;
            public static Func<FilterRestrictions, bool> <>9__23_1;

            internal Tuple<ValueData, ValueData> <CreateBetween>b__8_0(CriteriaOperator filter)
            {
                Func<string, ValueData, ValueData, Option<Tuple<ValueData, ValueData>>> between = <>9__8_1;
                if (<>9__8_1 == null)
                {
                    Func<string, ValueData, ValueData, Option<Tuple<ValueData, ValueData>>> local1 = <>9__8_1;
                    between = <>9__8_1 = (_, beginVal, endVal) => new Tuple<ValueData, ValueData>(beginVal, endVal).ToOption<Tuple<ValueData, ValueData>>();
                }
                NullMapper<Tuple<ValueData, ValueData>> @null = <>9__8_2;
                if (<>9__8_2 == null)
                {
                    NullMapper<Tuple<ValueData, ValueData>> local2 = <>9__8_2;
                    @null = <>9__8_2 = () => new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue);
                }
                return filter.MapExtended<Tuple<ValueData, ValueData>>(null, null, null, between, null, null, null, null, null, @null);
            }

            internal Option<Tuple<ValueData, ValueData>> <CreateBetween>b__8_1(string _, ValueData beginVal, ValueData endVal) => 
                new Tuple<ValueData, ValueData>(beginVal, endVal).ToOption<Tuple<ValueData, ValueData>>();

            internal Tuple<ValueData, ValueData> <CreateBetween>b__8_2() => 
                new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue);

            internal CriteriaOperator <CreateBetween>b__8_3(Tuple<ValueData, ValueData> valuesData, string propertyName) => 
                new BetweenOperator(propertyName.ToProperty(), valuesData.Item1.ToCriteria(), valuesData.Item2.ToCriteria());

            internal bool <CreateBetween>b__8_4(FilterRestrictions restrictions) => 
                restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.Between);

            internal Tuple<ValueData, ValueData> <CreateBetweenDates>b__9_0(CriteriaOperator filter)
            {
                Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> function = <>9__9_1;
                if (<>9__9_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<Tuple<ValueData, ValueData>>> local1 = <>9__9_1;
                    function = <>9__9_1 = delegate (string propertyName, ValueData[] valuesData, FunctionOperatorType operatorType) {
                        Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> evaluator = <>9__9_2;
                        if (<>9__9_2 == null)
                        {
                            Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> local1 = <>9__9_2;
                            evaluator = <>9__9_2 = range => new Tuple<ValueData, ValueData>(range.Value.From, range.Value.To);
                        }
                        return CriteriaConverterFactory.TryGetRangeFromSubstitutedValuesData(propertyName, valuesData, operatorType).Return<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>>(evaluator, (<>9__9_3 ??= ((Func<Tuple<ValueData, ValueData>>) (() => null)))).ToOption<Tuple<ValueData, ValueData>>();
                    };
                }
                FallbackMapper<Tuple<ValueData, ValueData>> fallback = <>9__9_4;
                if (<>9__9_4 == null)
                {
                    FallbackMapper<Tuple<ValueData, ValueData>> local2 = <>9__9_4;
                    fallback = <>9__9_4 = (FallbackMapper<Tuple<ValueData, ValueData>>) (_ => null);
                }
                return filter.MapExtended<Tuple<ValueData, ValueData>>(null, null, null, null, function, null, null, null, fallback, (<>9__9_5 ??= () => new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue)));
            }

            internal Option<Tuple<ValueData, ValueData>> <CreateBetweenDates>b__9_1(string propertyName, ValueData[] valuesData, FunctionOperatorType operatorType)
            {
                Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> evaluator = <>9__9_2;
                if (<>9__9_2 == null)
                {
                    Func<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>> local1 = <>9__9_2;
                    evaluator = <>9__9_2 = range => new Tuple<ValueData, ValueData>(range.Value.From, range.Value.To);
                }
                return CriteriaConverterFactory.TryGetRangeFromSubstitutedValuesData(propertyName, valuesData, operatorType).Return<Attributed<ValueDataRange>, Tuple<ValueData, ValueData>>(evaluator, (<>9__9_3 ??= ((Func<Tuple<ValueData, ValueData>>) (() => null)))).ToOption<Tuple<ValueData, ValueData>>();
            }

            internal Tuple<ValueData, ValueData> <CreateBetweenDates>b__9_2(Attributed<ValueDataRange> range) => 
                new Tuple<ValueData, ValueData>(range.Value.From, range.Value.To);

            internal Tuple<ValueData, ValueData> <CreateBetweenDates>b__9_3() => 
                null;

            internal Tuple<ValueData, ValueData> <CreateBetweenDates>b__9_4(CriteriaOperator _) => 
                null;

            internal Tuple<ValueData, ValueData> <CreateBetweenDates>b__9_5() => 
                new Tuple<ValueData, ValueData>(ValueData.NullValue, ValueData.NullValue);

            internal CriteriaOperator <CreateBetweenDates>b__9_6(Tuple<ValueData, ValueData> valuesData, string propertyName)
            {
                bool flag1;
                ValueData local1 = valuesData.Item1;
                if (local1 != null)
                {
                    flag1 = !local1.IsNull;
                }
                else
                {
                    ValueData local2 = local1;
                    flag1 = false;
                }
                if (flag1)
                {
                    bool flag2;
                    ValueData local3 = valuesData.Item2;
                    if (local3 != null)
                    {
                        flag2 = !local3.IsNull;
                    }
                    else
                    {
                        ValueData local4 = local3;
                        flag2 = false;
                    }
                    if (flag2)
                    {
                        return BetweenDatesHelper.CreateBetweenDatesFunction(propertyName.ToProperty(), valuesData.Item1, valuesData.Item2);
                    }
                }
                return null;
            }

            internal bool <CreateBetweenDates>b__9_7(FilterRestrictions restrictions) => 
                restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.BetweenDates);

            internal ValueData <CreateBinary>b__1_2() => 
                ValueData.NullValue;

            internal ValueData <CreateBinaryFunction>b__3_2() => 
                null;

            internal ValueData <CreateCustomBinaryFunction>b__4_2() => 
                null;

            internal bool <CreateCustomBinaryFunction>b__4_4(FilterRestrictions _) => 
                true;

            internal Tuple<ValueData, ValueData> <CreateCustomTernaryFunction>b__5_2() => 
                null;

            internal bool <CreateCustomTernaryFunction>b__5_4(FilterRestrictions _) => 
                true;

            internal ValueData[] <CreateCustomVariadicFunction>b__6_2() => 
                new ValueData[] { ValueData.NullValue };

            internal CriteriaOperator <CreateCustomVariadicFunction>b__6_4(ValueData x) => 
                x.ToCriteria();

            internal bool <CreateCustomVariadicFunction>b__6_5(FilterRestrictions _) => 
                true;

            internal ValueData[] <CreateIsAnyOf>b__2_0(CriteriaOperator filter)
            {
                Func<string, ValueData[], Option<ValueData[]>> @in = <>9__2_1;
                if (<>9__2_1 == null)
                {
                    Func<string, ValueData[], Option<ValueData[]>> local1 = <>9__2_1;
                    @in = <>9__2_1 = (_, values) => values.ToOption<ValueData[]>();
                }
                NullMapper<ValueData[]> @null = <>9__2_2;
                if (<>9__2_2 == null)
                {
                    NullMapper<ValueData[]> local2 = <>9__2_2;
                    @null = <>9__2_2 = () => new ValueData[] { ValueData.NullValue };
                }
                return filter.MapExtended<ValueData[]>(null, null, @in, null, null, null, null, null, null, @null);
            }

            internal Option<ValueData[]> <CreateIsAnyOf>b__2_1(string _, ValueData[] values) => 
                values.ToOption<ValueData[]>();

            internal ValueData[] <CreateIsAnyOf>b__2_2() => 
                new ValueData[] { ValueData.NullValue };

            internal CriteriaOperator <CreateIsAnyOf>b__2_3(ValueData[] valuesData, string propertyName)
            {
                Func<ValueData, CriteriaOperator> selector = <>9__2_4;
                if (<>9__2_4 == null)
                {
                    Func<ValueData, CriteriaOperator> local1 = <>9__2_4;
                    selector = <>9__2_4 = x => x.ToCriteria();
                }
                return new InOperator(propertyName.ToProperty(), valuesData.Select<ValueData, CriteriaOperator>(selector));
            }

            internal CriteriaOperator <CreateIsAnyOf>b__2_4(ValueData x) => 
                x.ToCriteria();

            internal bool <CreateIsAnyOf>b__2_5(FilterRestrictions restrictions) => 
                restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.AnyOf);

            internal bool <CreateIsNotOnDate>b__15_1(FilterRestrictions restrictions) => 
                restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsNotOnDate);

            internal ValueData <CreateIsOnDate>b__14_0(CriteriaOperator filter)
            {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>9__14_1;
                if (<>9__14_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>9__14_1;
                    function = <>9__14_1 = (propertyName, values, operatorType) => BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? values.Skip<ValueData>(1).Single<ValueData>().ToOption<ValueData>() : Option<ValueData>.Empty;
                }
                FallbackMapper<ValueData> fallback = <>9__14_2;
                if (<>9__14_2 == null)
                {
                    FallbackMapper<ValueData> local2 = <>9__14_2;
                    fallback = <>9__14_2 = (FallbackMapper<ValueData>) (_ => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, fallback, (<>9__14_3 ??= ((NullMapper<ValueData>) (() => null))));
            }

            internal Option<ValueData> <CreateIsOnDate>b__14_1(string propertyName, ValueData[] values, FunctionOperatorType operatorType) => 
                BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? values.Skip<ValueData>(1).Single<ValueData>().ToOption<ValueData>() : Option<ValueData>.Empty;

            internal ValueData <CreateIsOnDate>b__14_2(CriteriaOperator _) => 
                null;

            internal ValueData <CreateIsOnDate>b__14_3() => 
                null;

            internal CriteriaOperator <CreateIsOnDate>b__14_4(ValueData valueData, string propertyName) => 
                CriteriaConverterFactory.TryCreateIsOnDatesCriteria(propertyName, valueData.Yield<ValueData>());

            internal bool <CreateIsOnDate>b__14_5(FilterRestrictions restrictions) => 
                restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsOnDate);

            internal ValueData[] <CreateIsOnDates>b__13_0(CriteriaOperator filter)
            {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> function = <>9__13_1;
                if (<>9__13_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData[]>> local1 = <>9__13_1;
                    function = <>9__13_1 = (propertyName, values, operatorType) => !BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? Option<ValueData[]>.Empty : values.Skip<ValueData>(1).ToArray<ValueData>().ToOption<ValueData[]>();
                }
                NullMapper<ValueData[]> @null = <>9__13_2;
                if (<>9__13_2 == null)
                {
                    NullMapper<ValueData[]> local2 = <>9__13_2;
                    @null = <>9__13_2 = () => new ValueData[] { ValueData.NullValue };
                }
                return filter.MapExtended<ValueData[]>(null, null, null, null, function, null, null, null, null, @null);
            }

            internal Option<ValueData[]> <CreateIsOnDates>b__13_1(string propertyName, ValueData[] values, FunctionOperatorType operatorType) => 
                !BetweenDatesHelper.IsIsOnDatesFunction(values, operatorType) ? Option<ValueData[]>.Empty : values.Skip<ValueData>(1).ToArray<ValueData>().ToOption<ValueData[]>();

            internal ValueData[] <CreateIsOnDates>b__13_2() => 
                new ValueData[] { ValueData.NullValue };

            internal CriteriaOperator <CreateIsOnDates>b__13_3(ValueData[] valuesData, string propertyName) => 
                CriteriaConverterFactory.TryCreateIsOnDatesCriteria(propertyName, valuesData);

            internal bool <CreateIsOnDates>b__13_4(FilterRestrictions restrictions) => 
                restrictions.AllowedCustomDateFilters.HasFlag(AllowedCustomDateFilters.IsOnDates);

            internal ValueData <CreateLike>b__22_0(CriteriaOperator filter)
            {
                Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> function = <>9__22_1;
                if (<>9__22_1 == null)
                {
                    Func<string, ValueData[], FunctionOperatorType, Option<ValueData>> local1 = <>9__22_1;
                    function = <>9__22_1 = delegate (string propertyName, ValueData[] values, FunctionOperatorType operatorType) {
                        Func<ValueData[], ValueData> evaluator = <>9__22_2;
                        if (<>9__22_2 == null)
                        {
                            Func<ValueData[], ValueData> local1 = <>9__22_2;
                            evaluator = <>9__22_2 = x => x[1];
                        }
                        return values.Return<ValueData[], ValueData>(evaluator, new Func<ValueData>(CriteriaConverterFactory.InvalidFilterMap<ValueData>)).ToOption<ValueData>();
                    };
                }
                NullMapper<ValueData> @null = <>9__22_3;
                if (<>9__22_3 == null)
                {
                    NullMapper<ValueData> local2 = <>9__22_3;
                    @null = <>9__22_3 = (NullMapper<ValueData>) (() => null);
                }
                return filter.MapExtended<ValueData>(null, null, null, null, function, null, null, null, null, @null);
            }

            internal Option<ValueData> <CreateLike>b__22_1(string propertyName, ValueData[] values, FunctionOperatorType operatorType)
            {
                Func<ValueData[], ValueData> evaluator = <>9__22_2;
                if (<>9__22_2 == null)
                {
                    Func<ValueData[], ValueData> local1 = <>9__22_2;
                    evaluator = <>9__22_2 = x => x[1];
                }
                return values.Return<ValueData[], ValueData>(evaluator, new Func<ValueData>(CriteriaConverterFactory.InvalidFilterMap<ValueData>)).ToOption<ValueData>();
            }

            internal ValueData <CreateLike>b__22_2(ValueData[] x) => 
                x[1];

            internal ValueData <CreateLike>b__22_3() => 
                null;

            internal CriteriaOperator <CreateLike>b__22_4(ValueData valueData, string propertyName) => 
                (valueData == null) ? null : LikeCustomFunction.Create(propertyName.ToProperty(), valueData.ToCriteria());

            internal bool <CreateLike>b__22_5(FilterRestrictions restrictions) => 
                restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.Like);

            internal bool <CreateNoneOf>b__16_0(FilterRestrictions restrictions) => 
                restrictions.AllowedAnyOfFilters.HasFlag(AllowedAnyOfFilters.NoneOf);

            internal bool <CreateNotBetween>b__17_0(FilterRestrictions restrictions) => 
                restrictions.AllowedBetweenFilters.HasFlag(AllowedBetweenFilters.NotBetween);

            internal bool <CreateNotContains>b__18_0(FilterRestrictions restrictions) => 
                restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.DoesNotContain);

            internal bool <CreateNotLike>b__23_1(FilterRestrictions restrictions) => 
                restrictions.AllowedBinaryFilters.HasFlag(AllowedBinaryFilters.NotLike);

            internal bool <TryCreateIsOnDatesCriteria>b__12_0(ValueData x) => 
                (x != null) ? !x.IsNull : false;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__19<T>
        {
            public static readonly CriteriaConverterFactory.<>c__19<T> <>9;
            public static Predicate<UnaryOperator> <>9__19_1;
            public static Func<CriteriaOperator, T> <>9__19_3;

            static <>c__19()
            {
                CriteriaConverterFactory.<>c__19<T>.<>9 = new CriteriaConverterFactory.<>c__19<T>();
            }

            internal bool <Not>b__19_1(UnaryOperator unaryOperator) => 
                unaryOperator.OperatorType == UnaryOperatorType.Not;

            internal T <Not>b__19_3(CriteriaOperator _) => 
                CriteriaConverterFactory.InvalidFilterMap<T>();
        }
    }
}

