namespace DevExpress.Data.Linq
{
    using DevExpress.Data.Filtering;
    using System.Linq.Expressions;

    public interface ICriteriaToExpressionConverter
    {
        Expression Convert(ParameterExpression thisExpression, CriteriaOperator op);
    }
}

