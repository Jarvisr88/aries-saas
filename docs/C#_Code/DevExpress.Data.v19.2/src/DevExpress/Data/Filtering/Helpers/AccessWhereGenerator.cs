namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class AccessWhereGenerator : BaseWhereGenerator, ICriteriaVisitor<string>
    {
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        protected override string FormatOperandProperty(OperandProperty operand);
        protected override string VisitInternal(FunctionOperator theOperator);
    }
}

