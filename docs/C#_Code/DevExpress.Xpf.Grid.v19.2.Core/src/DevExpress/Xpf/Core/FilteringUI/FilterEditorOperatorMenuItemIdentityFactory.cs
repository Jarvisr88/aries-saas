namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class FilterEditorOperatorMenuItemIdentityFactory : OperatorMenuItemIdentityFactoryBase<IFilterEditorOperatorMenuItemIdentity>
    {
        public IFilterEditorOperatorMenuItemIdentity CreateAnyOf() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.AnyOf);

        public override IFilterEditorOperatorMenuItemIdentity CreateBetween() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Between);

        public override IFilterEditorOperatorMenuItemIdentity CreateBetweenDates() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.BetweenDates);

        public override IFilterEditorOperatorMenuItemIdentity CreateBinary(BinaryOperatorType type)
        {
            switch (type)
            {
                case BinaryOperatorType.Equal:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Equal);

                case BinaryOperatorType.NotEqual:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.NotEqual);

                case BinaryOperatorType.Greater:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Greater);

                case BinaryOperatorType.Less:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Less);

                case BinaryOperatorType.LessOrEqual:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.LessOrEqual);

                case BinaryOperatorType.GreaterOrEqual:
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.GreaterOrEqual);
            }
            return null;
        }

        public override IFilterEditorOperatorMenuItemIdentity CreateCustom(string name) => 
            new CustomOperatorMenuItemIdentity(name);

        public override IFilterEditorOperatorMenuItemIdentity CreateDoesNotContain() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.DoesNotContain);

        public override IFilterEditorOperatorMenuItemIdentity CreateFunction(FunctionOperatorType type)
        {
            if (type <= FunctionOperatorType.IsNullOrEmpty)
            {
                if (type == FunctionOperatorType.IsNull)
                {
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNull);
                }
                if (type == FunctionOperatorType.IsNullOrEmpty)
                {
                    return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNullOrEmpty);
                }
            }
            else
            {
                switch (type)
                {
                    case FunctionOperatorType.StartsWith:
                        return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.StartsWith);

                    case FunctionOperatorType.EndsWith:
                        return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.EndsWith);

                    case FunctionOperatorType.Contains:
                        return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Contains);

                    default:
                        switch (type)
                        {
                            case FunctionOperatorType.IsOutlookIntervalBeyondThisYear:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsBeyondThisYear);

                            case FunctionOperatorType.IsOutlookIntervalLaterThisYear:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsLaterThisYear);

                            case FunctionOperatorType.IsOutlookIntervalLaterThisMonth:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsLaterThisMonth);

                            case FunctionOperatorType.IsOutlookIntervalNextWeek:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNextWeek);

                            case FunctionOperatorType.IsOutlookIntervalLaterThisWeek:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsLaterThisWeek);

                            case FunctionOperatorType.IsOutlookIntervalTomorrow:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsTomorrow);

                            case FunctionOperatorType.IsOutlookIntervalToday:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsToday);

                            case FunctionOperatorType.IsOutlookIntervalYesterday:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsYesterday);

                            case FunctionOperatorType.IsOutlookIntervalEarlierThisWeek:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsEarlierThisWeek);

                            case FunctionOperatorType.IsOutlookIntervalLastWeek:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsLastWeek);

                            case FunctionOperatorType.IsOutlookIntervalEarlierThisMonth:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsEarlierThisMonth);

                            case FunctionOperatorType.IsOutlookIntervalEarlierThisYear:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsEarlierThisYear);

                            case FunctionOperatorType.IsOutlookIntervalPriorThisYear:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsPriorThisYear);

                            case FunctionOperatorType.IsSameDay:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsSameDay);

                            case FunctionOperatorType.IsJanuary:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsJanuary);

                            case FunctionOperatorType.IsFebruary:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsFebruary);

                            case FunctionOperatorType.IsMarch:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsMarch);

                            case FunctionOperatorType.IsApril:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsApril);

                            case FunctionOperatorType.IsMay:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsMay);

                            case FunctionOperatorType.IsJune:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsJune);

                            case FunctionOperatorType.IsJuly:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsJuly);

                            case FunctionOperatorType.IsAugust:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsAugust);

                            case FunctionOperatorType.IsSeptember:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsSeptember);

                            case FunctionOperatorType.IsOctober:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsOctober);

                            case FunctionOperatorType.IsNovember:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNovember);

                            case FunctionOperatorType.IsDecember:
                                return new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsDecember);

                            default:
                                break;
                        }
                        break;
                }
            }
            return null;
        }

        public override IFilterEditorOperatorMenuItemIdentity CreateIsNotNull() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNotNull);

        public override IFilterEditorOperatorMenuItemIdentity CreateIsNotNullOrEmpty() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNotNullOrEmpty);

        public override IFilterEditorOperatorMenuItemIdentity CreateIsNotOnDate() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNotOnDate);

        public override IFilterEditorOperatorMenuItemIdentity CreateIsNull() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsNull);

        public override IFilterEditorOperatorMenuItemIdentity CreateIsOnDate() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsOnDate);

        public IFilterEditorOperatorMenuItemIdentity CreateIsOnDates() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.IsOnDates);

        public override IFilterEditorOperatorMenuItemIdentity CreateLike() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.Like);

        public IFilterEditorOperatorMenuItemIdentity CreateNoneOf() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.NoneOf);

        public override IFilterEditorOperatorMenuItemIdentity CreateNotBetween() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.NotBetween);

        public override IFilterEditorOperatorMenuItemIdentity CreateNotLike() => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(FilterEditorOperatorType.NotLike);

        public override IFilterEditorOperatorMenuItemIdentity CreatePredefinedFormatCondition(PredefinedFormatConditionType type) => 
            new BuiltInFilterEditorOperatorMenuItemIdentity(GetOperatorTypeFromPredefinedFormatConditionType(type));

        private static FilterEditorOperatorType GetOperatorTypeFromPredefinedFormatConditionType(PredefinedFormatConditionType type)
        {
            switch (type)
            {
                case PredefinedFormatConditionType.Top:
                    return FilterEditorOperatorType.Top;

                case PredefinedFormatConditionType.Bottom:
                    return FilterEditorOperatorType.Bottom;

                case PredefinedFormatConditionType.AboveAverage:
                    return FilterEditorOperatorType.AboveAverage;

                case PredefinedFormatConditionType.BelowAverage:
                    return FilterEditorOperatorType.BelowAverage;

                case PredefinedFormatConditionType.Unique:
                    return FilterEditorOperatorType.Unique;

                case PredefinedFormatConditionType.Duplicate:
                    return FilterEditorOperatorType.Duplicate;
            }
            throw new IndexOutOfRangeException();
        }
    }
}

