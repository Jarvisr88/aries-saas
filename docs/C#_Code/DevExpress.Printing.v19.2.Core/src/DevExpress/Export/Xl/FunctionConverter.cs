namespace DevExpress.Export.Xl
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Globalization;

    internal abstract class FunctionConverter : IFunctionConverter
    {
        private CultureInfo culture;

        protected FunctionConverter()
        {
        }

        public abstract bool ConvertFunction(CriteriaOperatorCollection operands, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, XlExpression expression);
        protected XlPtgBase CreateFuncThing(int funcCode) => 
            new XlPtgFunc(funcCode, XlPtgDataType.Value);

        protected XlPtgBase CreateFuncVarThing(int funcCode, int paramCount) => 
            new XlPtgFuncVar(funcCode, paramCount, XlPtgDataType.Value);

        protected void ProcessOperand(CriteriaOperatorCollection collection, IClientCriteriaVisitor<CriteriaPriorityClass> visitor, int index)
        {
            collection[index].Accept<CriteriaPriorityClass>(visitor);
        }

        protected void ProcessOperandList(CriteriaOperatorCollection collection, IClientCriteriaVisitor<CriteriaPriorityClass> visitor)
        {
            foreach (CriteriaOperator @operator in collection)
            {
                @operator.Accept<CriteriaPriorityClass>(visitor);
            }
        }

        public CultureInfo Culture
        {
            get => 
                (this.culture != null) ? this.culture : CultureInfo.InvariantCulture;
            set => 
                this.culture = value;
        }
    }
}

