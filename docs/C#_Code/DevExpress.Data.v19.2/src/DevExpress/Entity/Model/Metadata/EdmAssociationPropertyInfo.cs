namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class EdmAssociationPropertyInfo : EdmPropertyInfo, IEdmAssociationPropertyInfo, IEdmPropertyInfo
    {
        private IEntityTypeInfo toEndEntityType;

        public EdmAssociationPropertyInfo(PropertyDescriptor property, DataColumnAttributes attributes, IEntityTypeInfo toEndEntityType, bool isNavigationProperty, IEnumerable<EdmMemberInfo> foreignKeyProperties, bool isForeignKey = false) : base(property, attributes, isNavigationProperty, isForeignKey)
        {
            this.toEndEntityType = toEndEntityType;
            this.ForeignKeyProperties = foreignKeyProperties;
        }

        public IEnumerable<IEdmMemberInfo> ForeignKeyProperties { get; private set; }

        public IEntityTypeInfo ToEndEntityType =>
            this.toEndEntityType;
    }
}

