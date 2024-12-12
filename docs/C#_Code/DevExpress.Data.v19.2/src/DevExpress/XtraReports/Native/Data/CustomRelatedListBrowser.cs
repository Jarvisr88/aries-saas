namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing;
    using System;
    using System.ComponentModel;

    public class CustomRelatedListBrowser : RelatedListBrowser, IPropertiesContainer
    {
        private CustomPropertiesContainer customPropertiesContainer;

        protected CustomRelatedListBrowser(IListController listController, bool suppressListFilling);
        public CustomRelatedListBrowser(DataBrowser parent, PropertyDescriptor listAccessor, IListController listController, bool suppressListFilling);
        void IPropertiesContainer.SetCustomProperties(PropertyDescriptor[] customProperties);
        public override PropertyDescriptorCollection GetItemProperties();
    }
}

