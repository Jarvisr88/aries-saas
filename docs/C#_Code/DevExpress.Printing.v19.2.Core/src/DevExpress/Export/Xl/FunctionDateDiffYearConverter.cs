namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionDateDiffYearConverter : FunctionConverter
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
            expression.Add(base.CreateFuncThing(0x45));
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(base.CreateFuncThing(0x45));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

