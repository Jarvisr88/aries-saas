namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlColumnFunction : XlFunctionBase
    {
        public XlColumnFunction() : base(new IXlFormulaParameter[0])
        {
        }

        public XlColumnFunction(XlCellRange range) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { range };
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            9;

        protected override string FunctionName =>
            "COLUMN";

        public override bool IsValidParametersCount =>
            (base.Parameters.Count == 0) || (base.Parameters.Count == 1);
    }
}

