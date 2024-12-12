namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Globalization;

    internal class XlDivideOperator : XlBinaryOperator
    {
        public XlDivideOperator(IXlFormulaParameter left, IXlFormulaParameter right) : base(left, right)
        {
        }

        protected override string ToStringCore(CultureInfo culture) => 
            "/";

        public override short TypeCode =>
            6;
    }
}

