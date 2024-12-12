namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.ComponentModel;

    public class SortingInfo
    {
        private readonly string fieldName;
        private readonly ListSortDirection orderBy;

        public SortingInfo(string fieldName, ListSortDirection orderBy)
        {
            this.fieldName = fieldName;
            this.orderBy = orderBy;
        }

        public string FieldName =>
            this.fieldName;

        public ListSortDirection OrderBy =>
            this.orderBy;
    }
}

