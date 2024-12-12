namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class CriteriaToBasicStyleParameterlessProcessor : CriteriaToStringParameterlessProcessor
    {
        protected static CriteriaToBasicStyleParameterlessProcessor Instance;

        static CriteriaToBasicStyleParameterlessProcessor();
        protected CriteriaToBasicStyleParameterlessProcessor();
        public static string GetBasicOperatorString(BinaryOperatorType opType);
        public static string GetBasicOperatorString(GroupOperatorType opType);
        public static string GetBasicOperatorString(UnaryOperatorType opType);
        public override string GetOperatorString(BinaryOperatorType opType);
        public override string GetOperatorString(GroupOperatorType opType);
        public override string GetOperatorString(UnaryOperatorType opType);
        public static string ToString(CriteriaOperator operand);
    }
}

