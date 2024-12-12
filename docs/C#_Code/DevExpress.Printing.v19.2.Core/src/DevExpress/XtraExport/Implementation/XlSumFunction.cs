namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlSumFunction : XlFunctionBase
    {
        public XlSumFunction(IEnumerable<IXlFormulaParameter> parameters) : base(parameters)
        {
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            4;

        protected override string FunctionName =>
            "SUM";
    }
}

