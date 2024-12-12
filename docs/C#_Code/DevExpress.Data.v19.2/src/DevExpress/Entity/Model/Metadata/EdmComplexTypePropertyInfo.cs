namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Runtime.CompilerServices;

    public class EdmComplexTypePropertyInfo : RuntimeWrapper, IEdmComplexTypePropertyInfo
    {
        public EdmComplexTypePropertyInfo(object value) : base(EdmConst.EdmProperty, value)
        {
            this.Name = PropertyAccessor.GetValue(value, "Name") as string;
            this.ClrType = PropertyAccessor.GetValue(PropertyAccessor.GetValue(value, "PrimitiveType"), "ClrEquivalentType") as Type;
            if (this.ClrType == null)
            {
                TypeUsageInfo info = new TypeUsageInfo(PropertyAccessor.GetValue(value, "TypeUsage"));
                this.ClrType = info.ClrType;
            }
        }

        public string Name { get; private set; }

        public Type ClrType { get; private set; }
    }
}

