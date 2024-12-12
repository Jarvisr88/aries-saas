namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionAddDaysConverter : FunctionConverter
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 2;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            base.ProcessOperandList(operands, visitor);
            expression.Add(new XlPtgBinaryOperator(3));
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

