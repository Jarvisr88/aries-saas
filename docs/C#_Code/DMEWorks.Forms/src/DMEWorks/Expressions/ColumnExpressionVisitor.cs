namespace DMEWorks.Expressions
{
    using DMEWorks.Forms;
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Text;

    internal sealed class ColumnExpressionVisitor : DmeworksExpressionVisitor
    {
        private readonly StringBuilder m_out = new StringBuilder();
        private readonly DropDownSupport.StringFilterType m_type;

        private ColumnExpressionVisitor(DropDownSupport.StringFilterType type)
        {
            this.m_type = type;
        }

        internal static string ExpressionToString(Expression node, DropDownSupport.StringFilterType type)
        {
            ColumnExpressionVisitor visitor1 = new ColumnExpressionVisitor(type);
            visitor1.Visit(node);
            return visitor1.ToString();
        }

        private void Out(char c)
        {
            this.m_out.Append(c);
        }

        private void Out(string s)
        {
            this.m_out.Append(s);
        }

        public override string ToString() => 
            this.m_out.ToString();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            string str;
            ExpressionType nodeType = node.NodeType;
            if ((nodeType - 2) <= ExpressionType.AddChecked)
            {
                str = "AND";
            }
            else
            {
                if ((nodeType - ExpressionType.Or) > ExpressionType.AddChecked)
                {
                    throw new NotSupportedException();
                }
                str = "OR";
            }
            this.Out('(');
            this.Visit(node.Left);
            this.Out(' ');
            this.Out(str);
            this.Out(' ');
            this.Visit(node.Right);
            this.Out(')');
            return node;
        }

        protected override Expression VisitLambda<TDelegate>(Expression<TDelegate> node)
        {
            throw new NotSupportedException();
        }

        protected internal override Expression VisitPrimitive(PrimitiveExpression node)
        {
            ColumnExpression expression = node as ColumnExpression;
            if (expression == null)
            {
                throw new NotSupportedException();
            }
            this.Out(DropDownSupport.QuoteName(expression.ColumnName));
            return node;
        }

        protected internal override Expression VisitPrimitiveComparison(PrimitiveComparisonExpression node)
        {
            switch (node.PrimitiveType)
            {
                case PrimitiveType.Date:
                {
                    DateTime time = (DateTime) node.Value;
                    this.Out('(');
                    this.Out(time.ToString("#MM'/'dd'/'yyyy#", CultureInfo.InvariantCulture));
                    this.Out(" <= ");
                    this.Visit(node.Expression);
                    this.Out(" AND ");
                    this.Visit(node.Expression);
                    this.Out(" < ");
                    this.Out(time.AddDays(1.0).ToString("#MM'/'dd'/'yyyy#", CultureInfo.InvariantCulture));
                    this.Out(')');
                    break;
                }
                case PrimitiveType.Decimal:
                case PrimitiveType.Float:
                case PrimitiveType.Int:
                    this.Visit(node.Expression);
                    this.Out(" = ");
                    this.Out(Convert.ToString(node.Value, CultureInfo.InvariantCulture));
                    break;

                case PrimitiveType.String:
                    this.Visit(node.Expression);
                    this.Out(" LIKE ");
                    this.Out(DropDownSupport.QuoteLike((string) node.Value, this.m_type));
                    break;

                default:
                    throw new NotSupportedException();
            }
            return node;
        }
    }
}

