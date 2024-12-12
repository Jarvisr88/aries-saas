namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    internal class EntitySetInfo : EntitySetInfoBase
    {
        protected EntitySetBaseInfo entitySetBase;

        public EntitySetInfo(EntitySetBaseInfo entitySet, IEntityContainerInfo entityContainerInfo, IDataColumnAttributesProvider dataColumnAttributesProvider, EntityTypeInfoFactory entityTypeInfoFactory) : base(entitySet.ElementType, entityContainerInfo, dataColumnAttributesProvider, entityTypeInfoFactory)
        {
            this.entitySetBase = entitySet;
        }

        public override bool IsView =>
            (base.Mapper != null) ? base.Mapper.HasView(this.entitySetBase) : false;

        public override string Name =>
            this.entitySetBase?.Name;
    }
}

