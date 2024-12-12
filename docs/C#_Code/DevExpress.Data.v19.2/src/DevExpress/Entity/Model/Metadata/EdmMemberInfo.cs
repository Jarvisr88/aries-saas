namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    public class EdmMemberInfo : RuntimeWrapper, IEdmMemberInfo
    {
        public EdmMemberInfo(object member) : base(EdmConst.EdmMember, member)
        {
        }

        public AssociationTypeInfo GetAssociationType() => 
            this.IsNavigationProperty ? new AssociationTypeInfo(base.GetPropertyAccessor("RelationshipType").Value) : null;

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public bool IsProperty =>
            (this.BuiltInTypeKind == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EdmProperty) || (this.BuiltInTypeKind == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.NavigationProperty);

        public bool IsNavigationProperty =>
            this.BuiltInTypeKind == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.NavigationProperty;

        public bool IsCollectionProperty
        {
            get
            {
                object source = base.GetPropertyAccessor("TypeUsage.EdmType.BuiltInTypeKind").Value;
                return ((source != null) && (((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(source)) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.CollectionType));
            }
        }

        public DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind =>
            ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(base.GetPropertyAccessor("BuiltInTypeKind").Value);

        public object FromEndMember =>
            base.GetPropertyAccessor("FromEndMember").Value;

        public bool IsKeyMember
        {
            get
            {
                object obj2 = base.GetPropertyAccessor("IsKeyMember").Value;
                return ((obj2 != null) ? ((bool) obj2) : false);
            }
        }

        internal object ToEndMember =>
            base.GetPropertyAccessor("ToEndMember").Value;

        internal object DeclaringType =>
            base.GetPropertyAccessor("DeclaringType").Value;

        public IPrimitiveType PrimitiveType =>
            new DevExpress.Entity.Model.Metadata.PrimitiveType(base.GetPropertyAccessor("PrimitiveType").Value);
    }
}

