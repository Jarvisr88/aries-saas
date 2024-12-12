namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public abstract class ClientCriteriaActionBase : IClientCriteriaVisitor, ICriteriaVisitor
    {
        protected ClientCriteriaActionBase();
        public void Process(CriteriaOperator op);
        protected void Process(IEnumerable<CriteriaOperator> ops);
        public abstract void Visit(AggregateOperand theOperand);
        public virtual void Visit(BetweenOperator theOperator);
        public virtual void Visit(BinaryOperator theOperator);
        public virtual void Visit(FunctionOperator theOperator);
        public virtual void Visit(GroupOperator theOperator);
        public virtual void Visit(InOperator theOperator);
        public abstract void Visit(JoinOperand theOperand);
        public virtual void Visit(OperandProperty theOperand);
        public virtual void Visit(OperandValue theOperand);
        public virtual void Visit(UnaryOperator theOperator);

        public abstract class AggregatesCommonProcessingBase : ClientCriteriaActionBase
        {
            protected AggregatesCommonProcessingBase();
            public override void Visit(AggregateOperand theOperand);
            public override void Visit(JoinOperand theOperand);
        }

        public abstract class IgnoreAggregatesBase : ClientCriteriaActionBase
        {
            protected IgnoreAggregatesBase();
            public override void Visit(AggregateOperand theOperand);
            public override void Visit(JoinOperand theOperand);
        }
    }
}

