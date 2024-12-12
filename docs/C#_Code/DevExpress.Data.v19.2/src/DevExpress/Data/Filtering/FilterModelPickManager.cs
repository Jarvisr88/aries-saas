namespace DevExpress.Data.Filtering
{
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Collections.Generic;

    public class FilterModelPickManager : PickManagerBase
    {
        private IIBoundPropertyCreator propertyCreator;

        public FilterModelPickManager(IIBoundPropertyCreator propertyCreator);
        protected override INode CreateDataMemberNode(object dataSource, string dataMember, string displayName, bool isList, object owner, IPropertyDescriptor property);
        protected override INode CreateDataSourceNode(object dataSource, string dataMember, string name, object owner);
        protected override INode CreateDummyNode(object owner);
        protected override object CreateNoneNode(object owner);
        protected override IPropertiesProvider CreateProvider();
        protected override bool NodeIsEmpty(INode node);
        public List<IBoundProperty> PickProperties(object dataSource, string dataMember, Type dataMemberType);
    }
}

