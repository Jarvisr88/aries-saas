namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Runtime.CompilerServices;

    public class SqlClientDescendantBuilderProvider : EF60DbDescendantBuilderProviderBase
    {
        public SqlClientDescendantBuilderProvider() : this(true, true, "11.0")
        {
        }

        public SqlClientDescendantBuilderProvider(bool isSqlExpressInstalled, bool isLocalDbInstalled, string localDbVersion)
        {
            this.IsSqlExpressInstalled = isSqlExpressInstalled;
            this.IsLocalDbInstalled = isLocalDbInstalled;
            this.LocalDbVersion = localDbVersion;
        }

        public override bool Available(Type dbContext, IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider) => 
            (this.IsLocalDbInstalled || this.IsSqlExpressInstalled) ? (base.Available(dbContext, dbDescendant, typesProvider) && (base.GetServicesAssembly(dbDescendant, typesProvider, "EntityFramework.SqlServer") != null)) : false;

        public override IDbDescendantBuilder GetBuilder(TypesCollector typesCollector, ISolutionTypesProvider typesProvider)
        {
            IDXAssemblyInfo servicesAssembly = base.GetServicesAssembly(typesCollector.DbDescendantInfo, typesProvider, "EntityFramework.SqlServer");
            return (!this.IsLocalDbInstalled ? new SqlExpressDescendantBuilder(typesCollector, servicesAssembly) : new LocalDbDescendantBuilder(typesCollector, servicesAssembly, this.LocalDbVersion));
        }

        protected override string ExpectedProviderName =>
            "System.Data.SqlClient";

        protected bool IsSqlExpressInstalled { get; set; }

        protected bool IsLocalDbInstalled { get; set; }

        protected string LocalDbVersion { get; set; }
    }
}

