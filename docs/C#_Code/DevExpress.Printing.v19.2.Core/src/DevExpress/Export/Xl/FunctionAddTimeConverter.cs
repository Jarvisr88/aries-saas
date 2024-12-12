namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionAddTimeConverter : FunctionConverter
    {
        private int divisor;

        public FunctionAddTimeConverter(int divisor)
        {
            this.divisor = divisor;
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
            if (this.divisor > 0x7fff)
            {
                expression.Add(new XlPtgNum((double) this.divisor));
            }
            else
            {
                expression.Add(new XlPtgInt(this.divisor));
            }
            expression.Add(new XlPtgBinaryOperator(6));
            expression.Add(new XlPtgBinaryOperator(3));
            expression.Add(new XlPtgParen());
            return true;
        }
    }
}

