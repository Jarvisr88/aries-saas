namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlCountFunction : XlFunctionBase
    {
        public XlCountFunction(IEnumerable<IXlFormulaParameter> parameters) : base(parameters)
        {
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            0;

        protected override string FunctionName =>
            "COUNT";
    }
}

