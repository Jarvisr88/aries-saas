namespace DevExpress.Entity.Model.Metadata
{
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Metadata.Edm;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class Mapper : IMapper, IPluralizationService
    {
        private IEnumerable<EdmTypeInfo> oSpaceEdmTypes;
        private IEnumerable<EntitySetBaseInfo> cViews;
        private TypesCollector typesCollector;

        public Mapper(MetadataWorkspaceInfo mw, TypesCollector typesCollector)
        {
            this.typesCollector = typesCollector;
            if (mw != null)
            {
                this.RegisterMetadataWorkspace(mw);
            }
        }

        EntityTypeBaseInfo IMapper.GetMappedOSpaceType(EntityTypeBaseInfo cSpaceType)
        {
            if ((cSpaceType == null) || (this.oSpaceEdmTypes == null))
            {
                return null;
            }
            EdmTypeInfo info = (from oSpaceType in this.oSpaceEdmTypes
                where (oSpaceType.BuiltInTypeKind == cSpaceType.BuiltInTypeKind) && (oSpaceType.Name == cSpaceType.Name)
                select oSpaceType).FirstOrDefault<EdmTypeInfo>();
            return ((info != null) ? new EntityTypeBaseInfo(info.Value) : null);
        }

        bool IMapper.HasView(EntitySetBaseInfo entitySetBase)
        {
            bool flag;
            if ((entitySetBase == null) || (this.cViews == null))
            {
                return false;
            }
            using (IEnumerator<EntitySetBaseInfo> enumerator = this.cViews.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        if (enumerator.Current != entitySetBase)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        Type IMapper.ResolveClrType(EntityTypeBaseInfo cSpaceType)
        {
            object obj2 = cSpaceType.Value;
            return (obj2.GetType().GetProperty("ClrType", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2, null) as Type);
        }

        string IPluralizationService.GetPluralizedName(string name) => 
            this.GetPluralizedNameCore(name);

        internal IEnumerable<EntityTypeBaseInfo> GetDescendatns(EntityTypeBaseInfo entityType) => 
            from et in this.CEntityTypes
                where et.BaseTypeFullName == entityType.FullName
                select et;

        protected virtual string GetPluralizedNameCore(string name) => 
            name;

        private static string GetPropertyValueByName(IEnumerable<object> metadataProperties, string propertyName)
        {
            string str;
            if (metadataProperties == null)
            {
                return null;
            }
            using (IEnumerator<object> enumerator = metadataProperties.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        if ((PropertyAccessor.GetValue(current, "Name") as string) != propertyName)
                        {
                            continue;
                        }
                        str = PropertyAccessor.GetValue(current, "Value") as string;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return str;
        }

        private StoreItemCollection GetSsdlFromDirectory<T>(string directoryPath) => 
            !string.IsNullOrEmpty(directoryPath) ? new StoreItemCollection(Directory.GetFiles(directoryPath, "*.ssdl", SearchOption.TopDirectoryOnly)) : null;

        public IEnumerable<EntityTypeBaseInfo> GetUndeclaredTypesFormHierarchy(IEnumerable<EntityTypeBaseInfo> declaredDbSetsTypes)
        {
            Dictionary<string, EntityTypeBaseInfo> types = new Dictionary<string, EntityTypeBaseInfo>();
            Dictionary<string, EntityTypeBaseInfo> sourceEntityTypes = new Dictionary<string, EntityTypeBaseInfo>();
            foreach (EntityTypeBaseInfo info in declaredDbSetsTypes)
            {
                types.Add(info.FullName, info);
                sourceEntityTypes.Add(info.FullName, info);
            }
            foreach (EntityTypeBaseInfo info2 in sourceEntityTypes.Values)
            {
                this.InitAllDescendants(info2, types);
            }
            return (from et in types.Values
                where (et != null) && (!et.Abstract && !sourceEntityTypes.ContainsKey(et.FullName))
                select et);
        }

        private void InitAllDescendants(EntityTypeBaseInfo type, Dictionary<string, EntityTypeBaseInfo> types)
        {
            IEnumerable<EntityTypeBaseInfo> descendatns = this.GetDescendatns(type);
            if (!descendatns.Any<EntityTypeBaseInfo>())
            {
                if (!types.ContainsKey(type.FullName))
                {
                    types.Add(type.FullName, type);
                }
            }
            else
            {
                foreach (EntityTypeBaseInfo info in descendatns)
                {
                    if (!types.ContainsKey(info.FullName))
                    {
                        this.InitAllDescendants(info, types);
                    }
                }
            }
        }

        private bool IsView(object set)
        {
            if (set == null)
            {
                return false;
            }
            IEnumerable source = PropertyAccessor.GetValue(set, "MetadataProperties") as IEnumerable;
            return ((source != null) ? ((GetPropertyValueByName(source.Cast<object>(), "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator:Type") == "Views") || !string.IsNullOrEmpty(GetPropertyValueByName(source.Cast<object>(), "DefiningQuery"))) : false);
        }

        private void RegisterCSpaceItems(MetadataWorkspaceInfo mw)
        {
            Func<object, bool> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = t => ((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) RuntimeWrapper.ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(PropertyAccessor.GetValue(t, "BuiltInTypeKind"))) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EntityType;
            }
            Func<object, EntityTypeBaseInfo> selector = <>c.<>9__10_1;
            if (<>c.<>9__10_1 == null)
            {
                Func<object, EntityTypeBaseInfo> local2 = <>c.<>9__10_1;
                selector = <>c.<>9__10_1 = x => new EntityTypeBaseInfo(x);
            }
            this.CEntityTypes = mw.GetItems(DevExpress.Entity.Model.Metadata.DataSpace.CSpace).Where<object>(predicate).Select<object, EntityTypeBaseInfo>(selector);
        }

        private void RegisterMetadataWorkspace(MetadataWorkspaceInfo mw)
        {
            this.RegisterOSpaceItems(mw.GetItems(DevExpress.Entity.Model.Metadata.DataSpace.OSpace));
            this.RegisterSSpaceItems(mw);
            this.RegisterCSpaceItems(mw);
        }

        private void RegisterOSpaceItems(IEnumerable<object> oSpaceItems)
        {
            if (oSpaceItems != null)
            {
                Func<object, EdmTypeInfo> selector = <>c.<>9__16_0;
                if (<>c.<>9__16_0 == null)
                {
                    Func<object, EdmTypeInfo> local1 = <>c.<>9__16_0;
                    selector = <>c.<>9__16_0 = delegate (object x) {
                        try
                        {
                            return new EdmTypeInfo(x);
                        }
                        catch
                        {
                            return null;
                        }
                    };
                }
                Func<EdmTypeInfo, bool> predicate = <>c.<>9__16_1;
                if (<>c.<>9__16_1 == null)
                {
                    Func<EdmTypeInfo, bool> local2 = <>c.<>9__16_1;
                    predicate = <>c.<>9__16_1 = x => x != null;
                }
                this.oSpaceEdmTypes = oSpaceItems.Select<object, EdmTypeInfo>(selector).Where<EdmTypeInfo>(predicate);
            }
        }

        private void RegisterSSpaceItems(MetadataWorkspaceInfo mw)
        {
            try
            {
                IEnumerable<object> items = mw.GetItems(DevExpress.Entity.Model.Metadata.DataSpace.SSpace);
                if (((items != null) && (this.typesCollector != null)) && (this.typesCollector.MetadataHelper != null))
                {
                    Type type = this.typesCollector.MetadataHelper.ResolveType();
                    if (type != null)
                    {
                        Func<object, bool> predicate = <>c.<>9__13_0;
                        if (<>c.<>9__13_0 == null)
                        {
                            Func<object, bool> local1 = <>c.<>9__13_0;
                            predicate = <>c.<>9__13_0 = x => ((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) RuntimeWrapper.ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(PropertyAccessor.GetValue(x, "BuiltInTypeKind"))) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EntityContainer;
                        }
                        object source = items.Where<object>(predicate).First<object>();
                        if (source != null)
                        {
                            List<object> list = new List<object>();
                            foreach (object obj3 in (PropertyAccessor.GetValue(source, "BaseEntitySets") as IEnumerable).Cast<object>())
                            {
                                if ((((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) RuntimeWrapper.ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(PropertyAccessor.GetValue(obj3, "BuiltInTypeKind"))) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EntitySet) && this.IsView(obj3))
                                {
                                    object[] parameters = new object[] { obj3, mw.Value };
                                    IEnumerable enumerable3 = type.GetMethod("GetInfluencingEntitySetsForTable", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, parameters) as IEnumerable;
                                    if (enumerable3 != null)
                                    {
                                        list.AddRange(enumerable3.Cast<object>());
                                    }
                                }
                            }
                            if (list.Count != 0)
                            {
                                Func<object, EntitySetBaseInfo> selector = <>c.<>9__13_1;
                                if (<>c.<>9__13_1 == null)
                                {
                                    Func<object, EntitySetBaseInfo> local2 = <>c.<>9__13_1;
                                    selector = <>c.<>9__13_1 = x => new EntitySetBaseInfo(x);
                                }
                                this.cViews = list.Select<object, EntitySetBaseInfo>(selector);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public IEnumerable<EntityTypeBaseInfo> CEntityTypes { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Mapper.<>c <>9 = new Mapper.<>c();
            public static Func<object, bool> <>9__10_0;
            public static Func<object, EntityTypeBaseInfo> <>9__10_1;
            public static Func<object, bool> <>9__13_0;
            public static Func<object, EntitySetBaseInfo> <>9__13_1;
            public static Func<object, EdmTypeInfo> <>9__16_0;
            public static Func<EdmTypeInfo, bool> <>9__16_1;

            internal bool <RegisterCSpaceItems>b__10_0(object t) => 
                ((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) RuntimeWrapper.ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(PropertyAccessor.GetValue(t, "BuiltInTypeKind"))) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EntityType;

            internal EntityTypeBaseInfo <RegisterCSpaceItems>b__10_1(object x) => 
                new EntityTypeBaseInfo(x);

            internal EdmTypeInfo <RegisterOSpaceItems>b__16_0(object x)
            {
                try
                {
                    return new EdmTypeInfo(x);
                }
                catch
                {
                    return null;
                }
            }

            internal bool <RegisterOSpaceItems>b__16_1(EdmTypeInfo x) => 
                x != null;

            internal bool <RegisterSSpaceItems>b__13_0(object x) => 
                ((DevExpress.Entity.Model.Metadata.BuiltInTypeKind) RuntimeWrapper.ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(PropertyAccessor.GetValue(x, "BuiltInTypeKind"))) == DevExpress.Entity.Model.Metadata.BuiltInTypeKind.EntityContainer;

            internal EntitySetBaseInfo <RegisterSSpaceItems>b__13_1(object x) => 
                new EntitySetBaseInfo(x);
        }
    }
}

