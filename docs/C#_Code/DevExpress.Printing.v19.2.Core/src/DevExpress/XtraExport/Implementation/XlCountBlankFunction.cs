namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlCountBlankFunction : XlFunctionBase
    {
        public XlCountBlankFunction(XlCellRange range) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { range };
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            0x15b;

        protected override string FunctionName =>
            "COUNTBLANK";
    }
}

