namespace DevExpress.Data.Browsing.Design
{
    using DevExpress.Data.Browsing;
    using System;
    using System.Windows.Forms;

    public abstract class PickManager : PickManagerBase
    {
        private Func<IPropertiesProvider> providerCreator;

        protected PickManager();
        protected override IPropertiesProvider CreateProvider();
        public static void FillDataSourceImageList(ImageList imageList);
        public DataInfo[] GetData(object dataSource, string dataMember);
        public DataInfo[] GetData(object dataSource, string dataMember, Predicate<IPropertyDescriptor> shouldCreateDataInfo);
        protected static Type GetPropertyType(IPropertyDescriptor property);
        protected virtual bool ShouldCreateDataInfo(IPropertyDescriptor propertyDescriptor);

        public Func<IPropertiesProvider> ProviderCreator { get; set; }
    }
}

