namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;
    using System.Runtime.CompilerServices;

    public class SqlCeDescendantBuilderProvider : EF60DbDescendantBuilderProviderBase
    {
        public SqlCeDescendantBuilderProvider() : this(false)
        {
        }

        public SqlCeDescendantBuilderProvider(bool isSqlCE40Installed)
        {
            this.IsSqlCE40Installed = isSqlCE40Installed;
        }

        public override bool Available(Type dbContext, IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider) => 
            base.Available(dbContext, dbDescendant, typesProvider) && ((base.GetServicesAssembly(dbDescendant, typesProvider, "EntityFramework.SqlServerCompact") != null) && this.IsSqlCE40Installed);

        public override IDbDescendantBuilder GetBuilder(TypesCollector typesCollector, ISolutionTypesProvider typesProvider) => 
            new SqlCeDescendantBuilder(typesCollector, base.GetServicesAssembly(typesCollector.DbDescendantInfo, typesProvider, "EntityFramework.SqlServerCompact"));

        protected override string ExpectedProviderName =>
            null;

        private bool IsSqlCE40Installed { get; set; }
    }
}

