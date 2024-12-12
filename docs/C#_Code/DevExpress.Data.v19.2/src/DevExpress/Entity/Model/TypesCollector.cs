namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Data.Metadata.Edm;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class TypesCollector
    {
        public TypesCollector(IDXTypeInfo typeInfo) : this(typeInfo.ResolveType())
        {
            this.DbDescendantInfo = typeInfo;
        }

        private TypesCollector(Type type)
        {
            this.DbDescendantType = type;
            this.Init(type);
            this.TryLoadSqlCeServer();
            this.InitSystemDataEntityTypes();
        }

        private void Init(Type type)
        {
            try
            {
                Type baseContextType = EntityFrameworkModelBase.GetBaseContextType(type, "System.Data.Entity.DbContext");
                Type type3 = baseContextType;
                if (baseContextType == null)
                {
                    Type local1 = baseContextType;
                    type3 = EntityFrameworkModelBase.GetBaseContextType(type, "Microsoft.EntityFrameworkCore.DbContext");
                }
                this.DbContextType = type3;
                if (this.DbContextType != null)
                {
                    foreach (Type type2 in this.DbContextType.Assembly.GetTypes())
                    {
                        if (this.IsCollected)
                        {
                            break;
                        }
                        this.TryInitFrameworkTypes(type2);
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
            this.InitializeDbContextForEFCore(type);
        }

        private void InitializeDbContextForEFCore(Type type)
        {
            if (this.DbContext == null)
            {
                for (Type type2 = type.BaseType; type2 != null; type2 = type2.BaseType)
                {
                    if (type2.FullName == "Microsoft.EntityFrameworkCore.DbContext")
                    {
                        this.DbContext = new DXTypeInfo(type.BaseType);
                        return;
                    }
                }
            }
        }

        private void InitSystemDataEntityTypes()
        {
            Assembly assembly = null;
            bool flag = EntityFrameworkModelBase.IsAtLeastEF6(this.DbContextType);
            bool flag2 = EntityFrameworkModelBase.IsEntityFrameworkCore(this.DbContextType);
            assembly = (flag | flag2) ? this.DbContextType.Assembly : typeof(MetadataItem).Assembly;
            if (assembly != null)
            {
                Type type = assembly.GetType(flag ? "System.Data.Entity.Core.Common.Utils.MetadataHelper" : "System.Data.Common.Utils.MetadataHelper");
                if (type != null)
                {
                    this.MetadataHelper = new DXTypeInfo(type);
                }
                if (flag2)
                {
                    Type type2 = assembly.GetType("Microsoft.EntityFrameworkCore.Internal.DbSetFinder");
                    if (type2 != null)
                    {
                        this.DbSetFinder = new DXTypeInfo(type2);
                    }
                    this.DbContextOptions = assembly.GetType("Microsoft.EntityFrameworkCore.DbContextOptions");
                    this.DbContextOptionsT = assembly.GetType("Microsoft.EntityFrameworkCore.DbContextOptions`1");
                    this.DbContextOptionsBuilder = assembly.GetType("Microsoft.EntityFrameworkCore.DbContextOptionsBuilder");
                    this.DbContextOptionsBuilderT = assembly.GetType("Microsoft.EntityFrameworkCore.DbContextOptionsBuilder`1");
                }
            }
        }

        private void TryInitFrameworkTypes(Type type)
        {
            string fullName = type.FullName;
            if ((fullName == "System.Data.Entity.DbContext") || (fullName == "Microsoft.EntityFrameworkCore.DbContext"))
            {
                this.DbContext = new DXTypeInfo(type);
            }
            else if (fullName == "System.Data.Entity.DbSet`1")
            {
                this.DbSet = new DXTypeInfo(type);
            }
            else if (fullName == "System.Data.Entity.Infrastructure.IObjectContextAdapter")
            {
                this.IObjectContextAdapter = new DXTypeInfo(type);
            }
        }

        private void TryLoadSqlCeServer()
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load("System.Data.SqlServerCe, Version=4.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                this.SqlProvider = "System.Data.SqlServerCe.4.0";
            }
            catch
            {
            }
            if (assembly == null)
            {
                try
                {
                    assembly = Assembly.Load("System.Data.SqlServerCe, Version=3.5.1.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                    this.SqlProvider = "System.Data.SqlServerCe.3.5";
                }
                catch
                {
                }
            }
            if (assembly == null)
            {
                try
                {
                    assembly = Assembly.Load("System.Data.SqlServerCe, Version=3.5.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                    this.SqlProvider = "System.Data.SqlServerCe.3.5";
                }
                catch
                {
                }
            }
            if (assembly != null)
            {
                Type type = assembly.GetType("System.Data.SqlServerCe.SqlCeConnection");
                if (type != null)
                {
                    this.SqlCeConnection = new DXTypeInfo(type);
                }
            }
        }

        private bool IsCollected =>
            (this.IObjectContextAdapter != null) && ((this.DbContext != null) && (this.DbSet != null));

        public string SqlProvider { get; private set; }

        public IDXTypeInfo MetadataHelper { get; protected set; }

        public IDXTypeInfo IObjectContextAdapter { get; private set; }

        public IDXTypeInfo DbSet { get; private set; }

        public IDXTypeInfo DbSetFinder { get; private set; }

        public IDXTypeInfo DbContext { get; private set; }

        public Type DbContextOptions { get; private set; }

        public Type DbContextOptionsT { get; private set; }

        public Type DbContextOptionsBuilder { get; private set; }

        public Type DbContextOptionsBuilderT { get; private set; }

        public IDXTypeInfo SqlCeConnection { get; set; }

        public IDXTypeInfo DbDescendantInfo { get; private set; }

        public Type DbContextType { get; private set; }

        public Type DbDescendantType { get; private set; }
    }
}

