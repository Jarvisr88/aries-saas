namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionContainsTextConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 2;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperand(operands, visitor, 1);
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(base.CreateFuncVarThing(0x52, 2));
            expression.Add(base.CreateFuncThing(3));
            expression.Add(base.CreateFuncThing(0x26));
            return true;
        }
    }
}

