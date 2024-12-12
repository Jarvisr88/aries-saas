namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.Model.Metadata;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class ContainerBuilder : IContainerBuilder
    {
        private DevExpress.Entity.Model.Metadata.EntityTypeInfoFactory entityTypeInfoFactory;

        protected ContainerBuilder()
        {
        }

        public abstract IDbContainerInfo Build(IDXTypeInfo info, ISolutionTypesProvider typesProvider);
        protected virtual IDataColumnAttributesProvider CreateDataColumnAttributesProvider() => 
            new EmptyDataColumnAttributesProvider();

        protected virtual DbContainerInfo CreateDbContainerInfo(IDXTypeInfo type, EntityContainerInfo result, MetadataWorkspaceInfo mw)
        {
            DbContainerInfo info1 = new DbContainerInfo(type.ResolveType(), result, mw);
            info1.Assembly = type.Assembly;
            return info1;
        }

        protected virtual DevExpress.Entity.Model.Metadata.EntityTypeInfoFactory CreateEntityTypeInfoFactory() => 
            new DevExpress.Entity.Model.Metadata.EntityTypeInfoFactory();

        protected virtual DbContainerInfo GetDbContainerInfo(IDXTypeInfo type, MetadataWorkspaceInfo mw, IMapper mapper)
        {
            if (mw == null)
            {
                return null;
            }
            Func<object, bool> predicate = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__1_0;
                predicate = <>c.<>9__1_0 = x => ((BuiltInTypeKind) RuntimeWrapper.ConvertEnum<BuiltInTypeKind>(PropertyAccessor.GetValue(x, "BuiltInTypeKind"))) == BuiltInTypeKind.EntityContainer;
            }
            object entityContainer = mw.GetItems(DataSpace.CSpace).FirstOrDefault<object>(predicate);
            if (entityContainer == null)
            {
                return null;
            }
            IDataColumnAttributesProvider dataColumnAttributesProvider = this.CreateDataColumnAttributesProvider();
            EntityContainerInfo result = new EntityContainerInfo(entityContainer, mapper, dataColumnAttributesProvider, this.EntityTypeInfoFactory);
            return this.CreateDbContainerInfo(type, result, mw);
        }

        public abstract DbContainerType BuilderType { get; }

        private DevExpress.Entity.Model.Metadata.EntityTypeInfoFactory EntityTypeInfoFactory
        {
            get
            {
                this.entityTypeInfoFactory ??= this.CreateEntityTypeInfoFactory();
                return this.entityTypeInfoFactory;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainerBuilder.<>c <>9 = new ContainerBuilder.<>c();
            public static Func<object, bool> <>9__1_0;

            internal bool <GetDbContainerInfo>b__1_0(object x) => 
                ((BuiltInTypeKind) RuntimeWrapper.ConvertEnum<BuiltInTypeKind>(PropertyAccessor.GetValue(x, "BuiltInTypeKind"))) == BuiltInTypeKind.EntityContainer;
        }
    }
}

