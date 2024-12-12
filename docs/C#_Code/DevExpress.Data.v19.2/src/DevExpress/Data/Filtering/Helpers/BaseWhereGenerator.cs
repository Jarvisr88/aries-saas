namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    public abstract class BaseWhereGenerator : IClientCriteriaVisitor<string>, ICriteriaVisitor<string>
    {
        protected BaseWhereGenerator();
        string IClientCriteriaVisitor<string>.Visit(AggregateOperand theOperand);
        string IClientCriteriaVisitor<string>.Visit(JoinOperand theOperand);
        string IClientCriteriaVisitor<string>.Visit(OperandProperty theOperand);
        string ICriteriaVisitor<string>.Visit(BetweenOperator theOperator);
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(FunctionOperator theOperator);
        string ICriteriaVisitor<string>.Visit(GroupOperator theOperator);
        string ICriteriaVisitor<string>.Visit(InOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        string ICriteriaVisitor<string>.Visit(UnaryOperator theOperator);
        private string FormatCustomFunction(FunctionOperator customOperator);
        protected abstract string FormatOperandProperty(OperandProperty operand);
        private static bool IsLocalDateTimeOrOutlookInterval(FunctionOperator op, out CriteriaOperator res);
        public string Process(CriteriaOperator operand);
        protected virtual string VisitInternal(FunctionOperator theOperator);
    }
}

