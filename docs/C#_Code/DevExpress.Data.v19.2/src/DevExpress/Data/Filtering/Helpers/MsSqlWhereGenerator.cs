namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Db;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    public class MsSqlWhereGenerator : BaseWhereGenerator, ICriteriaVisitor<string>
    {
        private MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion;
        private readonly Func<OperandProperty, string> propertyFormatter;

        public MsSqlWhereGenerator(MsSqlFormatterHelper.MSSqlServerVersion sqlServerVersion, Func<OperandProperty, string> propertyFormatter = null);
        public static string DefaultFormatOperandProperty(OperandProperty operand, bool squareBrackets);
        string ICriteriaVisitor<string>.Visit(BinaryOperator theOperator);
        string ICriteriaVisitor<string>.Visit(OperandValue theOperand);
        protected override string FormatOperandProperty(OperandProperty operand);
        protected override string VisitInternal(FunctionOperator theOperator);
    }
}

