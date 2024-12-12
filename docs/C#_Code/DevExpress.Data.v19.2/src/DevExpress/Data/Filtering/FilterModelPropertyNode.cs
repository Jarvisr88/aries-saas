namespace DevExpress.Data.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FilterModelPropertyNode : INode
    {
        private FilterModelPropertyNode parent;
        private IBoundProperty property;
        private List<FilterModelPropertyNode> children;
        private bool isDataSource;
        private bool isDummy;

        public FilterModelPropertyNode(IBoundProperty property);
        public FilterModelPropertyNode(IBoundProperty property, FilterModelPropertyNode parent);
        public FilterModelPropertyNode(bool isDataSource, bool isDummy);
        public void Expand(EventHandler callback);
        public bool HasDataSource(object dataSource);

        public IBoundProperty Property { get; }

        public IList ChildNodes { get; }

        public string DataMember { get; }

        public bool IsDataMemberNode { get; }

        public bool IsDataSourceNode { get; }

        public bool IsDummyNode { get; }

        public bool IsEmpty { get; }

        public bool IsList { get; }

        public bool IsComplex { get; }

        public object Parent { get; }
    }
}

