namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class EvaluatorCriteriaValidator : IClientCriteriaVisitor, ICriteriaVisitor
    {
        private readonly PropertyDescriptorCollection Properties;

        public EvaluatorCriteriaValidator(PropertyDescriptorCollection properties);
        public void Validate(CriteriaOperator criteria);
        public void Validate(IList operands);
        public virtual void Visit(AggregateOperand theOperand);
        public virtual void Visit(BetweenOperator theOperator);
        public virtual void Visit(BinaryOperator theOperator);
        public virtual void Visit(FunctionOperator theOperator);
        public virtual void Visit(GroupOperator theOperator);
        public virtual void Visit(InOperator theOperator);
        public virtual void Visit(JoinOperand theOperand);
        public virtual void Visit(OperandProperty theOperand);
        public virtual void Visit(OperandValue theOperand);
        public virtual void Visit(UnaryOperator theOperator);
    }
}

