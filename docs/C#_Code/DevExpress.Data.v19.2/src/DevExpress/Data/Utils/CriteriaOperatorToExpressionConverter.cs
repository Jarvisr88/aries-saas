namespace DevExpress.Data.Utils
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Linq;
    using System;
    using System.Linq.Expressions;

    public static class CriteriaOperatorToExpressionConverter
    {
        private static ICriteriaToExpressionConverter GetEntityFrameworkConverter();
        public static Expression<Func<T, TResult>> GetEntityFrameworkLambda<T, TResult>(CriteriaOperator criteria);
        public static Expression<Func<T, bool>> GetEntityFrameworkWhere<T>(CriteriaOperator criteria);
        private static ICriteriaToExpressionConverter GetGenericConverter();
        public static Expression<Func<T, TResult>> GetGenericLambda<T, TResult>(CriteriaOperator criteria);
        public static Expression<Func<T, bool>> GetGenericWhere<T>(CriteriaOperator criteria);
        public static Expression<Func<T, TResult>> GetLambda<T, TResult>(CriteriaOperator criteria, ICriteriaToExpressionConverter converter);
        private static ICriteriaToExpressionConverter GetLinqToObjectsConverter();
        public static Expression<Func<T, TResult>> GetLinqToObjectsLambda<T, TResult>(CriteriaOperator criteria);
        public static Expression<Func<T, bool>> GetLinqToObjectsWhere<T>(CriteriaOperator criteria);
        public static Expression<Func<T, bool>> GetWhere<T>(CriteriaOperator criteria, ICriteriaToExpressionConverter converter);
    }
}

