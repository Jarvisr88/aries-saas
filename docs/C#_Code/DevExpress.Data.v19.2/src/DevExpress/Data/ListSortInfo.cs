namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public class ListSortInfo
    {
        private string propertyName;
        private ListSortDirection sortDirection;

        public ListSortInfo(string propertyName, ListSortDirection sortDirection);

        public string PropertyName { get; }

        public ListSortDirection SortDirection { get; }
    }
}

