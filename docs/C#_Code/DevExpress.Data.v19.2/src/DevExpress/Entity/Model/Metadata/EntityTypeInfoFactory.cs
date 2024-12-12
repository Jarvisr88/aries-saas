namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Runtime.InteropServices;

    public class EntityTypeInfoFactory
    {
        public virtual IEntityTypeInfo Create(EntityTypeBaseInfo entityType, IAssociationTypeSource associationTypeSource, IMapper mapper, IDataColumnAttributesProvider attributesProvider = null)
        {
            EntityTypeBaseInfo mappedOSpaceType = mapper.GetMappedOSpaceType(entityType);
            IDataColumnAttributesProvider provider1 = attributesProvider;
            if (attributesProvider == null)
            {
                IDataColumnAttributesProvider local1 = attributesProvider;
                provider1 = new EmptyDataColumnAttributesProvider();
            }
            return new EntityTypeInfo(mappedOSpaceType, mapper.ResolveClrType(mappedOSpaceType), associationTypeSource, mapper, provider1, this);
        }
    }
}

