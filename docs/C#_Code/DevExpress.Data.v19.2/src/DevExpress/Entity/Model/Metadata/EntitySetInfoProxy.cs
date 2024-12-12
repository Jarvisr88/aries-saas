namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;

    internal class EntitySetInfoProxy : EntitySetInfoBase
    {
        protected string name;
        private IPluralizationService pluralizationService;

        public EntitySetInfoProxy(EntityTypeBaseInfo entityType, IEntityContainerInfo entityContainerInfo, IDataColumnAttributesProvider dataColumnAttributesProvider, IPluralizationService pluralizationService, EntityTypeInfoFactory entityTypeInfoFactory) : base(entityType, entityContainerInfo, dataColumnAttributesProvider, entityTypeInfoFactory)
        {
            this.pluralizationService = pluralizationService;
        }

        public override string Name
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(this.name))
                    {
                        this.name = this.pluralizationService.GetPluralizedName(base.EntityType.Name);
                    }
                }
                catch
                {
                    this.name = base.EntityType.Name;
                }
                return this.name;
            }
        }
    }
}

