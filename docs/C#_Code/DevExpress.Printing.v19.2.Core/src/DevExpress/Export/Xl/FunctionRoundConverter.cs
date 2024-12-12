namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionRoundConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            (actualParamsCount >= 1) && (actualParamsCount <= 2);

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            int count = operands.Count;
            if (!this.CheckCompliance(count))
            {
                return false;
            }
            base.ProcessOperandList(operands, visitor);
            if (count == 1)
            {
                expression.Add(new XlPtgInt(0));
            }
            expression.Add(base.CreateFuncThing(0x1b));
            return true;
        }
    }
}

