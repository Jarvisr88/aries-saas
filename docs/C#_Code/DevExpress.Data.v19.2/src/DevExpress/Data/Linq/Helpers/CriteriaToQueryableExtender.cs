namespace DevExpress.Data.Linq.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public static class CriteriaToQueryableExtender
    {
        private static readonly ICriteriaToExpressionConverter onInstanceConverter;
        public static bool? UseMS362794Workaround;

        static CriteriaToQueryableExtender();
        public static IQueryable AppendWhere(this IQueryable src, ICriteriaToExpressionConverter converter, CriteriaOperator op);
        public static IQueryable Cast(this IQueryable src, Type t);
        public static int Count(this IQueryable src);
        public static IEnumerable<object[]> DoSelectSeveral(this IQueryable src, ICriteriaToExpressionConverter converter, CriteriaOperator[] subjs);
        private static IEnumerable<object[]> DoSelectSeveral(IQueryable queryable, ParameterExpression parameter, IList<Expression> selectList);
        private static IEnumerable<object[]> DoSelectSeveralArrayTail(IQueryable queryable, ParameterExpression parameter, IList<Expression> selectList);
        private static IQueryable DoSelectSeveralMakeQuery(IQueryable queryable, ParameterExpression parameter, Expression selectList);
        private static IEnumerable<object[]> DoSelectSeveralMS362794Tail(IQueryable queryable, ParameterExpression parameter, IList<Expression> selectList);
        internal static IEnumerable<object[]> DoSelectSummary(this IQueryable grouped, ICriteriaToExpressionConverter converter, Type rowType, IList<ServerModeSummaryDescriptor> summary);
        public static object EvaluateOnInstance(Type instanceType, object instance, CriteriaOperator subj);
        public static Func<object, object> GetOnInstanceEvaluator(Type instanceType, CriteriaOperator subj);
        public static IQueryable MakeGroupBy(this IQueryable src, ICriteriaToExpressionConverter converter, CriteriaOperator subj);
        public static IQueryable MakeOrderBy(this IQueryable src, ICriteriaToExpressionConverter converter, IEnumerable<ServerModeOrderDescriptor> orders);
        public static IQueryable MakeOrderBy(this IQueryable src, ICriteriaToExpressionConverter converter, params ServerModeOrderDescriptor[] orders);
        public static IQueryable MakeSelect(this IQueryable src, ICriteriaToExpressionConverter converter, CriteriaOperator subj);
        public static IQueryable MakeSelectThis(this IQueryable src);
        public static IQueryable Skip(this IQueryable src, int count);
        public static IQueryable Take(this IQueryable src, int count);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CriteriaToQueryableExtender.<>c <>9;
            public static Func<object, object> <>9__1_1;
            public static Func<Expression, UnaryExpression> <>9__16_0;
            public static Func<Expression, Type> <>9__17_0;

            static <>c();
            internal UnaryExpression <DoSelectSeveralArrayTail>b__16_0(Expression expr);
            internal Type <DoSelectSeveralMS362794Tail>b__17_0(Expression expr);
            internal object <GetOnInstanceEvaluator>b__1_1(object instance);
        }
    }
}

