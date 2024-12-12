namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    public class EdmTypeInfo : RuntimeWrapper, IEdmTypeInfo
    {
        private TypeUsageInfo typeUsageInfo;

        public EdmTypeInfo(object value) : base(EdmConst.EdmType, value)
        {
        }

        public DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind =>
            ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(base.GetPropertyAccessor("BuiltInTypeKind").Value);

        public Type ClrType =>
            base.GetPropertyAccessor("ClrType").Value as Type;

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public IEdmTypeInfo CollectionElementType
        {
            get
            {
                if (this.typeUsageInfo == null)
                {
                    object source = PropertyAccessor.GetValue(PropertyAccessor.GetValue(PropertyAccessor.GetValue(base.GetMethodAccessor("GetCollectionType").Invoke(null), "TypeUsage"), "EdmType"), "TypeUsage");
                    this.typeUsageInfo = new TypeUsageInfo(source);
                }
                return this.typeUsageInfo.EdmType;
            }
        }
    }
}

