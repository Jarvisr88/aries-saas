namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class EntitySetInfoBase : IEntitySetInfo
    {
        private readonly IDataColumnAttributesProvider dataColumnAttributesProvider;
        private readonly EntityTypeInfoFactory entityTypeInfoFactory;
        protected IEntityTypeInfo elementTypeInfo;

        protected EntitySetInfoBase(EntityTypeBaseInfo entityType, IEntityContainerInfo entityContainerInfo, IDataColumnAttributesProvider dataColumnAttributesProvider, EntityTypeInfoFactory entityTypeInfoFactory)
        {
            this.EntityContainerInfo = entityContainerInfo;
            this.EntityType = entityType;
            this.dataColumnAttributesProvider = dataColumnAttributesProvider;
            this.entityTypeInfoFactory = entityTypeInfoFactory;
            this.AttachedInfo = new Dictionary<string, object>();
        }

        protected IDataColumnAttributesProvider DataColumnAttributesProvider =>
            this.dataColumnAttributesProvider;

        public bool IsAbstract =>
            this.EntityType.Abstract;

        public IEntityTypeInfo ElementType
        {
            get
            {
                this.elementTypeInfo ??= this.entityTypeInfoFactory.Create(this.EntityType, this.EntityContainerInfo as IAssociationTypeSource, this.Mapper, this.DataColumnAttributesProvider);
                return this.elementTypeInfo;
            }
        }

        public IEntityContainerInfo EntityContainerInfo { get; private set; }

        public EntityTypeBaseInfo EntityType { get; private set; }

        public virtual bool IsView =>
            false;

        public virtual string Name =>
            this.EntityType.Name;

        public bool ReadOnly =>
            (this.ElementType.KeyMembers != null) && (this.ElementType.KeyMembers.Any<IEdmPropertyInfo>() && (this.ElementType.KeyMembers.Count<IEdmPropertyInfo>() > 7));

        protected IMapper Mapper
        {
            get
            {
                DevExpress.Entity.Model.Metadata.EntityContainerInfo entityContainerInfo = this.EntityContainerInfo as DevExpress.Entity.Model.Metadata.EntityContainerInfo;
                return entityContainerInfo?.Mapper;
            }
        }

        public Dictionary<string, object> AttachedInfo { get; private set; }
    }
}

