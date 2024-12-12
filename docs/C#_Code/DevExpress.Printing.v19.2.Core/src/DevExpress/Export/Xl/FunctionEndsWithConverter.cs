namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionEndsWithConverter : FunctionConverter
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
            expression.Add(base.CreateFuncThing(0x20));
            expression.Add(base.CreateFuncVarThing(0x74, 2));
            base.ProcessOperand(operands, visitor, 1);
            expression.Add(new XlPtgBinaryOperator(11));
            return true;
        }
    }
}

