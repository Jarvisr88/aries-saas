namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class CriteriaToCStyleParameterlessProcessor : CriteriaToStringParameterlessProcessor
    {
        protected static CriteriaToCStyleParameterlessProcessor Instance;

        static CriteriaToCStyleParameterlessProcessor();
        protected CriteriaToCStyleParameterlessProcessor();
        public static string GetCOperatorString(BinaryOperatorType opType);
        public static string GetCOperatorString(GroupOperatorType opType);
        public static string GetCOperatorString(UnaryOperatorType opType);
        public override string GetOperatorString(BinaryOperatorType opType);
        public override string GetOperatorString(GroupOperatorType opType);
        public override string GetOperatorString(UnaryOperatorType opType);
        public static string ToString(CriteriaOperator operand);
    }
}

