namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public abstract class ClientCriteriaClonerBase : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        protected ClientCriteriaClonerBase();
        public CriteriaOperator Process(CriteriaOperator op);
        protected IEnumerable<CriteriaOperator> Process(IEnumerable<CriteriaOperator> ops);
        protected virtual OperandProperty ProcessCollectionProperty(OperandProperty collectionProperty);
        public virtual CriteriaOperator Visit(AggregateOperand theOperand);
        public virtual CriteriaOperator Visit(BetweenOperator theOperator);
        public virtual CriteriaOperator Visit(BinaryOperator theOperator);
        public virtual CriteriaOperator Visit(FunctionOperator theOperator);
        public virtual CriteriaOperator Visit(GroupOperator theOperator);
        public virtual CriteriaOperator Visit(InOperator theOperator);
        public virtual CriteriaOperator Visit(JoinOperand theOperand);
        public virtual CriteriaOperator Visit(OperandProperty theOperand);
        public abstract CriteriaOperator Visit(OperandValue theOperand);
        public virtual CriteriaOperator Visit(UnaryOperator theOperator);

        public abstract class DeepValuesCloneBase : ClientCriteriaClonerBase
        {
            protected DeepValuesCloneBase();
            public override CriteriaOperator Visit(OperandValue theOperand);
        }

        public abstract class NoValuesCloneBase : ClientCriteriaClonerBase
        {
            protected NoValuesCloneBase();
            public override CriteriaOperator Visit(OperandValue theOperand);
        }
    }
}

