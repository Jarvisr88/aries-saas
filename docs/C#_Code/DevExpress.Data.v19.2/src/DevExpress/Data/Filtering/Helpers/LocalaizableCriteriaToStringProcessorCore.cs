namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class LocalaizableCriteriaToStringProcessorCore : CriteriaToStringBase
    {
        public readonly ILocalaizableCriteriaToStringProcessorOpNamesSource OpNamesSource;

        protected LocalaizableCriteriaToStringProcessorCore(ILocalaizableCriteriaToStringProcessorOpNamesSource opNamesSource);
        protected override string GetBetweenText();
        protected override string GetFunctionText(FunctionOperatorType operandType);
        protected override string GetInText();
        protected override string GetIsNotNullText();
        protected override string GetIsNullText();
        protected override string GetNotLikeText();
        protected override string GetOperatorString(Aggregate operandType);
        public override string GetOperatorString(BinaryOperatorType opType);
        public override string GetOperatorString(GroupOperatorType opType);
        public override string GetOperatorString(UnaryOperatorType opType);
        public static string Process(ILocalaizableCriteriaToStringProcessorOpNamesSource opNamesSource, CriteriaOperator op);
        public override CriteriaToStringVisitResult Visit(OperandValue operand);
    }
}

