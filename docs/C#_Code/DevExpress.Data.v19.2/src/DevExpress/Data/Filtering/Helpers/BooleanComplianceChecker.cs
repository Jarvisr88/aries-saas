namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpo.DB;
    using System;

    public class BooleanComplianceChecker : IQueryCriteriaVisitor<BooleanCriteriaState>, ICriteriaVisitor<BooleanCriteriaState>, IClientCriteriaVisitor<BooleanCriteriaState>
    {
        private const string MustBeArithmetical = "Must be arithmetical bool";
        private const string MustBeLogical = "Must be logical bool";
        private static BooleanComplianceChecker instance;

        static BooleanComplianceChecker();
        public BooleanCriteriaState Process(CriteriaOperator operand);
        public void Process(CriteriaOperator operand, bool mustBeLogical);
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

        public static BooleanComplianceChecker Instance { get; }
    }
}

