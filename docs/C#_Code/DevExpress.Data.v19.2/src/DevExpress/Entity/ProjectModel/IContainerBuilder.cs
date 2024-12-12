namespace DevExpress.Entity.ProjectModel
{
    using DevExpress.Entity.Model;

    public interface IContainerBuilder
    {
        IDbContainerInfo Build(IDXTypeInfo info, ISolutionTypesProvider typesProvider);
    }
}

