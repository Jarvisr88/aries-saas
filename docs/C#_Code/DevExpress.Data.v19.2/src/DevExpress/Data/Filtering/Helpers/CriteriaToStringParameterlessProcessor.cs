namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    public abstract class CriteriaToStringParameterlessProcessor : CriteriaToStringBase
    {
        private const string nullString = "null";

        protected CriteriaToStringParameterlessProcessor();
        private static string FixNonFixedText(string toFix, bool isLegacy, TypeCode tc);
        private static string GetSuffix(TypeCode tc);
        internal static TypeConverter GetTypeConverter(object value);
        internal static TypeConverter GetTypeConverter(Type type);
        public static string OperandValueOrParameterToString(OperandValue val, bool isLegacy);
        private static string UserTypeToString(object value, bool isLegacy);
        public static CriteriaToStringVisitResult ValueToCriteriaToStringVisitResult(OperandValue operand);
        public static string ValueToString(object value);
        public static string ValueToString(object value, bool isLegacy);
        public override CriteriaToStringVisitResult Visit(OperandValue operand);
    }
}

