namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class CollectFirstLevelPropertiesHelper : IClientCriteriaVisitor, ICriteriaVisitor
    {
        private Dictionary<OperandProperty, object> _Dictionary;

        private CollectFirstLevelPropertiesHelper();
        public static IEnumerable<OperandProperty> Collect(CriteriaOperator opa);
        public static IEnumerable<OperandProperty> Collect(IEnumerable<CriteriaOperator> ops);
        public static IEnumerable<OperandProperty> Collect(params CriteriaOperator[] ops);
        void IClientCriteriaVisitor.Visit(AggregateOperand theOperand);
        void IClientCriteriaVisitor.Visit(JoinOperand theOperand);
        void IClientCriteriaVisitor.Visit(OperandProperty theOperand);
        void ICriteriaVisitor.Visit(BetweenOperator theOperator);
        void ICriteriaVisitor.Visit(BinaryOperator theOperator);
        void ICriteriaVisitor.Visit(FunctionOperator theOperator);
        void ICriteriaVisitor.Visit(GroupOperator theOperator);
        void ICriteriaVisitor.Visit(InOperator theOperator);
        void ICriteriaVisitor.Visit(OperandValue theOperand);
        void ICriteriaVisitor.Visit(UnaryOperator theOperator);
        private void Process(CriteriaOperator opa);
        private void Process(IEnumerable<CriteriaOperator> ops);
    }
}

