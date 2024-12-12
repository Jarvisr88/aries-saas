namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing;
    using System;
    using System.ComponentModel;

    public class CustomListBrowser : ListBrowser, IPropertiesContainer
    {
        private CustomPropertiesContainer customPropertiesContainer;

        protected CustomListBrowser(IListController listController, bool suppressListFilling);
        public CustomListBrowser(object dataSource, IListController listController, bool suppressListFilling);
        void IPropertiesContainer.SetCustomProperties(PropertyDescriptor[] customProperties);
        public override PropertyDescriptorCollection GetItemProperties();
        protected override bool IsStandardType(Type propType);
    }
}

