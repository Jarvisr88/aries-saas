namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionGetDayOfYearConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 1;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(new XlPtgInt(1));
            expression.Add(base.CreateFuncThing(0x11d));
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(base.CreateFuncThing(0x45));
            expression.Add(new XlPtgInt(1));
            expression.Add(new XlPtgInt(1));
            expression.Add(base.CreateFuncThing(0x41));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgInt(1));
            expression.Add(new XlPtgBinaryOperator(3));
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

