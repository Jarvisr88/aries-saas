namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    public class ClientCriteriaVisitorBase : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(AggregateOperand theOperand);
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(JoinOperand theOperand);
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(OperandProperty theOperand);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BetweenOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BinaryOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(FunctionOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(GroupOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(InOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(OperandValue theOperand);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(UnaryOperator theOperator);
        protected CriteriaOperator Process(CriteriaOperator input);
        protected CriteriaOperatorCollection ProcessCollection(CriteriaOperatorCollection operands, out bool modified);
        protected virtual CriteriaOperator Visit(BetweenOperator theOperator);
        protected virtual CriteriaOperator Visit(BinaryOperator theOperator);
        protected virtual CriteriaOperator Visit(FunctionOperator theOperator);
        protected virtual CriteriaOperator Visit(GroupOperator theOperator);
        protected virtual CriteriaOperator Visit(InOperator theOperator);
        protected virtual CriteriaOperator Visit(JoinOperand theOperand);
        protected virtual CriteriaOperator Visit(OperandProperty theOperand);
        protected virtual CriteriaOperator Visit(OperandValue theOperand);
        protected virtual CriteriaOperator Visit(UnaryOperator theOperator);
        protected virtual CriteriaOperator Visit(AggregateOperand theOperand, bool processCollectionProperty);
    }
}

