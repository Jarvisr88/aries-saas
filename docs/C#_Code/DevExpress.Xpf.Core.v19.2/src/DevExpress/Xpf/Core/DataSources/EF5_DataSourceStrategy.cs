namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Reflection;

    internal class EF5_DataSourceStrategy : GenericPropertyDataSourceStrategy
    {
        private const string EF5_EntityExtension = "System.Data.Entity.DbExtensions, EntityFramework";
        private const string EF6_EntityExtension = "System.Data.Entity.QueryableExtensions, EntityFramework";

        public EF5_DataSourceStrategy(IDataSource owner) : base(owner)
        {
        }

        public override object CreateData(object value)
        {
            object[] parameters = new object[] { value };
            this.GetLoadMethodExtensionType().GetMethod("Load", BindingFlags.Public | BindingFlags.Static).Invoke(null, parameters);
            return value.GetType().GetProperty("Local").GetValue(value, null);
        }

        private Type GetLoadMethodExtensionType()
        {
            Type type = Type.GetType("System.Data.Entity.DbExtensions, EntityFramework");
            return ((type == null) ? Type.GetType("System.Data.Entity.QueryableExtensions, EntityFramework") : type);
        }
    }
}

