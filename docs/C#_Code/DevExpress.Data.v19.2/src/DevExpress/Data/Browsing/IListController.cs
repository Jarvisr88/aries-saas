namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;

    public interface IListController
    {
        object GetColumnValue(int position, string columnName);
        object GetItem(int index);
        void SetList(IList list);

        int Count { get; }
    }
}

