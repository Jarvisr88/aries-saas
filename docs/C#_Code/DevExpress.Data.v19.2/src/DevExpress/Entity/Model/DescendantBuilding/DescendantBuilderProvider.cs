namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Entity.Model;
    using DevExpress.Entity.ProjectModel;
    using System;

    public abstract class DescendantBuilderProvider
    {
        protected DescendantBuilderProvider()
        {
        }

        public abstract bool Available(Type dbContext, IDXTypeInfo dbDescendant, ISolutionTypesProvider typesProvider);
        public abstract IDbDescendantBuilder GetBuilder(TypesCollector typesCollector, ISolutionTypesProvider typesProvider);
    }
}

