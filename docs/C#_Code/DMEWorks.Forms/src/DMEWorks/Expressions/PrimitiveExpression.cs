namespace DMEWorks.Expressions
{
    using System;
    using System.Linq.Expressions;

    public abstract class PrimitiveExpression : Expression
    {
        protected PrimitiveExpression()
        {
        }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            DmeworksExpressionVisitor visitor2 = visitor as DmeworksExpressionVisitor;
            if (visitor2 == null)
            {
                throw new NotSupportedException();
            }
            return visitor2.VisitPrimitive(this);
        }

        public virtual PrimitiveType GetPrimitiveType()
        {
            PrimitiveType type;
            if (!Utilities.TryGetPrimitiveType(this.Type, out type))
            {
                throw new NotSupportedException("data type is not supported");
            }
            return type;
        }

        public sealed override ExpressionType NodeType =>
            ExpressionType.MemberAccess;
    }
}

