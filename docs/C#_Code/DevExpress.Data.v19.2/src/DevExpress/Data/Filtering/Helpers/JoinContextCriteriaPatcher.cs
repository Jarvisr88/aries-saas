namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class JoinContextCriteriaPatcher : ClientCriteriaVisitorBase
    {
        private readonly JoinContextValueInfoSet valueInfoSet;

        public JoinContextCriteriaPatcher(JoinContextValueInfoSet valueInfoSet);
        public static CriteriaOperator Process(JoinContextValueInfoSet valueInfoSet, CriteriaOperator criteria);
        protected override CriteriaOperator Visit(OperandProperty theOperand);
        protected override CriteriaOperator Visit(AggregateOperand theOperand, bool processCollectionProperty);
    }
}

