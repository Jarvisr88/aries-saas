namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal static class FilterClauseHelper
    {
        private static bool AllowAutoFilterCondition(ColumnBase column)
        {
            List<ClauseType> listOperationsByTypesSimple = GetListOperationsByTypesSimple(column);
            if (listOperationsByTypesSimple.Count != 0)
            {
                switch (column.ResolveAutoFilterCondition())
                {
                    case AutoFilterCondition.Like:
                        return listOperationsByTypesSimple.Contains(ClauseType.Like);

                    case AutoFilterCondition.Equals:
                        return listOperationsByTypesSimple.Contains(ClauseType.Equals);

                    case AutoFilterCondition.Contains:
                        return listOperationsByTypesSimple.Contains(ClauseType.Contains);
                }
            }
            return false;
        }

        private static bool AllowAutoFilterCriteria(ColumnBase column)
        {
            if (column.AutoFilterCriteria == null)
            {
                return AllowAutoFilterCondition(column);
            }
            List<ClauseType> list = GetListOperationsByTypes(CreateFilterColumn(column, false, false), column, true);
            return ((list.Count != 0) ? list.Contains(column.AutoFilterCriteria.Value) : false);
        }

        public static CriteriaOperator CreateAutoFilterCriteria(string fieldName, object value, ClauseType clauseType, DataViewBase view)
        {
            switch (clauseType)
            {
                case ClauseType.Equals:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.Equal);

                case ClauseType.DoesNotEqual:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.NotEqual);

                case ClauseType.Greater:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.Greater);

                case ClauseType.GreaterOrEqual:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.GreaterOrEqual);

                case ClauseType.Less:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.Less);

                case ClauseType.LessOrEqual:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, BinaryOperatorType.LessOrEqual);

                case ClauseType.Contains:
                    return view.CreateAutoFilterCriteriaContains(fieldName, value);

                case ClauseType.DoesNotContain:
                    return !view.CreateAutoFilterCriteriaContains(fieldName, value);

                case ClauseType.BeginsWith:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, FunctionOperatorType.StartsWith);

                case ClauseType.EndsWith:
                    return view.CreateAutoFilterCriteriaCustom(fieldName, value, FunctionOperatorType.EndsWith);

                case ClauseType.Like:
                {
                    CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), new OperandValue(value) };
                    return new FunctionOperator("Like", operands);
                }
                case ClauseType.NotLike:
                {
                    CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), new OperandValue(value) };
                    return !new FunctionOperator("Like", operands);
                }
            }
            return null;
        }

        internal static FilterColumn CreateFilterColumn(ColumnBase column, bool getUniqueColumnValues = true, bool ignoreColumnFilterMode = false) => 
            ((column == null) || ((column.View == null) || (column.View.DataControl == null))) ? null : column.View.DataControl.GetFilterColumnFromGridColumn(column, getUniqueColumnValues, true, ignoreColumnFilterMode);

        public static ClauseType? CriteriaOperatorToClauseType(CriteriaOperator op)
        {
            if (op != null)
            {
                if (op is BinaryOperator)
                {
                    BinaryOperator operator2 = op as BinaryOperator;
                    if (operator2 != null)
                    {
                        switch (operator2.OperatorType)
                        {
                            case BinaryOperatorType.Equal:
                                return 0;

                            case BinaryOperatorType.NotEqual:
                                return 1;

                            case BinaryOperatorType.Greater:
                                return 2;

                            case BinaryOperatorType.Less:
                                return 4;

                            case BinaryOperatorType.LessOrEqual:
                                return 5;

                            case BinaryOperatorType.GreaterOrEqual:
                                return 3;
                        }
                    }
                    return null;
                }
                if (op is FunctionOperator)
                {
                    FunctionOperator fo = op as FunctionOperator;
                    if (fo != null)
                    {
                        FunctionOperatorType operatorType = fo.OperatorType;
                        if (operatorType == FunctionOperatorType.Custom)
                        {
                            return FunctionOperatorToLike(fo, false);
                        }
                        switch (operatorType)
                        {
                            case FunctionOperatorType.StartsWith:
                                return 10;

                            case FunctionOperatorType.EndsWith:
                                return 11;

                            case FunctionOperatorType.Contains:
                                return 8;
                        }
                    }
                    return null;
                }
                if (op is UnaryOperator)
                {
                    UnaryOperator operator4 = op as UnaryOperator;
                    if ((operator4 == null) || (operator4.OperatorType != UnaryOperatorType.Not))
                    {
                        return null;
                    }
                    GroupOperator operand = operator4.Operand as GroupOperator;
                    if (operand != null)
                    {
                        if (IsDayEqualsOperator(operand))
                        {
                            return 1;
                        }
                        return null;
                    }
                    FunctionOperator fo = operator4.Operand as FunctionOperator;
                    if (fo != null)
                    {
                        if (fo.OperatorType == FunctionOperatorType.Contains)
                        {
                            return 9;
                        }
                        if (fo.OperatorType == FunctionOperatorType.Custom)
                        {
                            return FunctionOperatorToLike(fo, true);
                        }
                    }
                }
                GroupOperator groupOperator = op as GroupOperator;
                if (groupOperator == null)
                {
                    return null;
                }
                if (IsDayEqualsOperator(groupOperator))
                {
                    return 0;
                }
            }
            return null;
        }

        private static ClauseType? FunctionOperatorToLike(FunctionOperator fo, bool notLike = false)
        {
            if ((fo != null) && ((fo.OperatorType == FunctionOperatorType.Custom) && ((fo.Operands != null) && (fo.Operands.Count > 0))))
            {
                ConstantValue value2 = fo.Operands[0] as ConstantValue;
                if ((value2 != null) && ((value2.Value as string) == "Like"))
                {
                    return new ClauseType?(notLike ? ClauseType.NotLike : ClauseType.Like);
                }
            }
            return null;
        }

        public static bool GetAllowAutoFilter(ColumnBase column) => 
            !column.GetActualShowCriteriaInAutoFilterRow() ? AllowAutoFilterCriteria(column) : ((column.AutoFilterCriteria == null) ? (GetDefaultOperation(column, false, true) != null) : IsValidClause(column, CreateFilterColumn(column, false, false), column.AutoFilterCriteria.Value, column.ColumnFilterMode == ColumnFilterMode.DisplayText, true));

        public static ClauseType? GetDefaultOperation(ColumnBase column, bool allowChangeDefaultOperation = true, bool checkSingleFieldClause = true) => 
            GetDefaultOperation(CreateFilterColumn(column, false, false), column, allowChangeDefaultOperation, checkSingleFieldClause);

        public static ClauseType? GetDefaultOperation(FilterColumn filterColumn, ColumnBase column, bool allowChangeDefaultOperation = true, bool checkSingleFieldClause = true)
        {
            List<ClauseType> clauseTypesList = GetListOperationsByTypes(filterColumn, column, checkSingleFieldClause);
            if (clauseTypesList.Count != 0)
            {
                return GetDefaultOperationCore(clauseTypesList, FilterControlHelper.GetDefaultOperation((filterColumn == null) ? FilterColumnClauseClass.Generic : filterColumn.ClauseClass), allowChangeDefaultOperation);
            }
            return null;
        }

        private static ClauseType? GetDefaultOperationCore(List<ClauseType> clauseTypesList, ClauseType defaultClauseType, bool allowChangeDefaultOperation)
        {
            if (clauseTypesList.Contains(defaultClauseType))
            {
                return new ClauseType?(defaultClauseType);
            }
            if (allowChangeDefaultOperation)
            {
                return new ClauseType?(clauseTypesList[0]);
            }
            return null;
        }

        private static bool GetIsVirtualSource(ColumnBase column) => 
            (column.View != null) && ((column.View.DataControl != null) && column.View.DataControl.DataProviderBase.IsVirtualSource);

        public static List<ClauseType> GetListOperationsByTypes(FilterColumn flterColumn, ColumnBase column, bool checkSingleFieldClause)
        {
            List<ClauseType> list = new List<ClauseType>();
            foreach (ClauseType type in typeof(ClauseType).GetValues())
            {
                if (IsValidClause(column, flterColumn, type, column.ColumnFilterMode == ColumnFilterMode.DisplayText, checkSingleFieldClause))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        public static List<ClauseType> GetListOperationsByTypesSimple(ColumnBase column)
        {
            List<ClauseType> list = new List<ClauseType>();
            foreach (ClauseType type in typeof(ClauseType).GetValues())
            {
                if (IsClauseEnabled(column, type))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        private static bool IsCheckEditClause(ClauseType type) => 
            (type == ClauseType.Equals) || (type == ClauseType.DoesNotEqual);

        public static bool IsClauseEnabled(ColumnBase column, ClauseType clause)
        {
            switch (clause)
            {
                case ClauseType.Equals:
                    return column.AllowFilter(AllowedBinaryFilters.Equals);

                case ClauseType.DoesNotEqual:
                    return column.AllowFilter(AllowedBinaryFilters.DoesNotEqual);

                case ClauseType.Greater:
                    return column.AllowFilter(AllowedBinaryFilters.Greater);

                case ClauseType.GreaterOrEqual:
                    return column.AllowFilter(AllowedBinaryFilters.GreaterOrEqual);

                case ClauseType.Less:
                    return column.AllowFilter(AllowedBinaryFilters.Less);

                case ClauseType.LessOrEqual:
                    return column.AllowFilter(AllowedBinaryFilters.LessOrEqual);

                case ClauseType.Between:
                    return column.AllowFilter(AllowedBetweenFilters.Between);

                case ClauseType.NotBetween:
                    return column.AllowFilter(AllowedBetweenFilters.NotBetween);

                case ClauseType.Contains:
                    return column.AllowFilter(AllowedBinaryFilters.Contains);

                case ClauseType.DoesNotContain:
                    return column.AllowFilter(AllowedBinaryFilters.DoesNotContain);

                case ClauseType.BeginsWith:
                    return column.AllowFilter(AllowedBinaryFilters.BeginsWith);

                case ClauseType.EndsWith:
                    return column.AllowFilter(AllowedBinaryFilters.EndsWith);

                case ClauseType.Like:
                    return column.AllowFilter(AllowedBinaryFilters.Like);

                case ClauseType.NotLike:
                    return column.AllowFilter(AllowedBinaryFilters.NotLike);

                case ClauseType.IsNull:
                    return column.AllowFilter(AllowedUnaryFilters.IsNull);

                case ClauseType.IsNotNull:
                    return column.AllowFilter(AllowedUnaryFilters.IsNotNull);

                case ClauseType.AnyOf:
                    return column.AllowFilter(AllowedAnyOfFilters.AnyOf);

                case ClauseType.NoneOf:
                    return column.AllowFilter(AllowedAnyOfFilters.NoneOf);

                case ClauseType.IsNullOrEmpty:
                    return column.AllowFilter(AllowedUnaryFilters.IsNullOrEmpty);

                case ClauseType.IsNotNullOrEmpty:
                    return column.AllowFilter(AllowedUnaryFilters.IsNotNullOrEmpty);

                case ClauseType.IsBeyondThisYear:
                    return column.AllowFilter(AllowedDateTimeFilters.IsBeyondThisYear);

                case ClauseType.IsLaterThisYear:
                    return column.AllowFilter(AllowedDateTimeFilters.IsLaterThisYear);

                case ClauseType.IsLaterThisMonth:
                    return column.AllowFilter(AllowedDateTimeFilters.IsLaterThisMonth);

                case ClauseType.IsNextWeek:
                    return column.AllowFilter(AllowedDateTimeFilters.IsNextWeek);

                case ClauseType.IsLaterThisWeek:
                    return column.AllowFilter(AllowedDateTimeFilters.IsLaterThisWeek);

                case ClauseType.IsTomorrow:
                    return column.AllowFilter(AllowedDateTimeFilters.IsTomorrow);

                case ClauseType.IsToday:
                    return column.AllowFilter(AllowedDateTimeFilters.IsToday);

                case ClauseType.IsYesterday:
                    return column.AllowFilter(AllowedDateTimeFilters.IsYesterday);

                case ClauseType.IsEarlierThisWeek:
                    return column.AllowFilter(AllowedDateTimeFilters.IsEarlierThisWeek);

                case ClauseType.IsLastWeek:
                    return column.AllowFilter(AllowedDateTimeFilters.IsLastWeek);

                case ClauseType.IsEarlierThisMonth:
                    return column.AllowFilter(AllowedDateTimeFilters.IsEarlierThisMonth);

                case ClauseType.IsEarlierThisYear:
                    return column.AllowFilter(AllowedDateTimeFilters.IsEarlierThisYear);

                case ClauseType.IsPriorThisYear:
                    return column.AllowFilter(AllowedDateTimeFilters.IsPriorThisYear);
            }
            return false;
        }

        private static bool IsCollectionClause(ClauseType type) => 
            (type == ClauseType.AnyOf) || (type == ClauseType.NoneOf);

        private static bool IsDayEqualsOperator(GroupOperator groupOperator) => 
            TryParseDayEqualsOperatorValue(groupOperator) != null;

        private static bool IsNotFieldsClause(ClauseType type) => 
            (type == ClauseType.IsBeyondThisYear) || ((type == ClauseType.IsEarlierThisMonth) || ((type == ClauseType.IsEarlierThisWeek) || ((type == ClauseType.IsEarlierThisYear) || ((type == ClauseType.IsLastWeek) || ((type == ClauseType.IsLaterThisMonth) || ((type == ClauseType.IsLaterThisWeek) || ((type == ClauseType.IsLaterThisYear) || ((type == ClauseType.IsNextWeek) || ((type == ClauseType.IsNotNull) || ((type == ClauseType.IsNotNullOrEmpty) || ((type == ClauseType.IsNull) || ((type == ClauseType.IsNullOrEmpty) || ((type == ClauseType.IsPriorThisYear) || ((type == ClauseType.IsToday) || ((type == ClauseType.IsTomorrow) || (type == ClauseType.IsYesterday))))))))))))))));

        public static bool IsSupportAutoFilter(ClauseType clauseType)
        {
            switch (clauseType)
            {
                case ClauseType.Equals:
                case ClauseType.DoesNotEqual:
                case ClauseType.Greater:
                case ClauseType.GreaterOrEqual:
                case ClauseType.Less:
                case ClauseType.LessOrEqual:
                case ClauseType.Contains:
                case ClauseType.DoesNotContain:
                case ClauseType.BeginsWith:
                case ClauseType.EndsWith:
                case ClauseType.Like:
                case ClauseType.NotLike:
                    return true;
            }
            return false;
        }

        private static bool IsTwoFieldsClause(ClauseType type) => 
            (type == ClauseType.Between) || (type == ClauseType.NotBetween);

        public static bool IsValidClause(ColumnBase column, ClauseType type, bool filterIsDisplayText, bool checkSingleFieldClause) => 
            IsValidClause(column, CreateFilterColumn(column, true, false), type, filterIsDisplayText, checkSingleFieldClause);

        public static bool IsValidClause(ColumnBase column, FilterColumn filterColumn, ClauseType type, bool filterIsDisplayText, bool checkSingleFieldClause) => 
            (filterColumn != null) ? ((!checkSingleFieldClause || (!IsCollectionClause(type) && (!IsTwoFieldsClause(type) && !IsNotFieldsClause(type)))) ? (!(filterColumn.EditSettings is CheckEditSettings) ? filterColumn.IsValidClause(type) : (IsCheckEditClause(type) && IsClauseEnabled(column, type))) : false) : false;

        private static DateTime? TryParseDateRangeLimitValue(CriteriaOperator criteriaOperator, BinaryOperatorType expectedBinaryOperatorType)
        {
            BinaryOperator @operator = criteriaOperator as BinaryOperator;
            if ((@operator != null) && (@operator.OperatorType == expectedBinaryOperatorType))
            {
                OperandValue rightOperand = @operator.RightOperand as OperandValue;
                if (rightOperand != null)
                {
                    return (rightOperand.Value as DateTime?);
                }
            }
            return null;
        }

        internal static DateTime? TryParseDayEqualsOperatorValue(GroupOperator groupOperator)
        {
            if (groupOperator.Operands.Count == 2)
            {
                DateTime? nullable = TryParseDateRangeLimitValue(groupOperator.Operands[0], BinaryOperatorType.GreaterOrEqual);
                if (nullable == null)
                {
                    return null;
                }
                DateTime? nullable2 = TryParseDateRangeLimitValue(groupOperator.Operands[1], BinaryOperatorType.Less);
                if (nullable2 == null)
                {
                    return null;
                }
                if (nullable.Value.AddDays(1.0) == nullable2.Value)
                {
                    return new DateTime?(nullable.Value);
                }
            }
            return null;
        }
    }
}

