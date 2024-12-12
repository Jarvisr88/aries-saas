namespace DevExpress.Xpf.Editors.Filtering.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Filtering;
    using System;
    using System.Collections.Generic;

    public static class FilterControlHelper
    {
        private static readonly FunctionOperatorType[] LocalDateTimeFuncs = new FunctionOperatorType[] { FunctionOperatorType.LocalDateTimeThisYear, FunctionOperatorType.LocalDateTimeThisMonth, FunctionOperatorType.LocalDateTimeLastWeek, FunctionOperatorType.LocalDateTimeThisWeek, FunctionOperatorType.LocalDateTimeYesterday, FunctionOperatorType.LocalDateTimeToday, FunctionOperatorType.LocalDateTimeTomorrow, FunctionOperatorType.LocalDateTimeDayAfterTomorrow, FunctionOperatorType.LocalDateTimeNextWeek, FunctionOperatorType.LocalDateTimeTwoWeeksAway, FunctionOperatorType.LocalDateTimeNextMonth, FunctionOperatorType.LocalDateTimeNextYear };

        public static ClauseType GetDefaultOperation(FilterColumnClauseClass clauseClass) => 
            (clauseClass == FilterColumnClauseClass.String) ? ClauseType.BeginsWith : ((clauseClass != FilterColumnClauseClass.Blob) ? ClauseType.Equals : ClauseType.IsNotNull);

        public static List<ClauseType> GetListOperationsByFilterColumn(FilterColumn column)
        {
            List<ClauseType> list = new List<ClauseType>();
            ClauseType[] typeArray1 = new ClauseType[] { 
                ClauseType.Equals, ClauseType.DoesNotEqual, ClauseType.Greater, ClauseType.GreaterOrEqual, ClauseType.Less, ClauseType.LessOrEqual, ClauseType.Between, ClauseType.NotBetween, ClauseType.Contains, ClauseType.DoesNotContain, ClauseType.BeginsWith, ClauseType.EndsWith, ClauseType.Like, ClauseType.NotLike, ClauseType.IsNull, ClauseType.IsNotNull,
                ClauseType.AnyOf, ClauseType.NoneOf, ClauseType.IsNullOrEmpty, ClauseType.IsNotNullOrEmpty, ClauseType.IsBeyondThisYear, ClauseType.IsLaterThisYear, ClauseType.IsLaterThisMonth, ClauseType.IsNextWeek, ClauseType.IsLaterThisWeek, ClauseType.IsTomorrow, ClauseType.IsToday, ClauseType.IsYesterday, ClauseType.IsEarlierThisWeek, ClauseType.IsLastWeek, ClauseType.IsEarlierThisMonth, ClauseType.IsEarlierThisYear,
                ClauseType.IsPriorThisYear
            };
            foreach (ClauseType type in typeArray1)
            {
                if ((type != ClauseType.Function) && ((column != null) && column.IsValidClause(type)))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        public static FunctionOperatorType[] GetLocalDateTimeFuncs() => 
            LocalDateTimeFuncs;

        public static OperandsCount GetSecondOperandsCount(ClauseType operation, IList<CriteriaOperator> additionalOperands) => 
            !IsCollectionClause(operation) ? (!IsTwoFieldsClause(operation) ? ((additionalOperands.Count != 0) ? (((additionalOperands.Count != 1) || !IsLocalDateTimeFunction(additionalOperands)) ? OperandsCount.One : OperandsCount.OneLocalDateTime) : OperandsCount.None) : OperandsCount.Two) : OperandsCount.Several;

        public static bool IsCollectionClause(ClauseType type) => 
            (type == ClauseType.AnyOf) || (type == ClauseType.NoneOf);

        public static bool IsLocalDateTimeFunction(IList<CriteriaOperator> additionalOperands)
        {
            if (additionalOperands.Count < 1)
            {
                return false;
            }
            FunctionOperator @operator = additionalOperands[0] as FunctionOperator;
            return ((@operator != null) ? @operator.OperatorType.IsLocalDateTimeFunction() : false);
        }

        private static bool IsTwoFieldsClause(ClauseType type) => 
            (type == ClauseType.Between) || (type == ClauseType.NotBetween);
    }
}

