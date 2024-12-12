namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    public class CriteriaToBasicStyleWithParametersProcessor : CriteriaToStringWithParametersProcessor
    {
        protected CriteriaToBasicStyleWithParametersProcessor();
        public override string GetOperatorString(BinaryOperatorType opType);
        public override string GetOperatorString(GroupOperatorType opType);
        public override string GetOperatorString(UnaryOperatorType opType);
        public static string ToString(CriteriaOperator criteria, out OperandValue[] parameters);
    }
}

