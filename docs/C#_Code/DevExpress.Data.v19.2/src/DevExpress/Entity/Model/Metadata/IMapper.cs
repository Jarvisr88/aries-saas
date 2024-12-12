namespace DevExpress.Entity.Model.Metadata
{
    using System;

    public interface IMapper : IPluralizationService
    {
        EntityTypeBaseInfo GetMappedOSpaceType(EntityTypeBaseInfo cSpaceType);
        bool HasView(EntitySetBaseInfo entitySetBase);
        Type ResolveClrType(EntityTypeBaseInfo cSpaceType);
    }
}

