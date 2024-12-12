namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    public class TypeUsageInfo : RuntimeWrapper
    {
        private EdmTypeInfo edmType;

        public TypeUsageInfo(object source) : base(EdmConst.TypeUsage, source)
        {
        }

        public EdmTypeInfo EdmType
        {
            get
            {
                if (this.edmType == null)
                {
                    this.edmType = new EdmTypeInfo(base.GetPropertyAccessor("EdmType").Value);
                }
                return this.edmType;
            }
        }

        public IEdmTypeInfo CollectionElementType =>
            this.EdmType.CollectionElementType;

        public string Name =>
            this.EdmType.Name;

        public Type ClrType =>
            this.EdmType.ClrType;
    }
}

