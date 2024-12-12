namespace DevExpress.Data.Browsing
{
    using System;
    using System.ComponentModel;

    public class RelatedDataBrowser : DataBrowser, IRelatedDataBrowser
    {
        private PropertyDescriptor prop;
        private DataBrowser parent;

        internal RelatedDataBrowser(DataBrowser parent, PropertyDescriptor prop, bool suppressListFilling);
        protected internal override void Close();
        private static PropertyDescriptor FindProperty(object component, string name);
        internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
        public override string GetListName();
        protected internal override string GetListName(PropertyDescriptorCollection listAccessors);
        public override object GetValue();
        public override void LoadState(object state);
        private void OnParentStateChanged(object sender, EventArgs e);
        protected override object RetrieveDataSource();
        public override object SaveState();

        public override DataBrowser Parent { get; }

        public override Type DataSourceType { get; }

        PropertyDescriptor IRelatedDataBrowser.RelatedProperty { get; }

        IRelatedDataBrowser IRelatedDataBrowser.Parent { get; }
    }
}

