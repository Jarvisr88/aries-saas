namespace DevExpress.Data.Browsing
{
    using System;
    using System.ComponentModel;

    public class RelatedListBrowser : ListBrowser, IRelatedDataBrowser
    {
        private IDisposable disposableListItem;
        private DataBrowser parent;
        private PropertyDescriptor listAccessor;

        protected RelatedListBrowser(IListController listController, bool suppressListFilling);
        public RelatedListBrowser(DataBrowser parent, PropertyDescriptor listAccessor, IListController listController, bool suppressListFilling);
        private object AddNewItemIfEmpty(IBindingList list);
        protected internal override void Close();
        internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public override string GetListName();
        protected internal override string GetListName(PropertyDescriptorCollection listAccessors);
        public override object GetValue();
        protected void Initialize(DataBrowser parent, PropertyDescriptor listAccessor);
        private void InitializeDataSource(ListBrowser parentBrowser);
        public override void LoadState(object state);
        private void OnParentStateChanged(object sender, EventArgs e);
        protected override object RetrieveDataSource();
        public override object SaveState();

        public override DataBrowser Parent { get; }

        public override Type DataSourceType { get; }

        PropertyDescriptor IRelatedDataBrowser.RelatedProperty { get; }

        IRelatedDataBrowser IRelatedDataBrowser.Parent { get; }

        public override object DataSource { get; }
    }
}

