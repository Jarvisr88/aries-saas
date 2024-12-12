namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlAverageFunction : XlFunctionBase
    {
        public XlAverageFunction(IEnumerable<IXlFormulaParameter> parameters) : base(parameters)
        {
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            5;

        protected override string FunctionName =>
            "AVERAGE";
    }
}

