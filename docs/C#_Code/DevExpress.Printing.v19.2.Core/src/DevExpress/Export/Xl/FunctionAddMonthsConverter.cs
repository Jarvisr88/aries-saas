namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionAddMonthsConverter : FunctionConverter
    {
        private int delta;

        public FunctionAddMonthsConverter(int delta)
        {
            this.delta = delta;
        }

        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 2;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperandList(operands, visitor);
            if (this.delta > 1)
            {
                expression.Add(new XlPtgInt(this.delta));
                expression.Add(new XlPtgBinaryOperator(5));
            }
            expression.Add(base.CreateFuncVarThing(0x1c1, 2));
            return true;
        }
    }
}

