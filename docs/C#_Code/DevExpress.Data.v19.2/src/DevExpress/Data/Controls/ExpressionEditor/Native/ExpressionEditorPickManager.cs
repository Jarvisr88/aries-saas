namespace DevExpress.Data.Controls.ExpressionEditor.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ExpressionEditorPickManager : PickManagerBase
    {
        public ExpressionEditorPickManager();
        public ExpressionEditorPickManager(IPropertiesProvider propertiesProvider);
        protected override INode CreateDataMemberNode(object dataSource, string dataMember, string displayName, bool isList, object owner, IPropertyDescriptor property);
        protected override INode CreateDataSourceNode(object dataSource, string dataMember, string name, object owner);
        protected override INode CreateDummyNode(object owner);
        protected override object CreateNoneNode(object owner);
        private IBoundProperty CreateProperty(object dataSource, string dataMember, string displayName, bool isList, PropertyDescriptor property);
        protected override IPropertiesProvider CreateProvider();
        private static Type GetDataType(Type propertyType);
        private static bool IsAggregate(PropertyDescriptor property);
        protected override bool NodeIsEmpty(INode node);
        public List<IBoundProperty> PickProperties(object dataSource, string dataMember, Type dataMemberType);

        public IPropertiesProvider PropertiesProvider { get; }

        internal class BoundColumnInfo : IBoundProperty
        {
            private readonly ExpressionEditorPickManager expressionEditorPickManager;
            private readonly List<ExpressionEditorPickManager.BoundColumnInfo> children;
            private readonly object sourceControl;
            private bool childrenWereExpanded;

            public BoundColumnInfo(object sourceControl, ExpressionEditorPickManager expressionEditorPickManager, PropertyDescriptor property);
            private void ExpandChildren();

            public string Name { get; set; }

            public string DisplayName { get; set; }

            public System.Type Type { get; set; }

            public bool IsAggregate { get; set; }

            public bool IsList { get; set; }

            public IBoundProperty Parent { get; private set; }

            public bool HasChildren { get; }

            public List<IBoundProperty> Children { get; }

            public PropertyDescriptor Property { get; }
        }
    }
}

