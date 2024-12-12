namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionIsThisWeekConverter : FunctionConverterWeekdayBase
    {
        private bool CheckCompliance(int actualParamsCount) => 
            actualParamsCount == 1;

        public override bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression)
        {
            if (!this.CheckCompliance(operands.Count))
            {
                return false;
            }
            expression.Volatile = true;
            expression.Add(base.CreateFuncThing(0xdd));
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(new XlPtgInt(0));
            expression.Add(base.CreateFuncThing(0xd5));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(new XlPtgInt(base.GetWeekdayReturnType()));
            expression.Add(new XlPtgFuncVar(70, 2, XlPtgDataType.Reference));
            expression.Add(new XlPtgInt(1));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgBinaryOperator(10));
            base.ProcessOperand(operands, visitor, 0);
            expression.Add(new XlPtgInt(0));
            expression.Add(base.CreateFuncThing(0xd5));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgInt(7));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(new XlPtgInt(base.GetWeekdayReturnType()));
            expression.Add(new XlPtgFuncVar(70, 2, XlPtgDataType.Reference));
            expression.Add(new XlPtgBinaryOperator(4));
            expression.Add(new XlPtgBinaryOperator(10));
            expression.Add(base.CreateFuncVarThing(0x24, 2));
            return true;
        }
    }
}

