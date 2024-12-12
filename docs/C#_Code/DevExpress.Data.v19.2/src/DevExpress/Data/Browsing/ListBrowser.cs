namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class ListBrowser : DataBrowser, IListBrowser
    {
        private static readonly object nullObject;
        private IList list;
        private int position;
        private object current;
        private IListController listController;

        static ListBrowser();
        protected ListBrowser(IListController listController, bool suppressListFilling);
        public ListBrowser(object dataSource, IListController listController, bool suppressListFilling);
        protected internal override void Close();
        public object GetColumnValue(int position, string columnName);
        public override string GetListName();
        protected internal override string GetListName(PropertyDescriptorCollection listAccessors);
        public object GetRow(int position);
        protected override void InvalidateDataSource();
        public override void LoadState(object state);
        private void NullifyList();
        protected virtual void OnPositionChanged(EventArgs e);
        public override object SaveState();
        protected override void SetDataSource(object value);
        protected virtual void SetList(IList list);
        protected void SetPositionCore(int value);
        private PropertyDescriptor[] ToArray(PropertyDescriptorCollection propCollection);
        private int ValidatePosition(int value);

        public override int Position { get; set; }

        public override int Count { get; }

        public override object Current { get; }

        public IList List { get; }

        public IListController ListController { get; }
    }
}

