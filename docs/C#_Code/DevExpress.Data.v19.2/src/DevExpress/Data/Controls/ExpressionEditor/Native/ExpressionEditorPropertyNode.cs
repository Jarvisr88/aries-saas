namespace DevExpress.Data.Controls.ExpressionEditor.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Browsing.Design;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class ExpressionEditorPropertyNode : INode
    {
        public ExpressionEditorPropertyNode(IBoundProperty property);
        public ExpressionEditorPropertyNode(IBoundProperty property, FilterModelPropertyNode parent);
        public ExpressionEditorPropertyNode(bool isDataSource, bool isDummy);
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

