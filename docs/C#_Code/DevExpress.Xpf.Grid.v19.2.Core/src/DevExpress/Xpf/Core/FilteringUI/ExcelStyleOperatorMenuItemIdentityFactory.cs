namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class ExcelStyleOperatorMenuItemIdentityFactory : OperatorMenuItemIdentityFactoryBase<IExcelStyleOperatorMenuItemIdentity>
    {
        public override IExcelStyleOperatorMenuItemIdentity CreateBetween() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Between);

        public override IExcelStyleOperatorMenuItemIdentity CreateBetweenDates() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.BetweenDates);

        public override IExcelStyleOperatorMenuItemIdentity CreateBinary(BinaryOperatorType type)
        {
            switch (type)
            {
                case BinaryOperatorType.Equal:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Equal);

                case BinaryOperatorType.NotEqual:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.NotEqual);

                case BinaryOperatorType.Greater:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Greater);

                case BinaryOperatorType.Less:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Less);

                case BinaryOperatorType.LessOrEqual:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.LessOrEqual);

                case BinaryOperatorType.GreaterOrEqual:
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.GreaterOrEqual);
            }
            return null;
        }

        public override IExcelStyleOperatorMenuItemIdentity CreateCustom(string name) => 
            new CustomOperatorMenuItemIdentity(name);

        public IExcelStyleOperatorMenuItemIdentity CreateDateOperators() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.DateOperators);

        public override IExcelStyleOperatorMenuItemIdentity CreateDoesNotContain() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.DoesNotContain);

        public IExcelStyleOperatorMenuItemIdentity CreateFormatConditions() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.FormatConditions);

        public override IExcelStyleOperatorMenuItemIdentity CreateFunction(FunctionOperatorType type)
        {
            if (type <= FunctionOperatorType.IsNullOrEmpty)
            {
                if (type == FunctionOperatorType.IsNull)
                {
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNull);
                }
                if (type == FunctionOperatorType.IsNullOrEmpty)
                {
                    return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNullOrEmpty);
                }
            }
            else
            {
                switch (type)
                {
                    case FunctionOperatorType.StartsWith:
                        return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.StartsWith);

                    case FunctionOperatorType.EndsWith:
                        return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.EndsWith);

                    case FunctionOperatorType.Contains:
                        return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Contains);

                    default:
                        switch (type)
                        {
                            case FunctionOperatorType.IsSameDay:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsSameDay);

                            case FunctionOperatorType.IsJanuary:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsJanuary);

                            case FunctionOperatorType.IsFebruary:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsFebruary);

                            case FunctionOperatorType.IsMarch:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsMarch);

                            case FunctionOperatorType.IsApril:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsApril);

                            case FunctionOperatorType.IsMay:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsMay);

                            case FunctionOperatorType.IsJune:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsJune);

                            case FunctionOperatorType.IsJuly:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsJuly);

                            case FunctionOperatorType.IsAugust:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsAugust);

                            case FunctionOperatorType.IsSeptember:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsSeptember);

                            case FunctionOperatorType.IsOctober:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsOctober);

                            case FunctionOperatorType.IsNovember:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNovember);

                            case FunctionOperatorType.IsDecember:
                                return new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsDecember);

                            default:
                                break;
                        }
                        break;
                }
            }
            return null;
        }

        public override IExcelStyleOperatorMenuItemIdentity CreateIsNotNull() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNotNull);

        public override IExcelStyleOperatorMenuItemIdentity CreateIsNotNullOrEmpty() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNotNullOrEmpty);

        public override IExcelStyleOperatorMenuItemIdentity CreateIsNotOnDate() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNotOnDate);

        public override IExcelStyleOperatorMenuItemIdentity CreateIsNull() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsNull);

        public override IExcelStyleOperatorMenuItemIdentity CreateIsOnDate() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.IsOnDate);

        public override IExcelStyleOperatorMenuItemIdentity CreateLike() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Like);

        public override IExcelStyleOperatorMenuItemIdentity CreateNotBetween() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.NotBetween);

        public override IExcelStyleOperatorMenuItemIdentity CreateNotLike() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.NotLike);

        public IExcelStyleOperatorMenuItemIdentity CreatePredefined() => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(ExcelStyleFilterElementOperatorType.Predefined);

        public override IExcelStyleOperatorMenuItemIdentity CreatePredefinedFormatCondition(PredefinedFormatConditionType type) => 
            new BuiltInExcelStyleOperatorMenuItemIdentity(GetOperatorTypeFromPredefinedFormatConditionType(type));

        private static ExcelStyleFilterElementOperatorType GetOperatorTypeFromPredefinedFormatConditionType(PredefinedFormatConditionType type)
        {
            switch (type)
            {
                case PredefinedFormatConditionType.Top:
                    return ExcelStyleFilterElementOperatorType.Top;

                case PredefinedFormatConditionType.Bottom:
                    return ExcelStyleFilterElementOperatorType.Bottom;

                case PredefinedFormatConditionType.AboveAverage:
                    return ExcelStyleFilterElementOperatorType.AboveAverage;

                case PredefinedFormatConditionType.BelowAverage:
                    return ExcelStyleFilterElementOperatorType.BelowAverage;

                case PredefinedFormatConditionType.Unique:
                    return ExcelStyleFilterElementOperatorType.Unique;

                case PredefinedFormatConditionType.Duplicate:
                    return ExcelStyleFilterElementOperatorType.Duplicate;
            }
            throw new IndexOutOfRangeException();
        }
    }
}

