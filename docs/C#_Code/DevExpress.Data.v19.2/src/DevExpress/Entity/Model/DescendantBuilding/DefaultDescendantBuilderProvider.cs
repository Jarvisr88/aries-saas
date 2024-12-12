namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;

    public class DefaultDescendantBuilderProvider : DescendantBuilderProvider
    {
        public override bool Available(Type dbContext, IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider) => 
            !EntityFrameworkModelBase.IsAtLeastEF6(dbContext);

        public override IDbDescendantBuilder GetBuilder(TypesCollector typesCollector, ISolutionTypesProvider typesProvider) => 
            new DefaultDescendantBuilder(typesCollector);
    }
}

