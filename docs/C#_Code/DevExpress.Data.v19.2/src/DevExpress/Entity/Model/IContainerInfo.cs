namespace DevExpress.Entity.Model
{
    using DevExpress.Entity.ProjectModel;

    public interface IContainerInfo : IDXTypeInfo, IHasName
    {
        DbContainerType ContainerType { get; }
    }
}

