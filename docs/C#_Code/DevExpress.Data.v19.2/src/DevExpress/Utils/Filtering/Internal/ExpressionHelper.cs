namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionHelper
    {
        private static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            Expression body = expression.Body;
            if (body is UnaryExpression)
            {
                body = ((UnaryExpression) body).Operand;
            }
            MemberExpression expression3 = body as MemberExpression;
            if (expression3 == null)
            {
                throw new ArgumentException("Expression: " + expression.ToString());
            }
            return expression3;
        }

        internal static string GetPropertyName<T>(Expression<Func<T>> expression) => 
            GetPropertyName(expression);

        internal static string GetPropertyName(LambdaExpression expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression);
            if (IsPropertyExpression(memberExpression.Expression as MemberExpression))
            {
                throw new ArgumentException("Expression: " + expression.ToString());
            }
            return memberExpression.Member.Name;
        }

        private static bool IsPropertyExpression(MemberExpression expression) => 
            (expression != null) && (expression.Member.MemberType == MemberTypes.Property);
    }
}

