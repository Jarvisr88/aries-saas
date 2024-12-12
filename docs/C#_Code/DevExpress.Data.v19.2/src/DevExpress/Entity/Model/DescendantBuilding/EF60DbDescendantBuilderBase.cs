namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.Model.DescendantBuilding.Native;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public abstract class EF60DbDescendantBuilderBase : DbDescendantBuilder
    {
        protected readonly IDXAssemblyInfo servicesAssembly;
        private static Type dbConfigurationType;
        private static object dbConfigurationTypeLockObject = new object();

        public EF60DbDescendantBuilderBase(TypesCollector typesCollector, IDXAssemblyInfo servicesAssembly) : base(typesCollector)
        {
            this.servicesAssembly = servicesAssembly;
        }

        public override object Build()
        {
            object descendantInstance;
            base.InstanceActivator = null;
            DXTypeInfo dbDescendantInfo = base.TypesCollector.DbDescendantInfo as DXTypeInfo;
            if (dbDescendantInfo == null)
            {
                return null;
            }
            EdmxResource edmxResource = EdmxResource.GetEdmxResource(dbDescendantInfo);
            bool isModelFirst = edmxResource != null;
            try
            {
                if (DbConfigurationHelper.UseStaticConfiguration)
                {
                    this.ClearDBConfiguration();
                }
                this.ClearMetadataCache();
                ModuleBuilder moduleBuilder = base.CreateDynamicAssembly();
                if (DbConfigurationHelper.UseStaticConfiguration)
                {
                    object dbConfigurationTypeLockObject = EF60DbDescendantBuilderBase.dbConfigurationTypeLockObject;
                    lock (dbConfigurationTypeLockObject)
                    {
                        if (dbConfigurationType == null)
                        {
                            dbConfigurationType = this.EmitDbConfigurationType(moduleBuilder);
                        }
                    }
                }
                Tuple<ConstructorInfo, Type[]> dbContextConstructor = base.GetDbContextConstructor(null, base.dbContext);
                if (dbContextConstructor == null)
                {
                    descendantInstance = null;
                }
                else
                {
                    Type resultType = this.EmitDbDescendant(base.TypesCollector, dbContextConstructor, moduleBuilder, isModelFirst, dbConfigurationType);
                    if (DbConfigurationHelper.UseStaticConfiguration)
                    {
                        this.SetDbConfiguration(Activator.CreateInstance(dbConfigurationType));
                    }
                    Expression connection = this.CreateDbConnection(base.dbContext, isModelFirst, base.TypesCollector);
                    if (connection == null)
                    {
                        descendantInstance = null;
                    }
                    else
                    {
                        if (isModelFirst)
                        {
                            this.PrepareEdmx(edmxResource);
                        }
                        base.InstanceActivator = base.CreateDescendantInstanceActivator(resultType, dbContextConstructor.Item2, connection, base.TempFolder);
                        descendantInstance = base.DescendantInstance;
                    }
                }
            }
            catch (TargetInvocationException exception)
            {
                if (!this.SuppressExceptions)
                {
                    if (exception.InnerException != null)
                    {
                        throw exception.InnerException;
                    }
                    throw;
                }
                descendantInstance = null;
            }
            catch (Exception)
            {
                if (!this.SuppressExceptions)
                {
                    throw;
                }
                descendantInstance = null;
            }
            return descendantInstance;
        }

        protected void ClearDBConfiguration()
        {
            Type type = this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DependencyResolution.DbConfigurationManager");
            Type type2 = this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DependencyResolution.DbConfigurationLoader");
            object obj2 = type2.GetConstructor(new Type[0]).Invoke(new object[0]);
            Type type3 = this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DependencyResolution.DbConfigurationFinder");
            object obj3 = type3.GetConstructor(new Type[0]).Invoke(new object[0]);
            Type[] types = new Type[] { type2, type3 };
            object[] parameters = new object[] { obj2, obj3 };
            object obj4 = type.GetConstructor(types).Invoke(parameters);
            type.GetField("_configManager", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, obj4);
        }

        private void ClearMetadataCache()
        {
            Type type = this.EntityFrameworkAssembly.GetType("System.Data.Entity.Core.Metadata.Edm.MetadataCache");
            type.GetMethod("Clear").Invoke(type.GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null), new object[0]);
        }

        protected override Expression CreateModelFirstDbConnection(TypesCollector typesCollector)
        {
            string dbFilePath = Path.Combine(base.TempFolder, "db.sdf");
            Type type = this.EntityFrameworkAssembly.GetType("System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder");
            object obj2 = Activator.CreateInstance(type);
            type.GetProperty("Provider", BindingFlags.Public | BindingFlags.Instance).SetValue(obj2, this.ProviderName, null);
            type.GetProperty("ProviderConnectionString", BindingFlags.Public | BindingFlags.Instance).SetValue(obj2, this.GetConnectionString(dbFilePath), null);
            type.GetProperty("Metadata", BindingFlags.Public | BindingFlags.Instance).SetValue(obj2, base.TempFolder, null);
            Type[] types = new Type[] { typeof(string) };
            Expression[] arguments = new Expression[] { Expression.Constant(obj2.ToString()) };
            return Expression.New(this.EntityFrameworkAssembly.GetType("System.Data.Entity.Core.EntityClient.EntityConnection").GetConstructor(types), arguments);
        }

        protected void EmitCallToAddDefaultResolver(Type dbConfigurationType, ILGenerator ilGenerator)
        {
            MethodInfo method = dbConfigurationType.GetMethod("AddDefaultResolver", BindingFlags.NonPublic | BindingFlags.Instance);
            ilGenerator.Emit(OpCodes.Call, method);
        }

        protected void EmitCallToAddDependencyResolver(Type dbConfigurationType, ILGenerator ilGenerator)
        {
            Func<ConstructorInfo, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = delegate (ConstructorInfo x) {
                    ParameterInfo[] parameters = x.GetParameters();
                    return (parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName));
                };
            }
            ConstructorInfo con = this.GetActivatedSingletonDependencyResolverType().GetConstructors().FirstOrDefault<ConstructorInfo>(predicate);
            ilGenerator.Emit(OpCodes.Newobj, con);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldloc_1);
            MethodInfo method = dbConfigurationType.GetMethod("AddDependencyResolver", BindingFlags.NonPublic | BindingFlags.Instance);
            ilGenerator.Emit(OpCodes.Call, method);
        }

        protected void EmitCallToBaseTypeCtor(Type dbConfigurationType, ILGenerator ilGenerator)
        {
            ilGenerator.Emit(OpCodes.Ldarg_0);
            Func<ConstructorInfo, bool> predicate = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__11_0;
                predicate = <>c.<>9__11_0 = delegate (ConstructorInfo x) {
                    ParameterInfo[] parameters = x.GetParameters();
                    return (parameters != null) && (parameters.Length == 0);
                };
            }
            ConstructorInfo con = dbConfigurationType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault<ConstructorInfo>(predicate);
            ilGenerator.Emit(OpCodes.Call, con);
        }

        protected virtual Type EmitDbConfigurationType(ModuleBuilder moduleBuilder)
        {
            Type dbConfigurationType = this.GetDbConfigurationType();
            TypeBuilder builder = moduleBuilder.DefineType(base.GetType().FullName + ".DbConfiguration", TypeAttributes.Public, dbConfigurationType, (Type[]) null);
            ILGenerator iLGenerator = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]).GetILGenerator();
            Type sqlProviderServicesType = this.GetSqlProviderServicesType();
            Type activatedSingletonDependencyResolverType = this.GetActivatedSingletonDependencyResolverType();
            iLGenerator.DeclareLocal(sqlProviderServicesType);
            this.EmitCallToBaseTypeCtor(dbConfigurationType, iLGenerator);
            MethodInfo getMethod = sqlProviderServicesType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetGetMethod();
            iLGenerator.Emit(OpCodes.Call, getMethod);
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.Emit(OpCodes.Ldstr, this.ProviderName);
            Func<ConstructorInfo, bool> predicate = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__15_0;
                predicate = <>c.<>9__15_0 = delegate (ConstructorInfo x) {
                    ParameterInfo[] parameters = x.GetParameters();
                    return (parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName));
                };
            }
            ConstructorInfo con = activatedSingletonDependencyResolverType.GetConstructors().FirstOrDefault<ConstructorInfo>(predicate);
            iLGenerator.Emit(OpCodes.Newobj, con);
            MethodInfo method = dbConfigurationType.GetMethod("AddDependencyResolver", BindingFlags.NonPublic | BindingFlags.Instance);
            iLGenerator.Emit(OpCodes.Call, method);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            this.EmitCallToAddDefaultResolver(dbConfigurationType, iLGenerator);
            iLGenerator.Emit(OpCodes.Ret);
            return builder.CreateType();
        }

        protected virtual Type EmitDbDescendant(TypesCollector typesCollector, Tuple<ConstructorInfo, Type[]> ctorTuple, ModuleBuilder mb, bool isModelFirst, Type dbConfigurationType)
        {
            TypeBuilder tb = mb.DefineType(base.descendant.FullName, TypeAttributes.Public, !isModelFirst ? base.descendant : base.dbContext);
            ILGenerator iLGenerator = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctorTuple.Item2).GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (ctorTuple.Item1.GetParameters().Length == 2)
            {
                iLGenerator.Emit(OpCodes.Ldarg_2);
            }
            iLGenerator.Emit(OpCodes.Call, ctorTuple.Item1);
            iLGenerator.Emit(OpCodes.Ret);
            if (isModelFirst)
            {
                foreach (PropertyInfo info in base.GetDbSetProperties(base.descendant))
                {
                    base.CreateProperty(tb, info, typesCollector);
                }
            }
            return tb.CreateType();
        }

        protected Type GetActivatedSingletonDependencyResolverType()
        {
            Type type2 = base.FindEFType("System.Data.Entity.Core.Common.DbProviderServices");
            Type[] typeArguments = new Type[] { type2 };
            return base.FindEFType("System.Data.Entity.Infrastructure.DependencyResolution.SingletonDependencyResolver").MakeGenericType(typeArguments);
        }

        protected abstract string GetConnectionString(string dbFilePath);
        protected Type GetDbConfigurationType() => 
            this.EntityFrameworkAssembly.GetType("System.Data.Entity.DbConfiguration");

        protected Type GetDbConfigurationTypeAttributeType() => 
            this.EntityFrameworkAssembly.GetType("System.Data.Entity.DbConfigurationTypeAttribute");

        protected Type GetDbContextInfoType() => 
            this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DbContextInfo");

        protected Type GetIDbDependencyResolverType() => 
            this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DependencyResolution.IDbDependencyResolver");

        protected Type GetIProviderInvariantNameType() => 
            this.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.IProviderInvariantName");

        protected Type GetSqlProviderServicesType()
        {
            IDXTypeInfo info = this.servicesAssembly.TypesInfo.FirstOrDefault<IDXTypeInfo>(x => x.FullName == this.SqlProviderServicesTypeName);
            if (info != null)
            {
                return info.ResolveType();
            }
            if (!this.SuppressExceptions)
            {
                throw new ArgumentException($"Could not find type {this.SqlProviderServicesTypeName}");
            }
            return null;
        }

        protected override void PrepareEdmx(EdmxResource edmxResource)
        {
            if (edmxResource != null)
            {
                edmxResource.WriteResources(base.TempFolder, new EdmxResource.SchemaAttributeValues(this.ProviderName, this.ProviderManifestToken));
            }
        }

        protected virtual void SetDbConfiguration(object configuration)
        {
            object[] parameters = new object[] { configuration };
            this.GetDbConfigurationType().GetMethod("SetConfiguration", BindingFlags.Public | BindingFlags.Static).Invoke(null, parameters);
        }

        public abstract string ProviderName { get; }

        public abstract string ProviderManifestToken { get; }

        public abstract string SqlProviderServicesTypeName { get; }

        protected Assembly EntityFrameworkAssembly =>
            base.dbContext.Assembly;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EF60DbDescendantBuilderBase.<>c <>9 = new EF60DbDescendantBuilderBase.<>c();
            public static Func<ConstructorInfo, bool> <>9__11_0;
            public static Func<ConstructorInfo, bool> <>9__13_0;
            public static Func<ConstructorInfo, bool> <>9__15_0;

            internal bool <EmitCallToAddDependencyResolver>b__13_0(ConstructorInfo x)
            {
                ParameterInfo[] parameters = x.GetParameters();
                return ((parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName)));
            }

            internal bool <EmitCallToBaseTypeCtor>b__11_0(ConstructorInfo x)
            {
                ParameterInfo[] parameters = x.GetParameters();
                return ((parameters != null) && (parameters.Length == 0));
            }

            internal bool <EmitDbConfigurationType>b__15_0(ConstructorInfo x)
            {
                ParameterInfo[] parameters = x.GetParameters();
                return ((parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName)));
            }
        }
    }
}

