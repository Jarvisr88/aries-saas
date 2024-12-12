namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public class SqlCeDescendantBuilder : EF60DbDescendantBuilderBase
    {
        public SqlCeDescendantBuilder(TypesCollector typesCollector, IDXAssemblyInfo servicesAssembly) : base(typesCollector, servicesAssembly)
        {
        }

        protected override Expression CreateDefaultDbConnection(Type dbContextType, TypesCollector typesCollector)
        {
            Type type = base.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.SqlCeConnectionFactory");
            Type[] types = new Type[] { typeof(string) };
            object[] parameters = new object[] { this.ProviderName };
            string dbFilePath = Path.Combine(base.TempFolder, "db.sdf");
            MethodInfo method = type.GetMethod("CreateConnection", BindingFlags.Public | BindingFlags.Instance);
            Expression[] arguments = new Expression[] { Expression.Constant(this.GetConnectionString(dbFilePath)) };
            return Expression.Call(Expression.Constant(type.GetConstructor(types).Invoke(parameters)), method, arguments);
        }

        protected override Type EmitDbConfigurationType(ModuleBuilder moduleBuilder)
        {
            Type dbConfigurationType = base.GetDbConfigurationType();
            TypeBuilder builder = moduleBuilder.DefineType(base.GetType().FullName + ".DbConfiguration", TypeAttributes.Public, dbConfigurationType, (Type[]) null);
            ILGenerator iLGenerator = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[0]).GetILGenerator();
            base.EmitCallToBaseTypeCtor(dbConfigurationType, iLGenerator);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            FieldInfo field = base.GetSqlProviderServicesType().GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            iLGenerator.Emit(OpCodes.Ldsfld, field);
            iLGenerator.Emit(OpCodes.Ldstr, this.ProviderName);
            Func<Type, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = x => x.FullName.StartsWith("System.Data.Entity.Infrastructure.DependencyResolution.SingletonDependencyResolver");
            }
            Type[] typeArguments = new Type[] { typeof(DbProviderServices) };
            Func<ConstructorInfo, bool> func2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<ConstructorInfo, bool> local2 = <>c.<>9__7_1;
                func2 = <>c.<>9__7_1 = delegate (ConstructorInfo x) {
                    ParameterInfo[] parameters = x.GetParameters();
                    return (parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName));
                };
            }
            ConstructorInfo con = base.dbContext.GetAssembly().GetTypes().FirstOrDefault<Type>(predicate).MakeGenericType(typeArguments).GetConstructors().FirstOrDefault<ConstructorInfo>(func2);
            iLGenerator.Emit(OpCodes.Newobj, con);
            MethodInfo method = dbConfigurationType.GetMethod("AddDependencyResolver", BindingFlags.NonPublic | BindingFlags.Instance);
            iLGenerator.Emit(OpCodes.Call, method);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldsfld, field);
            base.EmitCallToAddDefaultResolver(dbConfigurationType, iLGenerator);
            iLGenerator.Emit(OpCodes.Ret);
            return builder.CreateType();
        }

        private object GetAppConfigResolver(object firstResolver)
        {
            Func<object, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = x => x.GetType().FullName == "System.Data.Entity.Infrastructure.DependencyResolution.AppConfigDependencyResolver";
            }
            return (firstResolver.GetType().GetProperty("Resolvers", BindingFlags.Public | BindingFlags.Instance).GetValue(firstResolver, new object[0]) as IEnumerable).Cast<object>().FirstOrDefault<object>(predicate);
        }

        protected override string GetConnectionString(string dbFilePath) => 
            $"Data Source={dbFilePath}";

        protected override void SetDbConfiguration(object configuration)
        {
            base.SetDbConfiguration(configuration);
            object obj2 = base.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.DependencyResolution.InternalConfiguration").GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null, new object[0]);
            object obj3 = obj2.GetType().GetField("_resolvers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2);
            object firstResolver = obj3.GetType().GetField("_firstResolver", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj3);
            object appConfigResolver = this.GetAppConfigResolver(firstResolver);
            IDictionary dictionary = appConfigResolver.GetType().GetField("_providerFactories", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(appConfigResolver) as IDictionary;
            FieldInfo field = base.GetSqlProviderServicesType().GetField("Instance", BindingFlags.Public | BindingFlags.Static);
            if (!dictionary.Contains(this.ProviderName))
            {
                dictionary.Add(this.ProviderName, field.GetValue(null));
            }
        }

        public override bool SuppressExceptions =>
            true;

        public override string ProviderName =>
            "System.Data.SqlServerCe.4.0";

        public override string SqlProviderServicesTypeName =>
            "System.Data.Entity.SqlServerCompact.SqlCeProviderServices";

        public override string ProviderManifestToken =>
            "4.0";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SqlCeDescendantBuilder.<>c <>9 = new SqlCeDescendantBuilder.<>c();
            public static Func<Type, bool> <>9__7_0;
            public static Func<ConstructorInfo, bool> <>9__7_1;
            public static Func<object, bool> <>9__13_0;

            internal bool <EmitDbConfigurationType>b__7_0(Type x) => 
                x.FullName.StartsWith("System.Data.Entity.Infrastructure.DependencyResolution.SingletonDependencyResolver");

            internal bool <EmitDbConfigurationType>b__7_1(ConstructorInfo x)
            {
                ParameterInfo[] parameters = x.GetParameters();
                return ((parameters != null) && ((parameters.Length == 2) && (parameters[1].ParameterType.FullName == typeof(object).FullName)));
            }

            internal bool <GetAppConfigResolver>b__13_0(object x) => 
                x.GetType().FullName == "System.Data.Entity.Infrastructure.DependencyResolution.AppConfigDependencyResolver";
        }
    }
}

