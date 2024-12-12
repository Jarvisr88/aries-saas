namespace DevExpress.Entity.Model.Metadata
{
    using System;

    public class EntitySetBaseInfo : RuntimeWrapper
    {
        private EntityTypeBaseInfo elementType;

        public EntitySetBaseInfo(object source) : base(EdmConst.EntitySetBase, source)
        {
        }

        public EntityTypeBaseInfo ElementType
        {
            get
            {
                if (this.elementType == null)
                {
                    this.elementType = new EntityTypeBaseInfo(base.GetPropertyAccessor("ElementType").Value);
                }
                return this.elementType;
            }
        }

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind =>
            ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(base.GetPropertyAccessor("BuiltInTypeKind").Value);
    }
}

