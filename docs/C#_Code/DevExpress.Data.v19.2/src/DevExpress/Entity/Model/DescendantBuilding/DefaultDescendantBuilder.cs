namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using System;
    using System.Data.EntityClient;
    using System.IO;
    using System.Linq.Expressions;

    public class DefaultDescendantBuilder : DbDescendantBuilder, IDbDescendantBuilder, IDisposable
    {
        public DefaultDescendantBuilder(TypesCollector typesCollector) : base(typesCollector)
        {
        }

        protected override Expression CreateDefaultDbConnection(Type dbContextType, TypesCollector typesCollector)
        {
            try
            {
                string dataBaseFilePath = Path.Combine(base.TempFolder, "db.sdf");
                Type[] types = new Type[] { typeof(string) };
                Expression[] arguments = new Expression[] { Expression.Constant(GetCeProviderConnectionString(dataBaseFilePath)) };
                return Expression.New(typesCollector.SqlCeConnection.ResolveType().GetConstructor(types), arguments);
            }
            catch
            {
                return null;
            }
        }

        protected override Expression CreateModelFirstDbConnection(TypesCollector typesCollector)
        {
            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder {
                ProviderConnectionString = GetCeProviderConnectionString(Path.Combine(base.TempFolder, "db.sdf")),
                Provider = typesCollector.SqlProvider,
                Metadata = base.TempFolder
            };
            return Expression.Constant(builder.ToString());
        }

        public static string GetCeProviderConnectionString(string dataBaseFilePath) => 
            !string.IsNullOrEmpty(dataBaseFilePath) ? $"Data Source={dataBaseFilePath}" : string.Empty;

        public override bool SuppressExceptions =>
            true;
    }
}

