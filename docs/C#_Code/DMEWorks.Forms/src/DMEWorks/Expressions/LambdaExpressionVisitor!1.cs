namespace DMEWorks.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal sealed class LambdaExpressionVisitor<T> : DmeworksExpressionVisitor
    {
        private readonly ParameterExpression m_instance;

        private LambdaExpressionVisitor()
        {
            this.m_instance = Expression.Parameter(typeof(T), "instance");
        }

        internal static Expression<Func<T, bool>> CreateExpression(Expression node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            LambdaExpressionVisitor<T> visitor = new LambdaExpressionVisitor<T>();
            ParameterExpression[] parameters = new ParameterExpression[] { visitor.m_instance };
            return Expression.Lambda<Func<T, bool>>(visitor.Visit(node), parameters);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            ExpressionType nodeType = node.NodeType;
            if (((nodeType - 2) > ExpressionType.AddChecked) && ((nodeType - ExpressionType.Or) > ExpressionType.AddChecked))
            {
                throw new NotSupportedException();
            }
            return base.VisitBinary(node);
        }

        protected override Expression VisitLambda<TDelegate>(Expression<TDelegate> node)
        {
            Type type1 = typeof(TDelegate);
            if (!type1.IsGenericType)
            {
                throw new NotSupportedException();
            }
            Type local1 = type1;
            if (local1.GetGenericTypeDefinition() != typeof(Func<,>))
            {
                throw new NotSupportedException();
            }
            if (local1.GetGenericArguments()[0] != typeof(T))
            {
                throw new NotSupportedException();
            }
            Dictionary<Expression, Expression> expressionsChanges = new Dictionary<Expression, Expression>();
            expressionsChanges.Add(node.Parameters[0], this.m_instance);
            return SubstituteExpressionVisitor.Substitute(node.Body, expressionsChanges);
        }

        protected internal override Expression VisitPrimitive(PrimitiveExpression node)
        {
            throw new NotSupportedException();
        }

        protected internal override Expression VisitPrimitiveComparison(PrimitiveComparisonExpression node)
        {
            Expression right = this.Visit(node.Expression);
            switch (node.PrimitiveType)
            {
                case PrimitiveType.Date:
                {
                    DateTime date = ((DateTime) node.Value).Date;
                    return Expression.AndAlso(Expression.LessThanOrEqual(Expression.Constant(date, right.Type), right), Expression.LessThan(right, Expression.Constant(date.AddDays(1.0), right.Type)));
                }
                case PrimitiveType.Decimal:
                    return Expression.Equal(Expression.Convert(right, typeof(decimal?)), Expression.Constant(node.Value, typeof(decimal?)));

                case PrimitiveType.Float:
                    return Expression.Equal(Expression.Convert(right, typeof(double?)), Expression.Constant(node.Value, typeof(double?)));

                case PrimitiveType.Int:
                    return Expression.Equal(Expression.Convert(right, typeof(long?)), Expression.Constant(node.Value, typeof(long?)));

                case PrimitiveType.String:
                {
                    Type[] types = new Type[] { typeof(string), typeof(StringComparison) };
                    return Expression.AndAlso(Expression.Not(Expression.Call(typeof(string).GetMethod("IsNullOrEmpty"), right)), Expression.LessThanOrEqual(Expression.Constant(0), Expression.Call(right, typeof(string).GetMethod("IndexOf", types), Expression.Constant(node.Value), Expression.Constant(StringComparison.CurrentCultureIgnoreCase))));
                }
            }
            throw new NotSupportedException();
        }
    }
}

