namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Localization;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class LocalaizableCriteriaToStringProcessor
    {
        public static string Process(CriteriaOperator op, Func<AppliedFormatConditionFilterInfo, bool> isValidConditionFilterInfo = null)
        {
            Func<AppliedFormatConditionFilterInfo, bool> func1 = isValidConditionFilterInfo;
            if (isValidConditionFilterInfo == null)
            {
                Func<AppliedFormatConditionFilterInfo, bool> local1 = isValidConditionFilterInfo;
                func1 = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<AppliedFormatConditionFilterInfo, bool> local2 = <>c.<>9__0_0;
                    func1 = <>c.<>9__0_0 = _ => false;
                }
            }
            return DXLocalaizableCriteriaToStringProcessor.Process(new LocalaizableCriteriaToStringProcessorLocalizerWrapper(EditorLocalizer.Active), op, func1);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LocalaizableCriteriaToStringProcessor.<>c <>9 = new LocalaizableCriteriaToStringProcessor.<>c();
            public static Func<AppliedFormatConditionFilterInfo, bool> <>9__0_0;

            internal bool <Process>b__0_0(AppliedFormatConditionFilterInfo _) => 
                false;
        }

        private class DXLocalaizableCriteriaToStringProcessor : LocalaizableCriteriaToStringProcessorCore
        {
            private readonly Func<AppliedFormatConditionFilterInfo, bool> isValidConditionFilterInfo;

            protected DXLocalaizableCriteriaToStringProcessor(ILocalaizableCriteriaToStringProcessorOpNamesSource opNamesSource, Func<AppliedFormatConditionFilterInfo, bool> isValidConditionFilterInfo) : base(opNamesSource)
            {
                this.isValidConditionFilterInfo = isValidConditionFilterInfo;
            }

            private string GetPropertyString(OperandProperty property) => 
                base.Process(property).GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass.InBetween);

            private static EditorStringId GetStringId(ConditionFilterType type)
            {
                switch (type)
                {
                    case ConditionFilterType.TopItems:
                        return EditorStringId.FilterCriteriaToStringTopItems;

                    case ConditionFilterType.BottomItems:
                        return EditorStringId.FilterCriteriaToStringBottomItems;

                    case ConditionFilterType.TopPercent:
                        return EditorStringId.FilterCriteriaToStringTopPercent;

                    case ConditionFilterType.BottomPercent:
                        return EditorStringId.FilterCriteriaToStringBottomPercent;

                    case ConditionFilterType.AboveAverage:
                        return EditorStringId.FilterCriteriaToStringAboveAverage;

                    case ConditionFilterType.BelowAverage:
                        return EditorStringId.FilterCriteriaToStringBelowAverage;

                    case ConditionFilterType.Unique:
                        return EditorStringId.FilterCriteriaToStringUnique;

                    case ConditionFilterType.Duplicate:
                        return EditorStringId.FilterCriteriaToStringDuplicate;
                }
                throw new InvalidOperationException();
            }

            private CriteriaToStringVisitResult MakeSubstitutionResult(OperandProperty property, CriteriaOperator[] values, EditorStringId id)
            {
                string propertyString = this.GetPropertyString(property);
                return new CriteriaToStringVisitResult($"{propertyString} {EditorLocalizer.Active.GetLocalizedString(id)}({base.ProcessToCommaDelimitedList(values)})", CriteriaPriorityClass.InBetween);
            }

            public static string Process(ILocalaizableCriteriaToStringProcessorOpNamesSource opNamesSource, CriteriaOperator op, Func<AppliedFormatConditionFilterInfo, bool> isValidConditionFilterInfo) => 
                (op != null) ? new LocalaizableCriteriaToStringProcessor.DXLocalaizableCriteriaToStringProcessor(opNamesSource, isValidConditionFilterInfo).Process(op).Result : string.Empty;

            public override CriteriaToStringVisitResult Visit(FunctionOperator operand)
            {
                FormatConditionFilterInfo info1;
                Attributed<ValueDataRange> attributed = BetweenDatesHelper.TryGetRangeFromSubstituted<object>(operand);
                if (attributed != null)
                {
                    CriteriaOperator[] values = new CriteriaOperator[] { attributed.Value.From.ToCriteria(), attributed.Value.To.ToCriteria() };
                    return this.MakeSubstitutionResult(attributed.Property, values, EditorStringId.FilterCriteriaToStringBetweenDates);
                }
                Attributed<ValueData[]> attributed2 = BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(operand);
                if (attributed2 != null)
                {
                    Func<ValueData, CriteriaOperator> selector = <>c.<>9__3_0;
                    if (<>c.<>9__3_0 == null)
                    {
                        Func<ValueData, CriteriaOperator> local1 = <>c.<>9__3_0;
                        selector = <>c.<>9__3_0 = x => x.ToCriteria();
                    }
                    return this.MakeSubstitutionResult(attributed2.Property, attributed2.Value.Select<ValueData, CriteriaOperator>(selector).ToArray<CriteriaOperator>(), EditorStringId.FilterClauseIsOnAnyOfTheFollowing);
                }
                AppliedFormatConditionFilterInfo local2 = FormatConditionFiltersHelper.GetAppliedFormatConditionFilterInfo(operand).If<AppliedFormatConditionFilterInfo>(this.isValidConditionFilterInfo);
                if (local2 != null)
                {
                    info1 = local2.Info;
                }
                else
                {
                    AppliedFormatConditionFilterInfo local3 = local2;
                    info1 = null;
                }
                FormatConditionFilterInfo local4 = info1;
                FormatConditionFilterInfo topBottomFilterInfo = local4;
                if (local4 == null)
                {
                    FormatConditionFilterInfo local5 = local4;
                    topBottomFilterInfo = FormatConditionFiltersHelper.GetTopBottomFilterInfo(operand);
                }
                FormatConditionFilterInfo info = topBottomFilterInfo;
                if ((info == null) || !info.Type.IsTopBottomOrAverageOrUniqueDuplicate())
                {
                    return base.Visit(operand);
                }
                TopBottomFilterFunctionOperator @operator = (TopBottomFilterFunctionOperator) operand;
                return new CriteriaToStringVisitResult(this.GetPropertyString(new OperandProperty(@operator.DisplayPropertyName)) + " " + string.Format(EditorLocalizer.Active.GetLocalizedString(GetStringId(info.Type)), info.Value1), CriteriaPriorityClass.InBetween);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly LocalaizableCriteriaToStringProcessor.DXLocalaizableCriteriaToStringProcessor.<>c <>9 = new LocalaizableCriteriaToStringProcessor.DXLocalaizableCriteriaToStringProcessor.<>c();
                public static Func<ValueData, CriteriaOperator> <>9__3_0;

                internal CriteriaOperator <Visit>b__3_0(ValueData x) => 
                    x.ToCriteria();
            }
        }

        public class LocalaizableCriteriaToStringProcessorLocalizerWrapper : ILocalaizableCriteriaToStringProcessorOpNamesSource
        {
            private readonly XtraLocalizer<EditorStringId> localizer;

            public LocalaizableCriteriaToStringProcessorLocalizerWrapper(XtraLocalizer<EditorStringId> localizer)
            {
                this.localizer = localizer;
            }

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetBetweenString() => 
                this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBetween);

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetInString() => 
                this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringIn);

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetIsNotNullString() => 
                this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringIsNotNull);

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetIsNullString() => 
                this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorIsNull);

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetNotLikeString() => 
                this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringNotLike);

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetString(Aggregate opType) => 
                null;

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetString(BinaryOperatorType opType)
            {
                switch (opType)
                {
                    case BinaryOperatorType.Equal:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorEqual);

                    case BinaryOperatorType.NotEqual:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorNotEqual);

                    case BinaryOperatorType.Greater:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorGreater);

                    case BinaryOperatorType.Less:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorLess);

                    case BinaryOperatorType.LessOrEqual:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorLessOrEqual);

                    case BinaryOperatorType.GreaterOrEqual:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorGreaterOrEqual);

                    case BinaryOperatorType.Like:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorLike);

                    case BinaryOperatorType.BitwiseAnd:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorBitwiseAnd);

                    case BinaryOperatorType.BitwiseOr:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorBitwiseOr);

                    case BinaryOperatorType.BitwiseXor:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorBitwiseXor);

                    case BinaryOperatorType.Divide:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorDivide);

                    case BinaryOperatorType.Modulo:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorModulo);

                    case BinaryOperatorType.Multiply:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorMultiply);

                    case BinaryOperatorType.Plus:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorPlus);

                    case BinaryOperatorType.Minus:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringBinaryOperatorMinus);
                }
                return opType.ToString();
            }

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetString(FunctionOperatorType opType)
            {
                if (opType == FunctionOperatorType.IsNullOrEmpty)
                {
                    return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringFunctionIsNullOrEmpty);
                }
                switch (opType)
                {
                    case FunctionOperatorType.StartsWith:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringFunctionStartsWith);

                    case FunctionOperatorType.EndsWith:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringFunctionEndsWith);

                    case FunctionOperatorType.Contains:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringFunctionContains);

                    case FunctionOperatorType.LocalDateTimeThisYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeThisYear);

                    case FunctionOperatorType.LocalDateTimeThisMonth:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeThisMonth);

                    case FunctionOperatorType.LocalDateTimeLastWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeLastWeek);

                    case FunctionOperatorType.LocalDateTimeThisWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeThisWeek);

                    case FunctionOperatorType.LocalDateTimeYesterday:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeYesterday);

                    case FunctionOperatorType.LocalDateTimeToday:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeToday);

                    case FunctionOperatorType.LocalDateTimeTomorrow:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeTomorrow);

                    case FunctionOperatorType.LocalDateTimeDayAfterTomorrow:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeDayAfterTomorrow);

                    case FunctionOperatorType.LocalDateTimeNextWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeNextWeek);

                    case FunctionOperatorType.LocalDateTimeTwoWeeksAway:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeTwoWeeksAway);

                    case FunctionOperatorType.LocalDateTimeNextMonth:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeNextMonth);

                    case FunctionOperatorType.LocalDateTimeNextYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseLocalDateTimeNextYear);

                    case FunctionOperatorType.IsOutlookIntervalBeyondThisYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsBeyondThisYear);

                    case FunctionOperatorType.IsOutlookIntervalLaterThisYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsLaterThisYear);

                    case FunctionOperatorType.IsOutlookIntervalLaterThisMonth:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsLaterThisMonth);

                    case FunctionOperatorType.IsOutlookIntervalNextWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsNextWeek);

                    case FunctionOperatorType.IsOutlookIntervalLaterThisWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsLaterThisWeek);

                    case FunctionOperatorType.IsOutlookIntervalTomorrow:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsTomorrow);

                    case FunctionOperatorType.IsOutlookIntervalToday:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsToday);

                    case FunctionOperatorType.IsOutlookIntervalYesterday:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsYesterday);

                    case FunctionOperatorType.IsOutlookIntervalEarlierThisWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsEarlierThisWeek);

                    case FunctionOperatorType.IsOutlookIntervalLastWeek:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsLastWeek);

                    case FunctionOperatorType.IsOutlookIntervalEarlierThisMonth:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsEarlierThisMonth);

                    case FunctionOperatorType.IsOutlookIntervalEarlierThisYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsEarlierThisYear);

                    case FunctionOperatorType.IsOutlookIntervalPriorThisYear:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterClauseIsPriorThisYear);
                }
                return opType.ToString();
            }

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetString(GroupOperatorType opType) => 
                (opType == GroupOperatorType.And) ? this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringGroupOperatorAnd) : ((opType == GroupOperatorType.Or) ? this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringGroupOperatorOr) : opType.ToString());

            string ILocalaizableCriteriaToStringProcessorOpNamesSource.GetString(UnaryOperatorType opType)
            {
                switch (opType)
                {
                    case UnaryOperatorType.BitwiseNot:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorBitwiseNot);

                    case UnaryOperatorType.Plus:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorPlus);

                    case UnaryOperatorType.Minus:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorMinus);

                    case UnaryOperatorType.Not:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorNot);

                    case UnaryOperatorType.IsNull:
                        return this.localizer.GetLocalizedString(EditorStringId.FilterCriteriaToStringUnaryOperatorIsNull);
                }
                return opType.ToString();
            }
        }
    }
}

