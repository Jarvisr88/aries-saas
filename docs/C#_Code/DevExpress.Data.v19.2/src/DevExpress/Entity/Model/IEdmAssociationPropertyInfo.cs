namespace DevExpress.Entity.Model
{
    using System.Collections.Generic;

    public interface IEdmAssociationPropertyInfo : IEdmPropertyInfo
    {
        IEntityTypeInfo ToEndEntityType { get; }

        IEnumerable<IEdmMemberInfo> ForeignKeyProperties { get; }
    }
}

