namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;

    public abstract class ListServerDataViewBase : DefaultDataView
    {
        private static string ThisString = "This";

        protected ListServerDataViewBase(bool selectNullValue, IEnumerable serverSource, string valueMember, string displayMember) : base(selectNullValue, serverSource, valueMember, displayMember)
        {
        }

        public static CriteriaOperator CreateCriteriaForDisplayColumn(DataAccessor dataAccessor) => 
            dataAccessor.HasDisplayMember ? new OperandProperty(dataAccessor.DisplayMember) : new OperandProperty(ThisString);

        public static CriteriaOperator CreateCriteriaForValueColumn(DataAccessor dataAccessor) => 
            dataAccessor.HasValueMember ? new OperandProperty(dataAccessor.ValueMember) : new OperandProperty(ThisString);

        protected CriteriaOperator GetCriteriaForValueColumn() => 
            CreateCriteriaForValueColumn(base.DataAccessor);
    }
}

