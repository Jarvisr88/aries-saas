namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;

    public class IsLogicalCriteriaChecker : IQueryCriteriaVisitor<BooleanCriteriaState>, ICriteriaVisitor<BooleanCriteriaState>, IClientCriteriaVisitor<BooleanCriteriaState>
    {
        private static IsLogicalCriteriaChecker instance;

        static IsLogicalCriteriaChecker();
        public static BooleanCriteriaState GetBooleanState(CriteriaOperator operand);
        public BooleanCriteriaState Process(CriteriaOperator operand);
        public BooleanCriteriaState Visit(AggregateOperand theOperand);
        public BooleanCriteriaState Visit(BetweenOperator theOperator);
        public BooleanCriteriaState Visit(BinaryOperator theOperator);
        public BooleanCriteriaState Visit(FunctionOperator theOperator);
        public BooleanCriteriaState Visit(GroupOperator theOperator);
        public BooleanCriteriaState Visit(InOperator theOperator);
        public BooleanCriteriaState Visit(JoinOperand theOperand);
        public BooleanCriteriaState Visit(OperandProperty theOperand);
        public BooleanCriteriaState Visit(OperandValue theOperand);
        public BooleanCriteriaState Visit(UnaryOperator theOperator);
        public BooleanCriteriaState Visit(QueryOperand theOperand);
        public BooleanCriteriaState Visit(QuerySubQueryContainer theOperand);

        public static IsLogicalCriteriaChecker Instance { get; }
    }
}

