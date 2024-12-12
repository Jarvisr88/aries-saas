namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlTruncFunction : XlFunctionBase
    {
        public XlTruncFunction(IXlFormulaParameter value) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { value };
        }

        public XlTruncFunction(IXlFormulaParameter value, int num_digits) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { value, new XlFormulaParameter(num_digits) };
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            0xc5;

        protected override string FunctionName =>
            "TRUNC";

        public override bool IsValidParametersCount =>
            (base.Parameters.Count == 1) || (base.Parameters.Count == 2);
    }
}

