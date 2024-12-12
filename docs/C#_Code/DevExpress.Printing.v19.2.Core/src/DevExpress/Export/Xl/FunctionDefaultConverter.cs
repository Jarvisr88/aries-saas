namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionDefaultConverter : FunctionConverter
    {
        private readonly int funcCode;
        private readonly int expectedParamCount;

        public FunctionDefaultConverter(int funcCode, int expectedParamCount)
        {
            this.funcCode = funcCode;
            this.expectedParamCount = expectedParamCount;
        }

        protected virtual bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == this.expectedParamCount;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            int count = operands.Count;
            if (!this.CheckCompliance(count))
            {
                return false;
            }
            base.ProcessOperandList(operands, visitor);
            expression.Add(this.PrepareFuncThing(count));
            expression.Volatile |= this.IsVolatileFunction(this.FuncCode);
            return true;
        }

        protected bool IsVolatileFunction(int functionCode) => 
            (functionCode == 0x4a) || (functionCode == 0xdd);

        protected virtual XlPtgBase PrepareFuncThing(int actualParamsCount) => 
            base.CreateFuncThing(this.FuncCode);

        public int ExpectedParamCount =>
            this.expectedParamCount;

        public int FuncCode =>
            this.funcCode;
    }
}

