namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public interface IEntityTypeInfo : IEntityProperties
    {
        IEdmPropertyInfo GetDependentProperty(IEdmPropertyInfo foreignKey);
        IEdmPropertyInfo GetForeignKey(IEdmPropertyInfo dependentProperty);

        IEnumerable<IEdmPropertyInfo> KeyMembers { get; }

        IEnumerable<IEdmAssociationPropertyInfo> LookupTables { get; }

        System.Type Type { get; }
    }
}

