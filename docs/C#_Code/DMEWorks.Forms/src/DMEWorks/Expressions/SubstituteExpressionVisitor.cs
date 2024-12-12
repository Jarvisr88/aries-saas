namespace DMEWorks.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal sealed class SubstituteExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> m_substitutions;

        private SubstituteExpressionVisitor(Dictionary<Expression, Expression> expressionsChanges)
        {
            this.m_substitutions = new Dictionary<Expression, Expression>(expressionsChanges, null);
        }

        public static Expression Substitute(Expression expressionToVisit, Dictionary<Expression, Expression> expressionsChanges) => 
            new SubstituteExpressionVisitor(expressionsChanges).Visit(expressionToVisit);

        public override Expression Visit(Expression node)
        {
            Expression expression;
            return ((node != null) ? (!this.m_substitutions.TryGetValue(node, out expression) ? base.Visit(node) : expression) : null);
        }
    }
}

