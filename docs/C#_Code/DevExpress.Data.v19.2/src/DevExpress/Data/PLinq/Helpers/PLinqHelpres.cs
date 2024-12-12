namespace DevExpress.Data.PLinq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    internal static class PLinqHelpres
    {
        public static ParallelQuery ApplyWhere(this ParallelQuery source, Type sourceType, CriteriaOperator filterCriteria);
        public static ParallelQuery AsParallel(this IEnumerable enumerable, Type sourceType, int? degreeOfParallelism);
        public static int CallTotalSummary(this ParallelQuery source, Type sourceType);
        public static object CallTotalSummary(this ParallelQuery source, Type sourceType, ServerModeSummaryDescriptor summary);
        private static object CallTotalSummaryWithCustomAggregate(this ParallelQuery source, Type sourceType, ServerModeSummaryDescriptor summary);
        private static LambdaExpression GetLambda(CriteriaOperator criteriaOperator, Type sourceType);
        private static ParallelQuery Lambda(ParallelQuery source, Delegate predicate, ParameterExpression sourceParam, ParameterExpression predicateParam, MethodCallExpression methodCall);
        public static ParallelQuery MakeGroupBy(this ParallelQuery source, CriteriaOperator groupCriteria, Type sourceType);
        public static ParallelQuery MakeOrderBy(this ParallelQuery source, ICriteriaToExpressionConverter converter, Type sourceType, params ServerModeOrderDescriptor[] orders);
        public static IEnumerable MakeReverse(this IEnumerable source, Type sourceType);
        public static ParallelQuery MakeSelect(this ParallelQuery source, ICriteriaToExpressionConverter converter, CriteriaOperator selectCriteria, Type sourceType);
        public static ParallelQuery MakeSelect(this ParallelQuery source, ICriteriaToExpressionConverter converter, Delegate funcSelect, Type sourceType, Type resultType);
        public static ParallelQuery Take(this ParallelQuery source, Type sourceType, int count);
        public static object[] ToArray(this ParallelQuery source, Type sourceType);
        public static IList ToList(this IEnumerable source, Type sourceType);
        public static IList ToList(this ParallelQuery source, Type sourceType);
    }
}

