namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlIfFunction : XlFunctionBase
    {
        public XlIfFunction(IXlFormulaParameter predicate, IXlFormulaParameter thenParam, IXlFormulaParameter elseParam) : base(parameterArray1)
        {
            IXlFormulaParameter[] parameterArray1 = new IXlFormulaParameter[] { predicate, thenParam, elseParam };
        }

        public override XlPtgDataType ParamType =>
            XlPtgDataType.Reference;

        public override int FunctionCode =>
            1;

        protected override string FunctionName =>
            "IF";

        public override bool IsValidParametersCount =>
            base.Parameters.Count == 3;
    }
}

