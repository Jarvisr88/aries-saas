namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionDateTimeTodayConverter : FunctionConverter
    {
        private int dayDelta;

        public FunctionDateTimeTodayConverter(int dayDelta)
        {
            this.dayDelta = dayDelta;
        }

        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 0;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            expression.Volatile = true;
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(new XlPtgInt(Math.Abs(this.dayDelta)));
            if (this.dayDelta < 0)
            {
                expression.Add(new XlPtgBinaryOperator(4));
            }
            else
            {
                expression.Add(new XlPtgBinaryOperator(3));
            }
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

