namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class QueryOperatorsEventArgsBase<T> where T: OperatorItemBase
    {
        protected QueryOperatorsEventArgsBase(string fieldName)
        {
            this.<FieldName>k__BackingField = fieldName;
        }

        public string FieldName { get; }

        public T DefaultOperator { get; set; }
    }
}

