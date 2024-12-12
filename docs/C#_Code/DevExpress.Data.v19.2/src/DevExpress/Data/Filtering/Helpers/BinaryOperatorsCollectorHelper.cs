namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class BinaryOperatorsCollectorHelper : IClientCriteriaVisitor<IEnumerable<BinaryOperator>>, ICriteriaVisitor<IEnumerable<BinaryOperator>>
    {
        private BinaryOperatorsCollectorHelper();
        public static BinaryOperator[] Collect(CriteriaOperator criteria);
        IEnumerable<BinaryOperator> IClientCriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(AggregateOperand theOperand);
        IEnumerable<BinaryOperator> IClientCriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(JoinOperand theOperand);
        IEnumerable<BinaryOperator> IClientCriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(OperandProperty theOperand);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(BetweenOperator theOperator);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(BinaryOperator theOperator);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(FunctionOperator theOperator);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(GroupOperator theOperator);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(InOperator theOperator);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(OperandValue theOperand);
        IEnumerable<BinaryOperator> ICriteriaVisitor<IEnumerable<BinaryOperator>>.Visit(UnaryOperator theOperator);
        private IEnumerable<BinaryOperator> Process(CriteriaOperator op);
        private IEnumerable<BinaryOperator> Process(IEnumerable<CriteriaOperator> ops);
    }
}

