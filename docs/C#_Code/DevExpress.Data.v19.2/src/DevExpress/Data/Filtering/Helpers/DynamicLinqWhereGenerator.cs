namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class DynamicLinqWhereGenerator : BaseWhereGenerator, ICriteriaVisitor<string>
    {
        public static bool ThrowOnInvalidNames;

        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(InOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        string ICriteriaVisitor<string>.Visit(UnaryOperator theOperator);
        protected override string FormatOperandProperty(OperandProperty operand);
        private static string GetValidName(string identifier);
        private static bool isNeedAt(string validName);
        protected override string VisitInternal(FunctionOperator theOperator);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DynamicLinqWhereGenerator.<>c <>9;
            public static Func<char, int, bool> <>9__6_0;

            static <>c();
            internal bool <GetValidName>b__6_0(char ch, int pos);
        }
    }
}

