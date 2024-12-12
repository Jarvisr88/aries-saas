namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;

    public class ListControllerBase : IListController
    {
        protected IList list;

        public virtual object GetColumnValue(int position, string columnName);
        public virtual object GetItem(int index);
        public virtual void SetList(IList list);

        public virtual int Count { get; }
    }
}

