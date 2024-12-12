namespace DevExpress.Data.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionHelpers<T>
    {
        public static Expression<Func<T, object>> CreatePropertyExpression(string name);
        public static Expression<Func<T, TTarget>> CreatePropertyExpression<TTarget>(string name);
        public static object GetSummary(IQueryable<T> source, string propertyName, Aggregate aggregate);
        public static IQueryable<object> GetUniqueValues(IQueryable<T> source, string propertyName);
    }
}

