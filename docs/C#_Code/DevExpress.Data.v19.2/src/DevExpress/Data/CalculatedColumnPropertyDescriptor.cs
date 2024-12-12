namespace DevExpress.Data
{
    using DevExpress.Data.Browsing;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.ComponentModel;

    public class CalculatedColumnPropertyDescriptor : CalculatedPropertyDescriptorBase
    {
        private ComparativePropertyDescriptorCollection comparativeDescriptors;

        public CalculatedColumnPropertyDescriptor(DevExpress.Data.CalculatedColumn calculatedColumn, ComparativePropertyDescriptorCollection comparativeDescriptors);
        protected override ExpressionEvaluator CreateExpressionEvaluator(EvaluatorContextDescriptor contextDescriptor, CriteriaOperator criteriaOperator);

        public override string DisplayName { get; }

        public override string Name { get; }

        private DevExpress.Data.CalculatedColumn CalculatedColumn { get; }

        public override AttributeCollection Attributes { get; }
    }
}

