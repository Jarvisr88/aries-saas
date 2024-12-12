namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public abstract class ClientCriteriaLazyPatcherBase : IClientCriteriaVisitor<CriteriaOperator>, ICriteriaVisitor<CriteriaOperator>
    {
        protected ClientCriteriaLazyPatcherBase();
        protected static bool AreSequenceReferenceEqual<O>(IList<O> first, IList<O> second);
        [DXHelpExclude(true)]
        public static CriteriaOperator CreateFunctionOperatorNormalized(FunctionOperatorType opType, IEnumerable<CriteriaOperator> operands);
        public static CriteriaOperator NewIfDifferent(FunctionOperator theOperator, IEnumerable<CriteriaOperator> processedOperands);
        public static CriteriaOperator NewIfDifferent(GroupOperator theOperator, IEnumerable<CriteriaOperator> processedOperands);
        public static UnaryOperator NewIfDifferent(UnaryOperator theOperator, CriteriaOperator processedOperand);
        public static BinaryOperator NewIfDifferent(BinaryOperator theOperator, CriteriaOperator processedLeft, CriteriaOperator processedRight);
        public static InOperator NewIfDifferent(InOperator theOperator, CriteriaOperator processedLeft, IEnumerable<CriteriaOperator> processedOperands);
        public static JoinOperand NewIfDifferent(JoinOperand theOperand, CriteriaOperator processedCondition, CriteriaOperator processedExpression);
        public static JoinOperand NewIfDifferent(JoinOperand theOperand, CriteriaOperator processedCondition, IEnumerable<CriteriaOperator> processedCustomAggregateOperands);
        public static AggregateOperand NewIfDifferent(AggregateOperand theOperand, OperandProperty processedProperty, CriteriaOperator processedExpression, CriteriaOperator processedCondition);
        public static AggregateOperand NewIfDifferent(AggregateOperand theOperand, OperandProperty processedProperty, IEnumerable<CriteriaOperator> processedCustomAggregateOperands, CriteriaOperator processedCondition);
        public static BetweenOperator NewIfDifferent(BetweenOperator theOperator, CriteriaOperator processedTest, CriteriaOperator processedBegin, CriteriaOperator processedEnd);
        public CriteriaOperator Process(CriteriaOperator op);
        protected IEnumerable<CriteriaOperator> Process(IEnumerable<CriteriaOperator> ops);
        public abstract CriteriaOperator Visit(AggregateOperand theOperand);
        public virtual CriteriaOperator Visit(BetweenOperator theOperator);
        public virtual CriteriaOperator Visit(BinaryOperator theOperator);
        public virtual CriteriaOperator Visit(FunctionOperator theOperator);
        public virtual CriteriaOperator Visit(GroupOperator theOperator);
        public virtual CriteriaOperator Visit(InOperator theOperator);
        public abstract CriteriaOperator Visit(JoinOperand theOperand);
        public virtual CriteriaOperator Visit(OperandProperty theOperand);
        public virtual CriteriaOperator Visit(OperandValue theOperand);
        public virtual CriteriaOperator Visit(UnaryOperator theOperator);

        public abstract class AggregatesAsIsBase : ClientCriteriaLazyPatcherBase
        {
            protected AggregatesAsIsBase();
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(JoinOperand theOperand);
        }

        public abstract class AggregatesCommonProcessingBase : ClientCriteriaLazyPatcherBase
        {
            protected AggregatesCommonProcessingBase();
            protected virtual OperandProperty ProcessCollectionProperty(OperandProperty collectionProperty);
            public override CriteriaOperator Visit(AggregateOperand theOperand);
            public override CriteriaOperator Visit(JoinOperand theOperand);
        }
    }
}

