namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    public class PrimitiveType : RuntimeWrapper, IPrimitiveType
    {
        public PrimitiveType(object value) : base("EdmPrimitiveType", value)
        {
        }

        public Type ClrEquivalentType =>
            (Type) base.GetPropertyAccessor("ClrEquivalentType").Value;
    }
}

