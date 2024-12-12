namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal class TopBottomFilterFunctionOperator : FunctionOperator
    {
        public TopBottomFilterFunctionOperator(CriteriaOperator[] operands, string displayPropertyName) : base(FunctionOperatorType.Custom, operands)
        {
            this.<DisplayPropertyName>k__BackingField = displayPropertyName;
        }

        public string DisplayPropertyName { get; }
    }
}

