namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;

    public class CriteriaToExpressionConverterForObjectsInternal : CriteriaToExpressionConverterInternal
    {
        public CriteriaToExpressionConverterForObjectsInternal(ICriteriaToExpressionConverter owner, ParameterExpression thisExpression);
        public static int DateDiffDay(DateTime startDate, DateTime endDate);
        public static int? DateDiffDay(DateTime? startDate, DateTime? endDate);
        public static int DateDiffHour(DateTime startDate, DateTime endDate);
        public static int? DateDiffHour(DateTime? startDate, DateTime? endDate);
        public static int DateDiffMillisecond(DateTime startDate, DateTime endDate);
        public static int? DateDiffMillisecond(DateTime? startDate, DateTime? endDate);
        public static int DateDiffMinute(DateTime startDate, DateTime endDate);
        public static int? DateDiffMinute(DateTime? startDate, DateTime? endDate);
        public static int DateDiffMonth(DateTime startDate, DateTime endDate);
        public static int? DateDiffMonth(DateTime? startDate, DateTime? endDate);
        public static int DateDiffSecond(DateTime startDate, DateTime endDate);
        public static int? DateDiffSecond(DateTime? startDate, DateTime? endDate);
        public static int DateDiffYear(DateTime startDate, DateTime endDate);
        public static int? DateDiffYear(DateTime? startDate, DateTime? endDate);
        protected override Expression VisitInternal(FunctionOperator theOperator);

        public override bool ForceIifForInstance { get; }
    }
}

