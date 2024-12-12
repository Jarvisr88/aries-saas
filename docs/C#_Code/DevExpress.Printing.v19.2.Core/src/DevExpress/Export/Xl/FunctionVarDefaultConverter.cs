namespace DevExpress.Export.Xl
{
    using System;

    internal class FunctionVarDefaultConverter : FunctionDefaultConverter
    {
        private readonly int expectedMaxParamCount;

        public FunctionVarDefaultConverter(int funcCode, int expectedMinParamCount, int expectedMaxParamCount) : base(funcCode, expectedMinParamCount)
        {
            this.expectedMaxParamCount = expectedMaxParamCount;
        }

        protected override bool CheckCompliance(int actualParamsCount) => 
            (actualParamsCount >= base.ExpectedParamCount) && (actualParamsCount <= this.ExpectedMaxParamCount);

        protected override XlPtgBase PrepareFuncThing(int actualParamsCount) => 
            base.CreateFuncVarThing(base.FuncCode, actualParamsCount);

        public int ExpectedMaxParamCount =>
            this.expectedMaxParamCount;
    }
}

