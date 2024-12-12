namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EntityTypeInfo : IEntityTypeInfo, IEntityProperties
    {
        private ICollection<IEdmPropertyInfo> keyMembers;
        private ICollection<IEdmPropertyInfo> properties;
        private ICollection<IEdmAssociationPropertyInfo> lookupTables;
        private Dictionary<string, string> foreignKeysNames;
        private Dictionary<IEdmPropertyInfo, IEdmPropertyInfo> foreignKeyDependentProperties;
        private IDataColumnAttributesProvider attributesProvider;
        private readonly IMapper mapper;
        private readonly EntityTypeBaseInfo entityType;
        private readonly Type type;
        private readonly EntityTypeInfoFactory entityTypeInfoFactory;

        public EntityTypeInfo(EntityTypeBaseInfo entityType, Type clrType, IAssociationTypeSource associationTypeSource, IMapper mapper, IDataColumnAttributesProvider attributesProvider, EntityTypeInfoFactory entityTypeInfoFactory)
        {
            this.mapper = mapper;
            this.type = clrType;
            this.entityType = entityType;
            this.AssociationTypeSource = associationTypeSource;
            this.attributesProvider = attributesProvider;
            this.entityTypeInfoFactory = entityTypeInfoFactory;
        }

        protected virtual IEdmAssociationPropertyInfo CreateAssociationPropertyInfo(Type type, EdmMemberInfo edmProperty, bool isForeignKey, bool isNavigationProperty)
        {
            PropertyDescriptor property = TypeDescriptor.GetProperties(type)[edmProperty.Name];
            DataColumnAttributes atrributes = this.attributesProvider.GetAtrributes(property, type);
            object source = PropertyAccessor.GetValue(PropertyAccessor.GetValue(PropertyAccessor.GetValue(edmProperty.ToEndMember, "TypeUsage"), "EdmType"), "ElementType");
            if (source == null)
            {
                return null;
            }
            EntityTypeBaseInfo entityType = new EntityTypeBaseInfo(source);
            IEntityTypeInfo toEndEntityType = this.entityTypeInfoFactory.Create(entityType, this.AssociationTypeSource, this.mapper, this.attributesProvider);
            IEnumerable<EdmMemberInfo> foreignKeyProperties = this.GetToEndProperties(this, edmProperty, entityType);
            return ((foreignKeyProperties != null) ? new EdmAssociationPropertyInfo(property, atrributes, toEndEntityType, isNavigationProperty, foreignKeyProperties, isForeignKey) : null);
        }

        protected virtual EdmPropertyInfo CreateEdmPropertyInfo(Type type, EdmMemberInfo edmProperty, bool isForeignKey, bool isNavigationProperty)
        {
            PropertyDescriptor property = TypeDescriptor.GetProperties(type)[edmProperty.Name];
            return new EdmPropertyInfo(property, this.attributesProvider.GetAtrributes(property, type), isNavigationProperty, isForeignKey);
        }

        private Type GetClrType(EntityTypeBaseInfo entityType, IMapper mapper)
        {
            EntityTypeBaseInfo mappedOSpaceType = mapper.GetMappedOSpaceType(entityType);
            return mapper.ResolveClrType(mappedOSpaceType);
        }

        private IEnumerable<EdmMemberInfo> GetDependentProperties(IEntityTypeInfo declaringType, EdmMemberInfo navigationProperty)
        {
            AssociationTypeInfo associationType = navigationProperty.GetAssociationType();
            if ((associationType == null) || !associationType.IsForeignKey)
            {
                return null;
            }
            IEnumerable<EdmMemberInfo> dependentProperties = associationType.GetDependentProperties(navigationProperty);
            return associationType.GetCSpaceAssociationType(declaringType).GetDependentProperties(navigationProperty);
        }

        public IEdmPropertyInfo GetDependentProperty(IEdmPropertyInfo foreignKey)
        {
            IEdmPropertyInfo info;
            if ((foreignKey == null) || !foreignKey.IsForeignKey)
            {
                return null;
            }
            if (this.foreignKeyDependentProperties == null)
            {
                this.InitForeignKeyDependencies();
            }
            return (!this.foreignKeyDependentProperties.TryGetValue(foreignKey, out info) ? null : info);
        }

        private IEnumerable<string> GetDependentPropertyNames(IEntityTypeInfo declaringType, EdmMemberInfo navigationProperty)
        {
            IEnumerable<EdmMemberInfo> dependentProperties = this.GetDependentProperties(declaringType, navigationProperty);
            if (dependentProperties == null)
            {
                return new string[0];
            }
            Func<EdmMemberInfo, string> selector = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<EdmMemberInfo, string> local1 = <>c.<>9__26_0;
                selector = <>c.<>9__26_0 = x => x.Name;
            }
            return dependentProperties.Select<EdmMemberInfo, string>(selector);
        }

        public IEdmPropertyInfo GetForeignKey(IEdmPropertyInfo dependentProperty)
        {
            if (dependentProperty == null)
            {
                return null;
            }
            if (this.foreignKeyDependentProperties == null)
            {
                this.InitForeignKeyDependencies();
            }
            Func<KeyValuePair<IEdmPropertyInfo, IEdmPropertyInfo>, IEdmPropertyInfo> selector = <>c.<>9__22_1;
            if (<>c.<>9__22_1 == null)
            {
                Func<KeyValuePair<IEdmPropertyInfo, IEdmPropertyInfo>, IEdmPropertyInfo> local1 = <>c.<>9__22_1;
                selector = <>c.<>9__22_1 = x => x.Key;
            }
            return (from x in this.foreignKeyDependentProperties
                where x.Value == dependentProperty
                select x).Select<KeyValuePair<IEdmPropertyInfo, IEdmPropertyInfo>, IEdmPropertyInfo>(selector).FirstOrDefault<IEdmPropertyInfo>();
        }

        [IteratorStateMachine(typeof(<GetProperties>d__28))]
        private IEnumerable<EdmMemberInfo> GetProperties()
        {
            EntityTypeBaseInfo entityType = this.entityType;
            IEnumerator<EdmMemberInfo> enumerator = entityType.Properties.GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                EdmMemberInfo current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                enumerator = entityType.NavigationProperties.GetEnumerator();
            }
            if (!enumerator.MoveNext())
            {
                enumerator = null;
                yield break;
            }
            else
            {
                EdmMemberInfo current = enumerator.Current;
                yield return current;
                yield break;
            }
        }

        private IEnumerable<EdmMemberInfo> GetSortedProperties()
        {
            List<EdmMemberInfo> list = new List<EdmMemberInfo>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(this.type))
            {
                EdmMemberInfo item = this.GetProperties().FirstOrDefault<EdmMemberInfo>(x => x.Name == propertyDescriptor.Name);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private IEnumerable<EdmMemberInfo> GetToEndProperties(IEntityTypeInfo declaringType, EdmMemberInfo navigationProperty, EntityTypeBaseInfo toEndEntityTypeInfo)
        {
            AssociationTypeInfo associationType = navigationProperty.GetAssociationType();
            if ((associationType == null) || !associationType.IsForeignKey)
            {
                return null;
            }
            IEnumerable<EdmMemberInfo> toEndPropertyNames = associationType.GetToEndPropertyNames(navigationProperty, toEndEntityTypeInfo);
            return associationType.GetCSpaceAssociationType(declaringType).GetToEndPropertyNames(navigationProperty, toEndEntityTypeInfo);
        }

        private void Init()
        {
            this.foreignKeysNames = new Dictionary<string, string>();
            this.properties = new List<IEdmPropertyInfo>();
            this.lookupTables = new List<IEdmAssociationPropertyInfo>();
            if (this.EntityType != null)
            {
                IEnumerable<EdmMemberInfo> sortedProperties = this.GetSortedProperties();
                foreach (EdmMemberInfo info in sortedProperties)
                {
                    if (info.IsProperty && info.IsNavigationProperty)
                    {
                        foreach (string str in this.GetDependentPropertyNames(this, info))
                        {
                            if (!this.foreignKeysNames.ContainsKey(str))
                            {
                                this.foreignKeysNames.Add(str, info.Name);
                            }
                        }
                    }
                }
                foreach (EdmMemberInfo info2 in sortedProperties)
                {
                    if (info2.IsProperty)
                    {
                        if (info2.IsNavigationProperty && info2.IsCollectionProperty)
                        {
                            IEdmAssociationPropertyInfo property = this.CreateAssociationPropertyInfo(this.GetClrType(this.EntityType, this.mapper), info2, false, true);
                            if (!this.IsValidLookUpTableProperty(property))
                            {
                                continue;
                            }
                            this.lookupTables.Add(property);
                            continue;
                        }
                        if (info2.IsNavigationProperty)
                        {
                            this.properties.Add(this.CreateEdmPropertyInfo(this.GetClrType(this.EntityType, this.mapper), info2, false, true));
                            continue;
                        }
                        this.properties.Add(this.CreateEdmPropertyInfo(this.type, info2, this.foreignKeysNames.ContainsKey(info2.Name), false));
                    }
                }
            }
        }

        private void InitForeignKeyDependencies()
        {
            this.foreignKeyDependentProperties = new Dictionary<IEdmPropertyInfo, IEdmPropertyInfo>();
            foreach (string foreignKey in this.ForeignKeysNames.Keys)
            {
                IEdmPropertyInfo info = this.properties.FirstOrDefault<IEdmPropertyInfo>(x => x.IsForeignKey && (x.Name == foreignKey));
                string dependentPropertyName = this.ForeignKeysNames[foreignKey];
                IEdmPropertyInfo info2 = this.properties.FirstOrDefault<IEdmPropertyInfo>(x => x.Name == dependentPropertyName);
                if ((info != null) && (info2 != null))
                {
                    this.foreignKeyDependentProperties[info] = info2;
                }
            }
        }

        protected virtual bool IsValidLookUpTableProperty(IEdmAssociationPropertyInfo property) => 
            property != null;

        public Dictionary<string, string> ForeignKeysNames
        {
            get
            {
                if (this.foreignKeysNames == null)
                {
                    this.Init();
                }
                return this.foreignKeysNames;
            }
        }

        public IAssociationTypeSource AssociationTypeSource { get; private set; }

        public EntityTypeBaseInfo EntityType =>
            this.entityType;

        Type IEntityTypeInfo.Type =>
            this.type;

        IEnumerable<IEdmPropertyInfo> IEntityProperties.AllProperties =>
            this.PropertiesCore;

        private IEnumerable<IEdmPropertyInfo> PropertiesCore
        {
            get
            {
                if (this.properties == null)
                {
                    this.Init();
                }
                return this.properties;
            }
        }

        IEnumerable<IEdmPropertyInfo> IEntityTypeInfo.KeyMembers
        {
            get
            {
                if (this.keyMembers == null)
                {
                    if (this.EntityType == null)
                    {
                        return null;
                    }
                    this.keyMembers = new List<IEdmPropertyInfo>();
                    foreach (EdmMemberInfo info in this.entityType.KeyMembers)
                    {
                        if ((info != null) && (info.BuiltInTypeKind == BuiltInTypeKind.EdmProperty))
                        {
                            this.keyMembers.Add(this.CreateEdmPropertyInfo(this.type, info, false, false));
                        }
                    }
                }
                return this.keyMembers;
            }
        }

        public IEnumerable<IEdmAssociationPropertyInfo> LookupTables
        {
            get
            {
                if (this.lookupTables == null)
                {
                    this.Init();
                }
                return this.lookupTables;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EntityTypeInfo.<>c <>9 = new EntityTypeInfo.<>c();
            public static Func<KeyValuePair<IEdmPropertyInfo, IEdmPropertyInfo>, IEdmPropertyInfo> <>9__22_1;
            public static Func<EdmMemberInfo, string> <>9__26_0;

            internal string <GetDependentPropertyNames>b__26_0(EdmMemberInfo x) => 
                x.Name;

            internal IEdmPropertyInfo <GetForeignKey>b__22_1(KeyValuePair<IEdmPropertyInfo, IEdmPropertyInfo> x) => 
                x.Key;
        }

    }
}

