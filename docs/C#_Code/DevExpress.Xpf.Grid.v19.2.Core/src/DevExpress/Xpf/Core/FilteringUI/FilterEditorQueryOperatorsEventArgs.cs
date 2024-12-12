namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FilterEditorQueryOperatorsEventArgs : QueryOperatorsEventArgsBase<FilterEditorOperatorItem>
    {
        internal FilterEditorQueryOperatorsEventArgs(CriteriaOperator filter, string fieldName) : base(fieldName)
        {
            this.<Filter>k__BackingField = filter;
        }

        public CriteriaOperator Filter { get; }

        public FilterEditorOperatorItemList Operators { get; set; }
    }
}

