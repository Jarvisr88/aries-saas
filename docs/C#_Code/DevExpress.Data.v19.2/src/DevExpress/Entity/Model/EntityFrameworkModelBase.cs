namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.ProjectModel;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class EntityFrameworkModelBase : IEntityFrameworkModel
    {
        private Dictionary<DbContainerType, IContainerBuilder> builders;
        private Dictionary<Type, IDbContainerInfo> dbContainers;
        private IEnumerable<IContainerInfo> allContainersInfo;
        private static string[] assemblyFilters = new string[] { "EntityFramework" };

        protected EntityFrameworkModelBase()
        {
        }

        private static bool DbTypeFilter(IDXTypeInfo typeInfo, string baseClassName)
        {
            if (typeInfo == null)
            {
                return false;
            }
            foreach (string str in assemblyFilters)
            {
                if (typeInfo.Assembly.AssemblyFullName.StartsWith(str))
                {
                    return false;
                }
            }
            Type type = typeInfo.ResolveType();
            return ((type != null) ? (!typeInfo.Assembly.IsProjectAssembly ? (type.IsVisible && IsContextType(type, baseClassName)) : IsContextType(type, baseClassName)) : false);
        }

        public static Type GetBaseContextType(Type type, string baseContextName)
        {
            if ((type == null) || (type.FullName == baseContextName))
            {
                return null;
            }
            while ((type != null) && (!string.IsNullOrEmpty(type.Name) && (type.FullName != baseContextName)))
            {
                type = type.GetBaseType();
            }
            return (((type == null) || (type.FullName != baseContextName)) ? null : type);
        }

        private IContainerBuilder GetBuider(DbContainerType type)
        {
            this.builders ??= new Dictionary<DbContainerType, IContainerBuilder>();
            if (this.builders.ContainsKey(type))
            {
                return this.builders[type];
            }
            IContainerBuilder containerBuilderCore = this.GetContainerBuilderCore(type);
            if (containerBuilderCore != null)
            {
                this.builders.Add(type, containerBuilderCore);
            }
            return containerBuilderCore;
        }

        public IDbContainerInfo GetContainer(IContainerInfo info)
        {
            Type key = info.ResolveType();
            if (this.dbContainers == null)
            {
                this.dbContainers = new Dictionary<Type, IDbContainerInfo>();
            }
            else if (this.dbContainers.ContainsKey(key))
            {
                return this.dbContainers[key];
            }
            IDbContainerInfo info2 = this.GetBuider(info.ContainerType).Build(info, this.TypesProvider);
            if (info2 != null)
            {
                this.dbContainers.Add(key, info2);
            }
            return info2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDbContainerInfo GetContainer(string nameOrFullName) => 
            this.GetContainer(nameOrFullName, true);

        public IDbContainerInfo GetContainer(string nameOrFullName, bool returnNullOnError)
        {
            IEnumerable<IContainerInfo> containersInfo = this.GetContainersInfo(returnNullOnError);
            if (containersInfo != null)
            {
                using (IEnumerator<IContainerInfo> enumerator = containersInfo.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        IContainerInfo current = enumerator.Current;
                        if (nameOrFullName.Equals(current.Name) || nameOrFullName.Equals(current.FullName))
                        {
                            return this.GetContainer(current);
                        }
                    }
                }
            }
            return null;
        }

        protected virtual IContainerBuilder GetContainerBuilderCore(DbContainerType dbContainerType) => 
            (dbContainerType != DbContainerType.EntityFramework) ? null : new EFContainerBuilderBase();

        public IEnumerable<IContainerInfo> GetContainersInfo() => 
            this.GetContainersInfo(true);

        public IEnumerable<IContainerInfo> GetContainersInfo(bool returnNullOnError)
        {
            try
            {
                if (this.allContainersInfo == null)
                {
                    Func<IDXTypeInfo, bool> filter = <>c.<>9__22_0;
                    if (<>c.<>9__22_0 == null)
                    {
                        Func<IDXTypeInfo, bool> local1 = <>c.<>9__22_0;
                        filter = <>c.<>9__22_0 = x => DbTypeFilter(x, "System.Data.Services.Client.DataServiceContext");
                    }
                    Func<IDXTypeInfo, IContainerInfo> selector = <>c.<>9__22_1;
                    if (<>c.<>9__22_1 == null)
                    {
                        Func<IDXTypeInfo, IContainerInfo> local2 = <>c.<>9__22_1;
                        selector = <>c.<>9__22_1 = x => new ContainerInfo(x, DbContainerType.WCF);
                    }
                    IEnumerable<IContainerInfo> first = this.ProjectTypes.GetTypes(filter).Select<IDXTypeInfo, IContainerInfo>(selector);
                    Func<IDXTypeInfo, bool> func3 = <>c.<>9__22_2;
                    if (<>c.<>9__22_2 == null)
                    {
                        Func<IDXTypeInfo, bool> local3 = <>c.<>9__22_2;
                        func3 = <>c.<>9__22_2 = x => DbTypeFilter(x, "System.Data.Entity.DbContext");
                    }
                    Func<IDXTypeInfo, IContainerInfo> func4 = <>c.<>9__22_3;
                    if (<>c.<>9__22_3 == null)
                    {
                        Func<IDXTypeInfo, IContainerInfo> local4 = <>c.<>9__22_3;
                        func4 = <>c.<>9__22_3 = x => new ContainerInfo(x, DbContainerType.EntityFramework);
                    }
                    IEnumerable<IContainerInfo> second = this.ProjectTypes.GetTypes(func3).Select<IDXTypeInfo, IContainerInfo>(func4);
                    Func<IDXTypeInfo, bool> func5 = <>c.<>9__22_4;
                    if (<>c.<>9__22_4 == null)
                    {
                        Func<IDXTypeInfo, bool> local5 = <>c.<>9__22_4;
                        func5 = <>c.<>9__22_4 = x => DbTypeFilter(x, "Microsoft.EntityFrameworkCore.DbContext");
                    }
                    Func<IDXTypeInfo, IContainerInfo> func6 = <>c.<>9__22_5;
                    if (<>c.<>9__22_5 == null)
                    {
                        Func<IDXTypeInfo, IContainerInfo> local6 = <>c.<>9__22_5;
                        func6 = <>c.<>9__22_5 = x => new ContainerInfo(x, DbContainerType.EntityFramework);
                    }
                    IEnumerable<IContainerInfo> enumerable3 = this.ProjectTypes.GetTypes(func5).Select<IDXTypeInfo, IContainerInfo>(func6);
                    this.allContainersInfo = first.Concat<IContainerInfo>(second).Concat<IContainerInfo>(enumerable3);
                }
                return this.allContainersInfo;
            }
            catch
            {
                if (!returnNullOnError)
                {
                    throw;
                }
                return null;
            }
        }

        private static bool InheritFromContext(Type type, string baseContextName) => 
            GetBaseContextType(type, baseContextName) != null;

        public static bool IsAtLeastEF6(IContainerInfo containerInfo) => 
            ((containerInfo != null) && (containerInfo.ContainerType == DbContainerType.EntityFramework)) && IsAtLeastEF6(GetBaseContextType(containerInfo.ResolveType(), "System.Data.Entity.DbContext"));

        public static bool IsAtLeastEF6(Type dbContextType) => 
            (dbContextType != null) ? (dbContextType.Assembly.GetName().Version.Major >= 6) : false;

        private static bool IsContextType(Type type, string baseContextName) => 
            (type != null) ? (!type.IsAbstract && (!type.IsSealed && (!type.IsGenericType && (type.IsClass && InheritFromContext(type, baseContextName))))) : false;

        public static bool IsEntityFrameworkCore(IContainerInfo containerInfo) => 
            ((containerInfo != null) && (containerInfo.ContainerType == DbContainerType.EntityFramework)) && IsEntityFrameworkCore(GetBaseContextType(containerInfo.ResolveType(), "Microsoft.EntityFrameworkCore.DbContext"));

        public static bool IsEntityFrameworkCore(Type dbContextType) => 
            (dbContextType != null) ? (dbContextType.Assembly.GetName().Name == "Microsoft.EntityFrameworkCore") : false;

        protected abstract ISolutionTypesProvider TypesProvider { get; }

        protected virtual IProjectTypes ProjectTypes =>
            this.TypesProvider.ActiveProjectTypes;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EntityFrameworkModelBase.<>c <>9 = new EntityFrameworkModelBase.<>c();
            public static Func<IDXTypeInfo, bool> <>9__22_0;
            public static Func<IDXTypeInfo, IContainerInfo> <>9__22_1;
            public static Func<IDXTypeInfo, bool> <>9__22_2;
            public static Func<IDXTypeInfo, IContainerInfo> <>9__22_3;
            public static Func<IDXTypeInfo, bool> <>9__22_4;
            public static Func<IDXTypeInfo, IContainerInfo> <>9__22_5;

            internal bool <GetContainersInfo>b__22_0(IDXTypeInfo x) => 
                EntityFrameworkModelBase.DbTypeFilter(x, "System.Data.Services.Client.DataServiceContext");

            internal IContainerInfo <GetContainersInfo>b__22_1(IDXTypeInfo x) => 
                new ContainerInfo(x, DbContainerType.WCF);

            internal bool <GetContainersInfo>b__22_2(IDXTypeInfo x) => 
                EntityFrameworkModelBase.DbTypeFilter(x, "System.Data.Entity.DbContext");

            internal IContainerInfo <GetContainersInfo>b__22_3(IDXTypeInfo x) => 
                new ContainerInfo(x, DbContainerType.EntityFramework);

            internal bool <GetContainersInfo>b__22_4(IDXTypeInfo x) => 
                EntityFrameworkModelBase.DbTypeFilter(x, "Microsoft.EntityFrameworkCore.DbContext");

            internal IContainerInfo <GetContainersInfo>b__22_5(IDXTypeInfo x) => 
                new ContainerInfo(x, DbContainerType.EntityFramework);
        }
    }
}

