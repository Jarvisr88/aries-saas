namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class LocalDbDescendantBuilder : SqlExpressDescendantBuilder
    {
        public LocalDbDescendantBuilder(TypesCollector typesCollector, IDXAssemblyInfo servicesAssembly, string dbVersion) : base(typesCollector, servicesAssembly)
        {
            this.LocalDbVersion = dbVersion;
        }

        private object CreateLocalDbConnection(Type dbContextType, TypesCollector typesCollector)
        {
            Type type = base.EntityFrameworkAssembly.GetType("System.Data.Entity.Infrastructure.LocalDbConnectionFactory");
            Type[] types = new Type[] { typeof(string) };
            object[] parameters = new object[] { this.LocalDbVersion };
            string dbFilePath = Path.Combine(base.TempFolder, "db.sdf");
            object[] objArray2 = new object[] { this.GetConnectionString(dbFilePath) };
            return type.GetMethod("CreateConnection", BindingFlags.Public | BindingFlags.Instance).Invoke(type.GetConstructor(types).Invoke(parameters), objArray2);
        }

        protected override string GetConnectionString(string dbFilePath) => 
            $"Server=(localdb)\{this.LocalDbVersion};Integrated Security=SSPI;AttachDbFileName={dbFilePath};";

        public string LocalDbVersion { get; set; }
    }
}

