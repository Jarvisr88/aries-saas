namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal class XlMinFunction : XlFunctionBase
    {
        public XlMinFunction(IEnumerable<IXlFormulaParameter> parameters) : base(parameters)
        {
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            6;

        protected override string FunctionName =>
            "MIN";
    }
}

