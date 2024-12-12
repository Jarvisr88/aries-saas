namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class DataSetWhereGenerator : BaseWhereGenerator, ICriteriaVisitor<string>
    {
        private const string nullString = "null";

        private static string AsString(object value);
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        private static string FixNonFixedText(string toFix);
        protected override string FormatOperandProperty(OperandProperty operand);
        protected override string VisitInternal(FunctionOperator theOperator);
    }
}

