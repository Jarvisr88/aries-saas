namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Filtering;
    using System;

    public class ClientCriteriaVisitorBase : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(JoinOperand theOperand);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(GroupOperator theOperator);
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(UnaryOperator theOperator);
        protected CriteriaOperator Process(CriteriaOperator criteriaOperator);
        private void ProcessCollection(CriteriaOperatorCollection operands);
        public virtual CriteriaOperator Visit(AggregateOperand theOperand);
        public virtual CriteriaOperator Visit(BetweenOperator theOperator);
        public virtual CriteriaOperator Visit(BinaryOperator theOperator);
        public virtual CriteriaOperator Visit(FunctionOperator theOperator);
        public virtual CriteriaOperator Visit(InOperator theOperator);
        public virtual CriteriaOperator Visit(OperandProperty theOperand);
        public virtual CriteriaOperator Visit(OperandValue theOperand);
    }
}

