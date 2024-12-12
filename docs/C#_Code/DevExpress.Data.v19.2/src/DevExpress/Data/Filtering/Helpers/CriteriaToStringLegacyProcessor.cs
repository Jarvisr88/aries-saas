namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class CriteriaToStringLegacyProcessor : CriteriaToBasicStyleParameterlessProcessor
    {
        protected static CriteriaToStringLegacyProcessor Instance;

        static CriteriaToStringLegacyProcessor();
        protected CriteriaToStringLegacyProcessor();
        public static string ToString(CriteriaOperator operand);
        public override CriteriaToStringVisitResult Visit(OperandValue operand);
    }
}

