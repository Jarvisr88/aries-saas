﻿namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;

    internal class FunctionLocalDateTimeThisMonthConverter : FunctionConverter
    {
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
            expression.Add(base.CreateFuncThing(0x45));
            expression.Add(base.CreateFuncThing(0xdd));
            expression.Add(base.CreateFuncThing(0x44));
            expression.Add(new XlPtgInt(1));
            expression.Add(base.CreateFuncThing(0x41));
            return true;
        }
    }
}

