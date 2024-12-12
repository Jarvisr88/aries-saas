namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlConcatenateFunction : XlFunctionBase
    {
        public XlConcatenateFunction(IEnumerable<IXlFormulaParameter> parameters) : base(parameters)
        {
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Value;

        public override int FunctionCode =>
            0x150;

        protected override string FunctionName =>
            "CONCATENATE";
    }
}

