namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlRowFunction : XlFunctionBase
    {
        public XlRowFunction() : base(new IXlFormulaParameter[0])
        {
        }

        public XlRowFunction(XlCellRange range) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { range };
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            8;

        protected override string FunctionName =>
            "ROW";

        public override bool IsValidParametersCount =>
            (base.Parameters.Count == 0) || (base.Parameters.Count == 1);
    }
}

