namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    public class FunctionParameterInfo : RuntimeWrapper, IFunctionParameterInfo
    {
        public FunctionParameterInfo(object source) : base(EdmConst.FunctionParameter, source)
        {
        }

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind =>
            ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(base.GetPropertyAccessor("BuiltInTypeKind").Value);

        public string TypeName =>
            base.GetPropertyAccessor("TypeName").Value as string;

        public IEdmTypeInfo EdmType =>
            new TypeUsageInfo(base.GetPropertyAccessor("TypeUsage").Value).EdmType;

        public Type ClrType =>
            new TypeUsageInfo(base.GetPropertyAccessor("TypeUsage").Value).ClrType;

        internal EdmComplexTypePropertyInfo[] ResultTypeProperties =>
            new EdmComplexTypeInfo(new TypeUsageInfo(base.GetPropertyAccessor("TypeUsage").Value).CollectionElementType.Value).Properties;
    }
}

