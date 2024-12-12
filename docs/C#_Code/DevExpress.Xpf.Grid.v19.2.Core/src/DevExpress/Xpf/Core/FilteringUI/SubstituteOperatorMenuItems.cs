namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal delegate OperatorMenuItemsSubstitutionInfo<FilterEditorOperatorItem> SubstituteOperatorMenuItems(OperatorMenuItemsSubstitutionInfo<FilterEditorOperatorItem> operators, CriteriaOperator filter, string propertyName);
}

