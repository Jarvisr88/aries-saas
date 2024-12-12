namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionIsThisMonthConverter : FunctionConverter
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
            base.ProcessOperandList(operands, visitor);
            expression.Add(base.CreateFuncThing(0x45));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(base.CreateFuncThing(0x45));
            expression.Add(new XlPtgBinaryOperator(11));
            base.ProcessOperandList(operands, visitor);
            expression.Add(base.CreateFuncThing(0x44));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(base.CreateFuncThing(0x44));
            expression.Add(new XlPtgBinaryOperator(11));
            expression.Add(base.CreateFuncVarThing(0x24, 2));
            return true;
        }
    }
}

