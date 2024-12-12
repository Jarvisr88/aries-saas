namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionGetDayOfWeekConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 1;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperandList(operands, visitor);
            expression.Add(new XlPtgInt(1));
            expression.Add(base.CreateFuncVarThing(70, 2));
            expression.Add(new XlPtgInt(1));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

