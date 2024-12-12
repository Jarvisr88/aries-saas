namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.Model.DescendantBuilding.Internal;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SqlExpressDescendantBuilder : EF60DbDescendantBuilderBase
    {
        public SqlExpressDescendantBuilder(TypesCollector typesCollector, IDXAssemblyInfo servicesAssembly) : base(typesCollector, servicesAssembly)
        {
        }

        protected override Expression CreateDefaultDbConnection(Type dbContextType, TypesCollector typesCollector)
        {
            Type type = base.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.SqlConnectionFactory");
            MethodInfo method = type.GetMethod("CreateConnection", BindingFlags.Public | BindingFlags.Instance);
            Expression[] arguments = new Expression[] { Expression.Constant(this.GetConnectionString(Path.Combine(base.TempFolder, "db.sdf"))) };
            return Expression.Call(Expression.Constant(type.GetConstructor(new Type[0]).Invoke(new object[0])), method, arguments);
        }

        protected override string GetConnectionString(string dbFilePath)
        {
            Guid guid = Guid.NewGuid();
            bool useUserInstance = SqlExpressDescendantBuilderConfig.UseUserInstance;
            return $"Server=.\SQLEXPRESS;AttachDbFileName={dbFilePath};Database={guid};{(useUserInstance ? "User Instance=true;" : string.Empty)}Integrated Security=SSPI;MultipleActiveResultSets=True;Application Name=EntityFrameworkMUE";
        }

        public override bool SuppressExceptions =>
            true;

        public override string ProviderName =>
            "System.Data.SqlClient";

        public override string SqlProviderServicesTypeName =>
            "System.Data.Entity.SqlServer.SqlProviderServices";

        public override string ProviderManifestToken =>
            "2008";
    }
}

