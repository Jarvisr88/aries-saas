namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionDateDiffTimeConverter : FunctionConverter
    {
        private int multiplier;

        public FunctionDateDiffTimeConverter(int multiplier)
        {
            this.multiplier = multiplier;
        }

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
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgParen());
            if (this.multiplier > 0x7fff)
            {
                expression.Add(new XlPtgNum((double) this.multiplier));
            }
            else
            {
                expression.Add(new XlPtgInt(this.multiplier));
            }
            expression.Add(new XlPtgBinaryOperator(5));
            expression.Add(new XlPtgInt(1));
            expression.Add(base.CreateFuncThing(0x11d));
            return true;
        }
    }
}

