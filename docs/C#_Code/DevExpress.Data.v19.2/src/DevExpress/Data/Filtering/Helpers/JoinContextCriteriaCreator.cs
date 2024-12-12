namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class JoinContextCriteriaCreator : ClientCriteriaVisitorBase
    {
        private int level;
        private readonly bool zeroLevelLeave;
        private readonly EvaluatorContext[] contexts;
        private readonly Dictionary<JoinContextPropertyInfo, bool> zeroLevelProperies;
        private Dictionary<string, EvaluatorProperty> propertyCache;

        public JoinContextCriteriaCreator(EvaluatorContext[] contexts);
        public JoinContextCriteriaCreator(EvaluatorContext[] contexts, bool zeroLevelLeave);
        private EvaluatorProperty GetProperty(string propertyPath);
        public static CriteriaOperator Process(EvaluatorContext[] contexts, CriteriaOperator criteria);
        public static CriteriaOperator ProcessZeroLevelLeave(EvaluatorContext[] contexts, CriteriaOperator criteria, out JoinContextPropertyInfoSet zeroLevelProperties);
        protected override CriteriaOperator Visit(JoinOperand theOperand);
        protected override CriteriaOperator Visit(OperandProperty theOperand);
        protected override CriteriaOperator Visit(AggregateOperand theOperand, bool processCollectionProperty);
    }
}

