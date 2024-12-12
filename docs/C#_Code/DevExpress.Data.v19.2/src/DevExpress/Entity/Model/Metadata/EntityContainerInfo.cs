namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EntityContainerInfo : RuntimeWrapper, IEntityContainerInfo, IAssociationTypeSource
    {
        private IDataColumnAttributesProvider dataColumnAttributesProvider;
        private List<IEntitySetInfo> entitySets;
        private List<IEntityFunctionInfo> entityFunctions;
        private List<AssociationTypeInfo> associationTypesFromCSpace;
        private IMapper mapper;
        private EntityTypeInfoFactory entityTypeInfoFactory;

        public EntityContainerInfo(object entityContainer, IMapper mapper, IDataColumnAttributesProvider dataColumnAttributesProvider, EntityTypeInfoFactory entityTypeInfoFactory) : base(EdmConst.EntityContainer, entityContainer)
        {
            this.associationTypesFromCSpace = new List<AssociationTypeInfo>();
            this.mapper = mapper;
            this.dataColumnAttributesProvider = dataColumnAttributesProvider;
            this.entityTypeInfoFactory = entityTypeInfoFactory;
        }

        private void AddEntitySet(EntitySetBaseInfo entitySet)
        {
            if (entitySet != null)
            {
                BuiltInTypeKind builtInTypeKind = entitySet.BuiltInTypeKind;
                if (builtInTypeKind == BuiltInTypeKind.AssociationSet)
                {
                    this.associationTypesFromCSpace.Add(new AssociationTypeInfo(entitySet.ElementType.Value));
                }
                else if (builtInTypeKind == BuiltInTypeKind.EntitySet)
                {
                    this.entitySets ??= new List<IEntitySetInfo>();
                    this.entitySets.Add(new EntitySetInfo(entitySet, this, this.dataColumnAttributesProvider, this.entityTypeInfoFactory));
                }
            }
        }

        AssociationTypeInfo IAssociationTypeSource.GetAssociationTypeFromCSpace(string fullName)
        {
            AssociationTypeInfo info2;
            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }
            using (List<AssociationTypeInfo>.Enumerator enumerator = this.associationTypesFromCSpace.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        AssociationTypeInfo current = enumerator.Current;
                        if (current.FullName != fullName)
                        {
                            continue;
                        }
                        info2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        private void InitEntityFunctions()
        {
            this.entityFunctions = new List<IEntityFunctionInfo>();
            IEnumerable source = base.GetPropertyAccessor("FunctionImports").Value as IEnumerable;
            if ((source != null) && source.Cast<object>().Any<object>())
            {
                Func<object, EdmFunctionInfo> selector = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<object, EdmFunctionInfo> local1 = <>c.<>9__13_0;
                    selector = <>c.<>9__13_0 = x => new EdmFunctionInfo(x);
                }
                foreach (EdmFunctionInfo info in source.Cast<object>().Select<object, EdmFunctionInfo>(selector))
                {
                    this.entityFunctions.Add(new EntityFunctionInfo(info));
                }
            }
        }

        private void InitEntitySets()
        {
            IEnumerable source = base.GetPropertyAccessor("BaseEntitySets").Value as IEnumerable;
            if ((source != null) && source.Cast<object>().Any<object>())
            {
                Func<object, EntitySetBaseInfo> selector = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<object, EntitySetBaseInfo> local1 = <>c.<>9__12_0;
                    selector = <>c.<>9__12_0 = x => new EntitySetBaseInfo(x);
                }
                List<EntityTypeBaseInfo> declaredDbSetsTypes = new List<EntityTypeBaseInfo>();
                foreach (EntitySetBaseInfo info in source.Cast<object>().Select<object, EntitySetBaseInfo>(selector))
                {
                    EntityTypeBaseInfo elementType = info.ElementType;
                    if ((elementType == null) || !elementType.Abstract)
                    {
                        this.AddEntitySet(info);
                    }
                    if (elementType != null)
                    {
                        declaredDbSetsTypes.Add(elementType);
                    }
                }
                if (this.Mapper is DevExpress.Entity.Model.Metadata.Mapper)
                {
                    foreach (EntityTypeBaseInfo info3 in (this.Mapper as DevExpress.Entity.Model.Metadata.Mapper).GetUndeclaredTypesFormHierarchy(declaredDbSetsTypes))
                    {
                        this.entitySets ??= new List<IEntitySetInfo>();
                        this.entitySets.Add(new EntitySetInfoProxy(info3, this, this.dataColumnAttributesProvider, this.Mapper, this.entityTypeInfoFactory));
                    }
                }
                Func<IEntitySetInfo, string> keySelector = <>c.<>9__12_1;
                if (<>c.<>9__12_1 == null)
                {
                    Func<IEntitySetInfo, string> local2 = <>c.<>9__12_1;
                    keySelector = <>c.<>9__12_1 = s => s.Name;
                }
                this.entitySets = this.entitySets.OrderBy<IEntitySetInfo, string>(keySelector).ToList<IEntitySetInfo>();
            }
        }

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public IMapper Mapper =>
            this.mapper;

        public ICollection<IEntitySetInfo> BaseEntitySets =>
            null;

        public IEnumerable<IEntitySetInfo> EntitySets
        {
            get
            {
                if (this.entitySets == null)
                {
                    this.InitEntitySets();
                }
                return this.entitySets;
            }
        }

        public IEnumerable<IEntityFunctionInfo> EntityFunctions
        {
            get
            {
                if (this.entityFunctions == null)
                {
                    this.InitEntityFunctions();
                }
                return this.entityFunctions;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EntityContainerInfo.<>c <>9 = new EntityContainerInfo.<>c();
            public static Func<object, EntitySetBaseInfo> <>9__12_0;
            public static Func<IEntitySetInfo, string> <>9__12_1;
            public static Func<object, EdmFunctionInfo> <>9__13_0;

            internal EdmFunctionInfo <InitEntityFunctions>b__13_0(object x) => 
                new EdmFunctionInfo(x);

            internal EntitySetBaseInfo <InitEntitySets>b__12_0(object x) => 
                new EntitySetBaseInfo(x);

            internal string <InitEntitySets>b__12_1(IEntitySetInfo s) => 
                s.Name;
        }
    }
}

