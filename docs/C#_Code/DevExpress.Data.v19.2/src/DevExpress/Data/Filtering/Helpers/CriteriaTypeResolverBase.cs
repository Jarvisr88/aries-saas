namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public abstract class CriteriaTypeResolverBase : ICriteriaVisitor<CriteriaTypeResolverResult>
    {
        protected CriteriaTypeResolverBase();
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(BetweenOperator theOperator);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(BinaryOperator theOperator);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(FunctionOperator theOperator);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(GroupOperator theOperator);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(InOperator theOperator);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(OperandValue theOperand);
        CriteriaTypeResolverResult ICriteriaVisitor<CriteriaTypeResolverResult>.Visit(UnaryOperator theOperator);
        private Type FnCustom(FunctionOperator theOperator);
        public CriteriaTypeResolverResult FnIif(FunctionOperator theOperator);
        private Type FnIifProcess(CriteriaOperator operand);
        private static Type GetBinaryPromotionType(Type left, Type right, BinaryOperatorType exceptionType);
        protected virtual Type GetCustomAggregateType(string customAggregateName, params Type[] operands);
        protected virtual Type GetCustomFunctionType(string functionName, params Type[] operands);
        private static Type GetMaxMinPromotionType(Type left, Type right, FunctionOperatorType operatorType);
        public static Type GetTypeFromCode(TypeCode typeCode);
        protected CriteriaTypeResolverResult Process(CriteriaOperator criteria);
    }
}

