namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class OracleWhereGenerator : BaseWhereGenerator, ICriteriaVisitor<string>
    {
        private readonly Func<OperandProperty, string> propertyFormatter;

        public OracleWhereGenerator(bool forceQuotesOnOperandProperties);
        public OracleWhereGenerator(Func<OperandProperty, string> propertyFormatter = null);
        public static string DefaultFormatOperandProperty(OperandProperty operand, bool forceQuotesOnOperandProperties);
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        protected override string FormatOperandProperty(OperandProperty operand);
        private static bool isValidUnescapedName(string nm);
        protected override string VisitInternal(FunctionOperator theOperator);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OracleWhereGenerator.<>c <>9;
            public static Func<char, bool> <>9__6_0;

            static <>c();
            internal bool <isValidUnescapedName>b__6_0(char ch);
        }
    }
}

