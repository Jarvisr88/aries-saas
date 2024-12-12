namespace DMEWorks.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public sealed class PrimitiveComparisonExpression : System.Linq.Expressions.Expression
    {
        public PrimitiveComparisonExpression(System.Linq.Expressions.Expression expression, object value)
        {
            if (expression == null)
            {
                System.Linq.Expressions.Expression local1 = expression;
                throw new ArgumentNullException("expression");
            }
            this.<Expression>k__BackingField = expression;
            this.<PrimitiveType>k__BackingField = Utilities.GetPrimitiveType(expression);
            this.<Value>k__BackingField = value;
        }

        protected override System.Linq.Expressions.Expression Accept(ExpressionVisitor visitor)
        {
            DmeworksExpressionVisitor visitor2 = visitor as DmeworksExpressionVisitor;
            if (visitor2 == null)
            {
                throw new NotSupportedException();
            }
            return visitor2.VisitPrimitiveComparison(this);
        }

        public DMEWorks.Expressions.PrimitiveType PrimitiveType { get; }

        public System.Linq.Expressions.Expression Expression { get; }

        public object Value { get; }

        public sealed override ExpressionType NodeType =>
            ExpressionType.Call;

        public sealed override System.Type Type =>
            typeof(bool);
    }
}

