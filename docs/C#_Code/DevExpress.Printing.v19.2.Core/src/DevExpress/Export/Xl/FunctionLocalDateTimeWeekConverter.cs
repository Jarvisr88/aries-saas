namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionLocalDateTimeWeekConverter : FunctionConverterWeekdayBase
    {
        private int offset;

        public FunctionLocalDateTimeWeekConverter(int offset)
        {
            this.offset = offset;
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
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(new XlPtgInt(base.GetWeekdayReturnType()));
            expression.Add(base.CreateFuncVarThing(70, 2));
            expression.Add(new XlPtgBinaryOperator(4));
            if (this.offset < -1)
            {
                expression.Add(new XlPtgNum((double) (this.offset + 1)));
            }
            else
            {
                expression.Add(new XlPtgInt(this.offset + 1));
            }
            expression.Add(base.CreateFuncVarThing(4, 2));
            return true;
        }
    }
}

